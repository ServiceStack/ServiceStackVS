using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
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

namespace VSServiceStack
{
    /// <summary>
    /// Interaction logic for AddServiceStackReferencexaml.xaml
    /// </summary>
    public partial class AddServiceStackReference : Window
    {
        public bool AddReferenceSucceeded { get; set; }
        private string suggestedFileName;

        private Func<string,bool> DownloadDtoFunc;
        public AddServiceStackReference(Func<string, bool> downloadDtoFunc,string fileName)
        {
            suggestedFileName = fileName;
            DownloadDtoFunc = downloadDtoFunc;
            InitializeComponent();
            FileNameTextBox.Text = suggestedFileName;
            this.KeyUp += ListenForShortcutKeys;
            System.Drawing.Bitmap bmp = SystemIcons.Information.ToBitmap();
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();
            InformationIcon.Source = bi;
        }

        private void ListenForShortcutKeys(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key == Key.Enter)
            {
                Dispatcher.InvokeAsync(CreateServiceReference);
            }
            if (keyEventArgs.Key == Key.Escape)
            {
                Close();
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Dispatcher.InvokeAsync(CreateServiceReference);
        }

        private void CreateServiceReference()
        {
            ReferenceProgressBar.Visibility = Visibility.Visible;
            ErrorMessageBox.Visibility = Visibility.Hidden;
            ErrorMessage.Text = "";
            try
            {
                bool urlIsValid = DownloadDtoFunc(UrlTextBox.Text);
                if (urlIsValid)
                {
                    AddReferenceSucceeded = true;
                    Close();
                }
            }
            catch (WebException webException)
            {
                ErrorMessageBox.Visibility = Visibility.Visible;
                ErrorMessage.Text = "Failed to generated client types, server responded with '" +
                                    webException.Message + "'.";
            }
            catch (Exception ex)
            {
                ErrorMessageBox.Visibility = Visibility.Visible;
                ErrorMessage.Text = "Failed to generate client types - " + ex.Message;
            }
            ReferenceProgressBar.Visibility = Visibility.Hidden;
        }

        private void UrlTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (UrlTextBox.Text.StartsWith("http://", true, CultureInfo.InvariantCulture) ||
                UrlTextBox.Text.StartsWith("https://", true, CultureInfo.InvariantCulture))
            {
                OkButton.IsEnabled = true;
            }
            else
            {
                OkButton.IsEnabled = false;
            }
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
