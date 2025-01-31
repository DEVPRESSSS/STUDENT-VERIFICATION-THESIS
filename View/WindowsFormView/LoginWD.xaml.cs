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

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.WindowsFormView
{
    /// <summary>
    /// Interaction logic for LoginWD.xaml
    /// </summary>
    public partial class LoginWD : Window
    {

        private NotificationManager _notificationManager;

        private readonly ApplicationDbContext _context;
        public LoginWD() : this(new ApplicationDbContext())
        {


        }
        public LoginWD(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;

            DataContext = new LfcVM(context, this);
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();

        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
            {
                textEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                textEmail.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {

          
            if (!string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
                EyeBtn.Visibility = Visibility.Visible;

                if (DataContext is LfcVM viewModel)
                {
                    viewModel.Password = (sender as PasswordBox)?.Password;
                }
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
                EyeBtn.Visibility = Visibility.Collapsed;

            }
        }

        private void textPassword_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void EyeBtn_Click(object sender, RoutedEventArgs e)
        {

            // Show the unmasked password
       
            txtPasswordUnmask.Visibility = Visibility.Visible;
            txtPasswordUnmask.Text = txtPassword.Password;
            txtPassword.Visibility = Visibility.Collapsed;  

            // Toggle the visibility of the eye buttons
            EyeBtn.Visibility = Visibility.Collapsed;
            OffEyeBtn.Visibility = Visibility.Visible;


        }

        private void txtPasswordUnmask_PasswordChanged(object sender, RoutedEventArgs e)
        {
           
        }

        private void OffEyeBtn_Click(object sender, RoutedEventArgs e)
        {
            txtPassword.Visibility = Visibility.Visible;
            txtPassword.Password = txtPasswordUnmask.Text; 
            txtPasswordUnmask.Visibility = Visibility.Collapsed; 

            // Toggle the visibility of the eye buttons
            OffEyeBtn.Visibility = Visibility.Collapsed;
            EyeBtn.Visibility = Visibility.Visible;
        }

        private void txtPasswordUnmask_TextChanged(object sender, TextChangedEventArgs e)
        {
            //txtPassword.Password = txtPasswordUnmask.Text;

        }

        private void txtEmail_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space)
            {


                e.Handled= true; 

            }
        }
    }
}
