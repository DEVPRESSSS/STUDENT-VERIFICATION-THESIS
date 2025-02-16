using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for Students.xaml
    /// </summary>
    public partial class Students : UserControl
    {
        private readonly ApplicationDbContext _context;
        public Students(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;
     
            DataContext = new StudentViewModel(context);
        }

     


        private void new_Click(object sender, RoutedEventArgs e)
        {
            AddStudent obj = new AddStudent(_context)
            {

               DataContext = this.DataContext

            };

            obj.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateStudent obj = new UpdateStudent(_context)
            {

                DataContext = this.DataContext

            };

            obj.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

           

            AddGrade obj = new AddGrade(_context)
            {

                DataContext = this.DataContext

            };


            obj.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {


           
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            AddSub addSub = new AddSub(_context)
            {

                DataContext= this.DataContext
            };

            addSub.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

            ListOfSubjects obj = new ListOfSubjects(_context)
            {

                DataContext = this.DataContext

            };


            obj.ShowDialog();
        }
    }
    
}
