using GestionMedicoBackend.Data;
using GestionMedicoBackend.Services.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GestionMedicoBackend.Services.Auth;
using GestionMedicoBackend.Services.Medic;
using GestionMedicoBackend.Services.Patient;
using GestionMedicoBackend.Services;
using GestionMedicoBackend.Services.Specialty;
using GestionMedicoBackend.Services.Horario;
using GestionMedicoBackend.Models;
using GestionMedicoBackend.Services.Contacto;
using GestionMedicoBackend.Services.HistorialClinico;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<EmailServices>();
builder.Services.AddScoped<EmailTemplateService>();
builder.Services.AddScoped<TokenServices>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<MedicServices>();
builder.Services.AddScoped<PatientServices>();
builder.Services.AddScoped<AppointmentServices>();
builder.Services.AddScoped<SpecialtyServices>();
builder.Services.AddScoped<HorarioServices>();
builder.Services.AddScoped<ConsultorioService>();
builder.Services.AddScoped<HistorialClinicoServices>();
builder.Services.AddScoped<ContactoService>();
builder.Services.AddScoped<IRoleService, RolService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();

builder.Services.AddHostedService<MedicAvailabilityService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT Configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
