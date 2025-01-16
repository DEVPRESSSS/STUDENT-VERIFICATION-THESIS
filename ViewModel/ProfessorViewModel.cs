using Microsoft.EntityFrameworkCore;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Command;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.UsercontrolsView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    public class ProfessorViewModel:INotifyPropertyChanged
    {
        //Inject the applicationddbcontext

        private readonly ApplicationDbContext _context;

        //Declare observable collection of professors

        public ObservableCollection<ProfessorsEntity> ProfessorsCollection { get; private set; }


        //Declare Observable collection in Departments

        public ObservableCollection<Departments> DepartmentCollection { get; private set; }


        //CRUD commands
        public ICommand AddProfessorrsCommand { get; }
        public ICommand UpdateProfessorrsCommand { get; }
        public ICommand DeleteProfessorrsCommand { get; }
        public ICommand LoadProfessorrsCommand { get; }

       // public ICommand LoadDepartmentCommand { get; }



        public ProfessorViewModel(ApplicationDbContext context)
        {
            _context = context;

            ProfessorsCollection = new ObservableCollection<ProfessorsEntity>();
            DepartmentCollection = new ObservableCollection<Departments>();
            AddProfessorrsCommand = new RelayCommand(async _ => await AddProfessorAsync(), _ => CanExecuteAddStudent());
            UpdateProfessorrsCommand = new RelayCommand(async _ => await UpdateProfessorAsync(), _ => Selected_professor != null);
            DeleteProfessorrsCommand = new RelayCommand(async _ => await DeleteProfessorAsync(), _ => Selected_professor != null);
            LoadProfessorrsCommand = new RelayCommand(async _ => await LoadProfessorAsync());
            LoadProfessorrsCommand.Execute (null);

            _ = LoadDepartmentsAsync();






        }

   

        //Using encapsulation



        //Declare an Encapsulation 

        private ProfessorsEntity _selected_professor;


        public ProfessorsEntity Selected_professor
        {
            get => _selected_professor;

            set
            {

                _selected_professor = value;
                OnPropertyChanged(nameof(Selected_professor));
            }
        }


        //Department ID

        private string _selectedDepartmentID;


        public string SelectedDepartmentID
        {

            get => _selectedDepartmentID;


            set
            {

                _selectedDepartmentID = value;

                OnPropertyChanged();
            }


        }


        private string _professorID;

        public string ProfessorID
        {

            get => _professorID;

            set
            {

                _professorID = value;
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
                OnPropertyChanged ();
            }
        }


        //Age 
        private int _age;

        public int Age
        {

            get => _age;

            set
            {

                _age = value;
                OnPropertyChanged();
            }
        }

        //Addresss
        private string _address;

        public string Address
        {

            get => _address;

            set
            {

                _address = value;
                OnPropertyChanged();
            }
        }


        //Email
        private string _email;

        public string Email
        {

            get => _email;

            set
            {

                _email = value;
                OnPropertyChanged();
            }
        }

        //Profilepath
        private string _profilepath;

        public string ProfilePath
        {

            get => _profilepath;

            set
            {

                _profilepath = value;
                OnPropertyChanged();
            }
        }

        
        
        
        
        //Delete method
        private async  Task DeleteProfessorAsync()
        {

            MessageBoxResult confirmation = MessageBox.Show("Are you sure want to delete this record?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirmation == MessageBoxResult.Yes)
            {

                try
                {

                    if (Selected_professor != null)
                    {

                        _context.Professors.Remove(Selected_professor);

                        await _context.SaveChangesAsync();

                        ProfessorsCollection.Remove(Selected_professor);

                        MessageBox.Show("Professor record deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    }

                }
                catch (Exception e)
                {

                    MessageBox.Show($"Error:{e}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);


                }
            }




          

        }



        //Update method
        private async Task UpdateProfessorAsync()
        {
            if (Selected_professor == null)
            {
                MessageBox.Show("No professor selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(Selected_professor.ProfessorID))
            {
                MessageBox.Show("Selected professor does not have a valid ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var existingProfessor = await _context.Professors.FindAsync(Selected_professor.ProfessorID);
            if (existingProfessor == null)
            {
                MessageBox.Show("Selected professor does not exist in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(Name) || Age <= 20 || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Address))
            {
                MessageBox.Show("Please fill in all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                existingProfessor.Name = Selected_professor.Name;
                existingProfessor.Age = Selected_professor.Age;
                existingProfessor.Email = Selected_professor.Email;
                existingProfessor.Address = Selected_professor.Address;
                existingProfessor.DepartmentID = Selected_professor.DepartmentID;

                _context.Professors.Update(existingProfessor);
                await _context.SaveChangesAsync();

                MessageBox.Show("Professor updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                await LoadProfessorAsync();
                CloseCurrentActiveWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating professor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        //Load professors
        private async Task LoadProfessorAsync()
        {
            var prof = await _context.Professors
                .Include(p=>p.Departments).ToListAsync();

            ProfessorsCollection.Clear();
            foreach (var professor in prof)
            {

                ProfessorsCollection.Add(professor);
            }
            OnPropertyChanged(nameof(ProfessorsCollection));


        }



        //Add professors

        public async Task AddProfessorAsync()
        {
           string profkey = $"PROF-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

            try
            {
                if (string.IsNullOrEmpty(Name) || Age <= 20 || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Address))
                {
                    MessageBox.Show("Please fill in all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newProfessor = new ProfessorsEntity
                {
                    ProfessorID = profkey,
                    Name = Name,
                    Age = Age,
                    Email = Email,
                    Address = Address,
                    CreatedAt = DateTime.Now,
                    DepartmentID = SelectedDepartmentID
                };

                _context.Professors.Add(newProfessor);
                await _context.SaveChangesAsync();


                ProfessorsCollection.Add(newProfessor);

                await LoadProfessorAsync();


                MessageBox.Show("Professor added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding professor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        //Load departments table
        private async Task LoadDepartmentsAsync()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var departments = await context.Departments.ToListAsync();

                    DepartmentCollection.Clear();
                    foreach (var department in departments)
                    {
                        DepartmentCollection.Add(department);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading departments: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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




        //Boolean to flag if it is valid
        private bool CanExecuteAddStudent() => true;


        //Event handler
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }






    }
}
