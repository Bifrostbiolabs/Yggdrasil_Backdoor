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
        string Fuld_string;
        string[] stringpart_a = new string[100];




        float[] stringpart_c = new float[100];


        public float H = 0.50F;
        public float L = 0.50F;


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
            string[] number1_8 = { "1", "2", "3", "4", "5", "6", "7", "8" };
            comboBoxPeriod.DataSource = number1_8; comboBoxPeriod.Text = "1";

            


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
                String[] sepearator_a = { "," };
                String[] sepearator_b = { ";" };
                Int32 count = 100;
                

               


                chart1.Series["ADC Peak"].Points.Clear();
                chart1.Series["ADC Value"].Points.Clear();
                chart3.Series["Peak"].Points.Clear();
                chart3.Series["Average"].Points.Clear();
                richTextBoxRx.Text = "";
                int circel_count = 60;
                int xy_count = 0;


                for (int j = 0; j<8;j++)
                {
                    serialPort1.WriteLine("#," + circel_count.ToString() + "," + nr.ToString() + ",1,0,"); richTextBoxTx.Text = ("#," + circel_count.ToString() + "," + nr.ToString() + ",1,0,");
                    while (serialPort1.BytesToRead == 0) ;
                    Thread.Sleep(10);
                    Fuld_string = Convert.ToString(serialPort1.ReadLine()); richTextBoxRx.AppendText(Fuld_string);
                    stringpart_a = Fuld_string.Split(sepearator_a, count, StringSplitOptions.RemoveEmptyEntries);

                    string[] stringpart_b = new string[20];
                    stringpart_b =  stringpart_a[4].Split(sepearator_b, count, StringSplitOptions.RemoveEmptyEntries);
                   
                        for (int i = 0; i < 10; i++)
                        {

                            try
                            {
                            
                               
                            
                            
                                chart1.Series["ADC Value"].Points.AddXY(xy_count, stringpart_c[xy_count]);
                            if (comboBoxPeak.SelectedItem == "Yes") { chart1.Series["ADC Peak"].Points.AddXY(xy_count, stringpart_b[i]); }



                                


                            float adc_calc = float.Parse(stringpart_b[i]);
                                float temp_calc = stringpart_c[xy_count];
                                float result = ((adc_calc * 0.10F) + (temp_calc * 0.90F));
                                stringpart_c[xy_count] = result;
                            //richTextBox1.AppendText( result.ToString());

                            if ((comboBoxPeriod.SelectedItem == "1") & ( j == 0))
                            {
                                chart3.Series["Average"].Points.AddXY(xy_count, stringpart_c[xy_count]);
                                if (comboBoxPeak.SelectedItem == "Yes") { chart3.Series["Peak"].Points.AddXY(xy_count, stringpart_b[i]); }
                            }
                            if ((comboBoxPeriod.SelectedItem == "2") & (j == 1))
                            {
                                chart3.Series["Average"].Points.AddXY(xy_count, stringpart_c[xy_count]);
                                if (comboBoxPeak.SelectedItem == "Yes") { chart3.Series["Peak"].Points.AddXY(xy_count, stringpart_b[i]); }
                            }
                            if ((comboBoxPeriod.SelectedItem == "3") & (j == 2))
                            {
                                chart3.Series["Average"].Points.AddXY(xy_count, stringpart_c[xy_count]);
                                if (comboBoxPeak.SelectedItem == "Yes") { chart3.Series["Peak"].Points.AddXY(xy_count, stringpart_b[i]); }
                            }
                            if ((comboBoxPeriod.SelectedItem == "4") & (j == 3))
                            {
                                chart3.Series["Average"].Points.AddXY(xy_count, stringpart_c[xy_count]);
                                if (comboBoxPeak.SelectedItem == "Yes") { chart3.Series["Peak"].Points.AddXY(xy_count, stringpart_b[i]); }
                            }
                            if ((comboBoxPeriod.SelectedItem == "5") & (j == 4))
                            {
                                chart3.Series["Average"].Points.AddXY(xy_count, stringpart_c[xy_count]);
                                if (comboBoxPeak.SelectedItem == "Yes") { chart3.Series["Peak"].Points.AddXY(xy_count, stringpart_b[i]); }
                            }
                            if ((comboBoxPeriod.SelectedItem == "6") & (j == 5))
                            {
                                chart3.Series["Average"].Points.AddXY(xy_count, stringpart_c[xy_count]);
                                if (comboBoxPeak.SelectedItem == "Yes") { chart3.Series["Peak"].Points.AddXY(xy_count, stringpart_b[i]); }
                            }
                            if ((comboBoxPeriod.SelectedItem == "7") & (j == 6))
                            {
                                chart3.Series["Average"].Points.AddXY(xy_count, stringpart_c[xy_count]);
                                if (comboBoxPeak.SelectedItem == "Yes") { chart3.Series["Peak"].Points.AddXY(xy_count, stringpart_b[i]); }
                            }
                            if ((comboBoxPeriod.SelectedItem == "8") & (j == 7))
                            {
                                chart3.Series["Average"].Points.AddXY(xy_count, stringpart_c[xy_count]);
                                if (comboBoxPeak.SelectedItem == "Yes") { chart3.Series["Peak"].Points.AddXY(xy_count, stringpart_b[i]); }
                            }



                        }
                            catch
                            {

                            }




                            xy_count++;
                        }
                    
                        

                    
                    circel_count++;
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
                    chart2.Series["Calibration data"].Points.AddXY(strlista[4], strlista[6]); textBox100.Text = strlista[6]; chart2.Series["Calibration datab"].Points.AddXY(strlista[4], strlista[6]);

                    div = Convert.ToInt32(strlista[4]) / 100 * 85;
                    divb = Convert.ToInt32(div);
                    chart2.Series["Calibration data"].Points.AddXY(divb, strlista[7]); textBox85.Text = strlista[7]; chart2.Series["Calibration datab"].Points.AddXY(divb, strlista[7]);

                    div = Convert.ToInt32(strlista[4]) / 100 * 70;
                    divb = Convert.ToInt32(div);
                    chart2.Series["Calibration data"].Points.AddXY(divb, strlista[8]); textBox70.Text = strlista[8]; chart2.Series["Calibration datab"].Points.AddXY(divb, strlista[8]);

                    div = Convert.ToInt32(strlista[4]) / 100 * 55;
                    divb = Convert.ToInt32(div);
                    chart2.Series["Calibration data"].Points.AddXY(divb, strlista[9]); textBox55.Text = strlista[9]; chart2.Series["Calibration datab"].Points.AddXY(divb, strlista[9]);

                    div = Convert.ToInt32(strlista[4]) / 100 * 40;
                    divb = Convert.ToInt32(div);
                    chart2.Series["Calibration data"].Points.AddXY(divb, strlista[10]); textBox40.Text = strlista[10]; chart2.Series["Calibration datab"].Points.AddXY(divb, strlista[10]);

                    //  chart2.Series["Calibration data"].Points.AddXY(0, strlista[11]); textBox0.Text = strlista[11];// no need to show 0 point
                    richTextBoxRx.Text = strlista[4];
                    textBox1.Text = strlista[4]; textBox7.Text = strlista[4];







                    div = Convert.ToInt32(strlista[4]) / 100 * 85;
                    divb = Convert.ToInt32(div);


                    //chart2.Series["Calibration datab"].Points.AddXY(strlista[4], strlista[6]); 
                    //div = Convert.ToInt32(strlista[4]) / 100 * 85;
                    //divb = Convert.ToInt32(div);
                    //chart2.Series["Calibration datab"].Points.AddXY(div, strlista[7]); 
                    //div = Convert.ToInt32(strlista[4]) / 100 * 70;
                    //divb = Convert.ToInt32(div);
                    //chart2.Series["Calibration datab"].Points.AddXY(div, strlista[8]); 
                    //div = Convert.ToInt32(strlista[4]) / 100 * 55;
                    //divb = Convert.ToInt32(div);
                    //chart2.Series["Calibration datab"].Points.AddXY(div, strlista[9]); 
                    //div = Convert.ToInt32(strlista[4]) / 100 * 40;
                    //divb = Convert.ToInt32(div);
                    //chart2.Series["Calibration datab"].Points.AddXY(div, strlista[10]); 
                    //  chart2.Series["Calibration data"].Points.AddXY(0, strlista[11]); textBox0.Text = strlista[11];
                    //richTextBoxRx.Text = strlista[4];
                   // textBox1.Text = strlista[4];








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
                    serialPort1.ReadExisting();
                    serialPort1.WriteLine("#,55,0,1,0,");
                    while (serialPort1.BytesToRead == 0) ;
                    serialPort1.ReadLine();
                    serialPort1.WriteLine("#,55,0,1,0,"); richTextBoxTx.Text = ("#,55,0,1,0,");
                    while (serialPort1.BytesToRead == 0) ;
                    richTextBoxRx.Text = serialPort1.ReadLine();
                    //TjekSensor.Start();
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
                H = 0.00F;
                L = 0.00F;
            }
            if (comboBoxAverage.Text == "High")  
            {
                H = 0.01F;
                L = 0.99F;
            }
            if (comboBoxAverage.Text == "Medium")
            {
                H = 0.10F;
                L = 0.90F;
            }
            if (comboBoxAverage.Text == "Low")
            {
                H = 0.50F;
                L = 0.50F;
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

        private void TjekSensor_Tick(object sender, EventArgs e)
        {
            
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                try
                {
                    serialPort1.WriteLine("#,52,1,1,");
                    while (serialPort1.BytesToRead == 0) ;
                    Thread.Sleep(10);

                    String Jacks = Convert.ToString(serialPort1.ReadLine());
                    richTextBoxRx.Text = Jacks;

                    String[] spearatorA = { "," };
                    String[] spearatorB = { ";" };

                    Int32 count = 100;


                    string[] portsA = Jacks.Split(spearatorA, count, StringSplitOptions.RemoveEmptyEntries);
                    string[] portsB = portsA[4].Split(spearatorB, count, StringSplitOptions.RemoveEmptyEntries);



                    if (portsB[0] == "Y") { checkBox1.Checked = true; } else { checkBox1.Checked = false; }
                    if (portsB[1] == "Y") { checkBox2.Checked = true; } else { checkBox2.Checked = false; }
                    if (portsB[2] == "Y") { checkBox3.Checked = true; } else { checkBox3.Checked = false; }
                    if (portsB[3] == "Y") { checkBox4.Checked = true; } else { checkBox4.Checked = false; }
                    if (portsB[4] == "Y") { checkBox5.Checked = true; } else { checkBox5.Checked = false; }
                    if (portsB[5] == "Y") { checkBox6.Checked = true; } else { checkBox6.Checked = false; }
                    if (portsB[6] == "Y") { checkBox7.Checked = true; } else { checkBox7.Checked = false; }
                    if (portsB[7] == "Y") { checkBox8.Checked = true; } else { checkBox8.Checked = false; }
                    if (portsB[8] == "Y") { checkBox9.Checked = true; } else { checkBox9.Checked = false; }
                    if (portsB[9] == "Y") { checkBox10.Checked = true; } else { checkBox10.Checked = false; }
                    if (portsB[10] == "Y") { checkBox11.Checked = true; } else { checkBox11.Checked = false; }
                    if (portsB[11] == "Y") { checkBox12.Checked = true; } else { checkBox12.Checked = false; }
                    if (portsB[12] == "Y") { checkBox13.Checked = true; } else { checkBox13.Checked = false; }
                    if (portsB[13] == "Y") { checkBox14.Checked = true; } else { checkBox14.Checked = false; }
                    if (portsB[14] == "Y") { checkBox15.Checked = true; } else { checkBox15.Checked = false; }
                    if (portsB[15] == "Y") { checkBox16.Checked = true; } else { checkBox16.Checked = false; }

                }
                catch
                {

                }

            }
        }
    }
}
