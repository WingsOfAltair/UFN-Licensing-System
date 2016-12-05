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
    public partial class ufnls_register : Form
    {

        private int acctype, days;
        private string date, rank;
        private bool cancel = false, activated;
        private char[] SP = {'[', ']', '!' ,'@',  '#', '$', '%' , '^' , '&' , '*' , '(' , ')', '+'
                    , '=' , ' ' , ',' ,'{' , '}' ,':' , '/' ,'\'' };
                 

        Security Security = new Security();
        Connection Connection = new Connection();

        public ufnls_register()
        {
            InitializeComponent();
            Security.CaptchaGraphic = Graphics.FromImage(Security.CaptchaImage);
            CaptchaPictureBox.Image = Security.GenerateCaptcha();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < SP.Length; i++)
            {
                if (textBox1.Text.Contains(SP[i]))
                {
                    MessageBox.Show("Special characters are not allowed for security reasons, please remove them immediately.");
                    textBox1.Text = "";
                }
            }
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Please enter the required information to register.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!textBox3.Text.Contains("@"))
            {
                MessageBox.Show("Please enter a valid email address.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (comboBox2.SelectedIndex == 1)
            {
                if (textBox5.Text == "")
                {
                    MessageBox.Show("Please enter a valid serial key.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (Security.IsValidCaptcha(textBox6.Text) == false)
            {
                MessageBox.Show("Please enter the captcha correctly! If it was too hard, try to refresh it or click the Speak button.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult AccountRegistrationConfirmation = MessageBox.Show("Are you sure you want to register a new account with this information?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (AccountRegistrationConfirmation == DialogResult.Yes)
            {
                this.Hide();
                Security.SecurityCheck();
                register();
            }
        }

        private void register()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            if (comboBox2.SelectedIndex == 1)
                textBox5.Enabled = false;
            textBox6.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            button2.Enabled = false;
            button1.Enabled = false;
            this.Enabled = false;
            Application.UseWaitCursor = true;

            try
            {
                string registeredstatus = Connection.RegisterAccount(textBox1.Text, textBox2.Text, textBox3.Text, comboBox1.Text, textBox4.Text, textBox5.Text);
                Application.UseWaitCursor = false;
                if (registeredstatus == "1")
                {
                    MessageBox.Show("Your account has been registered.");
                    Application.OpenForms[1].Show();
                    this.Close();
                }
                else if (registeredstatus == "-1")
                {
                    MessageBox.Show("Code is either invalid or already registered.");
                }
                else if (registeredstatus == "2")
                {
                    MessageBox.Show("This username is already registered.");
                }
                else if (registeredstatus == "3")
                {
                    MessageBox.Show("This email is already registered.");
                }
                if (registeredstatus != "1")
                {
                    textBox1.Enabled = true;
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;
                    textBox4.Enabled = true;
                    if (comboBox2.SelectedIndex == 1)
                        textBox5.Enabled = true;
                    textBox6.Enabled = true;
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    button2.Enabled = true;
                    button1.Enabled = true;
                    this.Enabled = true;
                    Security.GenerateCaptcha();
                    textBox6.Text = "";
                    this.Show();
                    backgroundWorker1.CancelAsync();
                    return;
                }
            }
            catch
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                if (comboBox2.SelectedIndex == 1)
                    textBox5.Enabled = true;
                textBox6.Enabled = true;
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                button2.Enabled = true;
                button1.Enabled = true;
                this.Enabled = true;
                Application.UseWaitCursor = false;
                Security.GenerateCaptcha();
                textBox6.Text = "";
                this.Show();
                backgroundWorker1.CancelAsync();
                MessageBox.Show("Couldn't connect to UFN network.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Application.OpenForms[1].Show();
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                acctype = 0;
                textBox5.Enabled = false;
                textBox5.Text = "";
                rank = "Trial Account";
                days = 7;
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                acctype = 1;
                textBox5.Enabled = true;
                textBox5.Text = "";
                rank = "VIP Member";
                days = 30;
            }
        }

        private void ufnls_register_Load(object sender, EventArgs e)
        {
            Security.CaptchaGraphic = Graphics.FromImage(Security.CaptchaImage);
            Security.GenerateCaptcha();
        }

        private void button3_Click(object sender, EventArgs e)
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
                button2.PerformClick();
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '[' || e.KeyChar == ']' || e.KeyChar == '!' || e.KeyChar == '@' || e.KeyChar == '#' || e.KeyChar == '$' || e.KeyChar == '%' || e.KeyChar == '^' || e.KeyChar == '&' || e.KeyChar == '*' || e.KeyChar == '(' || e.KeyChar == ')' || e.KeyChar == '+'
                    || e.KeyChar == '=' || e.KeyChar == ' ' || e.KeyChar == ',' || e.KeyChar == '{' || e.KeyChar == '}' || e.KeyChar == ':' || e.KeyChar == '/' || e.KeyChar == '\'' || e.KeyChar == '?')
                {
                    MessageBox.Show("Special characters are not allowed for security reasons, please remove them immediately.");
                    textBox1.Clear();
                }
        }

    }
}
