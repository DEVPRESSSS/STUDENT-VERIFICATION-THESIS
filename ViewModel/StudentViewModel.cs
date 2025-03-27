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
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.Win32;
using OfficeOpenXml;
using Notification.Wpf;
using Microsoft.Data.SqlClient;
using DocumentFormat.OpenXml.InkML;
using System.Windows.Media;
using DocumentFormat.OpenXml.Vml.Spreadsheet;


namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    public class StudentViewModel:INotifyPropertyChanged
    {
        private readonly ApplicationDbContext _context;

        public ObservableCollection<StudentsEntity> StudentsCollection { get; private set; }
        public ObservableCollection<ProgramEntity> ProgramsCollection { get; private set; }
        public ObservableCollection<Year> YearCollection { get; private set; }
       public ObservableCollection<Grade> SubjectGrades { get; private set; } 
       public ObservableCollection<Scholarship> ScholarshipsCollection { get; private set; } 
       public ObservableCollection<SubjectsEntity> SubjectPerProgram { get; private set; }
       public ObservableCollection<SubjectsEnrolled> ListOfSubjectsEnrolled { get; private set; }
       public ObservableCollection<StudentsEntity> StudentBulkCollection { get; private set; }



        //Crud Commands
        public ICommand AddStudentscommand { get; }
        public ICommand UpdateStudentsCommand { get; set; }
        public ICommand DeleteStudentsCommand { get; }
        public ICommand LoadStudentsCommand { get; }
        public ICommand LoadGradeCommand { get; }
        public ICommand ClearCommand { get; }

        public ICommand InsertGradeCommand { get; }
        public ICommand printGradeCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand AddSubjectCommand { get; }
        public ICommand ChooseFileCommand { get; }
        public ICommand BulkInsertCommand { get; }
        public ICommand DeleteSubjectEnrolledCommand { get; }
        public ICommand EnrollSecondSemCommand { get; }
        public ICommand AssignedProfCommand { get; }

        public StudentViewModel(ApplicationDbContext context)
        {

            _context = context;
            StudentsCollection = new ObservableCollection<StudentsEntity>();

            SubjectGrades = new ObservableCollection<Grade>();

            AddStudentscommand = new RelayCommand(async _ => await AddStudentAsync());

            UpdateStudentsCommand = new RelayCommand(async _ => await UpdateStudentAsync(), _ => Selected_students != null);

            DeleteStudentsCommand = new RelayCommand(async _ => await DeleteStudentAsync(), _ => Selected_students != null);

            DeleteSubjectEnrolledCommand = new RelayCommand(async _ => await DeleteSubjectEnrolled(), _ => Selected_subjectEnrolled != null);

            LoadStudentsCommand = new RelayCommand(async _ => await LoadStudentAsync());

            AddSubjectCommand = new RelayCommand(async _ => await AddSubjectAsync());

            ClearCommand = new RelayCommand(_ => ClearAsync());

            ProgramsCollection = new ObservableCollection<ProgramEntity>(); 

            YearCollection = new ObservableCollection<Year>();

            ScholarshipsCollection = new ObservableCollection<Scholarship>();

            ListOfSubjectsEnrolled = new ObservableCollection<SubjectsEnrolled>();

            SubjectPerProgram = new ObservableCollection<SubjectsEntity>();

            StudentBulkCollection = new ObservableCollection<StudentsEntity>();

            LoadStudentsCommand.Execute(null);

            SearchCommand = new RelayCommand(async _ => await SearchProgramAsync(), _ => !string.IsNullOrWhiteSpace(SearchTerm));

            ChooseFileCommand = new RelayCommand(_ => ExtractStudent());
            BulkInsertCommand = new RelayCommand(_ => MultiInsertStudent());


            EnrollSecondSemCommand = new RelayCommand(_ =>EnrollSecondSem());


         

            _ = LoadProgramAsync();
            _ = LoadYearAsync();
            _ = LoadSubjectsAsync();
            _ = LoadSchoolar();
            _ = LoadStudentSubAsync();
          

        }

        
        private StudentsEntity _selected_students;

        public StudentsEntity Selected_students
        {
            get => _selected_students;

            set
            {

                _selected_students = value;

                if (Selected_students != null)
                {
                    _ = LoadSubjectsAsync();
                    _ = LoadSubjectsPerProgram();
                    _ = LoadStudentSubAsync();


                }
                OnPropertyChanged(nameof(Selected_students));


            }
        }



        private SubjectsEnrolled _selected_subjectenrolled;

        public SubjectsEnrolled Selected_subjectEnrolled
        {
            get => _selected_subjectenrolled;

            set
            {

                _selected_subjectenrolled = value;
            
                OnPropertyChanged(nameof(_selected_subjectenrolled));


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



        private Scholarship _selected_schoolar;


        public Scholarship Selected_schoolar
        {
            get => _selected_schoolar;

            set
            {

                _selected_schoolar = value;
                OnPropertyChanged(nameof(Selected_schoolar));
            }
        }




        private SubjectsEntity _selected_Subjects;


        public SubjectsEntity Selected_Subjects
        {
            get => _selected_Subjects;

            set
            {

                _selected_Subjects = value;
                OnPropertyChanged(nameof(Selected_Subjects));
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


        private string _professorID;

        public string ProfessorID
        {

            get => _professorID;


            set
            {

                _professorID = value;

                OnPropertyChanged(ProfessorID);
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
                _ = SearchProgramAsync();
            }

        }



        private string _schoolarID;

        public string Selected_SchoolarID
        {

            get => _schoolarID;


            set
            {

                _schoolarID = value;

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



        private string _searchTerm;

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                if (string.IsNullOrWhiteSpace(_searchTerm))
                {
                    _ = LoadStudentAsync();
                }
                else
                {

                    _ = SearchProgramAsync();
                }

                OnPropertyChanged();

            }
        }


        private string _semester;



        public string Semester
        {

            get => _semester;


            set
            {

                _semester = value;

                OnPropertyChanged(Semester);

            }


        }



        private string _encoderName;



        public string EncoderName
        {

            get => _encoderName;


            set
            {

                _encoderName = value;

                OnPropertyChanged(EncoderName);

            }


        }


        private string _syName;



        public string SchoolYearName
        {

            get => _syName;


            set
            {

                _syName = value;

                OnPropertyChanged(SchoolYearName);

            }


        }




        /// <summary>
        /// Search by program
        /// </summary>
        /// <returns></returns>
        private async Task SearchProgramAsync()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var query = context.Students
                        .Include(s => s.YearLevel)
                        .Include(s => s.Program)
                        .Include(s => s.Scholarship)
                        .AsQueryable();

                
                    // Apply general search term filter
                    if (!string.IsNullOrEmpty(SearchTerm))
                    {
                        query = query.Where(p =>
                            EF.Functions.Like(p.Name, $"%{SearchTerm}%") ||
                            EF.Functions.Like(p.Age.ToString(), $"%{SearchTerm}%") ||
                            EF.Functions.Like(p.IDnumber.ToString(), $"%{SearchTerm}%") ||
                            EF.Functions.Like(p.Address, $"%{SearchTerm}%") ||
                            EF.Functions.Like(p.YearLevel.Name, $"%{SearchTerm}%") ||
                            EF.Functions.Like(p.Scholarship.Name, $"%{SearchTerm}%") ||
                            EF.Functions.Like(p.Program.Acronym, $"%{SearchTerm}%")
                        );
                    }

                    // Apply year-specific filter
                

                    var result = await query.ToListAsync();

                    // Update the ObservableCollection
                    StudentsCollection.Clear();
                    foreach (var student in result)
                    {
                        StudentsCollection.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during search: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
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
                    .FirstOrDefaultAsync(s => s.Name == Name || s.IDnumber == IDnumber);



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
                    Address = Address,
                    YearID = Selected_yearID,
                    ProgramID = Selected_programID,
                    ScholarshipID= Selected_SchoolarID

                };



                _context.Students.Add(obj);

                await _context.SaveChangesAsync();


                StudentsCollection.Add(obj);

                //Enrolled the scholar to its subjects
                using (var db = new ApplicationDbContext())
                {
                    var fetchSubs = await db.Subjects
                        .Where(x => x.ProgramID == Selected_programID && x.YearID == Selected_yearID && x.SemesterID == "SEM101")
                        .ToListAsync(); 

                    if (fetchSubs.Any()) 
                    {
                        foreach (var sub in fetchSubs)
                        {
                            string enrollmentID = $"SUB-ENROLLED-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

                            var subjectEnrolled = new SubjectsEnrolled
                            {
                                EnrollmentID = enrollmentID,
                                SubjectID = sub.SubjectID,
                                StudentID = ID,
                                IsEnrolled = true
                            };

                            db.SubjectsEnrolled.Add(subjectEnrolled); 
                        }

                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        MessageBox.Show("No subjects found for the selected program and year.");
                    }
                }
                







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
            // Find the existing student in the database
            var existing_student = await _context.Students.FindAsync(Selected_students.StudentID);

            if (existing_student == null)
            {
                MessageBox.Show("Selected Student does not exist in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate if any field contains only whitespace
            if (string.IsNullOrWhiteSpace(Selected_students.Name) ||
                string.IsNullOrWhiteSpace(Selected_students.Address))
            {
                MessageBox.Show("Fields cannot contain only whitespace. Please fill out all fields correctly.",
                                "Validation Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            // Check if Name or Gmail already exists for another student
            var duplicateName = await _context.Students
                .AnyAsync(s => s.Name == Selected_students.Name && s.StudentID != Selected_students.StudentID);

    

            if (duplicateName)
            {
                MessageBox.Show("The Name is already associated with another student.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            

            try
            {
                // Check if YearID has changed
                bool isYearChanged = existing_student.YearID != Selected_students.YearID;

                // Update student fields
                existing_student.Name = Selected_students.Name;
                existing_student.Age = Selected_students.Age;
                existing_student.IDnumber = Selected_students.IDnumber;
                existing_student.Contact = Selected_students.Contact;
                existing_student.ProgramID = Selected_students.ProgramID;
                existing_student.YearID = Selected_students.YearID;
                existing_student.Address = Selected_students.Address;

                _context.Students.Update(existing_student);
                await _context.SaveChangesAsync();

                MessageBox.Show("Student updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                using (var db = new ApplicationDbContext())
                {
                    if (isYearChanged == false)
                    {
                        var enrolledSubjects = await db.SubjectsEnrolled
                            .Where(x => x.StudentID == Selected_students.StudentID && x.IsEnrolled)
                            .ToListAsync();

                        if (enrolledSubjects.Any())
                        {
                            foreach (var unenroll in enrolledSubjects)
                            {
                                unenroll.IsEnrolled = false;
                                db.SubjectsEnrolled.Update(unenroll);
                            }
                            await db.SaveChangesAsync();
                        }
                    }

                    var fetchSubs = await db.Subjects
                        .Where(x => x.ProgramID == Selected_students.ProgramID &&
                                    x.YearID == Selected_students.YearID &&
                                    x.SemesterID == "SEM101")
                        .ToListAsync();

                    if (fetchSubs.Any())
                    {
                        foreach (var sub in fetchSubs)
                        {
                            string enrollmentID = $"SUB-ENROLLED-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

                            var subjectEnrolled = new SubjectsEnrolled
                            {
                                EnrollmentID = enrollmentID,
                                SubjectID = sub.SubjectID,
                                StudentID = Selected_students.StudentID,
                                IsEnrolled = true
                            };

                            db.SubjectsEnrolled.Add(subjectEnrolled);
                        }

                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        MessageBox.Show("No subjects found for the selected program and year.");
                    }
                }

                await LoadStudentAsync();
                CloseCurrentActiveWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Oops, there is an error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        //Close the Current Window
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

            if (confirmation == MessageBoxResult.Yes)
            {
                if (Selected_students != null)
                {
                    try
                    {
                        // Check for related records in SubjectsEnrolled (or other dependent tables)
                        var relatedSubjects = await _context.SubjectsEnrolled
                            .FirstOrDefaultAsync(x => x.StudentID == Selected_students.StudentID);

                        if (relatedSubjects != null)
                        {
                            // If related records exist, inform the user and guide them to handle it first
                            MessageBox.Show("Cannot delete student as they are enrolled in one or more subjects. Please remove the student from their subjects first.", "Delete Failed", MessageBoxButton.OK, MessageBoxImage.Error);

                            // Reset the selected student to null after failure
                            Selected_students = null;

                            return;
                        }

                        // Proceed with deleting the student if no related records exist
                        _context.Students.Remove(Selected_students);
                        await _context.SaveChangesAsync();

                        StudentsCollection.Remove(Selected_students);

                        // Reset the selected student after successful deletion
                        Selected_students = null;

                        MessageBox.Show("Student deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (SqlException sqlEx)
                    {
                        // Handle SQL errors (foreign key constraint violation, etc.)
                        MessageBox.Show($"Error while deleting student: {sqlEx.Message}", "Delete Failed", MessageBoxButton.OK, MessageBoxImage.Error);

                        // Optionally reset the selection if an error occurs
                        Selected_students = null;
                    }
                    catch (Exception ex)
                    {
                        // General exception handling
                        MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                        // Optionally reset the selection if an error occurs
                        Selected_students = null;
                    }
                }
            }
        }


        //Load all the students


        private BrushConverter converter = new BrushConverter();
        private string[] myArray = { "#1098AD", "#1E88E5", "#FF8F00", "#FF5252", "#6741D9", "#0CA678" };
        private System.Windows.Media.Brush brush;
        private async Task LoadStudentAsync()
        {

           
            var student = await _context.Students
                 .Include(s => s.Program)
                 .Include(s => s.YearLevel)
                 .Include(s => s.Scholarship)
                 .ToListAsync();


            StudentsCollection.Clear();

            for (int i = 0; i < student.Count; i++)
            {
                var staff = student[i];
                string colorString = myArray[i % myArray.Length];
                brush = (System.Windows.Media.Brush)converter.ConvertFromString(colorString);

                // Get first letter of name as Character
                string name = staff.Name;
                staff.Character = name.Length > 0 ? name.Substring(0, 1) : string.Empty;

                // Set the background color
                staff.bgColor = brush;

                StudentsCollection.Add(staff);
                OnPropertyChanged(nameof(StudentsCollection));

            }
         



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

            Name = string.Empty;
            Age = 0;
            IDnumber = 0;
            Contact = 0;
            Gmail = string.Empty;
            Address = string.Empty;
           
        }








        /// <summary>
        /// Load the Subjects with grade
        /// </summary>
        /// <returns></returns>
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
                    var results = await context.Grades
                        .Where(g => g.StudentID == Selected_students.StudentID)
                        .Join(
                            context.Subjects,
                            grade => grade.SubjectID,
                            subject => subject.SubjectID,
                            (grade, subject) => new { grade, subject }
                        )
                        .GroupJoin(
                            context.Schedule,
                            gs => new { gs.subject.SubjectID, gs.subject.ProfessorID },
                            schedule => new { schedule.SubjectID, schedule.ProfessorID },
                            (gs, schedules) => new { gs.grade, gs.subject, schedules }
                        )
                        .SelectMany(
                            gss => gss.schedules.DefaultIfEmpty(),
                            (gss, schedule) => new
                            {
                                gss.grade.GradeID,
                                gss.grade.GradeValue,
                                gss.subject.SubjectID,
                                gss.subject.SubjectName,
                                gss.subject.CourseCode,
                                gss.subject.SemesterID,

                                Time = schedule != null ? schedule.Time : null,
                                gss.subject.ProfessorID,
                                ProfessorName = context.Professors
                                    .Where(p => p.ProfessorID == gss.subject.ProfessorID)
                                    .Select(p => p.Name)
                                    .FirstOrDefault(),
                                SemesterName = context.Semesters
                                .Where(s => s.SemesterID == gss.subject.SemesterID)
                                .Select(s => s.SemesterName)
                                .FirstOrDefault(),

                                EncoderName = context.Staffs
                                    .Where(s => s.StaffID == gss.grade.StaffID)
                                    .Select(s => s.Name)
                                    .FirstOrDefault(),
                                SchoolYear = context.SchoolYear
                                    .Where(sy => sy.SchoolYearID == gss.grade.SchoolYearID)
                                    .Select(sy => sy.SY)
                                    .FirstOrDefault()


                            }
                        )
                        .ToListAsync();

                    // Use the results, for example:
                    SubjectGrades.Clear();
                    foreach (var item in results)
                    {

                        SubjectGrades.Add(new Grade
                        {
                            GradeID = item.GradeID,
                            GradeValue = item.GradeValue,
                            SubjectID = item.SubjectID,
                            CourseCode = item.CourseCode,
                            ProfessorID = item.ProfessorID,
                            ProfessorName = item.ProfessorName,
                            Time = item.Time,
                            SemesterID = item.SemesterID,
                            SemesterName = item.SemesterName,


                        });

                        EncoderName = item.EncoderName;
                        SchoolYearName = item.SchoolYear;
                        Semester = item.SemesterName;
                    }
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading subjects with schedules: {ex.Message}");
            }
        }





        /// <summary>
        /// Load all the subs enrolled by the student for viewing
        /// </summary>
        /// <returns></returns>
        /// 


        private async Task LoadStudentSubAsync()
        {
           
                

                using (var context = new ApplicationDbContext())
                {

                    var subs = await context.SubjectsEnrolled
                        .Where(x => x.StudentID == Selected_students.StudentID && x.IsEnrolled == true)
                        .Include(x=>x.Subject)
                        .Include(x => x.Subject.Year)
                        .Include(x => x.Subject.Semester)
                        .ToListAsync();


                    ListOfSubjectsEnrolled.Clear();
                    foreach (var subject in subs)
                    {
                        ListOfSubjectsEnrolled.Add(subject);
                    }
                }
            
            
        }






        //Load all the subjects per program base on the student ProgramID
        private async Task LoadSubjectsPerProgram()
        {
            try
            {
                if (Selected_students == null)
                {
                    MessageBox.Show("No student selected.");
                    return;
                }

                using (var context = new ApplicationDbContext())
                {
                    var enrolledSubjectIds = await context.SubjectsEnrolled
                        .Where(e => e.StudentID == Selected_students.StudentID && e.IsEnrolled)
                        .Select(e => e.SubjectID)
                        .ToListAsync();

                    var subjectGradesList = await context.Subjects
                        .Where(x => x.ProgramID == Selected_students.ProgramID && !enrolledSubjectIds.Contains(x.SubjectID))
                        .Include(x => x.Year)
                        .Include(x => x.Semester)
                        .ToListAsync();

                    SubjectPerProgram.Clear();
                    foreach (var subject in subjectGradesList)
                    {
                        SubjectPerProgram.Add(subject);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading subjects: {ex.Message}");
            }
        }




        /// <summary>
        /// Load SchoolarShip 
        /// </summary>
        /// <returns></returns>


        private async Task LoadSchoolar()
        {
            using(var context= new ApplicationDbContext())
            {


                var schoolar = await context.Scholarship.ToListAsync();


                foreach(var item in schoolar)
                {


                    ScholarshipsCollection.Add(item);
                }

            }
        }




        /// <summary>
        /// Mark as enrolled sub
        /// </summary>
        /// <returns></returns>

        private async Task AddSubjectAsync()
        {
            try
            {
                int totalUnits = ListOfSubjectsEnrolled.Sum(x => x.Subject.Units); 

                using (var context = new ApplicationDbContext())
                {
                    List<SubjectsEnrolled> newEnrollments = new List<SubjectsEnrolled>();

                    // Iterate over selected subjects
                    foreach (var sub in SubjectPerProgram.Where(x => x.IsEnrolled==true))
                    {
                        // Check if subject is already enrolled
                        var existingSubject = await context.SubjectsEnrolled
                            .FirstOrDefaultAsync(x => x.SubjectID == sub.SubjectID && x.StudentID == Selected_students.StudentID);

                        if (existingSubject != null)
                        {
                            ShowNotification("Error", "Oops! One of the subjects you selected is already enrolled!", NotificationType.Error);
                            return; 
                        }

                        totalUnits += sub.Units;

                        if (totalUnits >= 27)
                        {
                            ShowNotification("Error", "The student's subjects already reached the unit quota (27 max)!", NotificationType.Error);
                            return;
                        }

                        string ID = $"SUB-ENROLLED-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

                        // Add new subject enrollment to list
                        var subjectEnrolled = new SubjectsEnrolled
                        {
                            EnrollmentID = ID,
                            SubjectID = sub.SubjectID,
                            StudentID = Selected_students.StudentID,
                            IsEnrolled = true
                        };

                        newEnrollments.Add(subjectEnrolled);
                    }

                    // Save all new enrollments to database
                    if (newEnrollments.Count > 0)
                    {
                        context.SubjectsEnrolled.AddRange(newEnrollments);
                        await context.SaveChangesAsync();
                        ShowNotification("Success", "Subjects enrolled successfully", NotificationType.Success);
                    }

                    // Refresh UI
                    _ = LoadSubjectsPerProgram();
                    _ = LoadStudentSubAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading subjects: {ex.Message}");
            }
        }


        /// <summary>
        /// Extract the student
        /// </summary>
        private void ExtractStudent()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select an Excel file.",
                Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filepath = openFileDialog.FileName;
                using (var package = new ExcelPackage(new FileInfo(filepath)))
                {
                    StudentBulkCollection.Clear();

                    foreach (var worksheet in package.Workbook.Worksheets)
                    {
                        int rowCount = worksheet.Dimension.Rows;





                        for (int row = 1; row <= rowCount; row++)
                        {
                            var firstCellText = worksheet.Cells[row, 1].Text?.Trim(); 
                            if (string.Equals(firstCellText, "Name", StringComparison.OrdinalIgnoreCase) ||
                                string.Equals(firstCellText, "name", StringComparison.OrdinalIgnoreCase))
                            {
                                continue; 
                            }

                            var name = worksheet.Cells[row, 1].Text?.Trim();
                            var idnumTtext = worksheet.Cells[row, 2].Text?.Trim();
                            var gmail = worksheet.Cells[row, 3].Text?.Trim();
                            var program = worksheet.Cells[row, 4].Text?.Trim();
                            var yearlevel = worksheet.Cells[row, 5].Text?.Trim();
                            var scholarship = worksheet.Cells[row, 6].Text?.Trim();

                            long? idnum = null; 
                            if (!string.IsNullOrEmpty(idnumTtext) && long.TryParse(idnumTtext, out long parsedId))
                            {
                                idnum = parsedId; 
                            }

                            if (!string.IsNullOrEmpty(name) ||
                                idnum.HasValue ||
                                !string.IsNullOrEmpty(gmail) ||
                                !string.IsNullOrEmpty(program) ||
                                !string.IsNullOrEmpty(yearlevel) ||
                                !string.IsNullOrEmpty(scholarship))
                            {
                                StudentBulkCollection.Add(new StudentsEntity
                                {
                                    Name = name,
                                    IDnumber = idnum ?? 0, 
                                    ProgramID = program,
                                    YearID = yearlevel,
                                    ScholarshipID = scholarship,
                                });
                            }
                        }



                    }



                }


            }

        }




        /// <summary>
        /// Automation when adding student       
        /// </summary>

        private void MultiInsertStudent()
        {
            foreach (var item in StudentBulkCollection)
            {
                try
                {
                    var student = _context.Students
                        .AsNoTracking() 
                        .FirstOrDefault(x => x.Name.ToLower() == item.Name.ToLower());

                    if (student != null)
                    {
                        ShowNotification("Error", $"Student '{item.Name}' already exists.", NotificationType.Error);
                        continue;
                    }

                    var programExist = _context.Programs
                        .FirstOrDefault(x => x.Acronym.ToLower() == item.ProgramID.ToLower());
                    if (programExist == null)
                    {
                        ShowNotification("Error", $"Program '{item.ProgramID}' does not exist.", NotificationType.Error);
                        continue;
                    }

                    var yearMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "4th Year", "Fourth Year" },
                        { "3rd Year", "Third Year" },
                        { "2nd Year", "Second Year" },
                        { "1st Year", "First Year" },
                    };

                    var normalizedYear = yearMapping.ContainsKey(item.YearID) ? yearMapping[item.YearID] : item.YearID;

                    var yearExist = _context.Year
                        .FirstOrDefault(x => x.Name.ToLower() == normalizedYear.ToLower());

                    if (yearExist == null)
                    {
                        ShowNotification("Error", $"Year level '{item.YearID}' does not exist.", NotificationType.Error);
                        continue;
                    }

                    // Normalize and trim both the database value and the extracted value
                    var scholarshipExist = _context.Scholarship
                        .FirstOrDefault(x => x.Name.ToUpper() == item.ScholarshipID.ToUpper());

                    if (scholarshipExist == null)
                    {
                        ShowNotification("Error", $"Scholarship '{item.ScholarshipID}' does not exist.", NotificationType.Error);
                        continue;
                    }




                    // Add new student

                    string ID = $"STU-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

                    var newStudent = new StudentsEntity
                    {
                        StudentID = ID,
                        Name = item.Name,
                        IDnumber = item.IDnumber,
                        Address = "N/A",
                        ProgramID = programExist.ProgramID,
                        YearID = yearExist.YearID,
                        ScholarshipID = scholarshipExist.ScholarshipID
                    };

                    _context.Students.Add(newStudent);

                    _context.SaveChanges();
                    StudentsCollection.Add(newStudent);








                    //Validate if the student is scholar
                    if(scholarshipExist.ScholarshipID == "SCHO-1001")
                    {

                        //Auto enrolled for scholar
                        using (var db = new ApplicationDbContext())
                        {


                            //Fetch all the subjects based on the Program and Year
                            var fetchSubs = db.Subjects
                                .Where(x => x.ProgramID == programExist.ProgramID && x.YearID == yearExist.YearID && x.SemesterID == "SEM101")
                                .ToList();

                            if (fetchSubs.Any())
                            {

                                //Iteration of subjects
                                foreach (var sub in fetchSubs)
                                {

                                    //EnrollmentID
                                    string enrollmentID = $"SUB-ENROLLED-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";


                                    //Peforms insert to enroll the subs
                                    var subjectEnrolled = new SubjectsEnrolled
                                    {
                                        EnrollmentID = enrollmentID,
                                        SubjectID = sub.SubjectID,
                                        StudentID = ID,
                                        IsEnrolled = true
                                    };


                                    //Add to the collection
                                    db.SubjectsEnrolled.Add(subjectEnrolled);
                                }


                                //Save the changes
                                db.SaveChanges();
                            }
                            else
                            {


                                ShowNotification("Error", $"Error: No subjects found", NotificationType.Error);
                            }
                        }


                    }

                  


                    //Push notifications
                    ShowNotification("Success", $"Student '{item.Name}' successfully added.", NotificationType.Success);
                }
                catch (Exception ex)
                {
                    ShowNotification("Error", $"Error: '{ex}'", NotificationType.Error);

                }
            }
            //This will clear the collection after successfull insert
            StudentBulkCollection.Clear();

            //Refresh the Students immediately
             _= LoadStudentAsync();
        }





        /// <summary>
        /// Enroll the second Sem of the Student
        /// </summary>
        private void EnrollSecondSem()
        {
            var confirmation = MessageBox.Show("Are you sure you want to enroll the second sem for this student?",
                                                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirmation == MessageBoxResult.Yes)
            {
                // Unenroll First Sem Subjects
                var studentSubFirstSem = _context.SubjectsEnrolled
                    .Where(x => x.StudentID == Selected_students.StudentID && x.Subject.SemesterID == "SEM101")
                    .ToList();

                foreach (var studentSub in studentSubFirstSem)
                {
                    studentSub.IsEnrolled = false;
                    _context.SubjectsEnrolled.Update(studentSub);
                }

                _context.SaveChanges();

                var existingSecondSemSubjects = _context.SubjectsEnrolled
                    .Where(x => x.StudentID == Selected_students.StudentID && x.Subject.SemesterID == "SEM102")
                    .Select(x => x.SubjectID)
                    .ToHashSet(); 

                var subjectsToEnroll = _context.Subjects
                    .Where(se => se.ProgramID == Selected_students.ProgramID &&
                                 se.YearID == Selected_students.YearID &&
                                 se.SemesterID == "SEM102" &&
                                 !existingSecondSemSubjects.Contains(se.SubjectID)) // Exclude already enrolled subjects
                    .ToList();

                List<SubjectsEnrolled> newEnrollments = new List<SubjectsEnrolled>();

                foreach (var subs in subjectsToEnroll)
                {
                    string ID = $"SUB-ENROLLED-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

                    var subjectEnrolled = new SubjectsEnrolled
                    {
                        EnrollmentID = ID,
                        SubjectID = subs.SubjectID,
                        StudentID = Selected_students.StudentID,
                        IsEnrolled = true
                    };

                    newEnrollments.Add(subjectEnrolled);
                }

                if (newEnrollments.Count > 0)
                {
                    _context.SubjectsEnrolled.AddRange(newEnrollments);
                    _context.SaveChangesAsync();
                    ShowNotification("Success", "Subjects enrolled successfully. First sem subjects have been unenrolled.", NotificationType.Success);
                    CloseCurrentActiveWindow();
                }
                else
                {
                    ShowNotification("Info", "No new subjects to enroll. The student is already enrolled in all available second-semester subjects.", NotificationType.Information);
                }
            }
        }






        /// <summary>
        /// Delete the subject enrolled by the student
        /// </summary>


        private async Task DeleteSubjectEnrolled()
        {
            try
            {

                using (var db = new ApplicationDbContext())
                {
                    db.SubjectsEnrolled.Remove(Selected_subjectEnrolled);
                    await db.SaveChangesAsync();
                }

                ListOfSubjectsEnrolled.Remove(Selected_subjectEnrolled);
                ShowNotification("Success", $"Subject unenrolled successfully", NotificationType.Success);
                _= LoadSubjectsPerProgram();
            }
            catch (Exception ex)
            {
                ShowNotification("Error", $"Error: {ex.Message}", NotificationType.Error);
            }
        }







       


        //Show notications
        private void ShowNotification(string title, string message, NotificationType notificationType)
        {

            var notificaficationManager = new NotificationManager();

            if (NotificationType.Success == notificationType)
            {
                notificaficationManager.Show(
                  new NotificationContent { Title = title, Message = message, Type = notificationType }, expirationTime: TimeSpan.FromSeconds(10));
                return;
            }
            else
            {
                notificaficationManager.Show(
                  new NotificationContent { Title = title, Message = message, Type = notificationType }, expirationTime: TimeSpan.FromSeconds(10));
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
