using System;
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
    public partial class ufnls_forgotuid : Form
    {
        private string email, uid, sq, sa, usq;

        Security Security = new Security();
        Connection Connection = new Connection();

        public ufnls_forgotuid()
        {
            InitializeComponent();
            Security.CaptchaGraphic = Graphics.FromImage(Security.CaptchaImage);
            CaptchaPictureBox.Image = Security.GenerateCaptcha();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Security.IsValidCaptcha(textBox6.Text) == false)
            {
                MessageBox.Show("Please enter the captcha correctly! If it was too hard, try to refresh it or click the Speak button.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Security.SecurityCheck();
            sa = textBox2.Text;
            uid = Connection.RecoverUsername(email, sa, sq);
            if (uid != "" && uid != "Invalid answer.")
                MessageBox.Show("Your username is: " + uid);
            else if (uid == "Invalid answer.")
                MessageBox.Show("You've entered an invalid answer.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            email = textBox1.Text;
            textBox1.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            Security.SecurityCheck();
            usq = Connection.LockUsername(email);
            if (usq != "" && usq != "0")
            {
                sq = usq;
                label3.Text = sq;
                label3.Show();
                textBox2.Enabled = true;
                textBox6.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                textBox2.Focus();
            }
            else
            {
                MessageBox.Show("No such email exists in the database.");
                textBox1.Enabled = true;
                textBox2.Enabled = false;
                textBox6.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                label3.Hide();
                label3.Text = "%sq%";
            }
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

        private void button5_Click(object sender, EventArgs e)
        {
            CaptchaPictureBox.Image = Security.GenerateCaptcha();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                backgroundWorker1.RunWorkerAsync();
            }
            catch
            {

            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button3.PerformClick();
        }

        private void ufnls_forgotuid_Load(object sender, EventArgs e)
        {
            Security.CaptchaGraphic = Graphics.FromImage(Security.CaptchaImage);
            Security.GenerateCaptcha();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1.PerformClick();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            SpVoice voice = new SpVoice();

            foreach (char Character in Security.SBuilder.ToString())
            {
                voice.Speak(Character.ToString());
                Delay(0.5);
            }
        }
    }
}
