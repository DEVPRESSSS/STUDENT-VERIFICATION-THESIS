
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.UsercontrolsView
{
    /// <summary>
    /// Interaction logic for Subjects.xaml
    /// </summary>
    public partial class Subjects : UserControl
    {
        private readonly ApplicationDbContext _context;

        public Subjects(ApplicationDbContext context)
        {
            InitializeComponent();
            _context =context;

            DataContext = new SubjectsViewModel(_context);
        }

        private void AddSubjects_Click(object sender, RoutedEventArgs e)
        {
            AddSubjects obj = new AddSubjects(_context)
            {
                DataContext= this.DataContext

            };
            obj.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateSubject obj = new UpdateSubject(_context)
            {
                DataContext = this.DataContext

            };
            obj.ShowDialog();
        }

        private void UsernameXZ_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox != null && e.Key == Key.Space)
            {
                if (textBox.Text.EndsWith(" "))
                {
                    e.Handled = true; 
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UpsertSchedule obj = new UpsertSchedule(_context)
            {

                DataContext= this.DataContext

            };
          
            
            obj.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ViewProfessorAssigned obj = new ViewProfessorAssigned(_context)
            {

                DataContext = DataContext

            };


            obj.ShowDialog();
        }
    }
}
