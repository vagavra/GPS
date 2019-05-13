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
    public partial class AddLocation : Form // forma za dodavanje cvora
    {
        DrawMap drawMap = new DrawMap();
        
        ErrorMsg f8 = new ErrorMsg();
        public AddLocation()
        {
            InitializeComponent();
            checkedListBox1.CheckOnClick = true;
            checkedListBox1.DataSource = Characteristic.CharacteristicTypes.GetNames(typeof(Characteristic.CharacteristicTypes));
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {         
            string sx = textBox1.Text.ToString();
            string sy = textBox2.Text.ToString();
            string sname = textBox3.Text.ToString();
            if (sx == "" || sy == "" || sname == "")
            {
                f8.ShowDialog();
                return;
            }

            List<Characteristic.CharacteristicTypes> enumlist = new List<Characteristic.CharacteristicTypes>();

           foreach (object item in checkedListBox1.CheckedItems)
           {
                Characteristic.CharacteristicTypes en = (Characteristic.CharacteristicTypes)Enum.Parse(typeof(Characteristic.CharacteristicTypes), item.ToString());
                enumlist.Add(en);               
           }

            int x = Int32.Parse(sx);
            int y = Int32.Parse(sy);
                       
            drawMap.AddNode(x, y, sname, enumlist);
            this.Close();
        }
        

        

    }
}
