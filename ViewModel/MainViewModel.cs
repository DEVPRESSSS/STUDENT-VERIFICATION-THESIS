using Microsoft.EntityFrameworkCore;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ApplicationDbContext _context;

        private int _studentsCount;
        private int _staffCount;
        private int _adminCount;
        private int _subjectsCount;
        private int _subjectsEnrolledCount;
        private int _departmentCount;
        private int _professorCount;
        private int _programCount;
        public int StudentsCount
        {
            get => _studentsCount;
            set
            {
                _studentsCount = value;
                OnPropertyChanged();
            }
        }

        public int ProgramsCount
        {
            get => _programCount;
            set
            {
                _programCount = value;
                OnPropertyChanged();
            }
        }
        public int StaffCount
        {
            get => _staffCount;
            set
            {
                _staffCount = value;
                OnPropertyChanged();
            }
        }

        public int ProfessorCount
        {
            get => _professorCount;
            set
            {
                _professorCount = value;
                OnPropertyChanged();
            }
        }

        public int AdminCount
        {
            get => _adminCount;
            set
            {
                _adminCount = value;
                OnPropertyChanged();
            }
        }

        public int SubjectsCount
        {
            get => _subjectsCount;
            set
            {
                _subjectsCount = value;
                OnPropertyChanged();
            }
        }

        public int SubjectsEnrolledCount
        {
            get => _subjectsEnrolledCount;
            set
            {
                _subjectsEnrolledCount = value;
                OnPropertyChanged();
            }
        }

        public int DepartmentCount
        {
            get => _departmentCount;
            set
            {
                _departmentCount = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(ApplicationDbContext context)
        {
            _context = context;
            LoadEntityCounts();
        }

        private async void LoadEntityCounts()
        {
            try
            {
                StudentsCount = await _context.Students.CountAsync();
                StaffCount = await _context.Staffs.CountAsync();
                AdminCount = await _context.Admin.CountAsync();
                SubjectsCount = await _context.Subjects.CountAsync();
                SubjectsEnrolledCount = await _context.SubjectsEnrolled.CountAsync();
                DepartmentCount = await _context.Departments.CountAsync();
                ProfessorCount = await _context.Professors.CountAsync();
                ProgramsCount = await _context.Professors.CountAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions here (optional)
                MessageBox.Show($"Error loading counts: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
