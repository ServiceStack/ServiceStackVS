using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using EnvDTE;
using ServiceStack;
using ServiceStackVS.Common;
using ServiceStackVS.NativeTypes;
using ServiceStackVS.NativeTypes.Handlers;
using Package = Microsoft.VisualStudio.Shell.Package;
using Window = System.Windows.Window;

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
        private INativeTypesHandler typesHandler;

        protected bool IsTypedScriptReference { get; set; }
        protected bool TypeScriptOnlyDefinitions { get; set; }

        public INativeTypesHandler GetLastNativeTypesHandler()
        {
            return typesHandler;
        }

        public AddServiceStackReference(string fileName, INativeTypesHandler nativeTypesHandler)
        {
            InitializeComponent();
            FileNameTextBox.Text = fileName;
            KeyDown += ListenForShortcutKeys;
            typesHandler = nativeTypesHandler;
            IsTypedScriptReference = nativeTypesHandler is TypeScriptConcreteNativeTypesHandler ||
                                     nativeTypesHandler is TypeScriptNativeTypesHandler;

            TypeScriptCheckbox.Visibility = IsTypedScriptReference ? Visibility.Visible : Visibility.Hidden;
            TypeScriptCheckbox.IsChecked = false;
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
            OkButton.IsEnabled = false;
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
                    webRequest.Credentials = CredentialCache.DefaultCredentials;
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
                    SubmitAddRefStats();
                    Close();
                }
                else
                {
                    OkButton.IsEnabled = true;
                    ReferenceProgressBar.Visibility = Visibility.Hidden;
                    ErrorMessageBox.Visibility = Visibility.Visible;
                    ErrorMessage.Text = errorMessage;
                    UrlTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void SubmitAddRefStats()
        {
            DTE dte = Package.GetGlobalService(typeof(DTE)) as DTE;
            if (dte != null)
            {
                bool optOutOfStats = dte.GetOptOutStatsSetting();
                if (!optOutOfStats)
                {
                    var langName = typesHandler.RelativeTypesUrl.Substring(6);
                    Analytics.SubmitAnonymousAddReferenceUsage(langName);
                }
            }
            else
            {
                OutputWindowWriter.WriterWindow.WriteLine("Warning: Failed to resolve DTE");
            }
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

            string result = serverUrl.ToParentPath();
            if (!IsTypedScriptReference)
                return result;


            result = result.AddQueryParam("ExportAsTypes", TypeScriptOnlyDefinitions ? "False" : "True");
            // Update native types handler to handle different file names.
            typesHandler = TypeScriptOnlyDefinitions
                ? new TypeScriptNativeTypesHandler()
                : new TypeScriptConcreteNativeTypesHandler() as INativeTypesHandler;
            return result;
        }

        private void TypeScriptCheckbox_OnChecked(object sender, RoutedEventArgs e)
        {
            Handle(sender as CheckBox);
        }

        private void TypeScriptCheckbox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            Handle(sender as CheckBox);
        }

        void Handle(CheckBox checkBox)
        {
            TypeScriptOnlyDefinitions = checkBox.IsChecked ?? false;
        }

        private void FileNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            FileNameTextBox.SelectAll();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UrlTextBox.Focus();
        }
    }
}
