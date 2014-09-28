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
using ServiceStack.Text;
using ServiceStackVS.Types;
using Color = System.Windows.Media.Color;

namespace ServiceStackVS
{
    /// <summary>
    /// Interaction logic for AddServiceStackReferencexaml.xaml
    /// </summary>
    public partial class AddServiceStackReference : Window
    {
        public bool AddReferenceSucceeded { get; set; }
        public string CodeTemplate { get; set; }
        private string suggestedFileName;
        private string codeTemplateBase;
        private string codeProviderName;

        public enum ServiceStackCodeProvider
        {
            CSharp,
            FSharp
        }

        public AddServiceStackReference(string fileName)
        {
            suggestedFileName = fileName;
            InitializeComponent();
            FileNameTextBox.Text = suggestedFileName;
            this.KeyUp += ListenForShortcutKeys;
        }

        public void UseCSharpProvider(string t4TemplateBase)
        {
            codeTemplateBase = t4TemplateBase;
            codeProviderName = "csharp";
        }

        public void UseFSharpProvider()
        {
            codeProviderName = "fsharp";
        }

        private ServiceStackCodeProvider codeProvider
        {
            get
            {
                switch (codeProviderName)
                {
                    case "csharp":
                        return ServiceStackCodeProvider.CSharp;
                    case "fsharp":
                        return ServiceStackCodeProvider.FSharp;
                    default:
                        return ServiceStackCodeProvider.CSharp;
                }
            }
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
            UrlTextBox.BorderBrush = new SolidColorBrush(Colors.Transparent);
            ErrorMessage.Text = "";
            try
            {
                string serverUrl = CreateUrl(UrlTextBox.Text);
                bool urlIsValid = ValidateUrl(serverUrl);
                if (codeProvider == ServiceStackCodeProvider.CSharp)
                {
                    CodeTemplate = codeTemplateBase.Replace("$serviceurl$", serverUrl); 
                }
                if (codeProvider == ServiceStackCodeProvider.FSharp)
                {
                    CodeTemplate = new WebClient().DownloadString(serverUrl);
                }
                
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
                UrlTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            catch (Exception ex)
            {
                ErrorMessageBox.Visibility = Visibility.Visible;
                ErrorMessage.Text = "Failed to generate client types - " + ex.Message;
                UrlTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
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

        private string CreateUrl(string url)
        {
            string serverUrl = url;
            //Remove any trailing forward slash to url
            if (serverUrl.EndsWith("/"))
            {
                serverUrl = serverUrl.Substring(0, serverUrl.Length - 1);
            }
            //Accept full types/csharp as input
            serverUrl = serverUrl.EndsWith("/types/" + codeProviderName) ? serverUrl : serverUrl + "/types/" + codeProviderName;
            return serverUrl;
        }

        private bool ValidateUrl(string url)
        {
            Uri validatedUri;
            bool isValidUri = Uri.TryCreate(url, UriKind.Absolute, out validatedUri) &&
                              validatedUri.Scheme == Uri.UriSchemeHttp;
            if (isValidUri)
            {
                string metadataJsonUrl = validatedUri.ToString().Replace("/" + codeProviderName, "/metadata") + "?format=json";
                string metadataResponse = new WebClient().DownloadString(metadataJsonUrl);
                MetadataTypes metaDataDto;
                try
                {
                    metaDataDto = JsonSerializer.DeserializeFromString<MetadataTypes>(metadataResponse);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed deserializing metadata from server", ex);
                }
                if (metaDataDto.Operations.Count == 0)
                {
                    throw new Exception("Invalid or empty metadata from server");
                }
                return true;
            }
            return false;
        }
    }

    public class DTOGenerationValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return new ValidationResult(false,"");
        }
    }
}
