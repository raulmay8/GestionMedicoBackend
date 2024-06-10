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
            .Select(user => new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Status = user.Status
            })
            .ToListAsync();
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Status = user.Status
        };
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        // Verificar unicidad
        if (await _context.Users.AnyAsync(u => u.Username == createUserDto.Username))
        {
            throw new Exception("El nombre de usuario ya está en uso.");
        }

        if (await _context.Users.AnyAsync(u => u.Email == createUserDto.Email))
        {
            throw new Exception("El correo electrónico ya está en uso.");
        }

        var user = new User
        {
            Username = createUserDto.Username,
            Email = createUserDto.Email,
            Password = HashHelper.HashPassword(createUserDto.Password),
            Status = false, 
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Generar y asignar el token al usuario
        var token = await _tokenServices.GenerateTokenAsync(user);

        // Generar el enlace de confirmación
        var confirmationLink = $"http://localhost/api/users/confirm-account?token={token}";
        var subject = "Confirma tu cuenta";
        var message = $"Por favor confirma tu cuenta haciendo clic en el siguiente enlace: <a href='{confirmationLink}'>Confirmar cuenta</a>";

        // Enviar correo de confirmación
        await _emailServices.SendEmailAsync(user.Email, subject, message);

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Status = user.Status,
            Token = token
        };
    }

    public async Task<bool> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        if (user.Username != updateUserDto.Username && await _context.Users.AnyAsync(u => u.Username == updateUserDto.Username))
        {
            throw new Exception("El nombre de usuario ya está en uso.");
        }

        if (user.Email != updateUserDto.Email && await _context.Users.AnyAsync(u => u.Email == updateUserDto.Email))
        {
            throw new Exception("El correo electrónico ya está en uso.");
        }

        user.Username = updateUserDto.Username;
        user.Email = updateUserDto.Email;
        user.Status = updateUserDto.Status;
        if (!string.IsNullOrEmpty(updateUserDto.Password))
        {
            user.Password = HashHelper.HashPassword(updateUserDto.Password);
        }
        user.ModifiedDate = DateTime.UtcNow;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ToggleUserStatusAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        user.Status = !user.Status;
        user.ModifiedDate = DateTime.UtcNow;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ConfirmAccountAsync(string token)
    {
        var userToken = await _context.Tokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Value == token);

        if (userToken == null)
        {
            return false;
        }

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
        if (user == null) return false;

        user.Status = true; 
        user.ModifiedDate = DateTime.UtcNow;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
