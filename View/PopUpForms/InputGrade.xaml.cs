using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms
{
    /// <summary>
    /// Interaction logic for InputGrade.xaml
    /// </summary>
    public partial class InputGrade : Window
    {

        private readonly ApplicationDbContext _context;
        public InputGrade(ApplicationDbContext context)
        {
            InitializeComponent();

            _context = context;

            DataContext = new GradeViewModel(_context);

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {

                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

      
        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {

                DragMove();
            }
        }
    }
}
