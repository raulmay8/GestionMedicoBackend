using GestionMedicoBackend.DTOs.User;
using GestionMedicoBackend.Models;
using GestionMedicoBackend.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using GestionMedicoBackend.Helpers;
using GestionMedicoBackend.Services.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public class UserService
{
    private readonly ApplicationDbContext _context;
    private readonly EmailServices _emailServices;
    private readonly TokenServices _tokenServices;

    public UserService(ApplicationDbContext context, EmailServices emailServices, TokenServices tokenServices)
    {
        _context = context;
        _emailServices = emailServices;
        _tokenServices = tokenServices;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        return await _context.Users
        .Include(u => u.Role)
        .Select(user => new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Status = user.Status,
            RoleId = user.Role.Id,
            RoleName = user.Role.Name
        })
        .ToListAsync();
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _context.Users
        .Include(u => u.Role)
        .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) throw new KeyNotFoundException("Usuario no encontrado");


        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Status = user.Status,
            RoleId = user.Role.Id,
            RoleName = user.Role.Name
        };
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        if (await _context.Users.AnyAsync(u => u.Username == createUserDto.Username))
        {
            throw new Exception("El nombre de usuario ya está en uso.");
        }

        if (await _context.Users.AnyAsync(u => u.Email == createUserDto.Email))
        {
            throw new Exception("El correo electrónico ya está en uso.");
        }
        //POR DEFECTO AL CREAR USUARIO SERÁ PACIENTE
        int roleId = createUserDto.RoleId ?? 1;

        var role = await _context.Roles.FindAsync(roleId);
        if (role == null)
        {
            throw new KeyNotFoundException("El rol especificado no existe.");
        }

        var user = new User
        {
            Username = createUserDto.Username,
            Email = createUserDto.Email,
            Password = HashHelper.HashPassword(createUserDto.Password),
            Status = false,
            RoleId = roleId,
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var code = await _tokenServices.GenerateRandomCodeAsync(user);

        await _emailServices.SendConfirmationEmailAsync(user.Email, user.Username, code);

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Status = user.Status,
            RoleId = role.Id,
            RoleName = role.Name,
        };
    }

    public async Task<bool> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException("Usuario no encontrado");
        }

        if (user.Username != updateUserDto.Username && await _context.Users.AnyAsync(u => u.Username == updateUserDto.Username))
        {
            throw new Exception("El nombre de usuario ya está en uso.");
        }

        if (user.Email != updateUserDto.Email && await _context.Users.AnyAsync(u => u.Email == updateUserDto.Email))
        {
            throw new Exception("El correo electrónico ya está en uso.");
        }

        var role = await _context.Roles.FindAsync(updateUserDto.RoleId);
        if (role == null)
        {
            throw new KeyNotFoundException("El rol especificado no existe.");
        }

        user.Username = updateUserDto.Username;
        user.Email = updateUserDto.Email;
        user.RoleId = updateUserDto.RoleId;
        user.ModifiedDate = DateTime.UtcNow;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) throw new KeyNotFoundException("Usuario no encontrado");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ToggleUserStatusAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) throw new KeyNotFoundException("Usuario no encontrado");

        user.Status = !user.Status;
        user.ModifiedDate = DateTime.UtcNow;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ConfirmAccountAsync(string code)
    {
        var userToken = await _context.Tokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Value == code);

        if (userToken == null) throw new ApplicationException("Código inválido o expirado");

        var user = userToken.User;
        user.Status = true;
        user.ModifiedDate = DateTime.UtcNow;

        _context.Users.Update(user);
        _context.Tokens.Remove(userToken);
        await _context.SaveChangesAsync();
        return true;
    }




    public async Task<bool> ConfirmEmailAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) throw new KeyNotFoundException("Usuario no encontrado");

        user.Status = true; 
        user.ModifiedDate = DateTime.UtcNow;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task RequestPasswordResetAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new KeyNotFoundException("Usuario no encontrado");

        if (!user.Status) throw new ApplicationException("Usuario no confirmado");

        var existingTokens = await _context.Tokens.Where(t => t.UserId == user.Id).ToListAsync();
        if (existingTokens.Any())
        {
            _context.Tokens.RemoveRange(existingTokens);
            await _context.SaveChangesAsync();
        }

        var code = await _tokenServices.GenerateRandomCodeAsync(user);
        var resetLink = $"http://localhost:5173/reset-password?token={code}";

        await _emailServices.SendPasswordResetEmailAsync(user.Email, user.Username, resetLink);
    }




    public async Task<bool> ResetPasswordAsync(string token, string newPassword)
    {
        var userToken = await _context.Tokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Value == token);

        if (userToken == null)
            throw new ApplicationException("Token inválido o no encontrado");

        var user = userToken.User;
        user.Password = HashHelper.HashPassword(newPassword);
        user.ModifiedDate = DateTime.UtcNow;

        _context.Users.Update(user);
        _context.Tokens.Remove(userToken);
        await _context.SaveChangesAsync();
        return true;
    }


}
