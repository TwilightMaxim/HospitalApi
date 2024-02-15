using HospitalApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Globalization;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HospitalizationController : Controller
    {
        private readonly ApplicationContext _context;
        public HospitalizationController(ApplicationContext Hospital)
        {
            _context = Hospital;
        }
        /// <summary>
        /// Регистрация пациента на госпитализацию
        /// </summary>
        /// <param name="hospital"></param>
        /// <returns></returns>
        [HttpPost("HospitalizAdd")]
        public async Task<IActionResult> PatientHospitalization(Hospitalization hospital)
        {
            _context.HospitalizationHistory.Add(hospital);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
        /// <summary>
        /// Поиск госпитализации по коду
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [HttpPost("SearchHospitalization")]
        public IActionResult ImformationHospitalization(int? hospitalizationCode)
        {
            if (hospitalizationCode != null)
            {
                Hospitalization? hospitaliz = _context.HospitalizationHistory.SingleOrDefault(p => p.Code == hospitalizationCode);
                if (hospitaliz  != null)
                {
                    return Json(hospitaliz);
                }
            }
            return NotFound();
        }
    }

}
