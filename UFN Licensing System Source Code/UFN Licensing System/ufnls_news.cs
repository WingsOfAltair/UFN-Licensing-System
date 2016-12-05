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
    public partial class ufnls_news : Form
    {
        public ufnls_news()
        {
            InitializeComponent();
        }

        private void ufnls_news_Load(object sender, EventArgs e)
        {
            Connection Connection = new Connection();
            label1.Text = "Getting news...";
            string newz = Connection.GetNews();
            string[] news = newz.Split('|');
            label1.Text = "";
            foreach (string newss in news)
                if (newss != "")
                    label1.Text += newss + Environment.NewLine;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
