using HospitalApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiagnosticMeasureController : Controller
    {
        private readonly ApplicationContext _context;
        public DiagnosticMeasureController(ApplicationContext Hospital)
        {
            _context = Hospital;
        }
        /// <summary>
        /// Поиск пациента по медкарте
        /// </summary>
        /// <param name="medicalCardNumber"></param>
        /// <returns></returns>
        [HttpPost("SearchPatients")]
        public IActionResult ImformationPatients(string? medicalCardNumber)
        {
            if (medicalCardNumber != null)
            {
                Patient? medicalCard = _context.Patients.SingleOrDefault(p => p.MedicalCardNumber == medicalCardNumber);
                if (medicalCard != null)
                {
                    return Json(medicalCard);
                }
            }
            return NotFound();
        }
    }
}
