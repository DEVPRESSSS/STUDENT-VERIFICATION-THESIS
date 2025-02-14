
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel;
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
using System.Text.RegularExpressions;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms
{
    /// <summary>
    /// Interaction logic for AddSubjects.xaml
    /// </summary>
    public partial class AddSubjects : Window
    {

        private readonly ApplicationDbContext _context;
        public AddSubjects(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;
            this.DataContext = new SubjectsViewModel(_context);

        }

      

        private void Close_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Automation_Click(object sender, RoutedEventArgs e)
        {
            BulkInsertInSubjects bulkInsertInSubjects = new BulkInsertInSubjects(_context);
            bulkInsertInSubjects.ShowDialog();

            this.Close();

        }

        private void SubjectName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);

        }

        private void CourseCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Units_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Units_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumAllowed(e.Text);

        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("^[a-zA-Z]+$"); // Only letters are allowed
            return regex.IsMatch(text);
        }

        private static bool IsNumAllowed(string text)
        {
            Regex regex = new Regex("^[0-9]+$");
            return regex.IsMatch(text);
        }

        private void SubjectName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            SingleSpace(sender,e);
        }

        private void CourseCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            SingleSpace(sender, e);
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            SingleSpace(sender, e);
        }


        private void SingleSpace(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && e.Key == Key.Space)
            {
                if (textBox.Text.EndsWith(" "))
                {
                    e.Handled = true;
                }
            }

        }
    }
}
