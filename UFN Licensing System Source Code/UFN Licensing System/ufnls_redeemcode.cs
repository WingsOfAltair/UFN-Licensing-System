using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UFN_Licensing_System
{
    public partial class ufnls_redeemcode : Form
    {
        private string username;

        Connection Connection = new Connection();

        public ufnls_redeemcode(string uid)
        {
            username = uid;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Invalid serial key.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult RedeemCodeConfirmation = MessageBox.Show("Are you sure you want to bind the key to this account?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (RedeemCodeConfirmation == DialogResult.Yes)
            {

                string activationstatus = Connection.CheckAndActivateKey(username, textBox1.Text);

                if (activationstatus == "1")
                {
                    MessageBox.Show("The serial key was successfully redeemed for " + username + ". The system will shut down to apply the changes.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    logout();
                }
                else if (activationstatus == "0")
                {
                    MessageBox.Show("The serial key you have entered is invalid.");
                    return;
                }
                else if (activationstatus == "-1")
                {
                    MessageBox.Show("The username " + username + " does not exist.");
                    return;
                }
            }
        }

        private void logout()
        {
            string loggedoutstatus = Connection.LogOut(username);
            if (loggedoutstatus == "1")
            {
                Application.Restart();
            }
            else
            {
                MessageBox.Show("Failed to log out from the network, please try again.");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
