using Microsoft.Win32;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms
{
    /// <summary>
    /// Interaction logic for BulkInsertInSubjects.xaml
    /// </summary>
    public partial class BulkInsertInSubjects : Window
    {
        private readonly ApplicationDbContext _context;
        public BulkInsertInSubjects(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;
            DataContext = new AutomationViewModel(_context);
            
        }


        private void Close_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
    }
}
