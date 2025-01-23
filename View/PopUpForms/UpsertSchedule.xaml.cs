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
    /// Interaction logic for UpsertSchedule.xaml
    /// </summary>
    public partial class UpsertSchedule : Window
    {
        private readonly ApplicationDbContext _context;
        
        public UpsertSchedule(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;

            DataContext = new SubjectsViewModel(_context);
        }

        private void Day_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space)
            {
                e.Handled = true;

            }
        }

        private void Time_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;

            }
        }

        private void Room_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;

            }
        }
        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("^[a-zA-Z]+$"); // Only letters are allowed
            return regex.IsMatch(text);
        }

        private void Time_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Day_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
