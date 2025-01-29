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
    /// Interaction logic for AddStaffForm.xaml
    /// </summary>
    public partial class AddStaffForm : Window
    {

        private readonly ApplicationDbContext _context;
        public AddStaffForm(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;

            DataContext = new StaffViewModel(_context);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Password_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space)
            {

                e.Handled = true;
            }
        }

        private void Name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^[a-zA-Z.,\s]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void Name_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && e.Key == Key.Space)
            {
                // Check if last character is already a space
                if (textBox.Text.EndsWith(" "))
                {
                    e.Handled = true; // Block the space
                }
            }
        }
    }
}
