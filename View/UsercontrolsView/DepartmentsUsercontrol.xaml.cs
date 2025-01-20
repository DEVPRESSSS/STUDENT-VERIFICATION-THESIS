using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.UsercontrolsView
{
    /// <summary>
    /// Interaction logic for DepartmentsUsercontrol.xaml
    /// </summary>
    public partial class DepartmentsUsercontrol : UserControl
    {
        private readonly ApplicationDbContext _context;
        public DepartmentsUsercontrol(ApplicationDbContext context)
        {
            InitializeComponent();
            _context= context;
            DataContext = new DepartmentViewModel(_context);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddDepartment departments = new AddDepartment(_context)
            {

                DataContext = this.DataContext

            };

            departments.ShowDialog();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateDepartment departments = new UpdateDepartment(_context)
            {

                DataContext = this.DataContext

            };

            departments.ShowDialog();
        }

        private void UsernameXZ_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox != null && e.Key == Key.Space)
            {
                // Check if the last character is a space
                if (textBox.Text.EndsWith(" "))
                {
                    e.Handled = true; // Prevent consecutive spaces
                }
            }
        }
    }
}
