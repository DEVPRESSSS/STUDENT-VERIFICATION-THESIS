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
    /// Interaction logic for GradesUsercontrol.xaml
    /// </summary>
    public partial class GradesUsercontrol : UserControl
    {
        private readonly ApplicationDbContext _context;
        public GradesUsercontrol(ApplicationDbContext context)
        {
            InitializeComponent();
            _context= context;
            DataContext = new GradeViewModel(_context);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InputGrade grade = new InputGrade(_context)
            {

                DataContext = this.DataContext
            };

           grade.ShowDialog();
        }
    }
}
