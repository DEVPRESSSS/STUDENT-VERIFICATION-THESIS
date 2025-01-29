using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Command;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    public class MainViewModel:INotifyPropertyChanged
    {

       

        private readonly ApplicationDbContext _context;
        public MainViewModel(ApplicationDbContext context) { 
        
            _context= context;

        
        }

       
        private bool canAddDepartment() => true;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
