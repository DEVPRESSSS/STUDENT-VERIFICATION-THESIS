using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using System.Configuration;
using System.Data;
using System.Windows;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static ServiceProvider?  ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            base.OnStartup(e);
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer("Server=(local)\\SQLEXPRESS;Database=umdb;Trusted_Connection=True;TrustServerCertificate=True;"));
            // Register other services or repositories here
        }


    }

}
