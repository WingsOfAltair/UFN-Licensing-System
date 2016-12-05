using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UFN_Licensing_System
{
    public partial class ufnls_changeaccdetails : Form
    {
        private string username, updatedetailstatus;

        Connection Connection = new Connection();

        public ufnls_changeaccdetails(string uid)
        {
            InitializeComponent();
            username = uid;
            label1.Text = "Username: " + username + ".";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult ChangeAccDetails = MessageBox.Show("Are you sure you want to change your account's log in details?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            updatedetailstatus = Connection.UpdateAccountDetails(username, textBox1.Text, textBox2.Text);
            if (updatedetailstatus == "1")
            {
                MessageBox.Show("Account details have changed successfully.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Could not update account details.");
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1.PerformClick();
        }
    }
}
