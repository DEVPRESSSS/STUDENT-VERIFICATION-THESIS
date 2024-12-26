using Microsoft.EntityFrameworkCore;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.UsercontrolsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext()
        {
        }

        public  ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base (options)
        {

            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(local)\\SQLEXPRESS;Database=umdb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
        public DbSet<ProfessorsEntity> Professors { get; set; }
        public DbSet<StudentsEntity> Students { get; set; }
        public DbSet<Departments> Departments { get; set; }
      //  public DbSet<Year> Year { get; set; }
        //public DbSet<Role> Role { get; set; }
       // public DbSet<ProgramEntity> Programs { get; set; }

       // public DbSet<StaffsEntity> Staffs { get; set; }



    }
}
