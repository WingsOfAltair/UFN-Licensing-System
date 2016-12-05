using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace UFN_Licensing_System
{
    public partial class ufnls_splash : Form
    {
        Connection Connection = new Connection();
        Security Security = new Security();

        public ufnls_splash()
        {
            InitializeComponent();
        }

        private void ufnls_splash_Load(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;
            x = Screen.PrimaryScreen.WorkingArea.Width;
            y = Screen.PrimaryScreen.WorkingArea.Height - this.Height;

            while (!(x == Screen.PrimaryScreen.WorkingArea.Width - this.Width))
            {
                x = x - 1;
                this.Location = new Point(x, y);
            }

            Application.UseWaitCursor = true;
            this.Show();
            label2.Show();
            Delay(1);
            label2.Hide();
            label3.Show();
            Security.SecurityCheck();
            Delay(0.2);
            label3.Hide();
            label4.Show();
            Delay(0.1);
            Connection.GetServerIP();

            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "This is a string which will be sent to the server to check if it's up or down.";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 10000;
            try
            {
                PingReply reply = pingSender.Send(Connection.serverIP, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    //Ping was successful
                    label4.Hide();
                    label6.Show();
                    Delay(2);
                    ufnls_login login = new ufnls_login();
                    login.Show();
                }
                else
                {
                    //Ping failed
                    label4.Hide();
                    label5.Show();
                    Delay(2);
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                //MOSTLY HOST NOT FOUND
                label4.Hide();
                label5.Show();
                Delay(2);
                Environment.Exit(0);
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            this.Visible = false;
        }

        #region "Functions"

        public void Delay(double dblSecs)
        {
            const double OneSec = 1.0 / (1440.0 * 60.0);
            System.DateTime dblWaitTil = default(System.DateTime);
            DateTime.Now.AddSeconds(OneSec);
            dblWaitTil = DateTime.Now.AddSeconds(OneSec).AddSeconds(dblSecs);
            while (!(DateTime.Now > dblWaitTil))
            {
                Application.DoEvents();
            }
        }

        #endregion

    }
}
