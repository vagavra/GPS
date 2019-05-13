using System;
using System.Windows.Forms;

namespace GPS
{
    public partial class SearchPath : UserControl
    {
        public SearchPath()
        {
            InitializeComponent();
        }

        
        internal int isChecked = -1;

        public void uncheckedAllRadioButtons()
        {
            foreach (var element in panel3.Controls)
            {
                if (element is Label) continue;
                RadioButton r=(RadioButton)element;
                r.Checked = false;
            }
            isChecked = -1;
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            isChecked = (int)Characteristic.CharacteristicTypes.Restoran;
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            isChecked = (int)Characteristic.CharacteristicTypes.Kafić;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            isChecked = (int)Characteristic.CharacteristicTypes.Garaža;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            isChecked = (int)Characteristic.CharacteristicTypes.Benzinska;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            isChecked = (int)Characteristic.CharacteristicTypes.Ljekarna;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            isChecked = (int)Characteristic.CharacteristicTypes.Pošta;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            isChecked = (int)Characteristic.CharacteristicTypes.Trgovina;
        }

        private void UserControl2_Load(object sender, EventArgs e)
        {

        }
    }
}
