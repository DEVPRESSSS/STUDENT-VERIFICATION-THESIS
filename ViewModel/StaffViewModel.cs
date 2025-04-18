﻿using Microsoft.EntityFrameworkCore;
using Notification.Wpf;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Command;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    public class StaffViewModel:INotifyPropertyChanged
    {


        private readonly ApplicationDbContext _context;


        public ObservableCollection<StaffsEntity> StaffCollection { get; private set; }
        public ObservableCollection<Role> RoleCollection { get; private set; }
        public ICommand? AddStaffCommand { get; }
        public ICommand? UpdateStaffCommand { get; }
        public ICommand? LoadStaffCommand { get; }
        public ICommand? DeleteStaffCommand { get; }
        public ICommand? ClearCommand { get; }
        public ICommand? CloseWindow { get; }


        private StaffsEntity _selected_Staff;


        public StaffsEntity Selected_staff
        {

            get => _selected_Staff;


            set
            {

                _selected_Staff = value;
                OnPropertyChanged(nameof(Selected_staff));
            }


        }



        private string _name;


        public string Name
        {

            get => _name;


            set
            {

                _name = value;
                OnPropertyChanged(nameof(Name));
            }


        }



        private string _email;


        public string Email
        {

            get => _email;


            set
            {

                _email = value;
                OnPropertyChanged(nameof(Email));
            }


        }


        private string _username;


        public string Username
        {

            get => _username;


            set
            {

                _username = value;
                OnPropertyChanged(nameof(Username));
            }


        }


        private string _password;


        public string Password
        {

            get => _password;


            set
            {

                _password = value;
                OnPropertyChanged(nameof(Password));
            }


        }
        private string _RoleID;


        public string RoleID
        {

            get => _RoleID;


            set
            {

                _RoleID = value;
                OnPropertyChanged(nameof(RoleID));
            }


        }

        private Role _role;


        public Role Selected_Role
        {

            get => _role;


            set
            {

                _role = value;
            
                OnPropertyChanged(nameof(Selected_Role));
            }


        }



        public StaffViewModel(ApplicationDbContext context)
        {
            _context= context;
            StaffCollection = new ObservableCollection<StaffsEntity>();
            RoleCollection = new ObservableCollection<Role>();
            LoadStaffCommand= new RelayCommand(async _ => await  LoadStaffAsync());
            LoadStaffCommand.Execute(null);

            AddStaffCommand = new RelayCommand(async _ => await AddStaffAsync());
            UpdateStaffCommand = new RelayCommand(async _ => await UpdateStaffAsync(), _ => Selected_staff!= null);
            DeleteStaffCommand = new RelayCommand(async _ => await DeleteStaffAsync(), _ => Selected_staff!= null);
            ClearCommand = new RelayCommand( _ => Clear());
            CloseWindow = new RelayCommand( _ => CloseCurrentActiveWindow());

            _ = LoadRoleAsync();
        }


        /// <summary>
        /// Delete stafd
        /// </summary>
        /// <returns></returns>
        private async Task DeleteStaffAsync()
        {

            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(messageBoxResult == MessageBoxResult.Yes)
            {


                try
                {

                    using (var context = new ApplicationDbContext())
                    {

                        context.Staffs.Remove(Selected_staff);
                        await context.SaveChangesAsync();
                        StaffCollection.Remove(Selected_staff);

                    }
                }
                catch
                {

                    ShowNotification("Error","Oops this User has a related record in Grade table! Delete it first but make sure it is not important!", NotificationType.Error);
                }
            

                   
                
            }
        }


        /// <summary>
        /// Add Staff method
        /// </summary>
        
        private async Task AddStaffAsync()
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Email))
            {
                MessageBox.Show("Name, email, username, or password can't be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            
            // Check for duplicate staff
            var existingEmailAndUserName = await _context.Staffs.FirstOrDefaultAsync(x =>
                x.Name == Name || x.Username == Username || x.Email == Email);

            if (existingEmailAndUserName != null)
            {
                MessageBox.Show("Name, email, or username already exists. Please use a different one!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);

            // Create new staff
            string ID = $"STAFF-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

            var newStaff = new StaffsEntity
            {
                StaffID = ID,
                Name = Name,
                Email = Email,
                Username = Username,
                Password = hashedPassword,
                RoleID = RoleID,
            };

            // Add to database
            try
            {
                _context.Staffs.Add(newStaff);
                await _context.SaveChangesAsync();
                StaffCollection.Add(newStaff);
                _ =LoadStaffAsync();
                Clear();
                MessageBox.Show("Staff added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding staff: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
               
            }
        }


        /// <summary>
        /// Update staff method
        /// </summary>
        private async Task UpdateStaffAsync()
        {

            try
            {

                if (string.IsNullOrWhiteSpace(Selected_staff.Name) || string.IsNullOrWhiteSpace(Selected_staff.Username) || string.IsNullOrWhiteSpace(Selected_staff.Password) || string.IsNullOrWhiteSpace(Selected_staff.Email))
                {


                    MessageBox.Show($"Name, email, or username can't be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;

                }

                var existingEmailAndUserName = await _context.Staffs
                    .Where(x => (x.Name == Selected_staff.Name || x.Username == Selected_staff.Username || x.Email == Selected_staff.Email)
                            && x.StaffID != Selected_staff.StaffID) 
                    .FirstOrDefaultAsync();


                if (existingEmailAndUserName != null)
                {
                    MessageBox.Show($"Name, email, or username is already exist please use different one!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;

                }


                var staffID = _context.Staffs.FirstOrDefault(x => x.StaffID == Selected_staff.StaffID);
                            string ID = $"STAFF-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

                if(staffID != null)
                {
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Selected_staff.Password);


                    staffID.Name = Selected_staff.Name;
                    staffID.Email = Selected_staff.Email;
                    staffID.Username = Selected_staff.Username;
                    staffID.Password = hashedPassword;
                    staffID.RoleID = Selected_staff.RoleID;



                    _context.Staffs.Update(staffID);
                    await _context.SaveChangesAsync();

                    MessageBox.Show($"Staff updated successfully!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                    _= LoadStaffAsync();
                    CloseCurrentActiveWindow();
                }

              

            


            }

            catch (Exception ex)
            {

                MessageBox.Show($"Error: {ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }










        /// <summary>
        /// Load all staff
        /// </summary>
        /// 


        private BrushConverter converter = new BrushConverter();
        private string[] myArray = { "#1098AD", "#1E88E5", "#FF8F00", "#FF5252", "#6741D9", "#0CA678" };
        private Brush brush;

        private async Task LoadStaffAsync()
        {
            using (var context = new ApplicationDbContext())
            {
                var staffList = await context.Staffs
                    .Include(x => x.Role)
                    .ToListAsync();

                StaffCollection.Clear();

                for (int i = 0; i < staffList.Count; i++)
                {
                    var staff = staffList[i];
                    string colorString = myArray[i % myArray.Length];
                    brush = (Brush)converter.ConvertFromString(colorString);

                    // Get first letter of name as Character
                    string name = staff.Name;
                    staff.Character = name.Length > 0 ? name.Substring(0, 1) : string.Empty;

                    // Set the background color
                    staff.bgColor = brush;

                    StaffCollection.Add(staff);
                }
            }
        }


        /// <summary>
        /// Load each role from Db
        /// </summary>


        private async Task LoadRoleAsync()
        {
           
                var stafflist = await _context.Role.ToListAsync();



                RoleCollection.Clear();

                foreach (var staff in stafflist)
                {

                    RoleCollection.Add(staff);
                }
            

        }


        /// <summary>
        /// Clear the fields
        /// </summary>
        private void Clear()
        {

            Name = string.Empty;
            Username = string.Empty;
            Email= string.Empty;
            Password = string.Empty;
            Selected_Role = null;
        }
        public void CloseCurrentActiveWindow()
        {
            if (string.IsNullOrWhiteSpace(Selected_staff.Name) || string.IsNullOrWhiteSpace(Selected_staff.Username) || string.IsNullOrWhiteSpace(Selected_staff.Password) || string.IsNullOrWhiteSpace(Selected_staff.Email))
            {


                MessageBox.Show($"Name, email, or username can't be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;

            }

            // Close the active window
            var activeWindow = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(window => window.IsActive);

            if (activeWindow != null)
            {
                activeWindow.Close();
            }
        }

        private void ShowNotification(string title, string message, NotificationType notificationType)
        {

            var notificaficationManager = new NotificationManager();

            if (NotificationType.Success == notificationType)
            {
                notificaficationManager.Show(
                  new NotificationContent { Title = title, Message = message, Type = notificationType }, expirationTime: TimeSpan.FromSeconds(5));

            }
            else
            {

                notificaficationManager.Show(
                      new NotificationContent { Title = title, Message = message, Type = notificationType }, expirationTime: TimeSpan.FromSeconds(5));

            }





        }


        /// <summary>
        /// Character Staff
        /// </summary>
        /// <returns></returns>


       


        private bool canAddDepartment()
        {


            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
