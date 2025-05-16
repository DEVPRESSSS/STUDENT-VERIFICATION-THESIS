using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.Student
{
    /// <summary>
    /// Interaction logic for AddStudentUC.xaml
    /// </summary>
    public partial class AddStudentUC : UserControl
    {

        private ApplicationDbContext _context;

        public AddStudentUC(ApplicationDbContext context)
        {
            InitializeComponent();

            _context = context;
        }

        private void FirstName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Address_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && e.Key == Key.Space)
            {
                if (textBox.Text.EndsWith(" "))
                {
                    e.Handled = true;
                }
            }
        }
        private void Gmail_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void IDnumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Age_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Name_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && e.Key == Key.Space)
            {
                if (textBox.Text.EndsWith(" "))
                {
                    e.Handled = true; 
                }
            }
        }

        private void Name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^[a-zA-Z.,\s]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }

       

        private static bool IsNumAllowed(string text)
        {
            Regex regex = new Regex("^[0-9]+$");
            return regex.IsMatch(text);
        }

  
    

        private void Contact_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumAllowed(e.Text);

        }

        private void IDnumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumAllowed(e.Text);

        }
    }
}
