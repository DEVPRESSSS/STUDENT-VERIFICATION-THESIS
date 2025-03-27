using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel;
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
using System.Windows.Shapes;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms
{
    /// <summary>
    /// Interaction logic for UpdateSubject.xaml
    /// </summary>
    public partial class UpdateSubject : Window
    {
        private readonly ApplicationDbContext _context;
        public UpdateSubject(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;

            DataContext= new SubjectsViewModel(_context);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(CourseCode.Text)&& !string.IsNullOrWhiteSpace(Name.Text) && !string.IsNullOrWhiteSpace(Description.Text)){

                this.Close();


            }
            else
            {

                MessageBox.Show("Please fill the fields correctly","Error",MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

      

        private void CourseCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            SingleSpace(sender, e);

        }

        private void CourseCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

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

        private void Name_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            SingleSpace(sender, e);

        }

        private void Description_PreviewKeyDown(object sender, KeyEventArgs e)
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
