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
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                // Ensure the window is fully loaded and rendered
                this.UpdateLayout();

                // Hide the scrollbars of the DataGrid temporarily
                var dataGrid = cashierDataGrid;
                if (dataGrid != null)
                {
                    // Save the current scrollbar visibility settings
                    var horizontalScrollbarVisibility = dataGrid.HorizontalScrollBarVisibility;
                    var verticalScrollbarVisibility = dataGrid.VerticalScrollBarVisibility;

                    // Temporarily hide the scrollbars
                    dataGrid.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    dataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;

                    print.Visibility = Visibility.Hidden;
                    Close.Visibility = Visibility.Hidden;
                    MainBorder.BorderThickness = new Thickness(0);
                    // Get the actual size of the window's layout
                    var bounds = VisualTreeHelper.GetDescendantBounds(this);

                    // Create a visual representation of the entire window
                    var visual = new DrawingVisual();
                    using (var context = visual.RenderOpen())
                    {
                        // Create a rectangle of the same size as the window's layout and render it
                        context.DrawRectangle(new VisualBrush(this), null, new Rect(new Point(), bounds.Size));
                    }

                    // Set the print document size to match the window layout's size
                    printDialog.PrintTicket.PageMediaSize = new System.Printing.PageMediaSize(bounds.Width, bounds.Height);

                    // Print the visual representation of the entire window
                    printDialog.PrintVisual(visual, "Printing Window");

                    // Restore the original scrollbar visibility settings
                    dataGrid.HorizontalScrollBarVisibility = horizontalScrollbarVisibility;
                    dataGrid.VerticalScrollBarVisibility = verticalScrollbarVisibility;
                    this.Close();
                }
            }
        }

        private async Task GeneratePdfAsync(FrameworkElement wpf_Element)
        {
            //------------< WPF_Print_current_Window >------------
            string ID = $"PRV-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

            //--< create xps document >--

            XpsDocument doc = new XpsDocument($"print_previw{ID}.xps", FileAccess.ReadWrite);

            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);

            SerializerWriterCollator preview_Document = writer.CreateVisualsCollator();

            preview_Document.BeginBatchWrite();

            preview_Document.Write(wpf_Element);  //*this or wpf xaml control

            preview_Document.EndBatchWrite();

            //--</ create xps document >--



            //var doc2 = new XpsDocument("Druckausgabe.xps", FileAccess.Read);



            FixedDocumentSequence preview = doc.GetFixedDocumentSequence();



            var window = new Window();

            window.Content = new DocumentViewer { Document = preview };

            window.ShowDialog();



            doc.Close();

            //------------</ WPF_Print_current_Window >------------




        }



    }
}
