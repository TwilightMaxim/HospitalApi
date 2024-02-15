using System.ComponentModel.DataAnnotations;

namespace HospitalApi.Model
{
    public class Patient
    {
        [Key]
        public int ID { get; set; }
        public string? Photo { get; set; }
        public string? PatientName { get; set; }
        public string? LastNamePatient { get; set; }
        public string? PatronymicPatient { get; set; }
        public string? PassportNumberSeries { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? MedicalCardNumber { get; set; }
        public DateOnly? LastVisit { get; set; }
        public DateOnly? NextVisit { get; set; }
        public string? PolicyNumber { get; set; }
        public DateOnly? EndPolicy { get; set; }
        public string? Diagnos { get; set; }
        public string? MedicalHistory { get; set; }
        public string? QRCodeImagePath { get; set; }
    }
}
