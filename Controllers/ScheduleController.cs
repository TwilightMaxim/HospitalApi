using HospitalApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : Controller
    {
        private readonly ApplicationContext _context;
        public ScheduleController(ApplicationContext Hospital)
        {
            _context = Hospital;
        }
        [HttpGet("Schedules")]
        public async Task<IActionResult> GetSchedules()
        {
            var schedules = await _context.Schedule.ToListAsync();
            return Ok(JsonConvert.SerializeObject(schedules));
        }
    }
}
