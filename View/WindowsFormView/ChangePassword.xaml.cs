using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        private string _email;
        public ChangePassword(string email)
        {
            InitializeComponent();
            _email = email;
        }

        private void NewPassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }

        }

        private void ConfirmPassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }

        }

        private async void Change_Click(object sender, RoutedEventArgs e)
        {
            string pass1;
            string pass2;

            // Determine which controls are visible and use their values
            if (NewPassword.Visibility == Visibility.Visible)
            {
                pass1 = NewPassword.Password;
                pass2 = ConfirmPassword.Password;
            }
            else
            {
                pass1 = NewPasswordTextBox.Text;
                pass2 = ConfirmPasswordTextBox.Text;
            }

            // Validate fields
            if (string.IsNullOrWhiteSpace(pass1) || string.IsNullOrWhiteSpace(pass2))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (pass1 != pass2)
            {
                MessageBox.Show("Passwords don't match", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                NewPassword.Password = string.Empty;
                ConfirmPassword.Password = string.Empty;
                NewPasswordTextBox.Text = string.Empty;
                ConfirmPasswordTextBox.Text = string.Empty;
                return;
            }


            await UpdatePasswordInTableAsync(pass2);


        }
        public async Task<bool> VerifyEmailInAdminAsync(string email)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // Check if the email exists in the Admin table
                    bool emailExists = await context.Admin
                        .AnyAsync(a => a.Email == email);

                    return emailExists;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        public async Task UpdatePasswordInTableAsync(string newPassword)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // Check if the email belongs to an Admin or Staff
                    var admin = await context.Admin.FirstOrDefaultAsync(a => a.Email == _email);
                    var staff = await context.Staffs.FirstOrDefaultAsync(s => s.Email == _email);

                    if (admin != null)
                    {
                        // Update password for Admin
                        if (admin.Password != newPassword)
                        {
                            admin.Password = newPassword;
                            await context.SaveChangesAsync();
                            MessageBox.Show("Admin password updated successfully!",
                                              "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("The new password is the same as the old one. Please choose a different password.",
                                              "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else if (staff != null)
                    {
                        // Update password for Staff
                        if (staff.Password != newPassword)
                        {
                            staff.Password = newPassword;
                            await context.SaveChangesAsync();
                            MessageBox.Show("Staff password updated successfully!",
                                              "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("The new password is the same as the old one. Please choose a different password.",
                                              "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to update the password. Email not found in either Admin or Staff records.",
                                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    // Redirect to LoginView or other actions
                    var loginView = new LoginForm();
                    loginView.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            ForgotPassword change = new ForgotPassword();
            
            change.Show();
            this.Close();
        }

        private void revealmode_Unchecked(object sender, RoutedEventArgs e)
        {
            ConfirmPassword.Password = ConfirmPasswordTextBox.Text;
            ConfirmPasswordTextBox.Visibility = Visibility.Collapsed;
            ConfirmPassword.Visibility = Visibility.Visible;

            NewPassword.Password = NewPasswordTextBox.Text;
            NewPasswordTextBox.Visibility = Visibility.Collapsed;
            NewPassword.Visibility = Visibility.Visible;
        }

        private void revealmode_Checked(object sender, RoutedEventArgs e)
        {
            ConfirmPasswordTextBox.Text = ConfirmPassword.Password;
            ConfirmPassword.Visibility = Visibility.Collapsed;
            ConfirmPasswordTextBox.Visibility = Visibility.Visible;

            NewPasswordTextBox.Text = NewPassword.Password;
            NewPassword.Visibility = Visibility.Collapsed;
            NewPasswordTextBox.Visibility = Visibility.Visible;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {

                DragMove();
            }
        }
    }
}
