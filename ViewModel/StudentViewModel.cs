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

            //printGradeCommand = new RelayCommand(async _ => await GeneratePdfAsync());
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

           // BulkInsertCommand = new RelayCommand(_ => ExtractStudent());


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

                OnPropertyChanged();

            }


        }

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
                            EF.Functions.Like(p.Gmail, $"%{SearchTerm}%") ||
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
                    ScholarshipID= Selected_SchoolarID

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
            // Find the existing student in the database
            var existing_student = await _context.Students.FindAsync(Selected_students.StudentID);

            if (existing_student == null)
            {
                MessageBox.Show("Selected Student does not exist in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Validate if any field contains only whitespace
            if (string.IsNullOrWhiteSpace(Selected_students.Name) ||
                string.IsNullOrWhiteSpace(Selected_students.Gmail) ||
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

            var duplicateGmail = await _context.Students
                .AnyAsync(s => s.Gmail == Selected_students.Gmail && s.StudentID != Selected_students.StudentID);

            if (duplicateName)
            {
                MessageBox.Show("The Name is already associated with another student.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (duplicateGmail)
            {
                MessageBox.Show("The Gmail is already associated with another student.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Update student fields
                existing_student.Name = Selected_students.Name;
                existing_student.Age = Selected_students.Age;
                existing_student.IDnumber = Selected_students.IDnumber;
                existing_student.Contact = Selected_students.Contact;
                existing_student.Gmail = Selected_students.Gmail;
                existing_student.ProgramID = Selected_students.ProgramID;
                existing_student.YearID = Selected_students.YearID;
                existing_student.Address = Selected_students.Address;

                _context.Students.Update(existing_student);
                await _context.SaveChangesAsync();

                MessageBox.Show("Student updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                await LoadStudentAsync();
                CloseCurrentActiveWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Oops, there is an error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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



        private async Task LoadStudentAsync()
        {

           
            var prof = await _context.Students
                 .Include(s => s.Program)
                 .Include(s => s.YearLevel)
                 .Include(s => s.Scholarship)
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
                            gs => new { gs.subject.SubjectID, gs.subject.ProfessorID}, 
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
                            SemesterName= item.SemesterName,
                           
                        });
                    }
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading subjects with schedules: {ex.Message}");
            }
        }

        private async Task LoadStudentSubAsync()
        {
           
                

                using (var context = new ApplicationDbContext())
                {

                    var subs = await context.SubjectsEnrolled
                        .Where(x => x.StudentID == Selected_students.StudentID)
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
                

                using (var context = new ApplicationDbContext())
                {



                    foreach (var sub in SubjectPerProgram.Where(x => x.IsEnrolled == true))
                    {

                        var exisitingEnrolledSubjects = await context.SubjectsEnrolled.FirstOrDefaultAsync(x=> x.SubjectID == sub.SubjectID && x.StudentID == Selected_students.StudentID);

                        if(exisitingEnrolledSubjects!= null)
                        {


                            MessageBox.Show("Oops one of the subjects you selected is already enrolled!", "Error" , MessageBoxButton.OK, MessageBoxImage.Error);

                            SubjectPerProgram.Remove(sub);
                            
                            return;
                        }



                        string ID = $"SUB-ENROLLED-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

                        var subjectEnrolled = new SubjectsEnrolled
                        {
                            EnrollmentID= ID,
                            SubjectID = sub.SubjectID,
                            StudentID= Selected_students.StudentID,
                            IsEnrolled= true


                        };

                        context.SubjectsEnrolled.Add(subjectEnrolled);
                        


                    }

                    await context.SaveChangesAsync();
                    MessageBox.Show($"Subjects enrolled successfully");
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
                            var firstCellText = worksheet.Cells[row, 1].Text?.Trim(); // Check the first column for "Name" or "Has name"
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
                                    Gmail = gmail,
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
        /// 
        /// </summary>

        private void MultiInsertStudent()
        {
            foreach (var item in StudentBulkCollection)
            {
                try
                {
                    // Check if the student already exists (case-insensitive using .ToLower())
                    var student = _context.Students
                        .AsNoTracking() // Disable tracking for this query
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
                    string ID = $"STU-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

                    // Add new student
                    var newStudent = new StudentsEntity
                    {
                        StudentID = ID,
                        Name = item.Name,
                        IDnumber = item.IDnumber,
                        Gmail = item.Gmail,
                        Address = "N/A",
                        ProgramID = programExist.ProgramID,
                        YearID = yearExist.YearID,
                        ScholarshipID = scholarshipExist.ScholarshipID
                    };

                    _context.Students.Add(newStudent);

                    _context.SaveChanges();
                    StudentsCollection.Add(newStudent);
                    ShowNotification("Success", $"Student '{item.Name}' successfully added.", NotificationType.Success);
                }
                catch (Exception ex)
                {
                    ShowNotification("Error", $"Error: '{ex}'", NotificationType.Error);

                }
            }
        }



        //Show notications
        private void ShowNotification(string title, string message, NotificationType notificationType)
        {

            var notificaficationManager = new NotificationManager();

            if (NotificationType.Success == notificationType)
            {
                notificaficationManager.Show(
                  new NotificationContent { Title = title, Message = message, Type = notificationType }, expirationTime: TimeSpan.FromSeconds(20));

            }

            notificaficationManager.Show(
                  new NotificationContent { Title = title, Message = message, Type = notificationType }, expirationTime: TimeSpan.FromSeconds(5));




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
