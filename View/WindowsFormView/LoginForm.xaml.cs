using Notification.Wpf;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using System.Windows;
using System.Windows.Input;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.WindowsFormView
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    /// 


    public partial class LoginForm : Window
    {
        private NotificationManager _notificationManager;

        private readonly ApplicationDbContext _context;
       public LoginForm() : this(new ApplicationDbContext()) 
{


}

   // Keep your parameterized constructor
    public LoginForm(ApplicationDbContext context)
    {
        InitializeComponent();
             _context = context;
            _notificationManager = new NotificationManager();
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
                // Check if the username and password match in Admin table
                var admin = _context.Admin.FirstOrDefault(a => a.Username == Usernametxt.Text && (a.Password == Passwordtxt.Password || a.Password == PasswordTextBox.Text));
                if (admin != null)
                {
                    ShowNotification("Success","Login successfully", NotificationType.Success);

                    MainWindow dashboard = new MainWindow(_context);
                    dashboard.Show();
                    this.Hide();
                    return;
                }

                var staff = _context.Staffs.FirstOrDefault(s => s.Username == Usernametxt.Text && (s.Password == Passwordtxt.Password || s.Password == PasswordTextBox.Text));
                if (staff != null)
                {
                    ShowNotification("Success", "Login successfully", NotificationType.Success);

                    EncoderDashboard staffDashboard = new EncoderDashboard(_context); 
                    staffDashboard.Show();
                    Clear();

                    this.Hide();
                    return;
                }

                ShowNotification("Error", "Invalid Username or Password", NotificationType.Error);

                Clear();
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
            if (string.IsNullOrWhiteSpace(e.Text)) 
            {
                e.Handled = true; 
            }
        }

        private void Usernametxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) 
            {
                e.Handled = true; 
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
            if (e.Key == Key.Space) 
            {
                e.Handled = true; 
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


        private void ShowNotification(string title, string message, NotificationType notificationType)
         {

            var notificaficationManager = new NotificationManager();

            
            notificaficationManager.Show(
                  new NotificationContent { Title = title, Message = message, Type= notificationType });
        }

       
    }
}
