using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using System.IO.Packaging;
using System.Windows.Markup;
using Notification.Wpf;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms
{
    /// <summary>
    /// Interaction logic for AddGrade.xaml
    /// </summary>
    public partial class AddGrade : Window
    {

        private readonly ApplicationDbContext _context;

        public AddGrade(ApplicationDbContext context)
        {
            InitializeComponent();
            _context= context;
            DataContext = new StudentViewModel(_context);
            

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Automation_Click(object sender, RoutedEventArgs e)
        {

        }

       

        private void pRINTEST_Click(object sender, RoutedEventArgs e)
        {
            var mainBorder = MainBorder;

            if (mainBorder == null)
            {
                MessageBox.Show("Could not find the main border control!");
                return;
            }

            // Create a fixed document for the preview
            FixedDocument document = new FixedDocument();

            // Save current layout settings
            Thickness originalMargin = mainBorder.Margin;
            mainBorder.UpdateLayout();

            // Create a page for the document
            PageContent pageContent = new PageContent();
            FixedPage fixedPage = new FixedPage();

            // Create a container for the entire UI visual
            Canvas container = new Canvas();
            container.Width = mainBorder.ActualWidth;
            container.Height = mainBorder.ActualHeight;

            // Use a visual brush to capture the entire UI
            VisualBrush vb = new VisualBrush(mainBorder);
            Rectangle rect = new Rectangle();
            rect.Width = mainBorder.ActualWidth;
            rect.Height = mainBorder.ActualHeight;
            rect.Fill = vb;
            container.Children.Add(rect);

            // Add the container to the page
            fixedPage.Children.Add(container);
            fixedPage.Width = mainBorder.ActualWidth;
            fixedPage.Height = mainBorder.ActualHeight;
            ((IAddChild)pageContent).AddChild(fixedPage);
            document.Pages.Add(pageContent);

            // Create a window with document viewer
            Window previewWindow = new Window();
            previewWindow.Title = "Print Preview";
            previewWindow.Width = 800;
            previewWindow.Height = 600;
            previewWindow.Owner = Window.GetWindow(this);
            DocumentViewer viewer = new DocumentViewer();
            viewer.Document = document;
            previewWindow.Content = viewer;
            previewWindow.ShowDialog();

            // Restore the original settings
            mainBorder.Margin = originalMargin;
            mainBorder.UpdateLayout();

        }




        private void ShowNotification(string title, string message, NotificationType notificationType)
        {

            var notificaficationManager = new NotificationManager();

            if (NotificationType.Success == notificationType)
            {
                notificaficationManager.Show(
                  new NotificationContent { Title = title, Message = message, Type = notificationType }, expirationTime: TimeSpan.FromSeconds(5));

            }
            else
            {
                notificaficationManager.Show(
                 new NotificationContent { Title = title, Message = message, Type = notificationType }, expirationTime: TimeSpan.FromSeconds(30));


            }





        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ShowNotification("Printing Information", "Press ESC if you want to close the form or press Enter to begin printing", NotificationType.Information);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if(e.Key == Key.Enter)
            {

                pRINTEST_Click(sender, e);
            }
            else if(e.Key == Key.Escape)
            {


                this.Close();
            }
        }
    }
}
