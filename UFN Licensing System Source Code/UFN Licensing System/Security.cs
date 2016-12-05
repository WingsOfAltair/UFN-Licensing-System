using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpeechLib;
using System.Drawing;

namespace UFN_Licensing_System
{
    public class Security
    {
        internal System.Text.StringBuilder SBuilder = new System.Text.StringBuilder();
        private Font DrawingFont = new Font("Verdana", 20);
        internal Bitmap CaptchaImage = new Bitmap(220, 63);
        internal Graphics CaptchaGraphic;

        private Random Ran = new Random();
        private string[] Alphabet = {
	"a",
	"b",
	"c",
	"d",
	"e",
	"f",
	"g",
	"h",
	"i",
	"j",
	"k",
	"l",
	"m",
	"n",
	"o",
	"p",
	"q",
	"r",
	"s",
	"t",
	"u",
	"v",
	"w",
	"x",
	"y",
	"z",
	"A",
	"B",
	"C",
	"D",
	"E",
	"F",
	"G",
	"H",
	"I",
	"J",
	"K",
	"L",
	"M",
	"N",
	"O",
	"P",
	"Q",
	"R",
	"S",
	"T",
	"U",
	"V",
	"W",
	"X",
	"Y",
	"Z",
	"0",
	"1",
	"2",
	"3",
	"4",
	"5",
	"6",
	"7",
	"8",
	"9"

};

        public void SecurityCheck()
        {
            foreach (System.Diagnostics.Process myProc in System.Diagnostics.Process.GetProcesses())
            {
                if (myProc.MainWindowTitle == "Wireshark")
                {
                    try
                    {
                        myProc.Kill();
                        MessageBox.Show("Wireshark was detected running on your PC and was terminated.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch
                    {
                        Environment.Exit(0);
                    }
                }
                if (myProc.MainWindowTitle == "Fiddler")
                {
                    try
                    {
                        myProc.Kill();
                        MessageBox.Show("Fiddler was detected running on your PC and was terminated.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }

        public Bitmap GenerateCaptcha()
        {
            SBuilder.Clear();

            for (int I = 1; I <= 5; I++)
            {
                SBuilder.Append(Alphabet[Ran.Next(0, Alphabet.Length)]);
            }

            Rectangle Rect = new Rectangle(0, 0, 220, 63);
            StringFormat StringFormat = new StringFormat();
            StringFormat.Alignment = StringAlignment.Center;
            StringFormat.LineAlignment = StringAlignment.Center;

            CaptchaGraphic.Clear(Color.White);
            CaptchaGraphic.DrawString(SBuilder.ToString(), DrawingFont, Brushes.Black, Rect, StringFormat);
            return CaptchaImage;

        }

        public bool IsValidCaptcha(string Input)
        {
            if ((Input == SBuilder.ToString()))
            {
                return true;
            }
            return false;
        }
    }
}
