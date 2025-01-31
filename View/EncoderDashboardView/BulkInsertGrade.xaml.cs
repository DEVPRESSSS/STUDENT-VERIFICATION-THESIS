using Notification.Wpf;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.EncoderDashboardView
{
    /// <summary>
    /// Interaction logic for BulkInsertGrade.xaml
    /// </summary>
    public partial class BulkInsertGrade : Window
    {
        private readonly ApplicationDbContext _context;

        public BulkInsertGrade(ApplicationDbContext context)
        {
            InitializeComponent();
            _context= context;

             DataContext= new SubjectsViewModel(_context);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var _notificationManager = new NotificationManager();
            _notificationManager.Show(
                  new NotificationContent { Title = "Grade warning", Message = "Note:Makesure the subjects and grades are accurate before you click the save!", Type = NotificationType.Warning});
        }
    }
}
