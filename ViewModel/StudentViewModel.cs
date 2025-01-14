using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Command;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.RegularExpressions;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms;
using System.ComponentModel.DataAnnotations;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.UsercontrolsView;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    public class StudentViewModel:INotifyPropertyChanged
    {
        private readonly ApplicationDbContext _context;

        public ObservableCollection<StudentsEntity> StudentsCollection { get; private set; }
        public ObservableCollection<ProgramEntity> ProgramsCollection { get; private set; }
        public ObservableCollection<Year> YearCollection { get; private set; }
       public ObservableCollection<Grade> SubjectGrades { get; private set; } 

        //Crud Commands

        public ICommand AddStudentscommand { get; }
        public ICommand UpdateStudentsCommand { get; set; }
        public ICommand DeleteStudentsCommand { get; }
        public ICommand LoadStudentsCommand { get; }
        public ICommand LoadGradeCommand { get; }
        public ICommand ClearCommand { get; }

        public ICommand InsertGradeCommand { get; }


        //Listener to close the form


        public StudentViewModel(ApplicationDbContext context)
        {

            _context = context;
            StudentsCollection = new ObservableCollection<StudentsEntity>();
            SubjectGrades = new ObservableCollection<Grade>();
            AddStudentscommand = new RelayCommand(async _ => await AddStudentAsync());
            UpdateStudentsCommand = new RelayCommand(async _ => await UpdateStudentAsync(), _ => Selected_students != null);
            DeleteStudentsCommand = new RelayCommand(async _ => await DeleteStudentAsync(), _ => Selected_students != null);
            LoadStudentsCommand = new RelayCommand(async _ => await LoadStudentAsync());


            ClearCommand = new RelayCommand(_ => ClearAsync());
            ProgramsCollection = new ObservableCollection<ProgramEntity>(); 
            YearCollection = new ObservableCollection<Year>();
            LoadStudentsCommand.Execute(null);
            _ = LoadProgramAsync();
            _ = LoadYearAsync();
            _ = LoadSubjectsAsync();
        }



        private StudentsEntity _selected_students;

        public StudentsEntity Selected_students
        {
            get => _selected_students;

            set
            {

                _selected_students = value;
                OnPropertyChanged(nameof(Selected_students));
                if (_selected_students != null)
                {
                    _ = LoadSubjectsAsync();
                }
            }
        }




        private Grade _selected_grade;


        public Grade Selected_grades
        {
            get => _selected_grade;

            set
            {

                _selected_grade = value;
                OnPropertyChanged(nameof(Selected_grades));
            }
        }


       

        //Program ID 

        private string _programID;

        public string Selected_programID
        {

            get => _programID;


            set
            {

                _programID = value;

                OnPropertyChanged();
            }

        }



        private string _yearID;

        public string Selected_yearID
        {

            get => _yearID;


            set
            {

                _yearID = value;

                OnPropertyChanged();
            }

        }

        //Name

        private string _name;


        [Required(ErrorMessage = "Name is Required")]

        public string Name
        {

            get => _name;


            set
            {

                _name = value;
                OnPropertyChanged();

            }
        }



        private int _age;

        [Required(ErrorMessage = "Age is Required")]


        public int Age
        {

            get => _age;


            set
            {

                _age = value;
                OnPropertyChanged();

            }

        }

        private long _IDnumber;


        [Required(ErrorMessage = "IDnumber is Required")]

        public long IDnumber
        {

            get => _IDnumber;


            set
            {

                _IDnumber = value;
                OnPropertyChanged();

            }

        }

        private long _contact;


        [Required(ErrorMessage = "Contact is Required")]

        public long Contact
        {

            get => _contact;


            set
            {

                _contact = value;

            }

        }




        private string _gmail;


        [Required(ErrorMessage = "Gmail is Required")]

        public string Gmail
        {

            get => _gmail;


            set
            {

                _gmail = value;
                OnPropertyChanged();

            }


        }



        private string _address;


        [Required(ErrorMessage = "Address is Required")]

        public string Address
        {

            get => _address;


            set
            {

                _address = value;

                OnPropertyChanged();

            }


        }









        //Insert Method 
        private async Task AddStudentAsync()
        {
            if (string.IsNullOrEmpty(Name))
            {
                MessageBox.Show("Name can't be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            if (IDnumber <= 0)
            {
                MessageBox.Show("ID Number is required and cannot be null or zero.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Contact <= 0)
            {
                MessageBox.Show("Contact number is required and cannot be null or zero.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            try
            {
                var exisitngStudent = await _context.Students
                    .FirstOrDefaultAsync(s => s.Name == Name || s.Gmail == Gmail || s.IDnumber == IDnumber);



                if (exisitngStudent != null)
                {
                    MessageBox.Show($"Student name or ID number or Gmail is already exists. Please use a different one.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string ID = $"STU-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

                var obj = new StudentsEntity
                {

                    StudentID = ID,
                    Name = Name,
                    Age = Age,
                    IDnumber = IDnumber,
                    Contact= Contact,
                    Gmail = Gmail,
                    Address = Address,
                    YearID = Selected_yearID,
                    ProgramID = Selected_programID,

                };



                _context.Students.Add(obj);

                await _context.SaveChangesAsync();
                StudentsCollection.Add(obj);





                MessageBox.Show("Student added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                ClearAsync();
            }
            catch(Exception ex)
            {

                MessageBox.Show($"Error{ex}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearAsync();

            }




        }

        //Update Student record
        private async Task UpdateStudentAsync()
        {

            

            var existing_student = await _context.Students.FindAsync(Selected_students.StudentID);

            if (existing_student == null)
            {
                MessageBox.Show("Selected Student does not exist in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            var existing_subjects = await _context.Students.FindAsync(Selected_students.StudentID);

            if (existing_subjects == null)
            {
                MessageBox.Show("Selected Student does not exist in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
            try
            {
         

                existing_subjects.Name = Selected_students.Name;
                existing_subjects.Age = Selected_students.Age;
                existing_subjects.IDnumber = Selected_students.IDnumber;
                existing_subjects.Contact = Selected_students.Contact;
                existing_subjects.Gmail = Selected_students.Gmail;
                existing_subjects.ProgramID = Selected_students.ProgramID;
                existing_subjects.YearID = Selected_students.YearID;
                existing_subjects.Address = Selected_students.Address;

                _context.Students.Update(existing_subjects);
                await _context.SaveChangesAsync();

                MessageBox.Show("Student updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                await LoadStudentAsync();

                CloseCurrentActiveWindow();




            }

            catch (Exception ex)
            {
                MessageBox.Show($"Oops there is an error:{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Information);

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

        //Delete Student
        private async Task DeleteStudentAsync()
        {


            MessageBoxResult confirmation = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(confirmation == MessageBoxResult.Yes)
            {
                if (Selected_students != null)
                {

                    _context.Students.Remove(Selected_students);
                    await _context.SaveChangesAsync();

                    StudentsCollection.Remove(Selected_students);

                }


            }




        }

        private async Task LoadStudentAsync()
        {

           
            var prof = await _context.Students
                 .Include(s => s.Program)
                 .Include(s => s.YearLevel)
                 .ToListAsync();


            StudentsCollection.Clear();
            foreach (var professor in prof)
            {

                StudentsCollection.Add(professor);
            }
            OnPropertyChanged(nameof(StudentsCollection));



        }

        //Method load Program
        private async Task LoadProgramAsync()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var departments = await context.Programs.ToListAsync();

                    ProgramsCollection.Clear();
                    foreach (var department in departments)
                    {
                        ProgramsCollection.Add(department);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading departments: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        //Method that load year 
        private async Task LoadYearAsync()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var departments = await context.Year.ToListAsync();

                    YearCollection.Clear();
                    foreach (var department in departments)
                    {
                        YearCollection.Add(department);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading departments: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        //Clear textfields
        public void ClearAsync()
        {
            Selected_students = null; // Assuming this is your property for binding selected students

            Name = string.Empty;
            Age = 0;
            IDnumber = 0;
            Contact = 0;
            Gmail = string.Empty;
            Address = string.Empty;
           
        }









        private async Task LoadSubjectsAsync()
        {
            try
            {
                if (Selected_students == null)
                {
                    Debug.WriteLine("No student selected.");
                    return;
                }

                using (var context = new ApplicationDbContext())
                {
                    var subjectGradesList = await context.Grades
                        .Where(x => x.Student.StudentID == Selected_students.StudentID) 
                        .Include(x => x.Subject)
                        .Include(x => x.Student)
                        .ToListAsync();

                    SubjectGrades.Clear();
                    foreach (var grade in subjectGradesList)
                    {
                        SubjectGrades.Add(grade);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading subjects: {ex.Message}");
            }
        }









        private bool CanExecuteInsertGrade()
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
