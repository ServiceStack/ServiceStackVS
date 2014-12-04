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
using ServiceStack;
using ServiceStack.Text;
using ServiceStackVS.NativeTypes;
using ServiceStackVS.NativeTypes.Types;
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
        public string ServerUrl { get; private set; }
        private string suggestedFileName;
        private readonly INativeTypesHandler typesHandler;

        public AddServiceStackReference(string fileName, INativeTypesHandler nativeTypesHandler)
        {
            suggestedFileName = fileName;
            InitializeComponent();
            FileNameTextBox.Text = suggestedFileName;
            this.KeyDown += ListenForShortcutKeys;
            typesHandler = nativeTypesHandler;
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
            this.OkButton.IsEnabled = false;
            bool success = false;
            string url = UrlTextBox.Text;
            string errorMessage = "";
            Task.Run(() =>
            {
                try
                {
                    string serverUrl = CreateUrl(url);
                    ServerUrl = serverUrl;
                    bool urlIsValid = ValidateUrl(serverUrl);
                    var webRequest = WebRequest.Create(serverUrl);
                    string result = webRequest.GetResponse().ReadToEnd();
                    CodeTemplate = result;
                    if (urlIsValid)
                    {
                        success = true;
                    }
                }
                catch (WebException webException)
                {
                    errorMessage = "Failed to generated client types, server responded with '" +
                                   webException.Message + "'.";
                }
                catch (Exception ex)
                {
                    errorMessage = "Failed to generate client types - " + ex.Message;
                }
            }).ContinueWith(task =>
            {
                if (success)
                {
                    AddReferenceSucceeded = true;
                    Close();
                }
                else
                {
                    this.OkButton.IsEnabled = true;
                    ReferenceProgressBar.Visibility = Visibility.Hidden;
                    ErrorMessageBox.Visibility = Visibility.Visible;
                    ErrorMessage.Text = errorMessage;
                    UrlTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void UrlTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(UrlTextBox.Text.Trim()))
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
            string serverUrl = url.WithTrailingSlash();
            //Ensure http/https present
            serverUrl =
                (serverUrl.ToLower().StartsWith("http://") || serverUrl.ToLower().StartsWith("https://"))
                    ? serverUrl
                    : "http://" + serverUrl;
            var uri = new Uri(serverUrl);
            string path = uri.PathAndQuery.Contains("?") ? uri.PathAndQuery.SplitOnFirst("?")[0] : uri.PathAndQuery;
            if (!path.EndsWith(typesHandler.RelativeTypesUrl + "/"))
            {
                serverUrl += typesHandler.RelativeTypesUrl + "/";
            }

            return serverUrl.ToParentPath();
        }

        private bool ValidateUrl(string url)
        {
            Uri validatedUri;
            bool isValidUri = Uri.TryCreate(url, UriKind.Absolute, out validatedUri) &&
                              (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps);
            if (isValidUri)
            {
                string metadataJsonUrl = validatedUri.ToString().ToLower().Replace("/" + typesHandler.RelativeTypesUrl.ToLowerInvariant(), "/types/metadata") + "?format=json";
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
}
