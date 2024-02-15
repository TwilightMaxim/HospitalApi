using HospitalApi.Model;
using IronBarCode;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientRegistrationController : Controller
    {
        private readonly ApplicationContext _context;
        public PatientRegistrationController(ApplicationContext Hospital)
        {
            _context = Hospital;
        }
        /// <summary>
        /// Сохрание данные о пациенте в БД
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpPost(Name = "PatientAdd")]
        public async Task<IActionResult> PatientRegistration(Patient patient)
        {
            Patient? patients = _context.Patients.SingleOrDefault(p => p.MedicalCardNumber == patient.MedicalCardNumber);
            if (patients != null)
            {
                return StatusCode(409);
            }
            else
            {
                patient.QRCodeImagePath = QrCodeGenerate(patient);
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
                return StatusCode(201);
            }
        }
        /// <summary>
        /// Генерация QR-кода по номеру медкарты
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private string QrCodeGenerate(Patient patient)
        {
            var qrCodeDirectoryPath = Path.Combine("img");
            if (!Directory.Exists(qrCodeDirectoryPath))
            {
                Directory.CreateDirectory(qrCodeDirectoryPath);
            }
            var qrCodeFileName = $"{Guid.NewGuid().ToString().Substring(0, 4)}.png";
            var qrCodeFilePath = Path.Combine(qrCodeDirectoryPath, qrCodeFileName);
            QRCodeWriter.CreateQrCode(patient.MedicalCardNumber, 300, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsPng(qrCodeFilePath);
            return qrCodeFilePath;
        }
    }
}
