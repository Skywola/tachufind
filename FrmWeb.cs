using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tachufind
{
    public partial class FrmWeb : Form
    {
        public FrmWeb()
        {
            InitializeComponent();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

		//https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.webbrowser?view=netcore-3.1
		/*The WebBrowser control is resource-intensive. Be sure to call the Dispose() method when 
		 * you are finished using the control to ensure that all resources are released in a timely 
		 * fashion. You must call the Dispose() method on the same thread that attached the events, 
		 * which should always be the message or user-interface (UI) thread.*/

		// Navigates to the given URL if it is valid.
		private void Navigate(String address)
		{
			if (String.IsNullOrEmpty(address)) return;
			if (address.Equals("about:blank")) return;
			if (!address.StartsWith("http://") &&
				!address.StartsWith("https://"))
			{
				address = "http://" + address;
			}
			try
			{
				webBrowser1.Navigate(new Uri(address));
			}
			catch (System.UriFormatException)
			{
				return;
			}
		}

	}
}
