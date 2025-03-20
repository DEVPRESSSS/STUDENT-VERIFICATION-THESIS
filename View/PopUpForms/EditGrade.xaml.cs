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
    /// Interaction logic for EditGrade.xaml
    /// </summary>
    public partial class EditGrade : Window
    {
        private readonly ApplicationDbContext _context;

        public EditGrade(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;

            DataContext = new GradeViewModel(_context);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(GradeTextBox.Text))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a grade before closing.", "Grade Required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }
        //Validations
        private void GradeTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if(e.Key == Key.Space)
            {

                e.Handled = true;

            }

        }
    }
}
