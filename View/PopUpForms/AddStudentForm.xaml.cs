using Notification.Wpf;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.Student;
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

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms
{
    /// <summary>
    /// Interaction logic for AddStudentForm.xaml
    /// </summary>
    public partial class AddStudentForm : Window
    {



        private ApplicationDbContext _context;
        public AddStudentForm(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;
            UnderGraduate.IsChecked = true;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          
        }

        private void UnderGraduate_Checked(object sender, RoutedEventArgs e)
        {
            if (UnderGraduate.IsChecked == true)
            {
                DefualtContent.Visibility = Visibility.Collapsed;

                MainContentArea.Content = new AddStudentUC(_context);

                ShowNotification("Information","You are viewing the form for Undergraduate/Current student!",NotificationType.Information);
            }
            else if(Alumni.IsChecked == true)
            {
                DefualtContent.Visibility = Visibility.Collapsed;

                MainContentArea.Content = new AlumniStudentUC(_context);
                ShowNotification("Information", "You are viewing the form for Alumni student!", NotificationType.Information);


            }
            else
            {
                DefualtContent.Visibility = Visibility.Visible;
            }
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
                  new NotificationContent { Title = title, Message = message, Type = notificationType }, expirationTime: TimeSpan.FromSeconds(10));
            }

        }


    }
}
