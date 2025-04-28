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

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.Student
{
    /// <summary>
    /// Interaction logic for StudentProfile.xaml
    /// </summary>
    public partial class StudentProfile : Window
    {



        private ApplicationDbContext _context;
        public StudentProfile(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;

            DataContext = new StudentViewModel(_context);
            MainContentArea.Content = new ListOfSubjectsEnrolled(_context);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new ListOfSubjectsEnrolled(_context);
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new EnrollmentHistory(_context);

        }

        private void borderdrag_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void GradeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new AllGrades(_context);

        }
    }
}
