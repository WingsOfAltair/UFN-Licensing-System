using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Game;

namespace UFN_Licensing_System
{
    public partial class ufnls_welcome : Form
    {
        private string username, rank, expirydate, loggedoutstatus;

        Security Security = new Security();
        Connection Connection = new Connection();

        public ufnls_welcome(string uid, string urank, string udate)
        {
            InitializeComponent();
            username = uid;
            rank = urank;
            expirydate = udate;
        }

        private void ufnls_welcome_Load(object sender, EventArgs e)
        {
            if (rank =="Administrator")
            {
                System.Diagnostics.Process.Start("http://" + Connection.masterServerIP + @"/ufndrm/ui");
                Application.Exit();
            }
            if (rank == "VIP Member" && expirydate != "Expired")
            {
                button3.Enabled = true;
                label1.Text = "Welcome, " + username + ", " + "Active VIP Member.";
            }
            else if (rank == "Trial Account" && expirydate != "Expired")
            {
                button3.Enabled = true;
                label1.Text = "Welcome, " + username + ", " + "Active Trial Account.";
            }
            else if (rank == "Expired" && expirydate == "Expired")
            {
                button3.Enabled = false;
                label1.Text = "Welcome, " + username + ", " + rank + ".";
            }
            else if (rank == "VIP Member" && expirydate == "Expired")
            {
                button3.Enabled = false;
                label1.Text = "Welcome, " + username + ", " + "Former VIP Member.";
            }
            else if (rank == "Trial Account" && expirydate == "Expired")
            {
                button3.Enabled = false;
                label1.Text = "Welcome, " + username + ", " + "Expired Trial Account.";
            }
            label2.Text = "Expiration date: " + expirydate + ".";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(@"This button is supposed to launch your program."
           //+ " It will only be enabled for VIP accounts with an active subscription,"
           //+ " or Trial accounts which didn't expire, either.");

            if (rank != "Expired" && expirydate != "Expired")
            {
                Game.Game Game = new Game.Game();
                Game.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ufnls_redeemcode redeemcode = new ufnls_redeemcode(username);
            redeemcode.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loggedoutstatus = Connection.LogOut(username);
            if (loggedoutstatus == "1")
            {
                Application.OpenForms[1].Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to log out from the network, please try again.");
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ufnls_changeaccdetails ChangeAcc = new ufnls_changeaccdetails(username);
            ChangeAcc.ShowDialog();
        }
    }
}
