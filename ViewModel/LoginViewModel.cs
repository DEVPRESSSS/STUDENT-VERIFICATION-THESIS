using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Command;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    class LoginViewModel: INotifyPropertyChanged
    {

        private ApplicationDbContext _context;
        public ICommand LoginCommad { get; }

        public LoginViewModel(ApplicationDbContext context)
        {
            _context = context;
            LoginCommad = new RelayCommand(async _ => await LoginAsync(), _ => canLogin());
        }


        private string _username;

        public string Username
        {

            get => _username;

            set
            {

                _username = value;

                OnPropertyChanged();
            }

        }

        private string _password;

        public string Password
        {

            get => _password;

            set
            {

                _password = value;

                OnPropertyChanged();
            }

        }
        private bool canLogin()
        {

            return true;
        }
        
        



        private async Task LoginAsync()
        {

            try
            {
                var admin = await Task.Run(() =>
                    _context.Admin.FirstOrDefault(a => a.Username == Username && a.Password == Password));

                if (admin != null)
                {
                    MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    MainWindow dashboard = new MainWindow(_context);                  
                    dashboard.Show();

                    //this.Close();



                 
                       
                    
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
