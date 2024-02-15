using System.ComponentModel.DataAnnotations;

namespace HospitalApi.Model
{
    public class DiagnosticMeasure
    {
        [Key]
        public Int16 ID { get; set; }
        public string? FIOPatient { get; set; }
        public DateOnly? DateEvent { get; set; }
        public string? Doctor { get; set; }
        public string? TypeEvent { get; set; }
        public string? NameEvent { get; set; }
        public string? ResultsEvent { get; set; }
        public string? Recommendations { get; set; }
        public string? Benefit { get; set; }
        public int? Price { get; set; }
    }
}
