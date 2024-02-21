using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Model
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Hospitalization> HospitalizationHistory { get; set; }
        public DbSet<DiagnosticMeasure> DiagnosticMeasures { get; set; }
        public DbSet<Personals> Personal { get; set; }
        public DbSet<Schedules> Schedule { get; set; }
    }
}
