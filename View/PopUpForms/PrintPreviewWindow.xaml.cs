using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms
{
    /// <summary>
    /// Interaction logic for PrintPreviewWindow.xaml
    /// </summary>
    public partial class PrintPreviewWindow : Window
    {
        private FrameworkElement contentToPrint;

        public PrintPreviewWindow(FrameworkElement contentToPrint)
        {
            InitializeComponent();
            this.contentToPrint = contentToPrint;
            LoadPreview();
        }

        private void LoadPreview()
        {
            try
            {
                // Create a copy of the content to avoid modifying the original
                FrameworkElement printContent = XamlReader.Parse(XamlWriter.Save(contentToPrint)) as FrameworkElement;

                // Prepare the document
                FixedDocument fixedDoc = CreateFixedDocument(printContent);

                // Set up the document viewer
                DocumentViewer viewer = new DocumentViewer();
                viewer.Document = fixedDoc;

                // Add the viewer to our window
                ContentControl.Content = viewer;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating print preview: " + ex.Message, "Print Preview Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private FixedDocument CreateFixedDocument(FrameworkElement element)
        {
            // Measure and arrange the element
            element.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            element.Arrange(new Rect(0, 0, element.DesiredSize.Width, element.DesiredSize.Height));

            // Create the FixedDocument
            FixedDocument fixedDoc = new FixedDocument();

            // Create a FixedPage for the document
            FixedPage fixedPage = new FixedPage();
            fixedPage.Width = 8.5 * 96; // 8.5 inches (standard paper width)
            fixedPage.Height = 11 * 96; // 11 inches (standard paper height)

            // Add the element to the FixedPage
            UIElement elementVisual = element;
            fixedPage.Children.Add(elementVisual);

            // Create a PageContent to host the FixedPage
            PageContent pageContent = new PageContent();
            ((IAddChild)pageContent).AddChild(fixedPage);

            // Add the PageContent to the FixedDocument
            fixedDoc.Pages.Add(pageContent);

            return fixedDoc;
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                // Print the document
                printDialog.PrintDocument(((DocumentViewer)ContentControl.Content).Document.DocumentPaginator, "Document Print Job");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a SaveFileDialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XPS Documents (*.xps)|*.xps|PDF Documents (*.pdf)|*.pdf";
            saveFileDialog.DefaultExt = ".xps";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filename = saveFileDialog.FileName;
                string extension = System.IO.Path.GetExtension(filename).ToLower();

                try
                {
                    if (extension == ".xps")
                    {
                        // Save as XPS
                        using (XpsDocument xpsDoc = new XpsDocument(filename, FileAccess.ReadWrite))
                        {
                            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDoc);
                            writer.Write(((DocumentViewer)ContentControl.Content).Document.DocumentPaginator);
                        }
                        MessageBox.Show("Document saved successfully!", "Save Complete", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (extension == ".pdf")
                    {
                        // For PDF conversion, you'd need a PDF library
                        // Here you would call your PDF conversion code
                        MessageBox.Show("PDF saving requires additional libraries like PdfSharp or a commercial library.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving document: " + ex.Message, "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }

    // Extension methods to help with printing
    public static class PrintHelper
    {
        public static void ShowPrintPreview(this FrameworkElement element)
        {
            PrintPreviewWindow previewWindow = new PrintPreviewWindow(element);
            previewWindow.Owner = Application.Current.MainWindow;
            previewWindow.ShowDialog();
        }

        public static void PrintDirectly(this FrameworkElement element)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                // Get the element's size
                element.Measure(new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight));
                element.Arrange(new Rect(0, 0, element.DesiredSize.Width, element.DesiredSize.Height));

                // Print the element
                printDialog.PrintVisual(element, "Print Job");
            }
        }
    }
}
