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
    public partial class AddEdge : Form // forma za dodavanje brida
    {
        DrawMap drawMap = new DrawMap();
        bool add = false;
        public AddEdge()
        {
            InitializeComponent();
            checkedListBox1.CheckOnClick = true;
            checkedListBox1.DataSource = Characteristic.CharacteristicTypes.GetNames(typeof(Characteristic.CharacteristicTypes));
            if (add == false)
            {
                List<string> l = gpsDatabase.GetAllNamesNodes();
                for (int i = 0; i < l.Count(); i++)
                {
                    comboBox1.Items.Add(l[i]);
                    comboBox2.Items.Add(l[i]);
                }
                add = true;
            }
            radioButton2.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sx = comboBox1.Text.ToString();
            string sy = comboBox2.Text.ToString();
            string sname = textBox1.Text.ToString();
            bool smjer = true;

            if (radioButton1.Checked == true) smjer = true;
            if (radioButton2.Checked == true) smjer = false;

            if(radioButton1.Checked == false && radioButton2.Checked == false)
            {
                ErrorMsg f8 = new ErrorMsg();
                f8.ShowDialog();
                return;
            }

            List<Characteristic.CharacteristicTypes> enumlist = new List<Characteristic.CharacteristicTypes>();

            foreach (object item in checkedListBox1.CheckedItems)
            {
                Characteristic.CharacteristicTypes en = (Characteristic.CharacteristicTypes)Enum.Parse(typeof(Characteristic.CharacteristicTypes), item.ToString());
                enumlist.Add(en);
            }

            if (sname == "") // ako nije unio nista za ime javi gresku
            {
                ErrorMsg f8 = new ErrorMsg();
                f8.ShowDialog();
                return;
            }
            this.Close();
            drawMap.AddEdge(sx, sy, sname, enumlist, smjer);
        }
    }
}
