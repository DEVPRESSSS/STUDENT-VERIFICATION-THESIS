using Microsoft.EntityFrameworkCore;
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
using System.Windows.Input;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    public class DepartmentViewModel : INotifyPropertyChanged
    {
        private readonly ApplicationDbContext _context;

        public ObservableCollection<Departments> DepartmentsCollection { get; private set; }

        //Crud Commands

        public ICommand AddDepartmentCommand { get; }
        public ICommand UpdateDepartmentCommand { get; }
        public ICommand DeleteDepartmentCommand { get; }
        public ICommand LoadDepartmentCommand { get; }


        public DepartmentViewModel(ApplicationDbContext context)
        {
            
            _context = context;

            DepartmentsCollection= new ObservableCollection<Departments>();
            AddDepartmentCommand = new RelayCommand(async _ => await AddDepartmentAsync(),_ => canAddDepartment());
            UpdateDepartmentCommand = new RelayCommand(async _ => await UpdateDepartmentAsync(), _ => Selected_departments != null);
            DeleteDepartmentCommand = new RelayCommand(async _ => await DeleteDepartmentAsync(), _ => Selected_departments != null);
            LoadDepartmentCommand = new RelayCommand(async _ => await LoadDepartmentsAsync());
            LoadDepartmentCommand.Execute(null);

        }


     


        //Get the selected departments

        private Departments _selected_departments;


        public Departments Selected_departments
        {
            get => _selected_departments;

            set
            {

                _selected_departments = value;
                OnPropertyChanged(nameof(Selected_departments));
            }
        }


        //Department ID 

        private string _departmentID;

        public string DepartmentID
        {

            get => _departmentID;


            set
            {

                _departmentID= value;

                OnPropertyChanged();
            }

        }


        //Name

        private string _name;

        public string Name
        {

            get => _name;


            set
            {

                _name = value;

                OnPropertyChanged();
            }

        }

        
        //Insert Method 
        private async Task AddDepartmentAsync()
        {
            if (string.IsNullOrEmpty(_name))
            {
                MessageBox.Show("Please fill in all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }


            string ID = $"DEPARTMENT-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

            var obj = new Departments
            {
                DepartmentID = ID,
                Name = Name,
                CreatedAt= DateTime.Now,


            };

          

           _context.Departments.Add(obj);

           await _context.SaveChangesAsync();

            //Application.Current.Dispatcher.Invoke(() => DepartmentsCollection.Add(obj));
            //wait LoadDepartmentsAsync();
            OnPropertyChanged(nameof(DepartmentsCollection));


            MessageBox.Show("Department added success","Success", MessageBoxButton.OK, MessageBoxImage.Information);



        }

           //Update Department
        private async Task UpdateDepartmentAsync()
        {

      


            var existing_departments= await _context.Departments.FindAsync(Selected_departments.DepartmentID);

            if(existing_departments == null)
            {
                MessageBox.Show("Selected departments does not exist in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
            try
            {


                existing_departments.Name= Selected_departments.Name;

                _context.Departments.Update(existing_departments);

                await _context.SaveChangesAsync();

                MessageBox.Show("Department updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }

            catch (Exception ex)
            {
                MessageBox.Show($"Oops there is an error:{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Information);

            }




        }


        //Delete Department
        private async Task DeleteDepartmentAsync()
        {

            if (Selected_departments != null)
            {

                _context.Departments.Remove(Selected_departments);
                await _context.SaveChangesAsync();

                DepartmentsCollection.Remove(Selected_departments);

            }
        }

        private async Task LoadDepartmentsAsync()
        {

            DepartmentsCollection.Clear();

            var departments = await _context.Departments.ToListAsync();

            foreach (var item in departments)
            {

                DepartmentsCollection.Add(item);
            }
             OnPropertyChanged(nameof(DepartmentsCollection));


        }


        //Boolean method to flag if is valid
        private bool canAddDepartment() => true;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
