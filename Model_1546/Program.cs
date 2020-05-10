﻿using SharpKml.Dom;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Threading;

namespace Model_1546
{
    public partial class Program : Form
    {
        private System.ComponentModel.BackgroundWorker bgw;
        private ProgressBar progressBar;
        private IContainer components;
        private Label label1;
        private TextBox textBox1;
        private Button button2;
        private OpenFileDialog openFileDialog1;
        private Label label2;
        private TextBox textBox2;
        private Button button3;
        private Label label3;
        private TextBox textBox3;
        private Button button4;
        private Label label4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Panel panel1;
        private Button button1;

        public Program()
        {
            InitializeComponent();
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
        }

        private void Program_Load(object sender, EventArgs e)
        {

        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Program());
            
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            Calculation();
            //bgw.ReportProgress(percentage);
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar.MarqueeAnimationSpeed = 0;
            progressBar.Style = ProgressBarStyle.Blocks;
            progressBar.Value = progressBar.Maximum;
            progressBar.Visible = false;
            MessageBox.Show("Calculation Finished", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            if(textBox1.Text.Length == 0)
            {
                string message = "You did not select a database";
                string caption = "Error detected in Input";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    openFileDialog1.InitialDirectory = @"B:\";
                    openFileDialog1.RestoreDirectory = true;
                    openFileDialog1.Filter = "db3 files (*.db3)|*.db3|All files (*.*)|*.*";
                    openFileDialog1.FilterIndex = 2;
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        textBox1.Text = openFileDialog1.FileName;
                        textBox1.AutoSize = true;
                    }
                }
            }
           
            if (textBox2.Text.Length == 0)
            {
                string message = "You did not select Statons file";
                string caption = "Error detected in Input";
                var result2 = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result2 == System.Windows.Forms.DialogResult.OK)
                {
                    openFileDialog1.InitialDirectory = @"B:\";
                    openFileDialog1.RestoreDirectory = true;
                    openFileDialog1.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                    openFileDialog1.FilterIndex = 2;
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        textBox2.Text = openFileDialog1.FileName;
                        textBox2.AutoSize = true;
                    }
                }
           }
          
            if (textBox3.Text.Length == 0)
            {
                string message = "You did not select Antenna file";
                string caption = "Error detected in Input";
                var result3 = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result3 == System.Windows.Forms.DialogResult.OK)
                {
                    openFileDialog1.InitialDirectory = @"B:\";
                    openFileDialog1.RestoreDirectory = true;
                    openFileDialog1.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                    openFileDialog1.FilterIndex = 2;
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        textBox3.Text = openFileDialog1.FileName;
                        textBox3.AutoSize = true;
                    }
                }
            }

            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.Value = 0;
            bgw.RunWorkerAsync();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"B:\";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Filter = "db3 files (*.db3)|*.db3|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                textBox1.AutoSize = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"B:\";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
                textBox2.AutoSize = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"B:\";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = openFileDialog1.FileName;
                textBox3.AutoSize = true;
            }
        }

        public int Calculation()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            double Distance, Heff, TCA, HorAten, VertAten, Gain, E, Powgain, Pow;
            double SumPow = 0;
            double aux = 0;

            var Doc = new Document();
            var doc = new Document();


            double[] LatRx = Station_Add.GetLatitude();
            double[] LongRx = Station_Add.GetLongitude();
            List<double> LatTx = BRIFIC_Database.GetLatTx();
            List<double> LongTx = BRIFIC_Database.GetLonTx();
            List<double> PowerTx = BRIFIC_Database.GetPowerTx();

            List<double> Power = new List<double>();
            IEnumerable<double> Top;

            double[] HeightRx = Station_Add.GetHeight();
            List<double> HeightTx = BRIFIC_Database.GetHeightTx();

            string[] NameRx = Station_Add.GetName();
            List<string> NameTx = BRIFIC_Database.GetNameTx();

            int[] Azimuth = Station_Add.GetAzimuth();
            int[] Tilt = Station_Add.GetTilt();

            Output.WriteHeaders();

            for (int i = 0; i < 90; i++)
            {
                var style = new Style();
                for (int j = 0; j < 43; j++)
                {
                    var nCoord = new GeoCoordinate(LatRx[i], LongRx[i]);
                    var eCoord = new GeoCoordinate(LatTx[j], LongTx[j]);

                    Distance = Math.Round(nCoord.GetDistanceTo(eCoord) / 1000, 1);
                    Heff = Calculate_Field.Heff(LatTx[j], LongTx[j], 15, HeightTx[j]);
                    TCA = Calculate_Field.TCA(LatTx[j], LongTx[j], LatRx[i], LongRx[i]);
                    HorAten = Antenna.GetHorAten(Azimuth[i], Tilt[i]);
                    VertAten = Antenna.GetVertAten(LatTx[j], LongTx[j], LatRx[i], LongRx[i], HeightRx[i], Tilt[i]);
                    Gain = Antenna.GetGain(Tilt[i]) - HorAten - VertAten;

                    E = Calculate_Field.CalculateField(Distance, 10, 700, Heff, "a", 0, false, TCA, PowerTx[j], 40);
                    Powgain = E - 20 * Math.Log10(0.7) - 137.2 + Math.Round(Gain, 2);
                    Power.Add(Powgain);
                    Pow = E - 20 * Math.Log10(0.7) - 137.2;
                    Output.WriteCSV(NameTx[j], NameRx[i], LatTx[i], LongTx[i], Distance, E, Gain, HorAten, VertAten, Powgain, Pow);
                }

                Top = Power.OrderByDescending(x => x).Take(10);
                foreach (var item in Top)
                {
                    aux += Math.Pow(10, item / 10);

                }
                SumPow = 10 * Math.Log10(aux);
                Power.Clear();
                Output.GetStyle(SumPow, style);
                Output.WriteDoc(Doc, NameRx[i], LatRx[i], LongRx[i], style);
                style = null;
            }
            Output.WriteKML(Doc);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            int time = Convert.ToInt32(elapsedMs);
            return time;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Program));
            this.button1 = new System.Windows.Forms.Button();
            this.bgw = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(291, 398);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar
            // 
            this.progressBar.Enabled = false;
            this.progressBar.Location = new System.Drawing.Point(138, 332);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(501, 23);
            this.progressBar.Step = 10000;
            this.progressBar.TabIndex = 1;
            this.progressBar.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select Database:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 36);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(172, 20);
            this.textBox1.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(193, 35);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(25, 20);
            this.button2.TabIndex = 4;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select Stations:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(256, 38);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(189, 20);
            this.textBox2.TabIndex = 6;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(451, 37);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(25, 20);
            this.button3.TabIndex = 7;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(499, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Select Antenna:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(502, 37);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(189, 20);
            this.textBox3.TabIndex = 9;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(697, 36);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(25, 20);
            this.button4.TabIndex = 10;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Select Propagation Model:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(3, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(91, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "ITU-R P.1546";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(3, 26);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(91, 17);
            this.radioButton2.TabIndex = 13;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "ITU-R P.1812";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(3, 49);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(85, 17);
            this.radioButton3.TabIndex = 13;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "ITU-R P.525";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Location = new System.Drawing.Point(15, 97);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 14;
            // 
            // Program
            // 
            this.ClientSize = new System.Drawing.Size(736, 472);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Program";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interference Calculator";
            this.Load += new System.EventHandler(this.Program_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        } 
    }
}


