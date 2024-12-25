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

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.WindowsFormView
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private readonly ApplicationDbContext _context;
       public LoginForm() : this(new ApplicationDbContext()) // Replace with a valid instance if applicable
{


}

   // Keep your parameterized constructor
    public LoginForm(ApplicationDbContext context)
    {
        InitializeComponent();
        _context = context;
    }


        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if(e.LeftButton== MouseButtonState.Pressed)
            {

                DragMove();
            }
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {

            MainWindow dash = new MainWindow(_context);
            dash.Show();

            this.Hide();
        }
    }
}
