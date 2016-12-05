using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography;

namespace UFN_Licensing_System___Key_generator
{
    public partial class ufnlskg_main : Form
    {

        private int amountofkeys;

        public ufnlskg_main()
        {
            InitializeComponent();
        }

        private void ufnlskg_main_Load(object sender, EventArgs e)
        {
            label1.Text = "Generate  UFN License key...";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            amountofkeys = (int)numericUpDown1.Value;
            DialogResult GenerateKeysConfirmation = MessageBox.Show("Are you sure you want to generate " + amountofkeys + " keys?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (GenerateKeysConfirmation == DialogResult.Yes)
            {
                try
                {
                    if (amountofkeys != 0)
                    {
                        this.Enabled = false;
                        label1.Text = "Generating " + amountofkeys + " keys, please wait...";

                        Connection Connection = new Connection();

                        string keys = Connection.GenerateKey(amountofkeys);
                        string[] keycodes = keys.Split('|');

                        System.IO.StreamWriter file = new System.IO.StreamWriter("generated key codes.txt", true);

                        foreach (string key in keycodes)
                            file.WriteLine(key);

                        file.Close();
                        keycodes = null;
                        keys = null;

                        this.Enabled = true;
                        this.Text = "UFN Licensing System - Key generator";
                        label1.Text = "All keys were generated!";
                    }
                    else
                    {
                        label1.Text = "0 keys were generated.";
                    }
                }
                catch
                {
                    MessageBox.Show("An error has occured, the application will now self terminate.", "UFN Licensing System - Key generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
            }
        }
    }
}