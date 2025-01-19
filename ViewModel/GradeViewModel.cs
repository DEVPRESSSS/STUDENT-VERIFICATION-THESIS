using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Command;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Notifications.Wpf;
using Notifications.Wpf.Controls;


namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    public class GradeViewModel:INotifyPropertyChanged
    {

        private readonly ApplicationDbContext _context;

        //Notification 
        private readonly NotificationManager _notificationManager;


        public ObservableCollection<Grade> GradeCollection { get; private set; }
        public ObservableCollection<StudentsEntity> StudentCollection { get; private set; }
        public ObservableCollection<SubjectsEntity> SubjectsCollection { get; private set; }
        public ObservableCollection<Semester> SemesterCollection { get; private set; }
        public ObservableCollection<ProgramEntity> ProgramCollection { get; private set; }
        public ObservableCollection<Year> YearCollection { get; private set; }
        public ObservableCollection<Scholarship> ScholarshipsCollection { get; private set; }



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
                if (Selected_subjects != null)
                {
                    LoadSubjectsAsync();
                    OnPropertyChanged(nameof(Selected_subjects));

                }
            }
        }


        private Semester _selected_semester;


        public Semester Selected_semester
        {
            get => _selected_semester;

            set
            {

                _selected_semester = value;

                if (Selected_semester != null)
                {
                    FilterSubjectsBasedOnSemesterAndStudentInfo();
                }
                OnPropertyChanged();


            }
        }


        private ProgramEntity _program;


        public ProgramEntity Selected_Program
        {

            get => _program;

            set
            {

                _program = value;


                if (Selected_Program != null)
                {
                    //Load the filter
                    FilterStudentsByProgramAndYear();
                }

                OnPropertyChanged();

            }
        }


        private Year _year;


        public Year Selected_Year
        {

            get => _year;

            set
            {

                _year = value;



                if (Selected_Year != null)
                {
                    //Load the filter
                    FilterStudentsByProgramAndYear();
                }
                OnPropertyChanged();

            }
        }



        private Scholarship _scholarship;


        public Scholarship Selected_scholar
        {

            get => _scholarship;

            set
            {

                _scholarship = value;



                if (Selected_scholar != null)
                {
                    //Load the filter
                    FilterStudentsByProgramAndYear();
                }
                OnPropertyChanged();

            }
        }



        

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
            ProgramCollection = new ObservableCollection<ProgramEntity>();
            YearCollection = new ObservableCollection<Year>();
            ScholarshipsCollection = new ObservableCollection<Scholarship>();
            LoadStudentSubs= new RelayCommand(async _=> await LoadSubjectForStudentsAsync());
            _notificationManager = new NotificationManager();

            //Load students 
            LoadStudentAsync();

            //Load subjects filter
            _=LoadSubjectForStudentsAsync();

            //All categories filter
            FilterSubjectsBasedOnSemesterAndStudentInfo();

            //Load semester
            _ = LoadSemesterAsync();

            //Load program
            _ = LoadProgram();

            //Load year
            _ = LoadYear();


            //Load Schoolar
            _= LoadSchoolar();
            //Insert Grade 
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

       

        /// <summary>
        /// list of methods
        /// </summary>
        /// 


        ///Load Grades per sub
        private void LoadSubjectsAsync()
        {
    
   
            var subjects = _context.Grades.ToList();

            GradeCollection.Clear();

            foreach (var subject in subjects)
                    {

                GradeCollection.Add(subject);    
             }


        }


        /// <summary>
        /// Loas student
        /// </summary>
        private void LoadStudentAsync()
        {
            using(var context = new ApplicationDbContext())
            {

                var subjects = context.Students.
                    Include(x=>x.Program).
                    Include(x=>x.YearLevel).
                    Include(x=>x.Scholarship).
                    ToList();

                StudentCollection.Clear();

                foreach (var subject in subjects)
                {

                    StudentCollection.Add(subject);
                }

            }

        }


        /// <summary>
        /// Load subjcts based on the student
        /// </summary>
        /// <returns></returns>
        private async Task LoadSubjectForStudentsAsync()
        {
            using (var context = new ApplicationDbContext())
            {

                var subjects = await context.Subjects.
                    Include(x=>x.Semester). ToListAsync();

                SubjectsCollection.Clear();

                foreach (var subject in subjects)
                {

                    SubjectsCollection.Add(subject);
                }

            }

        }


        /// <summary>
        /// Filter on subjects
        /// </summary>
        private void FilterSubjectsBasedOnSemesterAndStudentInfo()
        {
            // Clear the current collection
            SubjectsCollection.Clear();

            // Case: Filter based on both Selected_students and Selected_semester
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
                if (Selected_students.ScholarshipID == "SCHO-1002")
                {
                    var filteredSubjects = _context.Subjects
                        .Where(s => s.ProgramID == Selected_students.ProgramID)
                        .Include(x => x.Semester)
                        .ToList();

                    foreach (var subject in filteredSubjects)
                    {
                        SubjectsCollection.Add(subject);
                    }

                   /* _notificationManager.Show(new NotificationContent
                    {
                        Title = "Non-scholar subjects",
                        Message = "Note that the non-scholar student can add subjects.",
                        Type = NotificationType.Success
                    });*/
                }
                else
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
            }
            // Case: No filters matched
            else
            {
                SubjectsCollection.Clear();
            }
        }



        /// <summary>
        /// Load semester
        /// </summary>
        /// <returns></returns>
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

                
            }

        }


        /// <summary>
        /// Insert grade functions
        /// </summary>
        /// <returns></returns>
        private async Task InsertGrade()
        {
            using (var context = new ApplicationDbContext())
            {
                foreach (var subject in SubjectsCollection.Where(s => s.GradeValue.HasValue)) 
                {
                    var existingGrade = await context.Grades
                        .FirstOrDefaultAsync(g => g.StudentID == Selected_students.StudentID && g.SubjectID == subject.SubjectID);

                    if (existingGrade != null)
                    {
                        existingGrade.GradeValue = subject.GradeValue;
                        existingGrade.DateAssigned = DateTime.Now;

                        var gradeInUI = GradeCollection.FirstOrDefault(g => g.StudentID == Selected_students.StudentID && g.SubjectID == subject.SubjectID);
                        if (gradeInUI != null)
                        {
                            gradeInUI.GradeValue = subject.GradeValue;
                            gradeInUI.DateAssigned = DateTime.Now;
                        }
                    }
                    else
                    {
                        string ID = $"GRD-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

                        var newGrade = new Grade
                        {
                            GradeID = ID,
                            GradeValue = subject.GradeValue,
                            DateAssigned = DateTime.Now,
                            StudentID = Selected_students.StudentID,
                            SubjectID = subject.SubjectID
                        };

                        context.Grades.Add(newGrade);
                        GradeCollection.Add(newGrade);
                    }

                }

                await context.SaveChangesAsync();
                MessageBox.Show("Grades processed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }


        }





        /// <summary>
        /// Load Program
        /// </summary>
        /// <returns></returns>
        private async Task LoadProgram()
        {
            using(var context = new ApplicationDbContext())
            {

                var programs = await context.Programs.ToListAsync();


                foreach (var program in programs)
                {

                    ProgramCollection.Add(program);
                }
            }

        }

        /// <summary>
        /// Load year
        /// </summary>
        /// <returns></returns>

        private async Task LoadYear()
        {
            using (var context = new ApplicationDbContext())
            {

                var programs = await context.Year.ToListAsync();


                foreach (var program in programs)
                {

                    YearCollection.Add(program);
                }
            }

        }

        /// <summary>
        /// Filter students based on the program
        /// </summary>
        /// <returns></returns>


        private void FilterStudentsByProgramAndYear()
        {
            // If neither program nor year is selected, load all students
            if (Selected_Program == null && Selected_Year == null && Selected_scholar == null)
            {
                LoadStudentAsync();
                return;
            }

            using (var context = new ApplicationDbContext())
            {
                var query = context.Students
                    .Include(x => x.Program)
                    .Include(x => x.YearLevel)
                    .Include(x => x.Scholarship)
                    .AsQueryable();

                // Apply program filter if a program is selected
                if (Selected_Program != null)
                {
                    query = query.Where(s => s.ProgramID == Selected_Program.ProgramID);
                }

                // Apply year filter if a year is selected
                if (Selected_Year != null)
                {
                    query = query.Where(s => s.YearID == Selected_Year.YearID);
                }

                if (Selected_scholar != null)
                {
                    query = query.Where(s => s.ScholarshipID == Selected_scholar.ScholarshipID);
                }

                var filteredStudents = query.ToList();

                StudentCollection.Clear();

                foreach (var student in filteredStudents)
                {
                    StudentCollection.Add(student);
                }
            }
        }

        /// <summary>
        /// Load Scholar
        /// </summary>
        /// <returns></returns>


        private async Task LoadSchoolar()
        {
            using (var context = new ApplicationDbContext())
            {


                var schoolar = await context.Scholarship.ToListAsync();


                foreach (var item in schoolar)
                {


                    ScholarshipsCollection.Add(item);
                }

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

