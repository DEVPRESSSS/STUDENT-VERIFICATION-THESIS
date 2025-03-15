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
using System.Windows.Media;

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
                OnPropertyChanged();
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
                    _ = LoadProfessorAsync();
                }
                else
                {

                    _ = SearchProfessorsAsync();
                }
            }
        }


        //CRUD commands
        public ICommand AddProfessorrsCommand { get; }
        public ICommand UpdateProfessorrsCommand { get; }
        public ICommand DeleteProfessorrsCommand { get; }
        public ICommand LoadProfessorrsCommand { get; }
        public ICommand SearchCommand { get; set; }
        public ICommand ClearCommand { get; set; }


        public ProfessorViewModel(ApplicationDbContext context)
        {
            _context = context;

            ProfessorsCollection = new ObservableCollection<ProfessorsEntity>();
            DepartmentCollection = new ObservableCollection<Departments>();
            AddProfessorrsCommand = new RelayCommand(async _ => await AddProfessorAsync(), _ => CanExecuteAddStudent());
            UpdateProfessorrsCommand = new RelayCommand(async _ => await UpdateProfessorAsync(), _ => Selected_professor != null);
            DeleteProfessorrsCommand = new RelayCommand(async _ => await DeleteProfessorAsync(), _ => Selected_professor != null);
            LoadProfessorrsCommand = new RelayCommand(async _ => await LoadProfessorAsync());
            SearchCommand = new RelayCommand(async _ => await SearchProfessorsAsync(), _ => !string.IsNullOrWhiteSpace(SearchTerm));
            LoadProfessorrsCommand.Execute (null);
            ClearCommand = new RelayCommand(async _ => await Clear());
            _ = LoadDepartmentsAsync();

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



        // Update method
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

            // Validate fields
            if (string.IsNullOrWhiteSpace(Selected_professor.Name) ||
                string.IsNullOrWhiteSpace(Selected_professor.Email) ||
                string.IsNullOrWhiteSpace(Selected_professor.Address)) // Use Selected_professor.Address
            {
                MessageBox.Show("Please fill in all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Check for existing Name or Email
                var existingProfInfo = await _context.Professors
                    .FirstOrDefaultAsync(s => (s.Name == Selected_professor.Name || s.Email == Selected_professor.Email) &&
                                              s.ProfessorID != Selected_professor.ProfessorID);

                if (existingProfInfo != null)
                {
                    MessageBox.Show("Name or email already exists. Please use a different one.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Update the professor details
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




  

        private BrushConverter converter = new BrushConverter();
        private string[] myArray = { "#1098AD", "#1E88E5", "#FF8F00", "#FF5252", "#6741D9", "#0CA678" };
        private Brush brush;

        private async Task LoadProfessorAsync()
        {
            using (var context = new ApplicationDbContext())
            {
                var prof = await _context.Professors
                 .Include(p => p.Departments).ToListAsync();

                ProfessorsCollection.Clear();

                for (int i = 0; i < prof.Count; i++)
                {
                    var staff = prof[i];
                    string colorString = myArray[i % myArray.Length];
                    brush = (Brush)converter.ConvertFromString(colorString);

                    // Get first letter of name as Character
                    string name = staff.Name;
                    staff.Character = name.Length > 0 ? name.Substring(0, 1) : string.Empty;

                    // Set the background color
                    staff.bgColor = brush;

                    ProfessorsCollection.Add(staff);
                    OnPropertyChanged(nameof(ProfessorsCollection));

                }
            }
        }


        //Search function
        private async Task SearchProfessorsAsync()
        {
            try
            {

                

                    var query = _context.Professors.AsQueryable();

                    query = query.Where(p =>
                        EF.Functions.Like(p.ProfessorID, $"%{SearchTerm}%") ||
                        EF.Functions.Like(p.Name, $"%{SearchTerm}%") ||
                        EF.Functions.Like(p.Email, $"%{SearchTerm}%") ||
                        EF.Functions.Like(p.Address, $"%{SearchTerm}%") ||
                        EF.Functions.Like(p.DepartmentID, $"%{SearchTerm}%") ||
                        p.Age.ToString().Contains(SearchTerm)
                    );

                    var result = await query.ToListAsync();

                    // Update the ObservableCollection
                    ProfessorsCollection.Clear();
                    foreach (var professor in result)
                    {
                        ProfessorsCollection.Add(professor);
                    }
                
              
               

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during search: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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


                var existingProfInfo = await _context.Professors
                                   .FirstOrDefaultAsync(s => s.Name == Name || s.Email == Email);

                if (existingProfInfo != null)
                {
                    MessageBox.Show($"Name or email is already exists. Please use a different one.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                _= Clear();


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


        //Clear function

        private async Task  Clear()
        {


            Name = string.Empty;
            Address = string.Empty;
            Email = string.Empty;
            Age = 0;
            Name = string.Empty;
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
