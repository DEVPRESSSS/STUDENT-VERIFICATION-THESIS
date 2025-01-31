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

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.EncoderDashboardView
{
    /// <summary>
    /// Interaction logic for StudentsEncoderUC.xaml
    /// </summary>
    public partial class StudentsEncoderUC : UserControl
    {

        private readonly ApplicationDbContext _context;
        public StudentsEncoderUC(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;

            DataContext = new StudentViewModel(_context);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddGrade obj = new AddGrade(_context)
            {

                DataContext = this.DataContext

            };


            obj.ShowDialog();
        }
    }
}
