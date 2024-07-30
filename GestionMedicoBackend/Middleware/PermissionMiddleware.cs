using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using GestionMedicoBackend.Data;

public class PermissionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _scopeFactory;

    public PermissionMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
    {
        _next = next;
        _scopeFactory = scopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint == null)
        {
            await _next(context);
            return;
        }

        var permissionAttribute = endpoint.Metadata.GetMetadata<PermissionAttribute>();
        if (permissionAttribute == null)
        {
            await _next(context);
            return;
        }

        var userId = context.User.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
        if (userId == null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        using (var scope = _scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userPermissions = await GetUserPermissionsAsync(dbContext, userId);
            if (!userPermissions.Contains(permissionAttribute.Permission))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }
        }

        await _next(context);
    }

    private async Task<List<string>> GetUserPermissionsAsync(ApplicationDbContext dbContext, string userId)
    {
        if (!int.TryParse(userId, out int userIdInt))
        {
            return new List<string>();
        }

        var user = await dbContext.Users
                                  .Include(u => u.Role)
                                  .ThenInclude(r => r.RolePermissions)
                                  .ThenInclude(rp => rp.Permission)
                                  .FirstOrDefaultAsync(u => u.Id == userIdInt);

        if (user == null)
        {
            return new List<string>();
        }

        return user.Role.RolePermissions.Select(rp => rp.Permission.Name).ToList();
    }
}
