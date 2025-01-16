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
using Microsoft.EntityFrameworkCore;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.UsercontrolsView;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    public class GradeViewModel:INotifyPropertyChanged
    {

        private readonly ApplicationDbContext _context;


        public ObservableCollection<Grade> GradeCollection { get; private set; }
        public ObservableCollection<StudentsEntity> StudentCollection { get; private set; }
        public ObservableCollection<SubjectsEntity> SubjectsCollection { get; private set; }
        public ObservableCollection<Semester> SemesterCollection { get; private set; }
     
        public ICommand LoadSubjectsCommand { get; }
        public ICommand DeleteGradeCommand { get; }
        public ICommand LoadStudentSubs { get; }
        public ICommand InsertGradeCommand { get; }


        public GradeViewModel(ApplicationDbContext context)
        {

            _context = context;

            GradeCollection = new ObservableCollection<Grade>();

            LoadSubjectsCommand = new RelayCommand(_ => LoadSubjectsAsync());
            DeleteGradeCommand = new RelayCommand(async _ => await DeleteGrade(),_=> Selected_grades != null);
            LoadSubjectsCommand.Execute(null);

            StudentCollection = new ObservableCollection<StudentsEntity>();
            SubjectsCollection = new ObservableCollection<SubjectsEntity>();
            SemesterCollection = new ObservableCollection<Semester>();
            LoadStudentSubs= new RelayCommand(async _=> await LoadSubjectForStudentsAsync());
            LoadStudentAsync();
            _=LoadSubjectForStudentsAsync();
            FilterSubjectsBasedOnSemesterAndStudentInfo();
            _ = LoadSemesterAsync();
            InsertGradeCommand = new RelayCommand(async _ => await InsertGrade());

        }

        private async Task DeleteGrade()
        {

            MessageBoxResult confirmation = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirmation == MessageBoxResult.Yes)
            {
                using (var context = new ApplicationDbContext())
                {

                    if (Selected_grades != null)
                    {


                        _context.Grades.Remove(Selected_grades);
                        await _context.SaveChangesAsync();
                        GradeCollection.Remove(Selected_grades);

                    }
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



        private StudentsEntity _selected_students;


        public StudentsEntity Selected_students
        {
            get => _selected_students;

            set
            {

                _selected_students = value;
                OnPropertyChanged(nameof(Selected_students));
                FilterSubjectsBasedOnSemesterAndStudentInfo();
                
            }
        }





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


        private Semester _selected_semester;


        public Semester Selected_semester
        {
            get => _selected_semester;

            set
            {

                _selected_semester = value;
                OnPropertyChanged(nameof(_selected_semester));

                if (Selected_semester != null)
                {
                    FilterSubjectsBasedOnSemesterAndStudentInfo();
                }
              
            }
        }


        private void LoadSubjectsAsync()
        {
    
   
            var subjects = _context.Grades.ToList();

            GradeCollection.Clear();

            foreach (var subject in subjects)
                    {

                GradeCollection.Add(subject);    
             }


        }

        private void LoadStudentAsync()
        {
            using(var context = new ApplicationDbContext())
            {

                var subjects = context.Students.
                    Include(x=>x.Program).
                    Include(x=>x.YearLevel).
                    ToList();

                StudentCollection.Clear();

                foreach (var subject in subjects)
                {

                    StudentCollection.Add(subject);
                }

            }

        }

        private async Task LoadSubjectForStudentsAsync()
        {
            using (var context = new ApplicationDbContext())
            {

                var subjects = context.Subjects.
                    Include(x=>x.Semester). ToList();

                SubjectsCollection.Clear();

                foreach (var subject in subjects)
                {

                    SubjectsCollection.Add(subject);
                }

            }

        }

        private void FilterSubjectsBasedOnSemesterAndStudentInfo()
        {
            SubjectsCollection.Clear();

            if (Selected_students != null && Selected_semester != null)
            {
                var filteredSubjects = _context.Subjects
                    .Where(s => s.ProgramID == Selected_students.ProgramID
                                && s.YearID == Selected_students.YearID
                                && s.SemesterID == Selected_semester.SemesterID)
                    .Include(x => x.Semester)
                    .ToList();

                foreach (var subject in filteredSubjects)
                {
                    SubjectsCollection.Add(subject);
                }
            }
            else if (Selected_students != null)
            {
                var filteredSubjects = _context.Subjects
                    .Where(s => s.ProgramID == Selected_students.ProgramID
                                && s.YearID == Selected_students.YearID)
                    .Include(x => x.Semester)
                    .ToList();

                foreach (var subject in filteredSubjects)
                {
                    SubjectsCollection.Add(subject);
                }
            }
            else
            {
                SubjectsCollection.Clear();
            }
        }





        private async Task LoadSemesterAsync()
        {
            using (var context = new ApplicationDbContext())
            {
                var semesters = await context.Semesters.ToListAsync();
                SemesterCollection.Clear();

                foreach (var semester in semesters)
                {
                    SemesterCollection.Add(semester);
                }

                if (SemesterCollection.Any())
                {
                    Selected_semester = SemesterCollection.First(); // Set a default value
                }
            }

        }

        //Insert Grade


        private async Task InsertGrade()
        {
            using (var context = new ApplicationDbContext())
            {
                var subject = context.Subjects.FirstOrDefault(s => s.SubjectID == Selected_subjects.SubjectID);
                if (subject == null)
                {
                    throw new Exception("Subject not found.");
                }

          
                var grade = new Grade
                {
                    GradeID = Guid.NewGuid().ToString(),
                    GradeValue = Selected_subjects.GradeValue,
                    DateAssigned = DateTime.Now,
                    StudentID = Selected_students.StudentID,
                    SubjectID = Selected_subjects.SubjectID
                };

                 context.Grades.Add(grade);

                 subject.GradeValue = null;

                 context.SaveChanges();

                MessageBox.Show("Grade Added Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }


        }


        //Boolean method to flag if is valid
        private bool canAddGradement() => true;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

