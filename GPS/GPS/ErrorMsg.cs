using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPS
{
    public partial class ErrorMsg : Form
    {
        public ErrorMsg(string text = null) 
        {
            InitializeComponent();
            
            if (text == null)
                label1.Text = "Unijeli ste krive ili nepotpune podatke! Pokušajte ponovno :)" + Environment.NewLine + Environment.NewLine +
                "Napomene:" + Environment.NewLine + "raspon za X = < 0, 590 >"+ Environment.NewLine
                + "raspon za Y = < 0, 400 >" + Environment.NewLine + "Ime mora biti drugačije od postojećih na karti";
            else
                label1.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
