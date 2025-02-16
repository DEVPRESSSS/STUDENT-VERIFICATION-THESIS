using MaterialDesignThemes.Wpf;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
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
    /// Interaction logic for Staff.xaml
    /// </summary>
    public partial class Staff : UserControl
    {

        private readonly ApplicationDbContext _context;
        public Staff(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;

            DataContext= new StaffViewModel(_context);
        }

      

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddStaffForm obj = new AddStaffForm(_context)
            {

                DataContext= this.DataContext

            };
            obj.ShowDialog();
        }

        private void UsernameXZ_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateStaffForm obj = new UpdateStaffForm(_context)
            {

                DataContext = this.DataContext

            };
            obj.ShowDialog();
        }
    }
}
