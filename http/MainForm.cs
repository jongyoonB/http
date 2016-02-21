using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace http
{
    
    public partial class MainForm : Form
    {
        private System.Uri Url;
        public MainForm()
        {
            InitializeComponent();
        }

        public void set_Url(String req)
        {
            if (req != "")
            {
                try
                {
                    Url = new System.Uri(req);
                }
                catch
                {
                    Url = new System.Uri("http://" + req);
                }
            }
            else
            {
                MessageBox.Show("Enter Url");
            }
        }

        public void access_Url()
        {
            try
            {
                webBrowser.Navigate(Url);

            }
            catch
            {
                MessageBox.Show("Check Url");
            }
        }

        private void check_loaded(Uri loaded_Url)
        {
            if (loaded_Url.Equals(webBrowser.Url))
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webBrowser.Url);
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream receiveStream = response.GetResponseStream();
                        StreamReader readStream = null;

                        if (response.CharacterSet == null)
                        {
                            readStream = new StreamReader(receiveStream);
                        }
                        else
                        {
                            readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                        }

                        string data = readStream.ReadToEnd();
                        source.Text = data;
                        response.Close();
                        readStream.Close();
                    }
                    reqUrl.Text = webBrowser.Url.ToString();
                }
                catch
                {
                    MessageBox.Show("Check Url");
                    source.Text = "";
                }

            }
        }


        private void button_Click(object sender, EventArgs e)
        {
            set_Url(reqUrl.Text);
            access_Url();
        }


        private void reqUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                set_Url(reqUrl.Text);
                access_Url();
            }
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            check_loaded(e.Url);
        }


    }
}
