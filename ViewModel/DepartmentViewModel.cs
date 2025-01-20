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
        public ICommand SearchCommand { get; }


        public DepartmentViewModel(ApplicationDbContext context)
        {
            
            _context = context;

            DepartmentsCollection= new ObservableCollection<Departments>();
            AddDepartmentCommand = new RelayCommand(async _ => await AddDepartmentAsync(),_ => canAddDepartment());
            UpdateDepartmentCommand = new RelayCommand(async _ => await UpdateDepartmentAsync(), _ => Selected_departments != null);
            DeleteDepartmentCommand = new RelayCommand(async _ => await DeleteDepartmentAsync(), _ => Selected_departments != null);
            LoadDepartmentCommand = new RelayCommand(async _ => await LoadDepartmentsAsync());
            LoadDepartmentCommand.Execute(null);
            SearchCommand = new RelayCommand(async _ => await SearchProgramAsync(), _ => !string.IsNullOrWhiteSpace(SearchTerm));

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

                OnPropertyChanged(nameof(Name));
            }

        }



        //Search 
        private string _searchTerm;

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                if (string.IsNullOrWhiteSpace(_searchTerm))
                {
                    _ = LoadDepartmentsAsync();
                }
                else
                {

                    _ = SearchProgramAsync();
                }
            }
        }

        private async Task SearchProgramAsync()
        {
            try
            {

                using (var context = new ApplicationDbContext())
                {

                    var query = context.Departments.AsQueryable();

                    query = query.Where(p =>
                        EF.Functions.Like(p.Name, $"%{SearchTerm}%") ||
                        EF.Functions.Like(p.DepartmentID, $"%{SearchTerm}%")
                    );

                    var result = await query.ToListAsync();

                    // Update the ObservableCollection
                    DepartmentsCollection.Clear();
                    foreach (var professor in result)
                    {
                        DepartmentsCollection.Add(professor);
                    }


                }






            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during search: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Insert Method 
        private async Task AddDepartmentAsync()
        {
            if (string.IsNullOrEmpty(Name))
            {
                MessageBox.Show("Please fill in all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Check if the department name already exists
                var existingDepartment = await _context.Departments
                    .FirstOrDefaultAsync(d => d.Name == Name);

                if (existingDepartment != null)
                {
                    MessageBox.Show($"Department name '{Name}' already exists. Please use a different name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Clear();
                    return;
                }

                // Generate a unique Department ID
                string ID = $"DEPARTMENT-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

                // Create a new Department object
                var obj = new Departments
                {
                    DepartmentID = ID,
                    Name = Name,
                    CreatedAt = DateTime.Now,
                };

                // Add the department to the database
                _context.Departments.Add(obj);
                await _context.SaveChangesAsync();

                // Update the ObservableCollection only after saving to the database
                DepartmentsCollection.Add(obj);

                MessageBox.Show("Department added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        //Clear 

        private void Clear()
        {
            Name = string.Empty;


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

            if (string.IsNullOrEmpty(Selected_departments.Name))
            {
                MessageBox.Show("Please fill in all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                

                var existingDepartment = await _context.Departments
                .FirstOrDefaultAsync(d => d.Name == Name);

                if (existingDepartment != null)
                {
                    MessageBox.Show($"Department name '{Name}' already exists. Please use a different name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Clear();
                    return;
                }


                existing_departments.Name= Selected_departments.Name;

                _context.Departments.Update(existing_departments);

                await _context.SaveChangesAsync();

                MessageBox.Show("Department updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                CloseCurrentActiveWindow();
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Please provide a name", "Error", MessageBoxButton.OK, MessageBoxImage.Information);

              
            }




        }


        //Delete Department
        private async Task DeleteDepartmentAsync()
        {
            MessageBoxResult confiramtion = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confiramtion == MessageBoxResult.Yes)
            {
                if (Selected_departments != null)
                {
                    // Step 1: Set the DepartmentID of professors to null
                    var professorsInDepartment = _context.Professors.Where(p => p.DepartmentID == Selected_departments.DepartmentID).ToList();

                    foreach (var professor in professorsInDepartment)
                    {
                        professor.DepartmentID = null; // Nullify the DepartmentID
                    }

                    _context.Professors.UpdateRange(professorsInDepartment);

                    // Step 2: Now delete the department
                    _context.Departments.Remove(Selected_departments);

                    await _context.SaveChangesAsync();

                    DepartmentsCollection.Remove(Selected_departments);
                }
            }
               
        }

        private async Task LoadDepartmentsAsync()
        {


            using(var context = new ApplicationDbContext())
            {

                DepartmentsCollection.Clear();

                var departments = await context.Departments.ToListAsync();

                foreach (var item in departments)
                {

                    DepartmentsCollection.Add(item);
                }
                OnPropertyChanged(nameof(DepartmentsCollection));

            }
         


        }



        public void CloseCurrentActiveWindow()
        {
            var activeWindow = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(window => window.IsActive);

            if (activeWindow != null)
            {
                activeWindow.Close();
            }
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
