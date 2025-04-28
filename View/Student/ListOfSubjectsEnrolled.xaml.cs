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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.Student
{
    /// <summary>
    /// Interaction logic for ListOfSubjectsEnrolled.xaml
    /// </summary>
    public partial class ListOfSubjectsEnrolled : UserControl
    {

        private ApplicationDbContext _context;
        public ListOfSubjectsEnrolled(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;

            //DataContext = new StudentViewModel(_context);
        }




        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ShowNotification("Guide", "Here are the subjects Enrolled and grades", NotificationType.Information);
        }


        private void ShowNotification(string title, string message, NotificationType notificationType)
        {

            var notificaficationManager = new NotificationManager();

            if (NotificationType.Success == notificationType)
            {
                notificaficationManager.Show(
                  new NotificationContent { Title = title, Message = message, Type = notificationType }, expirationTime: TimeSpan.FromSeconds(10));
                return;
            }
            else
            {
                notificaficationManager.Show(
                  new NotificationContent { Title = title, Message = message, Type = notificationType }, expirationTime: TimeSpan.FromSeconds(5));
            }

        }
    }
}
