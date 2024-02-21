using System.ComponentModel.DataAnnotations;

namespace HospitalApi.Model
{
    public class Schedules
    {
        [Key]
        public int ID { get; set; }
        public int CabinetNumber { get; set; }
        public string Doctor { get; set; }
        public string FIO { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }
    }
}
