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
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    SubjectsCollection.Clear();

                    for (int row = 2; row <= rowCount; row++) 
                    {
                        var courseCode = worksheet.Cells[row, 1].Text?.Trim();
                        var descriptiveTitle = worksheet.Cells[row, 2].Text?.Trim();
                        var unitsText = worksheet.Cells[row, 3].Value?.ToString()?.Trim();

                        if (string.IsNullOrWhiteSpace(courseCode) || string.IsNullOrWhiteSpace(descriptiveTitle))
                        {
                            continue;
                        }

                        // Parse Units safely
                        int units = 0;
                        if (!string.IsNullOrWhiteSpace(unitsText) && int.TryParse(unitsText, out int parsedUnits))
                        {
                            units = parsedUnits;
                        }
                        var subject = new SubjectsEntity
                        {
                            CourseCode = courseCode,
                            SubjectName = descriptiveTitle,
                            Units = units
                        };

                        SubjectsCollection.Add(subject);
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
