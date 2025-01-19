using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
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
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private readonly ApplicationDbContext _context;
       public LoginForm() : this(new ApplicationDbContext()) // Replace with a valid instance if applicable
{


}

   // Keep your parameterized constructor
    public LoginForm(ApplicationDbContext context)
    {
        InitializeComponent();
        _context = context;
    }


        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if(e.LeftButton== MouseButtonState.Pressed)
            {

                DragMove();
            }
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                var admin = _context.Admin.FirstOrDefault(a => a.Username== Usernametxt.Text && a.Password == Passwordtxt.Password || a.Password == PasswordTextBox.Text);

                if (admin != null)
                {
                    MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    Clear();


                    MainWindow dashboard = new MainWindow(_context);
                    dashboard.Show();

                   this.Hide();

                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Clear()
        {

            Usernametxt.Text = "";
            Passwordtxt.Password = "";
            PasswordTextBox.Text = "";
        }

        private void Usernametxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Text)) // Check for spaces or empty input
            {
                e.Handled = true; // Block the input
            }
        }

        private void Usernametxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) // Check if the spacebar is pressed
            {
                e.Handled = true; // Block the spacebar input
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ForgotPassword obj = new ForgotPassword();
            obj.Show();

            this.Hide();
        }

        private void Passwordtxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsWhiteSpace(e.Text, 0))
            {
                e.Handled = true;
            }

        }

        private void Passwordtxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) // Check if the spacebar is pressed
            {
                e.Handled = true; // Block the spacebar input
            }
        }

        private void revealmode_Unchecked(object sender, RoutedEventArgs e)
        {
            Passwordtxt.Visibility = Visibility.Visible;
            PasswordTextBox.Visibility = Visibility.Collapsed;

        }

        private void revealmode_Checked(object sender, RoutedEventArgs e)
        {
            PasswordTextBox.Text = Passwordtxt.Password;
            PasswordTextBox.Visibility = Visibility.Visible;
            Passwordtxt.Visibility = Visibility.Collapsed;

        }

        private void PasswordTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) // Check if the spacebar is pressed
            {
                e.Handled = true; // Block the spacebar input
            }
        }

       
    
    }
}
