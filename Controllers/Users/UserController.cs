using Backend.Dtos;
using Backend.Mail;
using Backend.Services.UserRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers.Users
{
    [ApiController]  
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly MailController _mailController;

        public UsersController(IUserRepository userRepository, MailController mailController)
        {
            _userRepository = userRepository;
            _mailController = mailController;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto userRegistrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userRepository.RegisterUserAsync(userRegistrationDto);

                // Enviar correo de confirmación
                await _mailController.EnviarCorreo(user.Email, user.Name, userRegistrationDto.Password);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
