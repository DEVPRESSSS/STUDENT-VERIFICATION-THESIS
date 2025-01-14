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
using System.Windows.Input;
using System.Windows;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.UsercontrolsView;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    public class SubjectsViewModel:INotifyPropertyChanged
    {

        private readonly ApplicationDbContext _context;

        public ObservableCollection<SubjectsEntity> SubjectCollection { get; private set; }
        public ObservableCollection<ProfessorsEntity> ProfessorCollection { get; private set; }
        public ObservableCollection<ProgramEntity> ProgramsCollection { get; private set; }
        public ObservableCollection<Year> YearCollection { get; private set; }


        //Crud Commands

        public ICommand AddSubjectCommand { get; }
        public ICommand UpdateSubjectCommand { get; }
        public ICommand DeleteSubjectCommand { get; }
        public ICommand LoadSubjectCommand { get; }
        public ICommand ClearCommand { get; }

        public SubjectsViewModel(ApplicationDbContext context)
        {

            _context = context;

            SubjectCollection = new ObservableCollection<SubjectsEntity>();
            AddSubjectCommand = new RelayCommand(async _ => await AddSubjectAsync(), _ => canAddDepartment());
            UpdateSubjectCommand = new RelayCommand(async _ => await UpdateSubjectAsync(), _ => Selected_subjects != null);
            DeleteSubjectCommand = new RelayCommand(async _ => await DeleteSubjectAsync(), _ => Selected_subjects != null);
            LoadSubjectCommand = new RelayCommand(async _ => await LoadSubjectsAsync());
            LoadSubjectCommand.Execute(null);


            ProgramsCollection = new ObservableCollection<ProgramEntity>(); // Fix this
            ProfessorCollection = new ObservableCollection<ProfessorsEntity>(); // Fix this
            YearCollection = new ObservableCollection<Year>(); // Fix this
            ClearCommand = new RelayCommand(_ => Clear());

            _ = LoadProfessorsAsync();
            _= LoadProgramAsync();
            _ = LoadYearAsync();


        }





        //Get the selected departments

        private SubjectsEntity _selected_subjects;


        public SubjectsEntity Selected_subjects
        {
            get => _selected_subjects;

            set
            {

                _selected_subjects = value;
                OnPropertyChanged(nameof(Selected_subjects));
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


        private string _professorID;

        public string Selected_professorID
        {

            get => _professorID;


            set
            {

                _professorID = value;

                OnPropertyChanged();
            }

        }

        //Acronym

        private string _subjectname;

        public string SubjectName
        {

            get => _subjectname;


            set
            {
                
                    _subjectname = value;
                    OnPropertyChanged();
                
            }

        }

        //Name

        private string _courseCode;

        public string CourseCode
        {

            get => _courseCode;


            set
            {

                _courseCode = value;

                OnPropertyChanged();
            }

        }


        private int _units;

        public int Units
        {

            get => _units;


            set
            {

                _units = value;

                OnPropertyChanged();
            }

        }







        private string _selectedProgramID;


        public string SelectedProgramID
        {

            get => _selectedProgramID;


            set
            {

                _selectedProgramID = value;

                OnPropertyChanged();
            }


        }



        private string _description;

        public string Description
        {

            get => _description;


            set
            {

                _description = value;

                OnPropertyChanged();
            }

        }



     







        //Insert Method 
        private async Task AddSubjectAsync()
        {
            if (string.IsNullOrEmpty(SubjectName) || string.IsNullOrEmpty(CourseCode) || string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(SelectedProgramID) || string.IsNullOrEmpty(Selected_yearID))
            {
                MessageBox.Show("Please fill in all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }
            
            if(Units <= 0)
            {
                MessageBox.Show("Units can't be zero.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }
            


            try
            {


                var existingSubjects = await _context.Subjects
                  .FirstOrDefaultAsync(s => s.SubjectName == SubjectName || s.CourseCode == CourseCode);



                if (existingSubjects != null)
                {
                    MessageBox.Show($"Subjectname  or Coursecode is already exists. Please use a different one.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                string ID = $"SUB-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

                var obj = new SubjectsEntity
                {
                    SubjectID = ID,
                    SubjectName = SubjectName,
                    CourseCode = CourseCode,
                    Description = Description,
                    ProfessorID = Selected_professorID,
                    ProgramID = SelectedProgramID,
                    YearID = Selected_yearID,
                    Units = Units,



                };



                _context.Subjects.Add(obj);

                await _context.SaveChangesAsync();
                SubjectCollection.Add(obj);





                MessageBox.Show("Subject added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                OnPropertyChanged(nameof(SubjectCollection));
                await LoadSubjectsAsync();

                Clear();
            }
            catch(Exception ex)
            {

                MessageBox.Show($"Error{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }



        }

        //Update Department
        // Update Subject
        private async Task UpdateSubjectAsync()
        {
            if (Selected_subjects == null)
            {
                MessageBox.Show("Please select a subject to update.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Fetch the subject from the database
                var existingSubject = await _context.Subjects.FindAsync(Selected_subjects.SubjectID);
                if (existingSubject == null)
                {
                    MessageBox.Show("The selected subject does not exist in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Check for duplicate SubjectName or CourseCode, excluding the current subject
                var duplicateSubject = await _context.Subjects
                    .FirstOrDefaultAsync(s =>
                        (s.SubjectName == Selected_subjects.SubjectName || s.CourseCode == Selected_subjects.CourseCode) &&
                        s.SubjectID != Selected_subjects.SubjectID);

                if (duplicateSubject != null)
                {
                    MessageBox.Show("Another subject with the same name or course code already exists.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Update the properties of the existing subject
                existingSubject.SubjectName = Selected_subjects.SubjectName;
                existingSubject.CourseCode = Selected_subjects.CourseCode;
                existingSubject.Description = Selected_subjects.Description;
                existingSubject.Units = Selected_subjects.Units;
                existingSubject.ProfessorID = Selected_subjects.ProfessorID;
                existingSubject.ProgramID = Selected_subjects.ProgramID;
                existingSubject.YearID = Selected_subjects.YearID;

                // Mark entity as modified and save changes
                _context.Subjects.Update(existingSubject);
                await _context.SaveChangesAsync();

                MessageBox.Show("Subject updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Refresh the subject collection (optional: uncomment if needed)
                // await LoadSubjectsAsync();

                CloseCurrentActiveWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        //Delete Department
        private async Task DeleteSubjectAsync()
        {

            MessageBoxResult confirmation = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirmation == MessageBoxResult.Yes)
            {

                if (Selected_subjects != null)
                {

                    _context.Subjects.Remove(Selected_subjects);
                    await _context.SaveChangesAsync();

                    SubjectCollection.Remove(Selected_subjects);

                }

            }

         
        }

        private async Task LoadSubjectsAsync()
        {

            var prof = await _context.Subjects
                 .Include(s => s.Program) 
                 .Include(s => s.Professors) 
                 .Include(s => s.Year)
                 .ToListAsync();


            SubjectCollection.Clear();
            foreach (var professor in prof)
            {

                SubjectCollection.Add(professor);
            }



        }


        //Load departments table
        private async Task LoadProfessorsAsync()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var departments = await context.Professors.ToListAsync();

                    ProfessorCollection.Clear();
                    foreach (var department in departments)
                    {
                        ProfessorCollection.Add(department);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading departments: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        //Load program table
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


        //Load Year table
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

        //Close the form
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
        private void Clear()
        {
            Selected_subjects = null;
            SubjectName= string.Empty;
            CourseCode = string.Empty;
            Description = string.Empty;
            //ProfessorID = Selected_professorID,
           // ProgramID = SelectedProgramID,
            //YearID = Selected_yearID,
            Units = 0;
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
