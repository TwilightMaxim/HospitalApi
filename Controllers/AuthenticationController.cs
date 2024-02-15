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
        public IActionResult AuthDoctor(string loginDoctor, string passwordDoctor)
        {
            Personals? personal = _context.Personal.SingleOrDefault(p => p.Login == loginDoctor);
            if (personal != null)
            {
                if (Verify(passwordDoctor, personal.Password) == true)
                {
                    return StatusCode(200);
                }
                return StatusCode(400);
            }
            return BadRequest("Неправильный логин или пароль");
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> RegistrationDoctor(Personals personal)
        {
            personal.Password = Generate(personal.Password);
            _context.Personal.Add(personal);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public bool Verify(string password, string passwordHash)
        {
            bool result = BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash, HashType.SHA256);
            return result;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public string Generate(string password) =>
            BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA256);
    }
}
