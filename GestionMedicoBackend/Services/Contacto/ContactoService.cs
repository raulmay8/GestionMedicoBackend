using GestionMedicoBackend.Data;
using GestionMedicoBackend.DTOs.Contacto;
using GestionMedicoBackend.Models.Contact;
using GestionMedicoBackend.Services.User;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionMedicoBackend.Services.Contacto
{
    public class ContactoService
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailServices _emailServices;

        public ContactoService(ApplicationDbContext context, EmailServices emailServices)
        {
            _context = context;
            _emailServices = emailServices;
        }

        public async Task<IEnumerable<ContactoDto>> GetAllContactsAsync()
        {
            return await _context.ContactMessages
                .Select(contact => new ContactoDto
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    Email = contact.Email,
                    Message = contact.Message,
                    CreatedAt = contact.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<ContactoDto> GetContactByIdAsync(int id)
        {
            var contact = await _context.ContactMessages
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contact == null) throw new KeyNotFoundException("Mensaje de contacto no encontrado");

            return new ContactoDto
            {
                Id = contact.Id,
                Name = contact.Name,
                Email = contact.Email,
                Message = contact.Message,
                CreatedAt = contact.CreatedAt
            };
        }

        public async Task<string> CreateContactAsync(CreateContactoDto createContactoDto)
        {
            var contact = new ContactMessage
            {
                Name = createContactoDto.Name,
                Email = createContactoDto.Email,
                Message = createContactoDto.Message,
                CreatedAt = DateTime.UtcNow
            };

            _context.ContactMessages.Add(contact);
            await _context.SaveChangesAsync();

            // Enviar correo de confirmación
            await _emailServices.SendContactConfirmationEmailAsync(createContactoDto.Email, contact);

            return "Mensaje de contacto creado exitosamente";
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var contact = await _context.ContactMessages.FindAsync(id);
            if (contact == null) throw new KeyNotFoundException("Mensaje de contacto no encontrado");

            _context.ContactMessages.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
