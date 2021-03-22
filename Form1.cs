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
        int adcCount;



        public float H = 0.10f;
        public float L = 0.90f;


        public Form1()
        {
            InitializeComponent();

            

        }

       

        private void Form1_Load(object sender, EventArgs e)
        {

            //tildeler combobox port navne
            string[] ports = SerialPort.GetPortNames();
            comboBox_Port.DataSource = ports;

            //tildeler combobox sensor intervaler
            string[] number_Sensor = { "Yggdrasil","1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" };
            comboBoxSensor.DataSource = number_Sensor; comboBoxSensor.Text = "1";

            //tildeler combobox time delay intervaler
            string[] comboBox_Time = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            comboBoxTime_delay.DataSource = comboBox_Time; comboBoxTime_delay.Text = "1";

            //tildeler combobox Average intervaler
            string[] number_Average = { "None", "Low", "Medium", "High" };
            comboBoxAverage.DataSource = number_Average; comboBoxAverage.Text = "Medium";

            //tildeler combobox Peak intervaler
            string[] number_Peak = { "No", "Yes" };
            comboBoxPeak.DataSource = number_Peak; comboBoxPeak.Text = "Yes";

            //tildeler combobox Periode intervaler
            string[] number0_7 = { "0", "1", "2", "3", "4", "5", "6", "7" };
            comboBoxPeriod.DataSource = number0_7; comboBoxPeriod.Text = "0";

           
            

        }

        private void button12_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine(richTextBoxTx.Text);
            while (serialPort1.BytesToRead == 0) ;
            richTextBoxRx.Text = serialPort1.ReadLine();
        }

       

        public void button13_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                String nr = comboBoxSensor.SelectedItem.ToString();


                serialPort1.WriteLine("#,60," + nr.ToString() + ",1,0,") ;
                richTextBoxTx.Text = ("#,60," + nr.ToString() + ",1,0,");
                
                while (serialPort1.BytesToRead == 0);
                String str = Convert.ToString(serialPort1.ReadLine());
                richTextBoxRx.Text = str;
                String[] spearator = { ","};
                String[] spearatorb = { ";"};

                Int32 count = 400;

                
                string[] strlist = str.Split(spearator, count, StringSplitOptions.RemoveEmptyEntries);
                


                if (strlist.Length > 3)
                {
                    try
                    {
                        string[] mysplitdata = strlist[4].Split(spearatorb, count, StringSplitOptions.RemoveEmptyEntries);
                        richTextBoxRx.Text = strlist[4];
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
                try
                {
                    String nr = comboBoxSensor.SelectedItem.ToString();
                    serialPort1.WriteLine("#,25," + nr.ToString() + ",1,0,");
                    richTextBoxTx.Text = ("#,25," + nr.ToString() + ",1,0,");

                    while (serialPort1.BytesToRead == 0) ;
                    String str = Convert.ToString(serialPort1.ReadLine());
                    richTextBoxRx.Text = str;
                    String[] spearator = { ",", ";" };

                    Int32 count = 20;


                    string[] strlista = str.Split(spearator, count, StringSplitOptions.RemoveEmptyEntries);
                    chart2.Series["Calibration data"].Points.Clear(); chart2.Series["Calibration datab"].Points.Clear();

                    float div = Convert.ToInt32( strlista[4]);
                    int divb;
                     div = Convert.ToInt32(strlista[4]) / 100 * 85;
                    divb = Convert.ToInt32( div);


                    chart2.Series["Calibration data"].Points.AddXY(strlista[4], strlista[6]); textBox100.Text = strlista[6];
                    div = Convert.ToInt32(strlista[4]) / 100 * 85;
                    divb = Convert.ToInt32(div);
                    chart2.Series["Calibration data"].Points.AddXY(divb, strlista[7]); textBox85.Text = strlista[7];
                    div = Convert.ToInt32(strlista[4]) / 100 * 70;
                    divb = Convert.ToInt32(div);
                    chart2.Series["Calibration data"].Points.AddXY(divb, strlista[8]); textBox70.Text = strlista[8];
                    div = Convert.ToInt32(strlista[4]) / 100 * 55;
                    divb = Convert.ToInt32(div);
                    chart2.Series["Calibration data"].Points.AddXY(divb, strlista[9]); textBox55.Text = strlista[9];
                    div = Convert.ToInt32(strlista[4]) / 100 * 40;
                    divb = Convert.ToInt32(div);
                    chart2.Series["Calibration data"].Points.AddXY(divb, strlista[10]); textBox40.Text = strlista[10];
                  //  chart2.Series["Calibration data"].Points.AddXY(0, strlista[11]); textBox0.Text = strlista[11];
                    richTextBoxRx.Text = strlista[4];
                    textBox1.Text = strlista[4];




                    

                  
                    div = Convert.ToInt32(strlista[4]) / 100 * 85;
                    divb = Convert.ToInt32(div);


                    chart2.Series["Calibration datab"].Points.AddXY(strlista[4], strlista[6]); 
                    div = Convert.ToInt32(strlista[4]) / 100 * 85;
                    divb = Convert.ToInt32(div);
                    chart2.Series["Calibration datab"].Points.AddXY(div, strlista[7]); 
                    div = Convert.ToInt32(strlista[4]) / 100 * 70;
                    divb = Convert.ToInt32(div);
                    chart2.Series["Calibration datab"].Points.AddXY(div, strlista[8]); 
                    div = Convert.ToInt32(strlista[4]) / 100 * 55;
                    divb = Convert.ToInt32(div);
                    chart2.Series["Calibration datab"].Points.AddXY(div, strlista[9]); 
                    div = Convert.ToInt32(strlista[4]) / 100 * 40;
                    divb = Convert.ToInt32(div);
                    chart2.Series["Calibration datab"].Points.AddXY(div, strlista[10]); 
                    //  chart2.Series["Calibration data"].Points.AddXY(0, strlista[11]); textBox0.Text = strlista[11];
                    richTextBoxRx.Text = strlista[4];
                    textBox1.Text = strlista[4];








                }
                catch
                {

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (!serialPort1.IsOpen)
            {
                serialPort1.PortName = comboBox_Port.SelectedItem.ToString();
                serialPort1.Open();
                if (serialPort1.IsOpen)
                {
                    button1.BackColor = Color.Red;
                    button1.Text = "Connectet";
                    serialPort1.WriteLine("#,55,0,1,0,");
                    while (serialPort1.BytesToRead == 0) ;
                    serialPort1.ReadLine();
                    serialPort1.WriteLine("#,55,0,1,0,"); richTextBoxTx.Text = ("#,55,0,1,0,");
                    while (serialPort1.BytesToRead == 0) ;
                    richTextBoxRx.Text = serialPort1.ReadLine();
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
                timer1.Interval = Convert.ToInt32(comboBoxTime_delay.SelectedItem) * 1000;
                timer1.Start();
            }
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            button13.PerformClick();
        }

        private void comboBoxTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(comboBoxTime_delay .SelectedItem) * 1000;
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

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void timer_progres_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = progressBar1.Value+1;
            if (progressBar1.Value >= 255) progressBar1.Value = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            adcCount = 1;
            chart_ADC.Series["SeriesADC"].Points.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen==true)
            {
                if (timerADC.Enabled == false)
                {
                    adcCount = 1;
                    
                    button2.Text = "Stop timer";
                    
                    timerADC.Enabled = true;
                }
                else
                {
                    timerADC.Enabled = false;
                    button2.Text = "Start timer";
                }
            }
           
            
            
            
            
            

        }

        private void timerADC_Tick(object sender, EventArgs e)
        {
            int ge = Convert.ToInt32(comboBoxTime_delay.Text);
            timerADC.Interval = ge * 1000;
            serialPort1.WriteLine("#,13," + comboBoxSensor.SelectedItem + ",1,0,");
            richTextBoxTx.Text = ("#,13," + comboBoxSensor.SelectedItem + ",1,0,");
            while (serialPort1.BytesToRead == 0) ;
            String str = Convert.ToString(serialPort1.ReadLine());
            richTextBoxRx.Text = str;
            String[] spearator = { "," };
            String[] spearatorb = { ";" };

            Int32 count = 400;


            string[] strlist = str.Split(spearator, count, StringSplitOptions.RemoveEmptyEntries);



            chart_ADC.Series["SeriesADC"].Points.AddXY(adcCount, strlist[4]);
            textBox2.Text = strlist[4];
            //chart2.Series["Calibration data"].Points.AddXY(100, strlista[6]);
            adcCount++;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                string step = textBox1.Text;
                string stepLength = Convert.ToString( step.Length);

                serialPort1.Write("#,50," + comboBoxSensor.SelectedItem + "," + stepLength+ "," + step + ",");
                richTextBoxTx.Text = ("#,50," + comboBoxSensor.SelectedItem + "," + stepLength + "," + step + ",");

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }
    }
}
