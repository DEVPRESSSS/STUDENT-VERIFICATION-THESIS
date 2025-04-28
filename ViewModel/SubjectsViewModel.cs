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
using System.Windows.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using OfficeOpenXml;
using System.IO;
using Notification.Wpf;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Windows.Media;
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
        public ObservableCollection<Grade> GradeSheetCollection { get; private set; }
        public ObservableCollection<SchoolYear> SchoolYearCollection { get; private set; }
        public ObservableCollection<SubjectProfessor> ProfessorAssignedCollection { get; private set; }


        //Crud Commands

        public ICommand AddSubjectCommand { get; }
        public ICommand UpdateSubjectCommand { get; }
        public ICommand DeleteSubjectCommand { get; }
        public ICommand LoadSubjectCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand? UpsertCommand { get; }
        public ICommand? ExtractGradeCommand { get; }
        public ICommand? ExtractGradeCommandDocs { get; }
        public ICommand? AddGrade { get; }
        public ICommand AssignedProfCommand { get; }
        public ICommand UnassignedProfCommand { get; }

        public SubjectsViewModel(ApplicationDbContext context)
        {

            _context = context;

            SubjectCollection = new ObservableCollection<SubjectsEntity>();
            AddSubjectCommand = new RelayCommand(async _ => await AddSubjectAsync(), _ => canAddDepartment());
            UpdateSubjectCommand = new RelayCommand(async _ => await UpdateSubjectAsync(), _ => Selected_subjects != null);
            DeleteSubjectCommand = new RelayCommand(async _ => await DeleteSubjectAsync(), _ => Selected_subjects != null);
            LoadSubjectCommand = new RelayCommand(async _ => await LoadSubjectsAsync());
            LoadSubjectCommand.Execute(null);


            ProgramsCollection = new ObservableCollection<ProgramEntity>(); 
            ProfessorCollection = new ObservableCollection<ProfessorsEntity>(); 
            YearCollection = new ObservableCollection<Year>();
            SchoolYearCollection = new ObservableCollection<SchoolYear>();
            GradeSheetCollection = new ObservableCollection<Grade>();
            ProfessorAssignedCollection = new ObservableCollection<SubjectProfessor>();
            ClearCommand = new RelayCommand(_ => Clear());
            SearchCommand = new RelayCommand(async _ => await SearchProgramAsync(), _ => !string.IsNullOrWhiteSpace(SearchTerm));
            UpsertCommand = new RelayCommand(async _ => await AddScheduleAsync());
            ExtractGradeCommand = new RelayCommand(_ =>  BullExtractGradePerSub());
            ExtractGradeCommandDocs =new RelayCommand(_ => ExtractGradeFromWord());
            AddGrade = new RelayCommand(async _ => await AddExtractedGrade());

            _ = LoadProfessorsAsync();
            _= LoadProgramAsync();
            _ = LoadYearAsync();
            _ = LoadSchoolYearAsync();

        }






        //Get the selected departments

        private SubjectsEntity _selected_subjects;


        public SubjectsEntity Selected_subjects
        {
            get => _selected_subjects;

            set
            {

                if (Selected_subjects != null)
                {

                    //_ = LoadProfessorAssigned();

                }

                _selected_subjects = value;
                OnPropertyChanged(nameof(Selected_subjects));
            }
        }


        private SubjectProfessor _selectedSubjectProfessor;


        public SubjectProfessor Selected_Subject_Professor
        {
            get => _selectedSubjectProfessor;

            set
            {

                _selectedSubjectProfessor = value;
                OnPropertyChanged(nameof(Selected_Subject_Professor));
            }
        }


        /// <summary>
        /// School Year
        /// </summary>
        /// 
        private string _syID;

        public string Selected_syID
        {

            get => _syID;


            set
            {

                _syID = value;

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


        //Get day
        private string _day;


        public string Day
        {

            get => _day;

            set
            {


                _day = value;

                OnPropertyChanged(nameof(Day));




            }
        }






        //Get time
        private string _time;


        public string Time
        {

            get => _time;

            set
            {


                _time = value;

                OnPropertyChanged(nameof(Time));




            }
        }



        //Get room
        private string _room;


        public string Room
        {

            get => _room;

            set
            {


                _room = value;

                OnPropertyChanged(nameof(Room));




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
                    _ = LoadSubjectsAsync();
                }
                else
                {

                    _ = SearchProgramAsync();
                }
            }
        }

      

        private async Task SearchProgramAsync()
        {
            try
            {

                using (var context = new ApplicationDbContext())
                {

                    var query = context.Subjects
                     .Include(p => p.Program)
                     .Include(p => p.Year)
                     .Include(p => p.Professors)
                     .AsQueryable();

                    query = query.Where(p =>
                        EF.Functions.Like(p.SubjectID, $"%{SearchTerm}%") ||
                        EF.Functions.Like(p.SubjectName, $"%{SearchTerm}%") ||
                        EF.Functions.Like(p.Description, $"%{SearchTerm}%") ||
                        EF.Functions.Like(p.Program.Acronym, $"%{SearchTerm}%") ||
                        EF.Functions.Like(p.Year.Name, $"%{SearchTerm}%") ||
                        EF.Functions.Like(p.Units.ToString(), $"%{SearchTerm}%") ||
                        EF.Functions.Like(p.CourseCode, $"%{SearchTerm}%")
                    );

                    var result = await query.ToListAsync();

                    // Update the ObservableCollection
                    SubjectCollection.Clear();
                    foreach (var professor in result)
                    {
                        SubjectCollection.Add(professor);
                    }


                }






            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during search: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

            if (string.IsNullOrWhiteSpace(SubjectName) || string.IsNullOrWhiteSpace(CourseCode) || string.IsNullOrWhiteSpace(Description))
            {
                MessageBox.Show("Please fill in all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            if (Units <= 0)
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



            if (string.IsNullOrWhiteSpace(Selected_subjects.SubjectName) || string.IsNullOrWhiteSpace(Selected_subjects.CourseCode) || string.IsNullOrWhiteSpace(Selected_subjects.Description))
            {
                MessageBox.Show("Please fill in all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            var existingSubject = await _context.Subjects.FindAsync(Selected_subjects.SubjectID);
            if (existingSubject == null)
            {
                MessageBox.Show("The selected subject does not exist in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {

               


                var existingProgram = await _context.Subjects
                        .FirstOrDefaultAsync(s => (s.SubjectName == Selected_subjects.SubjectName || s.CourseCode == Selected_subjects.CourseCode) &&
                                                  s.SubjectID != Selected_subjects.SubjectID);

               

                

                existingSubject.SubjectName = Selected_subjects.SubjectName;
                existingSubject.CourseCode = Selected_subjects.CourseCode;
                existingSubject.Description = Selected_subjects.Description;
                existingSubject.Units = Selected_subjects.Units;
                existingSubject.ProfessorID = Selected_subjects.ProfessorID;
                existingSubject.ProgramID = Selected_subjects.ProgramID;
                existingSubject.YearID = Selected_subjects.YearID;

                _context.Subjects.Update(existingSubject);
                await _context.SaveChangesAsync();

                MessageBox.Show("Subject updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                _= LoadSubjectsAsync();
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
                    var enrolledSubjects = _context.SubjectsEnrolled.Where(se => se.SubjectID == Selected_subjects.SubjectID);
                    _context.SubjectsEnrolled.RemoveRange(enrolledSubjects);

                    _context.Subjects.Remove(Selected_subjects);

                    await _context.SaveChangesAsync();

                    SubjectCollection.Remove(Selected_subjects);
                }
            }
        }


        private async Task LoadSubjectsAsync()
        {
            using(var context = new ApplicationDbContext())
            {

                var prof = await context.Subjects
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

        /// <summary>
        /// Load school year
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


        /// <summary>
        /// Clear the inputs
        /// </summary>
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






        /// <summary>
        /// Insert method of schedule
        /// </summary>
        /// <returns></returns>
        private async Task AddScheduleAsync()
        {
            if (!ValidateInputs())
            {
                return;
            }
            if (Selected_subjects.ProfessorID == null)
            {

                MessageBox.Show("Please add professor first", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;

            }

            string ID = $"GRD-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";
            var newSched = new ScheduleOfSubjects
            {
                ScheduleID = ID,
                SubjectID = Selected_subjects.SubjectID,
                Day = Day,
                Time = Time,
                Room = Room,
                ProfessorID = Selected_subjects.ProfessorID,
            };

            _context.Schedule.Add(newSched);
            await _context.SaveChangesAsync();
            MessageBox.Show("Schedule added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            CloseCurrentActiveWindow();


        }





       /// <summary>
       /// Extraction of data using Excel
       /// </summary>
        private void BullExtractGradePerSub()
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
                    GradeSheetCollection.Clear();

                    foreach (var worksheet in package.Workbook.Worksheets)
                    {
                        int rowCount = worksheet.Dimension.Rows;


                        
                       

                        for (int row = 1; row <= rowCount; row++)
                        {
                            var cellValue = worksheet.Cells[row, 1].Text?.Trim();

                            var name = worksheet.Cells[row, 1].Text?.Trim();
                            var year = worksheet.Cells[row, 2].Text?.Trim();
                            var gradeText = worksheet.Cells[row, 3].Text?.Trim();




                            if (!string.IsNullOrEmpty(gradeText))
                            {
                                // Add valid data to the collection
                                GradeSheetCollection.Add(new Grade
                                {
                                    StudentName = name,
                                    GradeValue = gradeText,
                                    //SchoolYearID = Selected_syID,

                                });
                            }
                        }

                    }



                }


            }

        }



        /// <summary>
        /// Method that pass the grades to Insert Grade
        /// </summary>
        /// <returns></returns>
        private async Task AddExtractedGrade()
        {
            MessageBoxResult dr = MessageBox.Show(
                "Are you sure you want to insert these grades? Please double-check the subject name and professor.",
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (dr == MessageBoxResult.Yes)
            {
                List<Grade> newGrades = new List<Grade>();

                foreach (var grade in GradeSheetCollection.ToList())
                {
                    var student = await CheckIfStudentExistsAndGetIDAsync(grade.StudentName);

                    if (student != null)
                    {
                        var enrollment = await _context.SubjectsEnrolled
                            .FirstOrDefaultAsync(e => e.StudentID == student.StudentID && e.SubjectID == Selected_subjects.SubjectID);

                        if (enrollment == null)
                        {
                            ShowNotification("Error", $"{grade.StudentName} is not enrolled in {Selected_subjects.SubjectName}. Grade not recorded.", NotificationType.Error);
                            continue;
                        }

                        var studentWithExistingGrade = await _context.Grades
                            .FirstOrDefaultAsync(g => g.EnrollmentID == enrollment.EnrollmentID);

                        if (studentWithExistingGrade != null)
                        {
                            ShowNotification("Error", $"{grade.StudentName} already has a grade recorded for {Selected_subjects.SubjectName}.", NotificationType.Error);
                            continue;
                        }
                        if (string.IsNullOrWhiteSpace(Selected_syID))
                        {
                            ShowNotification("Error", "Please select a school year ID", NotificationType.Error);

                            return;
                        }
                        string ID = $"GRD-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

                        var newGrade = new Grade
                        {
                            GradeID = ID,
                            StudentID = student.StudentID,
                            GradeValue = grade.GradeValue,
                            DateAssigned = DateTime.Now,
                            SubjectID = Selected_subjects.SubjectID,
                            EnrollmentID = enrollment.EnrollmentID,
                            //SchoolYearID= Selected_syID,
                            StaffID = UserSessionService.Instance.LoggedInStaffID
                        };

                        newGrades.Add(newGrade);
                    }
                    else
                    {
                        ShowNotification("Error", $"{grade.StudentName} does not exist in the database. Grade not recorded.", NotificationType.Error);
                    }
                }

                if (newGrades.Any())
                {
                    await InsertGradesAsync(newGrades);
                    ShowNotification("Success", "Grades have been successfully added!", NotificationType.Success);
                }
            }
        }



        /// <summary>
        /// Perform the insertion of grades
        /// </summary>
        /// <returns></returns>
        private async Task InsertGradesAsync(List<Grade> newGrades)
        {
            _context.Grades.AddRange(newGrades); 
            await _context.SaveChangesAsync();

            ShowNotification("Success", $"{newGrades.Count} grades were successfully inserted", NotificationType.Success);
            GradeSheetCollection.Clear();
        }


        /// <summary>
        /// Check if the student exist in the student table
        /// </summary>
        /// <returns></returns>
        private async Task<StudentsEntity?> CheckIfStudentExistsAndGetIDAsync(string studentName)
        {
            var student = await _context.Students
                                          .FirstOrDefaultAsync(s => s.Name == studentName); 
            return student; 
        }

     

        //Extraction of Grades using MS WORD
        private void ExtractGradeFromWord()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a Word file.",
                Filter = "Word Documents (*.docx;*.doc)|*.docx;*.doc"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filepath = openFileDialog.FileName;

                if (!File.Exists(filepath))
                {
                    MessageBox.Show("The specified file does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string fileExtension = Path.GetExtension(filepath).ToLower();
                if (fileExtension != ".docx" && fileExtension != ".doc")
                {
                    MessageBox.Show("Please select a valid Word document (.docx or .doc).", "Invalid File", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    using (WordprocessingDocument wDoc = WordprocessingDocument.Open(filepath, false))
                    {
                        var parts = wDoc.MainDocumentPart.Document.Descendants().FirstOrDefault();
                        if (parts != null)
                        {
                            GradeSheetCollection.Clear();

                            foreach (var node in parts.ChildElements)
                            {
                         

                                if (node is Table)
                                {
                                    ProcessTable((Table)node);
                                }
                            }
                        }
                    }
                }
                catch (FileFormatException ex)
                {
                    GradeSheetCollection.Clear();

                    MessageBox.Show($"The file is corrupted or not a valid Word document.\n\nError: {ex.Message}", "File Corrupted", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    GradeSheetCollection.Clear();

                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        /// <summary>
        /// Process the data from the table
        /// </summary>
        private void ProcessTable(Table node)
        {
            bool isHeader = true; 

            foreach (var row in node.Descendants<TableRow>())
            {
                if (isHeader)
                {
                    isHeader = false;
                    continue;
                }

                var cells = row.Descendants<TableCell>().ToList();

                if (cells.Count > 1)
                {
                    string studentName = GetCellText(cells[1]); 
                    string gradeText = GetCellText(cells[6]); 

                    decimal gradeValue = 0m; 

                    if (!decimal.TryParse(gradeText, out gradeValue))
                    {
                        continue;
                    }
                    GradeSheetCollection.Add(new Grade
                    {
                        StudentName = studentName,
                        GradeValue = Convert.ToString(gradeValue),
                        SchoolYearID = Selected_syID,

                    });
                }
            }
        }

        private string GetCellText(TableCell cell)
        {
            return string.Join(" ", cell.Descendants<Text>().Select(t => t.Text));
        }



        /// <summary>
        /// Validation for Schedule Insert
        /// </summary>
        /// <returns></returns>
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(Day) ||
                string.IsNullOrWhiteSpace(Time) ||
                string.IsNullOrWhiteSpace(Room) 
                )
            {
                MessageBox.Show("All fields are required and cannot contain only white spaces.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }







        //Show notications
        private void ShowNotification(string title, string message, NotificationType notificationType)
        {

            var notificaficationManager = new NotificationManager();

            if (NotificationType.Success == notificationType)
            {
                notificaficationManager.Show(
                  new NotificationContent { Title = title, Message = message, Type = notificationType }, expirationTime: TimeSpan.FromSeconds(5));

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
