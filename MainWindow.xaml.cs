using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.UsercontrolsView;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.WindowsFormView;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApplicationDbContext _context;
        public MainWindow(ApplicationDbContext context)
        {
            InitializeComponent();
            MainContentArea.Content = new DashboardOverview();
            _context= context;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton== MouseButtonState.Pressed)
            {
                DragMove();
            }
        }


        private void DashboardOverview_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new DashboardOverview();
        }

        private void ProfessorBtn_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new Professors(_context);

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {


            //Confirmation if the user want to exit

            MessageBoxResult dr = MessageBox.Show("Are you sure want to exit??", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(dr == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown(0);
            }
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            //Normal and maximize logic

            if(WindowState == WindowState.Normal)
            {

              WindowState=   WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {


            WindowState= WindowState.Minimized;
        }

     

        private void Subjects_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new Subjects(_context);

        }

     

        private void StudentsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new Students(_context);

        }

        private void DepartmentBtn_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new DepartmentsUsercontrol(_context);

        }


        private void ProgamBtn_Click_1(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new ProgramsUsercontrol(_context);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult dr = MessageBox.Show("Are you sure want to log out?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (dr == MessageBoxResult.Yes)
            {

                LoginForm test= new LoginForm();

                test.Show();

                this.Hide();
            }
        }

        private void Grades_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}