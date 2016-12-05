using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using System.Windows.Forms;

namespace UFN_Licensing_System
{
    public class Connection
    {
        internal static string serverIP { get; set; }
        internal string masterServerIP = "localhost";

        #region SEND POST REQUEST FUNCTION
        private string SendPost(string url, string postData)
        {
            string webpageContent = string.Empty;

            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;

                using (Stream webpageStream = webRequest.GetRequestStream())
                {
                    webpageStream.Write(byteArray, 0, byteArray.Length);
                }

                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
                    {
                        webpageContent = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                //throw or return an appropriate response/exception
            }

            return webpageContent;
        }

        #endregion

        internal void GetServerIP()
        {
            string tempServerList = SendPost("http://" + masterServerIP + "/ufndrm/api/serverslist.php", String.Format("serverslist={0}", 1));
            string[] serversList = tempServerList.Split('|');
            string minPingIP = "";
            int minPing = 999999;
            foreach (string IP in serversList)
            {
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                options.DontFragment = true;
                // Create a buffer of 32 bytes of data to be transmitted.
                string data = "This is a string which will be sent to the server to check if it's up or down.";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 10000;
                try
                {
                    PingReply reply = pingSender.Send(IP, timeout, buffer, options);
                    if (reply.Status == IPStatus.Success)
                    {
                        //Ping was successful
                        if ((int)reply.RoundtripTime < minPing)
                        {
                            minPing = (int)reply.RoundtripTime;
                            minPingIP = IP;
                        }
                    }
                    else
                    {
                        //Ping failed
                    }
                }
                catch (Exception ex)
                {
                    //MOSTLY HOST NOT FOUND
                }
            }
            serverIP = minPingIP;
        }

        internal string UpdateAndConnect()
        {
            return SendPost("http://" + serverIP + "/ufndrm/api/checkupdate.php", String.Format("version={0}", Application.ProductVersion));
        }

        internal void UpdateDialog(string updatestatus)
        {
            string[] updateprocessor = updatestatus.Split('|');
            DialogResult result = MessageBox.Show(String.Format("A new update was released. Would you like to download it?"
                + Environment.NewLine + updateprocessor[1]), "New update version: " + updateprocessor[0] + " was released.", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.OK)
            {
                System.Diagnostics.Process.Start(updateprocessor[2]);
                Application.Exit();
            }
            else
            {
                Application.Exit();
            }
        }

        internal string LogIn(string uid, string pwd)
        {
           return SendPost("http://" + serverIP + "/ufndrm/api/login.php", String.Format("uid={0}&pwd={1}", uid, pwd));
        }

        internal string LogOut(string username)
        {
            return SendPost("http://" + serverIP + "/ufndrm/api/logout.php", String.Format("uid={0}", username));
        }

        internal string UpdateAccountDetails(string uid, string pwd, string npwd)
        {
            return SendPost("http://" + serverIP + "/ufndrm/api/updatedetails.php", String.Format("uid={0}&pwd={1}&npwd={2}", uid, pwd, npwd));
        }

        internal string RegisterAccount(string uid, string pwd, string email, string sq, string sa, string key)
        {
            return SendPost("http://" + serverIP + "/ufndrm/api/register.php", String.Format("uid={0}&pwd={1}&email={2}&sq={3}&sa={4}&key={5}", uid, pwd, email, sq, sa, key));
        }

        internal string LockUsername(string email)
        {
            return SendPost("http://" + serverIP + "/ufndrm/api/checksq2.php", String.Format("email={0}", email));
        }

        internal string LockPassword(string username)
        {
            return SendPost("http://" + serverIP + "/ufndrm/api/checksq.php", String.Format("uid={0}", username));
        }

        internal string RecoverUsername(string email, string sa, string sq)
        {
            return SendPost("http://" + serverIP + "/ufndrm/api/forgotuid.php", String.Format("email={0}&sq={1}&sa={2}", email, sq, sa));
        }

        internal string RecoverPassword(string email, string sa, string sq)
        {
            return SendPost("http://" + serverIP + "/ufndrm/api/forgotpwd.php", String.Format("email={0}&sq={1}&sa={2}", email, sq, sa));
        }

        internal string CheckAndActivateKey(string uid, string key)
        {
            return SendPost("http://" + serverIP + "/ufndrm/api/activatekey.php", String.Format("uid={0}&key={1}", uid, key));
        }

        internal string GetNews()
        {
            return SendPost("http://" + serverIP + "/ufndrm/api/news.php", String.Format("news={0}", 1));
        }

    }
}
