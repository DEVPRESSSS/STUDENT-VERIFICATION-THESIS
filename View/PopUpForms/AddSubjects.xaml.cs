
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
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
    /// Interaction logic for AddSubjects.xaml
    /// </summary>
    public partial class AddSubjects : Window
    {

        private readonly ApplicationDbContext _context;
        public AddSubjects(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;
            this.DataContext = new SubjectsViewModel(_context);

        }

      

        private void Close_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Automation_Click(object sender, RoutedEventArgs e)
        {
            BulkInsertInSubjects bulkInsertInSubjects = new BulkInsertInSubjects(_context);
            bulkInsertInSubjects.ShowDialog();

            this.Close();

        }
    }
}
