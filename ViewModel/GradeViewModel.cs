using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Command;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.InkML;
using Notification.Wpf;


namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    public class GradeViewModel:INotifyPropertyChanged
    {

        private readonly ApplicationDbContext _context;

  


        public ObservableCollection<Grade> GradeCollection { get; private set; }
        public ObservableCollection<SubjectsEnrolled> SubjectEnrolledCollection { get; private set; }
        public ObservableCollection<StudentsEntity> StudentCollection { get; private set; }
        public ObservableCollection<SubjectsEntity> SubjectsCollection { get; private set; }
        public ObservableCollection<SubjectsEntity> SubjectPerProfCollection { get; private set; }
        public ObservableCollection<Semester> SemesterCollection { get; private set; }
        public ObservableCollection<ProgramEntity> ProgramCollection { get; private set; }
        public ObservableCollection<Year> YearCollection { get; private set; }
        public ObservableCollection<Scholarship> ScholarshipsCollection { get; private set; }
        public ObservableCollection<SchoolYear> SchoolYearCollection { get; private set; }
        public ObservableCollection<ProfessorsEntity> ProfessorsCollection { get; private set; }


        private ProfessorsEntity _selected_professor;


        public ProfessorsEntity Selected_professor
        {
            get => _selected_professor;

            set
            {

                _selected_professor = value;
                if(Selected_professor!= null)
                {
                    _ = LoadAssignedSubsAsync();

                }
                OnPropertyChanged(nameof(Selected_professor));
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


        private string _subjectID;

        public string Selected_subjectID
        {

            get => _subjectID;


            set
            {

                _subjectID = value;

               

                OnPropertyChanged();
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
                    _= LoadGradesAsync();
                    LoadSubjectsEnrolled();
                    OnPropertyChanged(nameof(Selected_subjects));

                }
            }
        }


        private Semester _selected_semester;


        public Semester? Selected_semester
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



        //Search Term in the Grades List
        private string _searchTerm;

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                if (string.IsNullOrWhiteSpace(_searchTerm))
                {
                    _ = LoadGradesAsync();
                }
                else
                {

                    _ = SearchGradeAsync();
                }
                OnPropertyChanged();
            }
        }





        //Search Term in the Grades List
        private string _searchTerm2;

        public string SearchTerm2
        {
            get => _searchTerm2;
            set
            {
                _searchTerm2 = value;
                if (string.IsNullOrWhiteSpace(_searchTerm2))
                {
                    _ = LoadStudentAsync();

                }
                else
                {

                    _ = SearchStudentAndSubjects();
                }
                OnPropertyChanged();
            }
        }

        //School Year

        private string _syID;

        public string Selected_syID
        {

            get => _syID;


            set
            {

                _syID = value;

                if(!string.IsNullOrWhiteSpace(Selected_syID))
                {
                    if(Selected_syID == "SCH0100")
                    {
                        _ = LoadGradesAsync();
                    }
                    else
                    {

                        _ = FilterBySchoolYear();


                    }
                }

                OnPropertyChanged();
            }

        }



        private SchoolYear _selected_schoolyear;


        public SchoolYear Selected_schoolyear
        {
            get => _selected_schoolyear;

            set
            {

                _selected_schoolyear = value;
                OnPropertyChanged(nameof(Selected_schoolyear));
            }
        }

        public ICommand LoadSubjectsCommand { get; }
        public ICommand DeleteGradeCommand { get; }
        public ICommand LoadStudentSubs { get; }
        public ICommand LoadStudentCommand { get; }
        public ICommand InsertGradeCommand { get; }
        public ICommand InsertManualCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand SearchCommand2 { get; }


        public GradeViewModel(ApplicationDbContext context)
        {

            _context = context;

            GradeCollection = new ObservableCollection<Grade>();
            LoadSubjectsCommand = new RelayCommand(_ => LoadGradesAsync());
            DeleteGradeCommand = new RelayCommand(async _ => await DeleteGrade(),_=> Selected_grades != null);
            LoadSubjectsCommand.Execute(null);
            StudentCollection = new ObservableCollection<StudentsEntity>();
            SubjectsCollection = new ObservableCollection<SubjectsEntity>();
            SubjectPerProfCollection = new ObservableCollection<SubjectsEntity>();
            SemesterCollection = new ObservableCollection<Semester>();
            ProgramCollection = new ObservableCollection<ProgramEntity>();
            YearCollection = new ObservableCollection<Year>();
            SchoolYearCollection = new ObservableCollection<SchoolYear>();
            ScholarshipsCollection = new ObservableCollection<Scholarship>();
            LoadStudentSubs= new RelayCommand(async _=> await LoadSubjectForStudentsAsync());
            LoadStudentCommand = new RelayCommand(async _=> await LoadStudentAsync());
            SearchCommand = new RelayCommand(async _ => await SearchGradeAsync(), _ => !string.IsNullOrWhiteSpace(SearchTerm));
            SearchCommand2 = new RelayCommand(async _ => await SearchStudentAndSubjects(), _ => !string.IsNullOrWhiteSpace(SearchTerm2));
            InsertGradeCommand = new RelayCommand(async _ => await InsertGrade());
            InsertManualCommand = new RelayCommand(async _ => await InsertGradePerSub());
            ProfessorsCollection = new ObservableCollection<ProfessorsEntity>();
            SubjectEnrolledCollection = new ObservableCollection<SubjectsEnrolled>();


            //Load students 
            _ = LoadStudentAsync();

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

            //Load School Year
            _ = LoadSchoolYearAsync();


            _ = LoadAssignedSubsAsync();

            LoadProfessorAsync();


        }

        private async Task DeleteGrade()
        {
            MessageBoxResult confirmation = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirmation == MessageBoxResult.Yes)
            {
                using (var context = new ApplicationDbContext()) // New DbContext instance
                {
                    if (Selected_grades != null)
                    {
                        // Attach the entity to the new context before removing
                        context.Attach(Selected_grades);
                        context.Grades.Remove(Selected_grades);
                        await context.SaveChangesAsync();

                        // Remove from local collection
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
        private async Task LoadGradesAsync()
        {
    
   
            var subjects =  await _context.Grades.
                Include(x=> x.Student).
                 Include(x => x.Subject).
                 Include(x => x.User).
                 Include(x => x.SY).
                ToListAsync();

            GradeCollection.Clear();

            foreach (var subject in subjects)
                    {

                GradeCollection.Add(subject);    
             }


        }


        /// <summary>
        /// Loas student
        /// </summary>
        private async Task  LoadStudentAsync()
        {
            using(var context = new ApplicationDbContext())
            {

                var subjects = await context.Students.
                    Include(x=>x.Program).
                    Include(x=>x.YearLevel).
                    Include(x=>x.Scholarship).
                    ToListAsync();

                StudentCollection.Clear();

                foreach (var subject in subjects)
                {

                    StudentCollection.Add(subject);
                }

                Selected_semester = null;

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
            else if (Selected_students != null )
            {
                if (Selected_students.ScholarshipID == "SCHO-1002")
                {
                    var filteredSubjects = _context.SubjectsEnrolled
                       .Where(se => se.StudentID == Selected_students.StudentID && se.IsEnrolled)
                       .Select(se => se.Subject) 
                       .ToList();

                    foreach (var subject in filteredSubjects)
                    {
                        SubjectsCollection.Add(subject);
                    }
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


                foreach (var subject in SubjectsCollection) 
                {
                    /*if(subject.GradeValue >= 100 || subject.GradeValue < 70)
                    {

                        MessageBox.Show("Grade is invalid","Error", MessageBoxButton.OK, MessageBoxImage.Error);

                        return;
                    }*/

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

                        context.Grades.Add (newGrade);
                        GradeCollection.Add(newGrade);
                    }

                }

                await context.SaveChangesAsync();
                MessageBox.Show("Grades processed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _= LoadGradesAsync();
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
        /// School Year
        /// </summary>
        /// <returns></returns>
        private async Task LoadSchoolYearAsync()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var sys = await context.SchoolYear.ToListAsync();

                    SchoolYearCollection.Clear();
                    foreach (var sy in sys)
                    {
                        SchoolYearCollection.Add(sy);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading departments: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                _= LoadStudentAsync();
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







        /// <summary>
        /// Search Grade
        /// </summary>
        /// <returns></returns>
        private async Task SearchGradeAsync()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var query = context.Grades
                        .Include(x => x.Student)
                        .Include(x => x.Subject)
                        .Include(x => x.User)
                        .Include(x => x.SY)
                        .AsQueryable();

                    // Apply search term filter
                    if (!string.IsNullOrWhiteSpace(SearchTerm))
                    {
                        query = query.Where(p =>
                            EF.Functions.Like(p.StudentID, $"%{SearchTerm}%") ||
                            EF.Functions.Like(p.GradeValue.ToString(), $"%{SearchTerm}%") ||
                            EF.Functions.Like(p.SubjectID.ToString(), $"%{SearchTerm}%") ||
                            EF.Functions.Like(p.Subject.CourseCode, $"%{SearchTerm}%") ||
                            EF.Functions.Like(p.GradeID, $"%{SearchTerm}%") ||
                            EF.Functions.Like(p.Student.Name, $"%{SearchTerm}%")
                        );
                    }

                  
                    var result = await query.ToListAsync();

                    // Update the ObservableCollection
                    GradeCollection.Clear();
                    foreach (var grade in result)
                    {
                        GradeCollection.Add(grade);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during search: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async Task FilterBySchoolYear()
        {
            using (var context = new ApplicationDbContext())
            {
                var query = context.Grades
                 .Include(x => x.Student)
                 .Include(x => x.Subject)
                 .Include(x => x.SY)
                 .Include(x => x.User)
                 .AsQueryable();

                query = query.Where(p => p.SchoolYearID == Selected_syID);

               

                var result = await query.ToListAsync();

                // Update the ObservableCollection
                GradeCollection.Clear();
                foreach (var grade in result)
                {
                    GradeCollection.Add(grade);
                }
            }

        }




        private async Task SearchStudentAndSubjects()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var query = context.Students
                        .Include(x => x.Program)
                        .Include(x => x.YearLevel)
                        .Include(x => x.Scholarship)
                        .AsQueryable();

                    query = query.Where(p =>
                        EF.Functions.Like(p.StudentID, $"%{SearchTerm2}%") ||
                        EF.Functions.Like(p.Name, $"%{SearchTerm2}%") ||
                        EF.Functions.Like(p.Program.Acronym, $"%{SearchTerm2}%") ||
                        EF.Functions.Like(p.YearLevel.Name, $"%{SearchTerm2}%") ||
                        EF.Functions.Like(p.IDnumber.ToString(), $"%{SearchTerm2}%") ||
                        EF.Functions.Like(p.Scholarship.Name, $"%{SearchTerm2}%")
                    );

                    var result = await query.ToListAsync();

                    StudentCollection.Clear();
                    foreach (var student in result)
                    {
                        StudentCollection.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during search: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        /// <summary>
        /// This will load the professor
        /// </summary>
        private void LoadProfessorAsync()
        {

            using (var context = new ApplicationDbContext())
            {
                var prof = context.Professors
                .Include(p => p.Departments).ToList();

                foreach (var professor in prof)
                {

                    ProfessorsCollection.Add(professor);
                }
                OnPropertyChanged(nameof(ProfessorsCollection));

            }
        }





        /// <summary>
        /// This will load the assigned Subjects
        /// </summary>
        /// <returns></returns>
        private async Task LoadAssignedSubsAsync()
        {
            using(var context = new ApplicationDbContext())
            {

                var subs = await context.Subjects.Where(x => x.ProfessorID == Selected_professor.ProfessorID).ToListAsync();

                SubjectPerProfCollection.Clear();
                foreach (var sub in subs)
                {

                    SubjectPerProfCollection.Add(sub);
                }
                OnPropertyChanged(nameof(SubjectPerProfCollection));



            }



        }





        private void LoadSubjectsEnrolled()
        {
            if (Selected_subjects == null)
            {
                return;
            }

            using (var context = new ApplicationDbContext())
            {
                var enrolled = context.SubjectsEnrolled
                    .Include(x => x.Subject)
                    .Include(x => x.Student)                    
                    .Where(x=> x.SubjectID == Selected_subjects.SubjectID)                 
                    .ToList();

                if (!enrolled.Any())
                {
                    ShowNotification("Error", "There is no enrolled students to this subject yet", NotificationType.Information);
                }

                SubjectEnrolledCollection.Clear();
                foreach (var subject in enrolled)
                {
                    SubjectEnrolledCollection.Add(subject);
                }
            }
        }

        private async Task InsertGradePerSub()
        {
            using (var context = new ApplicationDbContext())
            {
                foreach (var subject in SubjectEnrolledCollection)
                {
                    // 🚨 Validate if GradeValue is null or empty
                    if (string.IsNullOrWhiteSpace(subject.GradeValue?.ToString()))
                    {
                        ShowNotification("Error", $"Grade value for Student {subject.StudentID} in Subject {subject.SubjectID} is required.", NotificationType.Error);
                        return; // Stop execution if any GradeValue is invalid
                    }

                    // Check if a grade already exists for this student and subject
                    var existingGrade = context.Grades
                        .FirstOrDefault(g => g.StudentID == subject.StudentID && g.SubjectID == subject.SubjectID);

                    if (existingGrade != null)
                    {
                        // Update existing grade
                        existingGrade.GradeValue = subject.GradeValue;
                        existingGrade.DateAssigned = DateTime.Now;

                        // Update UI collection
                        var gradeInUI = GradeCollection.FirstOrDefault(g => g.StudentID == subject.StudentID && g.SubjectID == subject.SubjectID);
                        if (gradeInUI != null)
                        {
                            gradeInUI.GradeValue = subject.GradeValue;
                            gradeInUI.DateAssigned = DateTime.Now;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(Selected_syID))
                        {
                            ShowNotification("Error", "Please select a school year ID", NotificationType.Error);
                            return;
                        }

                        // Generate a unique Grade ID
                        string ID = $"GRD-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

                        var newGrade = new Grade
                        {
                            GradeID = ID,
                            GradeValue = subject.GradeValue?.ToString(),
                            DateAssigned = DateTime.Now,
                            StudentID = subject.StudentID,
                            SubjectID = subject.SubjectID,
                            EnrollmentID = subject.EnrollmentID,
                            SchoolYearID = Selected_syID,
                            StaffID = UserSessionService.Instance.LoggedInStaffID
                        };

                        context.Grades.Add(newGrade);
                        GradeCollection.Add(newGrade);
                    }
                }

                // Save changes asynchronously
                await context.SaveChangesAsync();
                MessageBox.Show("Grades processed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                await LoadGradesAsync();
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

            notificaficationManager.Show(
                  new NotificationContent { Title = title, Message = message, Type = notificationType }, expirationTime: TimeSpan.FromSeconds(5));




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

