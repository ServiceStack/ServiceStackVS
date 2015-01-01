using System;
using Gtk;
using ServiceStackVS.NativeTypes;
using ServiceStackVS.NativeTypes.Types;
using System.Windows.Threading;
using System.Net;
using ServiceStack;
using ServiceStack.Text;

namespace ServiceStackXS
{
	public partial class AddReferenceDialog : Gtk.Dialog
	{
		public bool AddReferenceSucceeded { get; set; }
		public string CodeTemplate { get; set; }
		public string ServerUrl { get; private set; }
		private string suggestedFileName;
		private readonly INativeTypesHandler typesHandler;

		string errorMessage;

		public AddReferenceDialog (string fileName, INativeTypesHandler nativeTypesHandler)
		{
			this.Build ();
			suggestedFileName = fileName;
			typesHandler = nativeTypesHandler;
			nameEntry.Text = suggestedFileName;
			nameEntry.KeyReleaseEvent += (o, keyEventArgs) => { 
				if (keyEventArgs.Event.Key == Gdk.Key.ISO_Enter) {
					Dispatcher.CurrentDispatcher.InvokeAsync (() => CreateServiceReference());
				}
				if (keyEventArgs.Event.Key == Gdk.Key.Escape) {
					OnClose ();
				}
			};
		}

		protected void CancelAdd (object sender, EventArgs e)
		{
			this.OnClose ();
		}

		protected void AddReference (object sender, EventArgs e)
		{
			Dispatcher.CurrentDispatcher.InvokeAsync (() => CreateServiceReference ());
		}

		private bool GetCodeFromServer(string url)
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
					return true;
				}
				else
				{
					throw new Exception("Failed to contact server. Unable to validate provided URL end point.");
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
			return false;
		}


		private void CreateServiceReference()
		{
			//ErrorMessage.Text = "";
			this.buttonOk.Sensitive = false;
			bool success = false;
			string url = addressEntry.Text;
			string errorMessage = "";
			if (GetCodeFromServer (url)) {
				AddReferenceSucceeded = true;
				this.OnClose();
			} else {
				AddReferenceSucceeded = false;
				//TODO Add error message
			}
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

