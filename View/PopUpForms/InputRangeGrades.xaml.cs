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
    /// Interaction logic for InputRangeGrades.xaml
    /// </summary>
    public partial class InputRangeGrades : Window
    {
        private readonly ApplicationDbContext _context;

        public InputRangeGrades( ApplicationDbContext context)
        {
          
            InitializeComponent();
            _context = context;
            DataContext = new GradeViewModel(_context);


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ListOfStudentsToBeGraded listOfStudentsToBeGraded = new ListOfStudentsToBeGraded(_context)
            {

                DataContext = this.DataContext

            };
            listOfStudentsToBeGraded.ShowDialog();
        }
    }
}
