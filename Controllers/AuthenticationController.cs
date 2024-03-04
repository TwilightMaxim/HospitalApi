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
        /// <summary>
        /// Метод авторизации пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("Authorization")]
        public IActionResult Auth(string login, string password)
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
        /// <summary>
        /// Метод регестрации пользователя
        /// </summary>
        /// <param name="personal"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Верефикация зашифровоного пароля пользователя
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public bool Verify(string password, string salt, string passwordHash)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return (hashedPassword.Trim() == passwordHash.Trim());
        }
        /// <summary>
        /// Метод генерации зашифровонного пароля пользователя
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public (string, string) Generate(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return (hashedPassword, salt);
        }
    }
}
