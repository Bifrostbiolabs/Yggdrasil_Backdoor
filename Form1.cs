using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace Yggdrasil_Backdoor
{
    public partial class Form1 : Form
    {
        public string[] averragedata;
        public bool first = true;
        float[] cal;



        public float H = 0.10f;
        public float L = 0.90f;


        public Form1()
        {
            InitializeComponent();

            

        }

       

        private void Form1_Load(object sender, EventArgs e)
        {

            
            string[] ports = SerialPort.GetPortNames();
            comboBox1.DataSource = ports;





            string[] numberA = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            comboBoxTime.DataSource = numberA; comboBoxTime.Text = "1";

            string[] numberB = { "0","1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" };
            comboBoxSensornr.DataSource = numberB; comboBoxSensornr.Text = "0";

            string[] numberC = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" };
            comboBox4.DataSource = numberC; comboBox4.Text = "0";

            string[] numberD = { "None", "Low", "Medium", "High" };
            comboBoxAverage.DataSource = numberD; comboBoxAverage.Text = "Medium";

            string[] numberE = { "No","Yes" };
            comboBoxPeak.DataSource = numberE; comboBoxPeak.Text = "Yes";

            List<int> lista = new List<int>();
            for (int i = 0; i < 360; i++)
            {
                lista.Add(i);
            }
            List<int> listb = new List<int>();
            for (int i = 0; i < 360; i++)
            {
                listb.Add(i);
            }
            //return list.ToArray();
            comboBox2.DataSource = lista;
            comboBox3.DataSource = listb; comboBox3.Text = "359";

        }

        private void button12_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine(richTextBox1.Text);
            while (serialPort1.BytesToRead == 0) ;
            richTextBox2.Text = serialPort1.ReadLine();
        }

       

        public void button13_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                String nr = comboBoxSensornr.SelectedItem.ToString();


                serialPort1.WriteLine("#,60," + nr.ToString() + ",1,0,") ;
                richTextBox1.Text = ("#,60," + nr.ToString() + ",1,0,");
                
                while (serialPort1.BytesToRead == 0);
                String str = Convert.ToString(serialPort1.ReadLine());
                richTextBox2.Text = str;
                String[] spearator = { ","};
                String[] spearatorb = { ";"};

                Int32 count = 400;

                
                string[] strlist = str.Split(spearator, count, StringSplitOptions.RemoveEmptyEntries);
                


                if (strlist.Length > 3)
                {
                    try
                    {
                        string[] mysplitdata = strlist[4].Split(spearatorb, count, StringSplitOptions.RemoveEmptyEntries);
                        richTextBox2.Text = strlist[4];
                        if (first == true & mysplitdata.Length > 90)
                        {
                            averragedata = mysplitdata;
                            first = false;

                        }

                        chart1.Series["ADC Value"].Points.Clear();
                        chart1.Series["ADC Peak"].Points.Clear();
                        chart3.Series["Average"].Points.Clear();
                        chart3.Series["Peak"].Points.Clear();
                        //chart1.Series["top"].Points.AddXY(0,100);

                        for (int i = 0; i < 360; i++)
                        {
                            float a = float.Parse(mysplitdata[i]);
                            float b = float.Parse(averragedata[i]);

                            float s = (a * H) + (b * L);

                            averragedata[i] = s.ToString();
                            int o = (int)Math.Round(s, 2);
                            averragedata[i] = s.ToString();

                            chart1.Series["ADC Value"].Points.AddXY(i, o);
                            if (comboBoxPeak.Text == "Yes")
                            {
                                chart1.Series["ADC Peak"].Points.AddXY(i, mysplitdata[i]);
                            }
                            
                            

                            if (i >= Convert.ToInt32(comboBox2.SelectedItem) & i <= Convert.ToInt32(comboBox3.SelectedItem))
                            {
                                chart3.Series["Average"].Points.AddXY(i, o);
                                if (comboBoxPeak.Text == "Yes")
                                {
                                    chart3.Series["Peak"].Points.AddXY(i, mysplitdata[i]);
                                }
                                
                            }


                        }
                    }
                    catch
                    {

                    }

                    

                }
            }
        }
        private void button14_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                
                String nr = comboBox4.SelectedItem.ToString();
                serialPort1.WriteLine("#,25," + nr.ToString() + ",1,0,");
                richTextBox1.Text = ("#,25," + nr.ToString() + ",1,0,");

                while (serialPort1.BytesToRead == 0) ;
                String str = Convert.ToString(serialPort1.ReadLine());
                richTextBox2.Text = str;
                String[] spearator = { ",",";" };

                Int32 count = 20;


                string[] strlista = str.Split(spearator, count, StringSplitOptions.RemoveEmptyEntries);
                chart2.Series["Calibration data"].Points.Clear();

                chart2.Series["Calibration data"].Points.AddXY(100, strlista[6]);
                chart2.Series["Calibration data"].Points.AddXY(85, strlista[7]);
                chart2.Series["Calibration data"].Points.AddXY(70, strlista[8]);
                chart2.Series["Calibration data"].Points.AddXY(55, strlista[9]);
                chart2.Series["Calibration data"].Points.AddXY(40, strlista[10]);
                chart2.Series["Calibration data"].Points.AddXY(0, strlista[11]);
                textBox1.Text = strlista[4];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (!serialPort1.IsOpen)
            {
                serialPort1.PortName = comboBox1.SelectedItem.ToString();
                serialPort1.Open();
                if (serialPort1.IsOpen)
                {
                    button1.BackColor = Color.Red;
                    button1.Text = "Connectet";
                    serialPort1.WriteLine("#,55,0,1,0,");
                    while (serialPort1.BytesToRead == 0) ;
                    serialPort1.ReadLine();
                    serialPort1.WriteLine("#,55,0,1,0,"); richTextBox1.Text = ("#,55,0,1,0,");
                    while (serialPort1.BytesToRead == 0) ;
                    richTextBox2.Text = serialPort1.ReadLine();
                }
            }
            else
            {
                serialPort1.Close();
                button1.BackColor = Color.LightGray;
                button1.Text = "Connect";

            }
            
            
           

        }

       

        

       

        private void button15_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            else
            {
                timer1.Interval = Convert.ToInt32(comboBoxTime.SelectedItem) * 1000;
                timer1.Start();
            }
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            button13.PerformClick();
        }

        private void comboBoxTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(comboBoxTime.SelectedItem) * 1000;
        }

       

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void comboBoxAverage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAverage.Text == "None")
            {
                H = 0.00f;
                L = 0.00f;
            }
            if (comboBoxAverage.Text == "High")  
            {
                H = 0.01f;
                L = 0.99f;
            }
            if (comboBoxAverage.Text == "Medium")
            {
                H = 0.10f;
                L = 0.90f;
            }
            if (comboBoxAverage.Text == "Low")
            {
                H = 0.50f;
                L = 0.50f;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
