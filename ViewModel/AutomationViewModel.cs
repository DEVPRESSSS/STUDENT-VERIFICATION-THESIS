using Microsoft.Win32;
using OfficeOpenXml;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Command;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    public class AutomationViewModel:INotifyPropertyChanged
    {

        private readonly ApplicationDbContext _context;
        public ObservableCollection<SubjectsEntity> SubjectsCollection { get; private set; }


        public ICommand? OpenFileDialogCommand { get; }
        public ICommand? InsertCommand { get; }

        public AutomationViewModel(ApplicationDbContext context)
        {
            

            _context = context;
            SubjectsCollection = new ObservableCollection<SubjectsEntity>();
            OpenFileDialogCommand = new RelayCommand(async _ => await ChooseFileAsync(), _ => canExtract());
            InsertCommand = new RelayCommand(async _ => await BulkInsert());

        }

       
    




        //Selected Subjects

        private SubjectsEntity _selectedSubjects;

        public SubjectsEntity SelectedSubjects
        {
            get=> _selectedSubjects;

            set
            {

                _selectedSubjects = value;
                OnPropertyChanged(nameof(SelectedSubjects));
            }

        }

        //Subject Name

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



        //Excel extraction Method

          private async Task ChooseFileAsync()
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
                    SubjectsCollection.Clear();

                    foreach (var worksheet in package.Workbook.Worksheets)
                    {
                        int rowCount = worksheet.Dimension.Rows;

                        string currentYear = "";
                        string currentSemester = "";
                        string program = "";

                        // First Loop: Process Columns (1, 2, 4)
                        for (int row = 1; row <= rowCount; row++)
                        {
                            var cellValue = worksheet.Cells[row, 1].Text?.Trim();

                            // Identify Program
                            if (!string.IsNullOrWhiteSpace(cellValue) &&
                                (cellValue.Contains("BACHELOR OF SCIENCE", StringComparison.OrdinalIgnoreCase) ||
                                 cellValue.Contains("EDUCATION", StringComparison.OrdinalIgnoreCase)))
                            {
                                program = cellValue.Trim();
                                continue;
                            }

                            // Identify Year Level
                            if (!string.IsNullOrWhiteSpace(cellValue) &&
                                (cellValue.Contains("First Year", StringComparison.OrdinalIgnoreCase) ||
                                 cellValue.Contains("Second Year", StringComparison.OrdinalIgnoreCase) ||
                                 cellValue.Contains("Third Year", StringComparison.OrdinalIgnoreCase) ||
                                 cellValue.Contains("Fourth Year", StringComparison.OrdinalIgnoreCase)))
                            {
                                currentYear = cellValue.Trim();
                                continue;
                            }

                            // Identify Semester
                            if (!string.IsNullOrWhiteSpace(cellValue) &&
                                (cellValue.Contains("First Semester", StringComparison.OrdinalIgnoreCase))                                )
                            {
                                currentSemester = cellValue.Trim();
                                continue;
                            }

                            // Process Data for Columns (1, 2, 4)
                            if (!string.IsNullOrWhiteSpace(currentYear) && !string.IsNullOrWhiteSpace(currentSemester))
                            {
                                var courseCode1 = worksheet.Cells[row, 1].Text?.Trim();
                                var descriptiveTitle1 = worksheet.Cells[row, 2].Text?.Trim();
                                var unitsText1 = worksheet.Cells[row, 4].Text?.Trim();

                                if (!string.IsNullOrWhiteSpace(courseCode1) &&
                                    !string.IsNullOrWhiteSpace(descriptiveTitle1) &&
                                    !courseCode1.Contains("Course Code") &&
                                    !descriptiveTitle1.Contains("Descriptive Title"))
                                {
                                    int.TryParse(unitsText1, out int units1);
                                    SubjectsCollection.Add(new SubjectsEntity
                                    {
                                        CourseCode = courseCode1,
                                        SubjectName = descriptiveTitle1,
                                        Units = units1,
                                        ProgramID = program,
                                        YearID = currentYear,
                                        SemesterID = currentSemester
                                    });
                                }
                            }
                        }

                        // Second Loop: Process Columns (5, 6, 7)
                        for (int row = 1; row <= rowCount; row++)
                        {
                            var cellValue = worksheet.Cells[row, 1].Text?.Trim();

                            // Identify Program
                            if (!string.IsNullOrWhiteSpace(cellValue) &&
                                (cellValue.Contains("BACHELOR OF SCIENCE", StringComparison.OrdinalIgnoreCase) ||
                                 cellValue.Contains("EDUCATION", StringComparison.OrdinalIgnoreCase)))
                            {
                                program = cellValue.Trim();
                                continue;
                            }

                            // Identify Year Level
                            if (!string.IsNullOrWhiteSpace(cellValue) &&
                                (cellValue.Contains("First Year", StringComparison.OrdinalIgnoreCase) ||
                                 cellValue.Contains("Second Year", StringComparison.OrdinalIgnoreCase) ||
                                 cellValue.Contains("Third Year", StringComparison.OrdinalIgnoreCase) ||
                                 cellValue.Contains("Fourth Year", StringComparison.OrdinalIgnoreCase)))
                            {
                                currentYear = cellValue.Trim();
                                continue;
                            }

                     
                            // Process Data for Columns (5, 6, 7)
                            if (!string.IsNullOrWhiteSpace(currentYear) && !string.IsNullOrWhiteSpace(currentSemester))
                            {
                                var courseCode2 = worksheet.Cells[row, 5].Text?.Trim();
                                var descriptiveTitle2 = worksheet.Cells[row, 6].Text?.Trim();
                                var unitsText2 = worksheet.Cells[row, 7].Text?.Trim();

                                if (!string.IsNullOrWhiteSpace(courseCode2) &&
                                    !string.IsNullOrWhiteSpace(descriptiveTitle2) &&
                                    !courseCode2.Contains("Course Code") &&
                                    !descriptiveTitle2.Contains("Descriptive Title"))
                                {
                                    int.TryParse(unitsText2, out int units2);
                                    SubjectsCollection.Add(new SubjectsEntity
                                    {
                                        CourseCode = courseCode2,
                                        SubjectName = descriptiveTitle2,
                                        Units = units2,
                                        ProgramID = program,
                                        YearID = currentYear,
                                        SemesterID = "Second Semester"
                                    });
                                }
                            }
                        }
                    }



                }


                MessageBox.Show("Data extraction completed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
             }

           

        }



        //Insert Method for the data Extracted



        private async Task BulkInsert()
        {
                
                foreach (var item in SubjectsCollection)
                {


                try
                {
                    string ID = $"SUB-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";
                    
                    var existingProgram = _context.Programs.FirstOrDefault(x => x.Name == item.ProgramID);

                    var existingYear = _context.Year.FirstOrDefault(x => x.Name == item.YearID);

                    var sem = _context.Semesters.FirstOrDefault(x => x.SemesterName == item.SemesterID);


                    if (existingYear == null)
                    {


                        var newYear = new Year { Name = item.YearID };
                        _context.Year.Add(newYear);
                        _context.SaveChanges();
                        existingYear = newYear; // Reassign to the newly added year
                    }

                  

                    if (existingProgram != null)
                    {
                        var programID = _context.Programs
                            .Where(p => p.Name == item.ProgramID)
                            .Select(p => p.ProgramID)
                            .FirstOrDefault();

                        var semID= _context.Semesters
                            .Where(p => p.SemesterName == item.SemesterID)
                            .Select(p => p.SemesterID)
                            .FirstOrDefault();


                        using (var dbContext = new ApplicationDbContext())
                        {
                            if (existingProgram != null && existingYear != null)
                            {
                                var subjectsEntity = new SubjectsEntity
                                {
                                    SubjectID = ID,
                                    SubjectName = item.SubjectName,
                                    CourseCode = item.CourseCode,
                                    YearID = existingYear.YearID,
                                    SemesterID= semID,
                                    ProgramID = programID,
                                    Description = "No description",
                                    Units = item.Units
                                };

                                dbContext.Subjects.Add(subjectsEntity);

                                await dbContext.SaveChangesAsync();

                            }
                            else
                            {
                                MessageBox.Show($"Program or Year not found for Subject: {item.CourseCode}, Program: {item.ProgramID}, Year: {item.YearID}");
                            }

                        }

                    }


               

                   

                
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            
        }
                 MessageBox.Show("Data extraction completed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                 SubjectsCollection.Clear();










        }




   
        private bool canExtract() => true;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
