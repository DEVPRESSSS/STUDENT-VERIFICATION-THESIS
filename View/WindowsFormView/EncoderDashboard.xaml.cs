using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.EncoderDashboardView;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.UsercontrolsView;
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

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.WindowsFormView
{
    /// <summary>
    /// Interaction logic for EncoderDashboard.xaml
    /// </summary>
    public partial class EncoderDashboard : Window
    {
        private readonly ApplicationDbContext _context;

        public EncoderDashboard(ApplicationDbContext context,string username)
        {
            InitializeComponent();

            _context = context;
            usernametxt.Text = $"Welcome back {username}!";
            MainContentArea.Content = new DashboardOverview();
            var user = _context.Staffs.FirstOrDefault(x => x.Username == username);
            if (user != null)
            {
                UserSessionService.Instance.SetLoggedInUser(user.StaffID, username);
            }

        }


    




        private void Maximize_Click_1(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {

                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dr = MessageBox.Show("Are you sure want to exit??", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (dr == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown(0);
            }
        }

        private void DashboardOverview_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new DashboardOverview();

        }

        private void Grades_Click(object sender, RoutedEventArgs e)
        {
            
               MainContentArea.Content = new EncodeGradeUC(_context);
       


        }

 
     

        private void Subjects_Click_1(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new SubjectEncoderUC(_context);

        }

        private void Students_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new StudentsEncoderUC(_context);
        }

        private void Grade_Click(object sender, RoutedEventArgs e)
        {
         
            MainContentArea.Content = new GradesEncoderUC(_context);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dr = MessageBox.Show("Are you sure want to logout??", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (dr == MessageBoxResult.Yes)
            {
                LoginWD OBJ = new LoginWD();
                OBJ.Show();

                this.Close();
            }
        }
    }
}
