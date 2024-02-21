using HospitalApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonLocationsController : Controller
    {
        private static List<string> PersonCodes = new List<string> { "code1", "code2", "code3" };
        private static List<string> PersonRoles = new List<string> { "Клиент", "Сотрудник" };
        private static List<int> LastSecurityPointNumbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 };
        private static List<string> LastSecurityPointDirections = new List<string> { "in", "out" };
        [HttpGet("Locations")]
        public IActionResult LocationsPerson()
        {
            Random rnd = new Random();

            var personCode = PersonCodes[rnd.Next(PersonCodes.Count)];

            var personRole = PersonRoles[rnd.Next(PersonRoles.Count)];

            var lastSecurityPointNumber = LastSecurityPointNumbers[rnd.Next(LastSecurityPointNumbers.Count)];

            var lastSecurityPointDirection = LastSecurityPointDirections[rnd.Next(LastSecurityPointDirections.Count)];

            var lastSecurityPointTime = DateTime.Now;

            var result = new
            {
                PersonCode = personCode,
                PersonRole = personRole,
                LastSecurityPointNumber = lastSecurityPointNumber,
                LastSecurityPointDirection = lastSecurityPointDirection,
                LastSecurityPointTime = lastSecurityPointTime
            };

            return Ok(JsonConvert.SerializeObject(result));
        }
    }
}
