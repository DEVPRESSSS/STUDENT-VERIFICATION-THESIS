using Microsoft.EntityFrameworkCore;
using Notification.Wpf;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Command;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.WindowsFormView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    public class LfcVM : INotifyPropertyChanged
    {

        private readonly ApplicationDbContext _context;


        private Window _loginWindow;
        private PasswordBox _passwordBox;

        public ICommand? loginCommand { get; }
        public LfcVM(ApplicationDbContext context, Window loginWindow, PasswordBox passwordBox)
        {
            
            _context = context;
            _loginWindow = loginWindow;
            loginCommand = new RelayCommand(async _=> await Login(), _=> canLogin());
            _passwordBox = passwordBox;


        }


        //Username get and setter
        private string? _username;


        public string? Username
        {
            get => _username;

            set
            {

                _username = value;

                OnPropertyChanged();
            }


        }


        //Password get and setter
        private string? _password;


        public string? Password
        {
            get => _password;

            set
            {

                _password = value;

                OnPropertyChanged();
            }


        }

        private async Task Login()
        {
            try
            {

                

                if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                {
                    ShowNotification("Error", "All fields are required", NotificationType.Error);

                    return;
                }


                //Find admin username and password
                var findAdmin = await _context.Admin
                     .FirstOrDefaultAsync(x => x.Username == Username && x.Password == Password);
              

                if (findAdmin != null)
                {

                    ShowNotification("Success", "Login successfully", NotificationType.Success);

                    //Redirect to Mainwindow
                    MainWindow dashboard = new MainWindow(_context);
                    dashboard.Show();
                    CloseCurrentActiveWindow();
                    return;
                }

                //Find staff username and password
                var staff = _context.Staffs.FirstOrDefault(s => s.Username == Username && s.Password == Password);
                if (staff != null)
                {
                    ShowNotification("Success", "Login successfully", NotificationType.Success);

                    //Redirect to Encoder dashboard
                    EncoderDashboard staffDashboard = new EncoderDashboard(_context,staff.Username);
                    staffDashboard.Show();
                    CloseCurrentActiveWindow();
                    return;
                }


                //Notify the user if iinvalid username or password
                ShowNotification("Error", "Invalid Username or Password", NotificationType.Error);
                Password = string.Empty;

                Clear();
            }
            catch
            {


            }


        }


        //Notications WPF intrgartion
        private void ShowNotification(string title, string message, NotificationType notificationType)
        {

            var notificaficationManager = new NotificationManager();


            notificaficationManager.Show(
                  new NotificationContent { Title = title, Message = message, Type = notificationType });
        }

        public void Clear()
        {

            Username = string.Empty;
            _passwordBox.Clear();
        }




        //Validation for Login
        private bool canLogin()
        {

            if(_username == null || _password == null)
            {

                return false;
            }

            return true;
        }

        //Close the login form if the username and password are correct
        public void CloseCurrentActiveWindow()
        {
           _loginWindow.Hide();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


