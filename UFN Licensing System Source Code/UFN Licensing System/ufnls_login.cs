using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpeechLib;

namespace UFN_Licensing_System
{
    public partial class ufnls_login : Form
    {

        private string username, password, rank, expirydate;
        private string[] accountinfo;

        private int loggedin;

        private bool allow = false, cancel = false;

        Properties.Settings settings = new Properties.Settings();

        Security Security = new Security();
        Connection Connection = new Connection();

        public ufnls_login()
        {
            string updatestatus = Connection.UpdateAndConnect();
            if (updatestatus != "0")
                Connection.UpdateDialog(updatestatus);
            else {
                ufnls_news News = new ufnls_news();
                Application.UseWaitCursor = false;
                Application.OpenForms[0].Hide();
                News.ShowDialog();
            }

            InitializeComponent();
            Security.SecurityCheck();

            try
            {
                if (settings.username != "")
                {
                    if (settings.password != "")
                    {
                        textBox1.Text = settings.username;
                        textBox2.Text = settings.password;
                        checkBox1.Checked = true;
                        button1.Focus();
                    }
                    else
                    {
                        checkBox1.Checked = false;
                    }
                }
                else
                {
                    checkBox1.Checked = false;
                }
                if (checkBox1.Checked == false)
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    settings.Save();
                    textBox1.Focus();
                }
                Security.CaptchaGraphic = Graphics.FromImage(Security.CaptchaImage);
                CaptchaPictureBox.Image = Security.GenerateCaptcha();
            }
            catch
            {
            }
        }

        private void ufnls_login_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ask the user if he/she wants to really exit the application or not.
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Please enter the required information to login.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Security.IsValidCaptcha(textBox6.Text) == false)
            {
                MessageBox.Show("Please enter the captcha correctly! If it was too hard, try to refresh it or click the Speak button.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CaptchaPictureBox.Image = Security.GenerateCaptcha();
            textBox6.Text = "";
            this.Hide();
            Security.SecurityCheck();
            if (backgroundWorker1.IsBusy == false)
                backgroundWorker1.RunWorkerAsync();
            Application.UseWaitCursor = true;
            login();
            if (allow == true)
            {
                Application.UseWaitCursor = false;
                ufnls_welcome welcome = new ufnls_welcome(username, rank, expirydate);
                welcome.Show();
            }
        }

        private void login()
        {

            if (checkBox1.Checked == true)
            {
                settings.username = textBox1.Text;
                settings.password = textBox2.Text;
                settings.Save();
            }
            else
            {
                settings.username = "";
                settings.password = "";
                settings.Save();
            }

            username = textBox1.Text;
            password = textBox2.Text;

            try
            {
                string loggedinstatus = Connection.LogIn(username, password);
                accountinfo = loggedinstatus.Split('|');

                loggedinstatus = accountinfo[0];
                rank = accountinfo[1];
                expirydate = accountinfo[2];
                accountinfo = null;

                if (loggedinstatus == "2")
                {
                    allow = true;
                }
                else if (loggedinstatus == "0")
                {
                    Application.UseWaitCursor = false;
                    this.Show();
                    backgroundWorker1.CancelAsync();
                    MessageBox.Show("Incorrect username/password combination.");
                }
                else if (loggedinstatus == "1")
                {
                    Application.UseWaitCursor = false;
                    this.Show();
                    backgroundWorker1.CancelAsync();
                    MessageBox.Show("Incorrect username/password combination.");
                }
                else if (loggedinstatus == "-1")
                {
                    Application.UseWaitCursor = false;
                    this.Show();
                    backgroundWorker1.CancelAsync();
                    MessageBox.Show("Your account is banned.");
                }
                else if (loggedinstatus == "3")
                {
                    Application.UseWaitCursor = false;
                    backgroundWorker1.CancelAsync();
                    ufnls_welcome welcome = new ufnls_welcome(username, rank, "Expired");
                    welcome.Show();
                }
                else if (loggedinstatus == "5")
                {
                    Application.UseWaitCursor = false;
                    this.Show();
                    backgroundWorker1.CancelAsync();
                    MessageBox.Show("This account is already in use, please try again later or contact customer support.");
                }
                else
                {
                    Application.UseWaitCursor = false;
                    this.Show();
                    backgroundWorker1.CancelAsync();
                    MessageBox.Show("Failed to log in, please try again later.");
                }
            }
            catch
            {
                Application.UseWaitCursor = false;
                this.Show();
                backgroundWorker1.CancelAsync();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ufnls_register register = new ufnls_register();
            register.ShowDialog();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CaptchaPictureBox.Image = Security.GenerateCaptcha();
                textBox6.Text = "";
                this.Hide();
                CaptchaPictureBox.Image = Security.GenerateCaptcha();
                backgroundWorker1.RunWorkerAsync();
                login();
                if (allow == true)
                {
                    ufnls_welcome welcome = new ufnls_welcome(username, rank, expirydate);
                    welcome.Show();
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CaptchaPictureBox.Image = Security.GenerateCaptcha();
                textBox6.Text = "";
                this.Hide();
                Security.SecurityCheck();
                backgroundWorker1.RunWorkerAsync();
                login();
                if (allow == true)
                {
                    ufnls_welcome welcome = new ufnls_welcome(username, rank, expirydate);
                    welcome.Show();
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ufnls_forgotpwd forgotpwd = new ufnls_forgotpwd();
            forgotpwd.ShowDialog();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ufnls_waiting waiting = new ufnls_waiting();
            waiting.Show();
            while (allow == false)
            {
                if (backgroundWorker1.CancellationPending == true)
                {
                    waiting.Dispose();
                    cancel = true;
                }
                if (cancel == true)
                {
                    cancel = false;
                    return;
                }
                Delay(0.1);
            }
            if (allow == true)
            {
                waiting.Dispose();
                allow = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CaptchaPictureBox.Image = Security.GenerateCaptcha();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                backgroundWorker2.RunWorkerAsync();
            }
            catch
            {
                // Unhandled exception here.
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            SpVoice voice = new SpVoice();

            foreach (char Character in Security.SBuilder.ToString())
            {
                voice.Speak(Character.ToString());
                Delay(0.5);
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Please enter the required information to login.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (Security.IsValidCaptcha(textBox6.Text) == false)
                {
                    MessageBox.Show("Please enter the captcha correctly! If it was too hard, try to refresh it or click the Speak button.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CaptchaPictureBox.Image = Security.GenerateCaptcha();
                textBox6.Text = "";
                this.Hide();
                Security.SecurityCheck();
                backgroundWorker1.RunWorkerAsync();
                login();
                if (allow == true)
                {
                    ufnls_welcome welcome = new ufnls_welcome(username, rank, expirydate);
                    welcome.Show();
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ufnls_forgotuid forgotuid = new ufnls_forgotuid();
            forgotuid.ShowDialog();
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