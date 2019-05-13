using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace GPS
{
    public partial class Map : Form
    {
        DrawMap drawMap = new DrawMap();
        ShortestPath path;
        int upDownInfo = 0;
        bool streets = false;

        public Map()
        {
            InitializeComponent();
            //postavlja prozor na sredinu ekrana
            this.StartPosition = FormStartPosition.CenterScreen;

            userControl11.Hide();
            userControl21.Hide();
            button4.Hide();
            userControl31.Hide();
            AddAutocomplete(); 
        }

        private void AddAutocomplete()
        {
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            AutoCompleteStringCollection colNodes = new AutoCompleteStringCollection();

            GPSContext context = new GPSContext();
            foreach (Element el in context.Elements)
            {
                col.Add(el.Name);

                if (el.IsNode)
                    colNodes.Add(el.Name);
            }

            textBox1.AutoCompleteCustomSource = col;
            userControl21.textBox1.AutoCompleteCustomSource = colNodes;
            userControl21.textBox2.AutoCompleteCustomSource = colNodes;
        }
        
        //button1 = Prikaži kartu
        private void button1_Click(object sender, EventArgs e)
        {         
            if(!streets)
            {
                button1.Text = "Sakrij ulice";
            } else
            {
                button1.Text = "Prikaži ulice";
            }

            streets = !streets;

            pictureBox1.Refresh();
        }

       
        // izlaz gumb
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void čvorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLocation f4 = new AddLocation();
            f4.FormClosed += SubformClosed;
            f4.ShowDialog();
        }

        private void SubformClosed(object sender, FormClosedEventArgs e)
        {
            AddAutocomplete();
            pictureBox1.Refresh();
        }

        private void općenitoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppInfo f5 = new AppInfo();
            f5.ShowDialog();
        }


        private void bridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEdge f7 = new AddEdge();
            f7.FormClosed += SubformClosed;
            f7.ShowDialog();
        }


        // klik na slicicu za pronaci sve karakteristike
        private void tempFunctionForGetAllCharacteristic(Characteristic.CharacteristicTypes type)
        {
            //type=Store, Pharmacy...
            GPSDatabase gpsDatabase = new GPSDatabase();
            Graphics g = pictureBox1.CreateGraphics();

            List<Element> list = gpsDatabase.GetAllElementsWithCharacteristics(type);

            drawMap.drawAllElementsWithCharacteristic(g,list);
        }

        
      
        //klikovi na slicice
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            tempFunctionForGetAllCharacteristic( Characteristic.CharacteristicTypes.Kafić);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            tempFunctionForGetAllCharacteristic(Characteristic.CharacteristicTypes.Garaža);

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            tempFunctionForGetAllCharacteristic(Characteristic.CharacteristicTypes.Benzinska);

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            tempFunctionForGetAllCharacteristic(Characteristic.CharacteristicTypes.Ljekarna);

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            tempFunctionForGetAllCharacteristic(Characteristic.CharacteristicTypes.Restoran);

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            tempFunctionForGetAllCharacteristic(Characteristic.CharacteristicTypes.Pošta);

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            tempFunctionForGetAllCharacteristic(Characteristic.CharacteristicTypes.Trgovina);

        }

        //provjerava jel name ime nekog node-a
        private int checkIfExistsNodeWithName(string name)
        {
            GPSDatabase gpsDatabase = new GPSDatabase();
            return gpsDatabase.FindIdByName(name);
        }


        //na klik stijelice gore/dolje
        private void button5_Click(object sender, EventArgs e)
        {
            userControl11.Hide();
            userControl31.Hide();
            userControl21.Hide();
            button4.Hide();

            button5.BackgroundImage = Image.FromFile("../../Pictures/dole.png");

            if (upDownInfo == 1)
            {
                userControl11.Show();
                button5.BackgroundImage = Image.FromFile("../../Pictures/gore.png");
                upDownInfo = 2;
            }
            else if (upDownInfo == 2)
            {
                button5.BackgroundImage = Image.FromFile("../../Pictures/dole.png");
                upDownInfo = 1;
            }      
        }




        //na klik one ikone koja trazi put
        private void button6_Click(object sender, EventArgs e)
        {
            upDownInfo = 0;

            userControl11.Hide();
            userControl31.Hide();

            userControl21.Show();
            button4.Show();
            button4.BringToFront();
            

            button5.BackgroundImage = Image.FromFile("../../Pictures/dole.png");

        }


        //kada se klikne na ikonu ---search---
        private void button8_Click(object sender, EventArgs e)
        {
           
            GPSDatabase gpsDatabase = new GPSDatabase();

            //gledamo 1. postoji li uneseni element u bazi
            Element temp = gpsDatabase.FindElementByName(textBox1.Text);

            userControl21.Hide();
            button4.Hide();

            userControl31.Hide();

            if (upDownInfo != 2)
            {
                upDownInfo = 1;
            }


            if (temp == null) {
                upDownInfo = 0;
                ErrorMsg notFoundForm = new ErrorMsg("Unijeli ste lokaciju\nkoja ne postoji na karti!");
                notFoundForm.ShowDialog();   
                return;
            }

            List<Characteristic.CharacteristicTypes> locations = gpsDatabase.GetCharacteristicsByElementId(temp.ElementId);

            userControl11.label1.Text = temp.Name;
            userControl11.label3.Text = "";
            textBox1.Text = "";

            foreach (Characteristic.CharacteristicTypes type in locations)
            {
                userControl11.label3.Text +=" - "+ type + Environment.NewLine;
            }

            Graphics g = pictureBox1.CreateGraphics();
            drawMap.drawGraph(g, streets);
            drawMap.drawNodeOrEdgeLocation(g, temp.ElementId);
  
        }

        private void OnTextboxClick(object sender, EventArgs e)
        {
         
            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            drawMap.drawGraph(g, streets);

            userControl31.label6.Text = "";
            userControl31.label5.Text = "";

            userControl31.pictureBox3.Image = null;
            userControl31.label8.Text = "";
            userControl31.label9.Text = "";

            userControl21.uncheckedAllRadioButtons();

            if (path != null)
            {
                if (path.exists)
                {
                    drawMap.colorLines(g, path.pathEdges);
                    userControl31.label5.Text = Math.Round(path.distance / 54, 3).ToString() + " km";

                    int i;

                    for (i = 0; i < path.pathEdges.Count; i++)
                    {
                        userControl31.label6.Text += (i + 1).ToString() + ". " +
                            path.pathEdges[i].Name + Environment.NewLine;
                    }

                    userControl31.label7.ForeColor = Color.DodgerBlue;
                    userControl31.label7.Text = "Sretan put!";


                }
                else
                {
                    userControl31.label7.ForeColor = Color.Crimson;
                    userControl31.label7.Text = "Put ne postoji!";
                }

                drawMap.drawNode(g, path.start);
                drawMap.drawNode(g, path.end);
                if (path.stop != null)
                {
                    drawMap.drawNodeOrEdgeLocation(g, path.stop.ElementId, 0);
                    userControl31.label9.Text = "Posao obavite na";
                    userControl31.pictureBox3.Image = Image.FromFile(@"../../Pictures/location1.png");
                    userControl31.pictureBox3.BackgroundImageLayout = ImageLayout.Zoom;
                    userControl31.label8.Text = path.stop.Name;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            path = null;
            pictureBox1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            userControl11.Hide();
            userControl31.Hide();

            GPSDatabase gpsDatabase = new GPSDatabase();
            Node startNode = gpsDatabase.FindNodeByName(userControl21.textBox1.Text);
            Node endNode = gpsDatabase.FindNodeByName(userControl21.textBox2.Text);

            if (startNode != null && endNode != null)
            {
                userControl21.Hide();
                button4.Hide();

                Graphics g = pictureBox1.CreateGraphics();

                userControl31.Show();

                userControl31.label1.Text = startNode.Name;
                userControl31.label2.Text = endNode.Name;

                GPSNavigation gps = new GPSNavigation();

                if (userControl21.isChecked == -1)
                {
                    path = new ShortestPath(gps, startNode, endNode);
                }
                else
                {
                    path = new ShortestPath(gps, startNode, endNode, (Characteristic.CharacteristicTypes)userControl21.isChecked);
                }

                pictureBox1.Refresh();
            }
            else
            {
                string errorMessage = "Navedene lokacije ne postoje na karti:\n";
                errorMessage += (startNode == null ? userControl21.textBox1.Text : "") + Environment.NewLine;

                errorMessage += (endNode == null ? userControl21.textBox2.Text : "");
                userControl21.Show();
                button4.Show();
                ErrorMsg notFoundForm = new ErrorMsg(errorMessage);
                notFoundForm.ShowDialog();
            }
        }
    }
}

