using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.UsercontrolsView;
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
        public MainWindow()
        {
            InitializeComponent();
            MainContentArea.Content = new DashboardOverview();

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
            MainContentArea.Content = new Professors();

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
    }
}