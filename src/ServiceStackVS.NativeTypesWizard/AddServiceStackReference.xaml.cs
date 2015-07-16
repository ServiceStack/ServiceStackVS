using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ServiceStack;
using ServiceStack.Text;
using ServiceStackVS.NativeTypes;
using ServiceStackVS.NativeTypes.Types;

namespace ServiceStackVS.NativeTypesWizard
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
                bool setSslValidationCallback = false;
                try
                {
                    string serverUrl = CreateUrl(url);
                    ServerUrl = serverUrl;
                    
                    //Don't set validation callback if one has already been set for VS.
                    if (ServicePointManager.ServerCertificateValidationCallback == null)
                    {
                        //Temp set validation callback to return true to avoid common dev server ssl certificate validation issues.
                        setSslValidationCallback = true;
                        ServicePointManager.ServerCertificateValidationCallback =
                            (sender, certificate, chain, errors) => true;
                    }

                    var webRequest = WebRequest.Create(serverUrl);
                    string result = webRequest.GetResponse().ReadToEnd();

                    if (typesHandler.IsValidResponse(result))
                    {
                        success = true;
                    }
                    else
                    {
                        throw new Exception("Unrecognized response from server. Please check if have provided the correct base URL for a ServiceStack API.");
                    }

                    CodeTemplate = result;
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
                finally
                {
                    if (setSslValidationCallback)
                    {
                        //If callback was set to return true, reset back to null.
                        ServicePointManager.ServerCertificateValidationCallback = null;
                    }
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
    }
}
