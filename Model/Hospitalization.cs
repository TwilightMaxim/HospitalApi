using System.ComponentModel.DataAnnotations;

namespace HospitalApi.Model
{
    public class Hospitalization
    {
        [Key]
        public int ID { get; set; }
        public int? MedicalCardNumber { get; set; }
        public int? Code { get; set; }
        public DateTime? Data { get; set; }
        public string? Reason { get; set; }
        public string? HospitalizationUnit { get; set; }
        public string? Conditions { get; set; }
        public int? Period { get; set; }
    }
}
