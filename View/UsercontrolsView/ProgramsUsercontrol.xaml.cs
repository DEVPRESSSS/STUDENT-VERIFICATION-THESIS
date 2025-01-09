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
    /// Interaction logic for ProgramsUsercontrol.xaml
    /// </summary>
    public partial class ProgramsUsercontrol : UserControl
    {
        private readonly ApplicationDbContext _context;
        public ProgramsUsercontrol(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;
            DataContext = new ProgramsViewModel(_context);
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {

            UpdateProgram obj = new UpdateProgram(_context)
            {

                DataContext = DataContext
            };

            obj.ShowDialog();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddProgram obj = new AddProgram(_context)
            {

                DataContext = DataContext
            };

            obj.ShowDialog();
        }
    }
}
