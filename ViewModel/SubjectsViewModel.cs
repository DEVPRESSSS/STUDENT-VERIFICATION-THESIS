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

        //Crud Commands

        public ICommand AddSubjectCommand { get; }
        public ICommand UpdateSubjectCommand { get; }
        public ICommand DeleteSubjectCommand { get; }
        public ICommand LoadSubjectCommand { get; }


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

            _ = LoadProfessorsAsync();
            _= LoadProgramAsync();

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


        //Department ID 

        private string _programID;

        public string ProgramID
        {

            get => _programID;


            set
            {

                _programID = value;

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
            if (string.IsNullOrEmpty(SubjectName) || string.IsNullOrEmpty(CourseCode) || string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(SelectedProgramID))
            {
                MessageBox.Show("Please fill in all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }


            string ID = $"SUB-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

            var obj = new SubjectsEntity
            {
                SubjectID= ID,
                SubjectName= SubjectName,
                CourseCode=CourseCode,
                Description= Description,
                ProfessorID= Selected_professorID,
                ProgramID= SelectedProgramID,
                Units=Units,



            };



            _context.Subjects.Add(obj);

            await _context.SaveChangesAsync();
            SubjectCollection.Add(obj);



            OnPropertyChanged(nameof(SubjectCollection));


            MessageBox.Show("Subject added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);



        }

        //Update Department
        private async Task UpdateSubjectAsync()
        {




            var existing_subjects = await _context.Subjects.FindAsync(Selected_subjects.SubjectID);

            if (existing_subjects == null)
            {
                MessageBox.Show("Selected subjects does not exist in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
            try
            {

                existing_subjects.SubjectName = Selected_subjects.SubjectName;
                existing_subjects.CourseCode = Selected_subjects.CourseCode;
                existing_subjects.Units = Selected_subjects.Units;
                existing_subjects.ProgramID = Selected_subjects.ProgramID;
                existing_subjects.Description = Selected_subjects.Description;
                existing_subjects.ProfessorID = Selected_subjects.ProfessorID;

                _context.Subjects.Update(existing_subjects);
                await _context.SaveChangesAsync();

                MessageBox.Show("Subject updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }

            catch (Exception ex)
            {
                MessageBox.Show($"Oops there is an error:{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Information);

            }




        }


        //Delete Department
        private async Task DeleteSubjectAsync()
        {

            if (Selected_subjects != null)
            {

                _context.Subjects.Remove(Selected_subjects);
                await _context.SaveChangesAsync();

                SubjectCollection.Remove(Selected_subjects);

            }
        }

        private async Task LoadSubjectsAsync()
        {

            var prof = await _context.Subjects
                 .Include(p => p.Program).ToListAsync();

            SubjectCollection.Clear();
            foreach (var professor in prof)
            {

                SubjectCollection.Add(professor);
            }
            OnPropertyChanged(nameof(SubjectCollection));



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



        //Load departments table
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


        //Boolean method to flag if is valid
        private bool canAddDepartment() => true;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
