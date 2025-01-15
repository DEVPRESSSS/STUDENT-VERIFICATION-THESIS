using Microsoft.Win32;
using OfficeOpenXml;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Command;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    public class AutomationViewModel:INotifyPropertyChanged
    {

        private readonly ApplicationDbContext _context;
        public ObservableCollection<SubjectsEntity> SubjectsCollection { get; private set; }
        public AutomationViewModel(ApplicationDbContext context)
        {
            

            _context = context;
            SubjectsCollection = new ObservableCollection<SubjectsEntity>();
            OpenFileDialogCommand = new RelayCommand(async _ => await ChooseFileAsync(), _ => canExtract());

        }


        public ICommand? OpenFileDialogCommand { get;}

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





        private bool canExtract() => true;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
