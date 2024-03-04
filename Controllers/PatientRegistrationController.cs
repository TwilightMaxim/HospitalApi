using DocumentFormat.OpenXml.Wordprocessing;
using HospitalApi.Model;
using IronBarCode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Globalization;
using System.IO;
using TemplateEngine.Docx;

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
                return FillDocument(patient);
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
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult FillDocument(Patient patient)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var inputFilePath = Path.Combine(currentDirectory, "Docs", "Input.docx");
            var outputFilePath = Path.Combine(currentDirectory, "Docs", $"{patient.PatientName}.docx");
            System.IO.File.Copy(inputFilePath, outputFilePath);
            DateTime now = DateTime.Now;
            string dayAsString = now.ToString("dd");
            var valuesToFill = new Content(
                new FieldContent("FIO", $"{patient.LastNamePatient} {patient.PatientName} {patient.PatronymicPatient}"),
                new FieldContent("Passport", patient.PassportNumberSeries),
                new FieldContent("Address", patient.Address),
                new FieldContent("Day", dayAsString));
            using (var outputDocument = new TemplateProcessor(outputFilePath).SetRemoveContentControls(false))
            {
                outputDocument.FillContent(valuesToFill);
                outputDocument.SaveChanges();
            }
            return PhysicalFile(outputFilePath, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
        }
    }
}
