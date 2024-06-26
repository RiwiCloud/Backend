using AutoMapper;
using Backend.Dtos;
using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Backend.Services.Email;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Backend.Services.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BaseContext _context;
        private readonly IMapper _mapper;
        private readonly MailerSendService _mailerSendService;

        public UserRepository(BaseContext context, IMapper mapper, MailerSendService mailerSendService)
        {
            _context = context;
            _mapper = mapper;
            _mailerSendService = mailerSendService;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> RegisterUserAsync(UserRegistrationDto userRegistrationDto)
        {
            if (await UserExistsAsync(userRegistrationDto.Email))
            {
                throw new Exception("User already exists");
            }

            var user = _mapper.Map<User>(userRegistrationDto);
            user.Password = user.Password; // Aquí puedes hashear la contraseña

            user = await AddUserAsync(user);

            await SendWelcomeEmail(user);

            return user;
        }

        private async Task SendWelcomeEmail(User user)
        {
            var subject = "Registro con exito - Detalles Registro";
            var textContent = $"Hola {user.Name},\n\n";
            var htmlContent = $"<p>Hola <strong>{user.Name}</strong>,</p>" +
                $"<p>Nos complace informarte que se ha registrado exitosamente <strong></strong>.</p>" +
                $"<p><strong>Detalles del registro:</strong></p>" +
                $"<ul>" +
                $"<li><strong>Nombre:</strong> {user.Name}</li>" +
                $"<li><strong>Correo Electrónico:</strong> {user.Email}</li>" +
                $"<li><strong>Fecha:</strong> {DateTime.UtcNow}</li>" +
                $"<li><strong>Contraseña:</strong> {user.Password}</li>" +
                $"</ul>" +
                $"<p>. Gracias por utilizar nuestro servicio.</p>" +
                $"<p>Si tienes alguna pregunta o necesitas más información, no dudes en contactarnos.</p>" +
                $"<p>Saludos cordiales,</p>" +
                $"<p>RiwiCould<br>" +
                $"<a href='mailto:[Correo Electrónico de Soporte]'>riwicouldo.soporte@riwicould.com</a><br>" +
                $"Teléfono de Soporte 8654324</p>";

            await _mailerSendService.SendEmailAsync(
                from: "MS_MZLq3j@trial-jpzkmgq9opy4059v.mlsender.net",
                fromName: "RiwiCould",
                to: new List<string> { user.Email },
                toNames: new List<string> { user.Name },
                subject: subject,
                text: textContent,
                html: htmlContent
            );
        }

  
  }
}
