using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
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
using System.Windows.Shapes;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms
{
    /// <summary>
    /// Interaction logic for ListOfStudentsToBeGraded.xaml
    /// </summary>
    public partial class ListOfStudentsToBeGraded : Window
    {

        private readonly ApplicationDbContext _context;
        public ListOfStudentsToBeGraded(ApplicationDbContext context)
        {
            InitializeComponent();
            _context= context;
           // DataContext = new GradeViewModel(_context);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(studentGradeDataGrid.Items.Count > 0) {


                    EmptyMessage.Visibility = Visibility.Collapsed;

            }
        }

        private void DragBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {


            if(e.ChangedButton == MouseButton.Left)
            {

                DragMove();
            }
        }
    }
}
