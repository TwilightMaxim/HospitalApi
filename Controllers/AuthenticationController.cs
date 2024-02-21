using HospitalApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly ApplicationContext _context;
        public AuthenticationController(ApplicationContext Hospital)
        {
            _context = Hospital;
        }
        [HttpPost("Authorization")]
        public IActionResult AuthDoctor(string login, string password)
        {
            Personals? personal = _context.Personal.SingleOrDefault(p => p.Login == login);
            if (personal != null)
            {
                if (Verify(password, personal.PasswordSalt, personal.Password) == true)
                {
                    return Ok(new { Role = personal.Role });
                }
                return StatusCode(400);
            }
            return BadRequest("Неправильный логин или пароль");
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> RegistrationDoctor(Personals personal)
        {
            (string hashedPassword, string salt) = Generate(personal.Password);
            personal.Password = hashedPassword;
            personal.PasswordSalt = salt;
            _context.Personal.Add(personal);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public bool Verify(string password, string salt, string passwordHash)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return (hashedPassword.Trim() == passwordHash.Trim());
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public (string, string) Generate(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return (hashedPassword, salt);
        }
    }
}
