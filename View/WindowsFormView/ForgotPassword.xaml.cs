using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }



        //Validation for email
        private void Email_PreviewKeyDown(object sender, KeyEventArgs e)
        {
           if(e.Key== Key.Space)
            {

                e.Handled = true;
            }
        }

        private void Code_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]) || char.IsWhiteSpace(e.Text[0]))
            {
                e.Handled = true;
            }

        }

        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            string emailPattern = @"^[a-zA-Z0-9]+(\.{1}[a-zA-Z0-9]+)*@gmail\.com$";
            string emailInput = Email.Text.Trim();
            if (!Regex.IsMatch(emailInput, emailPattern) && !string.IsNullOrEmpty(emailInput))
            {
                MessageBox.Show("Invalid email address format. Email should only contain letters, numbers, dot (.) and at (@) symbols, and end with @gmail.com.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Email.Text = string.Empty;
            }
        }

        private void SEND_Click(object sender, RoutedEventArgs e)
        {
            string emailInput = Email.Text.Trim();

            if (string.IsNullOrEmpty(emailInput))
            {
                MessageBox.Show("Please enter an email address.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string otp = GenerateOTP();

            Application.Current.Properties["OTP"] = otp;

            SendOTPEmail(emailInput, otp);
        }

        private void OTP_Click(object sender, RoutedEventArgs e)
        {
            string enteredOtp = Code.Text;
            string? storedOtp = Application.Current.Properties["OTP"] as string;

            if (string.IsNullOrEmpty(storedOtp))
            {
                MessageBox.Show("No OTP generated. Please request a new one.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Email.Text = string.Empty;
                Code.Text = string.Empty;
                return;
            }

            if (enteredOtp == storedOtp)
            {
                MessageBox.Show("OTP verified successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                string emails = Email.Text;
                ChangePassword change = new ChangePassword(emails);
                 change.Show();
                this.Close();
                Email.Text = string.Empty;
                Clear();
            }
            else
            {
                MessageBox.Show("Invalid OTP. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Code.Text = string.Empty;
            }

        }
        private void Clear()
        {
            Email.Text = string.Empty;
            Code.Text = string.Empty;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            LoginWD change = new LoginWD();

            change.Show();
            this.Close();
        }

        public string GenerateOTP()
        {
            Random random = new Random();
            string otp = random.Next(100000, 999999).ToString();
            return otp;
        }

        public void SendOTPEmail(string emails, string otp)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("xmontemorjerald@gmail.com");
            mail.To.Add(emails);
            mail.Subject = "Your OTP Code";
            mail.Body = "Your OTP is: " + otp;

            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("xmontemorjerald@gmail.com", "wkwp iyfm umur lhse");
            smtpServer.EnableSsl = true;

            try
            {
                smtpServer.Send(mail);
                MessageBox.Show("OTP sent successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending OTP: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                // Optionally, clear the Email TextBox
                Email.Text = string.Empty;
            }
        }

        private void Code_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) 
            {
                e.Handled = true; 
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if(e.LeftButton == MouseButtonState.Pressed)
            {

                DragMove();
            }
        }
    }
}
