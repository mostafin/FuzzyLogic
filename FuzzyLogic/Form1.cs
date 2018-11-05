using AForge;
using AForge.Controls;
using AForge.Fuzzy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FuzzyLogic
{
    public partial class Form1 : Form
    {

        ProblemSolver problemSolver;
        float[] results;
     
        public Form1()
        {
            InitializeComponent();
            SetInitialCharts();
            problemSolver = new ProblemSolver(chart4, chart5, chart6);      
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                ClearCharts();
                problemSolver = new ProblemSolver(chart4, chart5, chart6);
            }

            try
            {    
                results = problemSolver.Solve(Convert.ToDouble(textBoxSpeed.Text), Convert.ToDouble(textBoxTemp.Text));

                textBox3.Text = "Wysokie na poziomie " + results[0].ToString();
                textBox4.Text = "Średnio wysokie na poziomie " + results[1].ToString();
                textBox5.Text = "Średnio wysokie na poziomie " + results[2].ToString();
                textBox6.Text = "Niskie na poziomie " + results[3].ToString();
                textBox1.Text = "Średnio niskie na poziomie " + results[4].ToString();
                textBox2.Text = "Niskie na poziomie " + results[5].ToString();
                textBox7.Text = "Średnio niskie na poziomie " + results[6].ToString();
                textBox8.Text = "Średnie na poziomie " + results[7].ToString();
                textBox9.Text = "Wysokie na poziomie " + results[8].ToString();
            }
            catch(Exception)
            {
                MessageBox.Show("Nie podano parametrów");
            }
        }


        public void SetInitialCharts()
        {
             this.SuspendLayout();
            // Series
            {
                chart4.Series.Add("Zimne");
                chart4.Series.Add("Średnie");
                chart4.Series.Add("Gorące");
                chart5.Series.Add("Mała");
                chart5.Series.Add("Średnia");
                chart5.Series.Add("Duża");
                chart6.Series.Add("Niskie");
                chart6.Series.Add("Śr_nisk");
                chart6.Series.Add("Średnie");
                chart6.Series.Add("Śr_wys");
                chart6.Series.Add("Wysokie");
            }

            // Areas
            {
                chart4.ChartAreas.Add("Temperatura");
                chart5.ChartAreas.Add("Predkość");
                chart6.ChartAreas.Add("Ryzyko");
            }

            // Axis
            {
                chart4.ChartAreas["Temperatura"].AxisX.Minimum = 0;
                chart4.ChartAreas["Temperatura"].AxisY.Maximum = 1;
                chart4.ChartAreas["Temperatura"].AxisX.Title = "°C";
                chart4.ChartAreas["Temperatura"].AxisY.Title = "μ";
                chart4.ChartAreas["Temperatura"].AxisY.TitleFont = new Font("Microsoft Sans Serif",10);
                chart4.ChartAreas["Temperatura"].AxisX.TitleFont = new Font("Microsoft Sans Serif", 10);
                chart4.ChartAreas["Temperatura"].AxisX.Interval = 20;

                chart5.ChartAreas["Predkość"].AxisX.Minimum = 20;
                chart5.ChartAreas["Predkość"].AxisY.Maximum = 1;
                chart5.ChartAreas["Predkość"].AxisX.Title = "km/h";
                chart5.ChartAreas["Predkość"].AxisY.Title = "μ";
                chart5.ChartAreas["Predkość"].AxisX.TitleFont = new Font("Microsoft Sans Serif", 10);
                chart5.ChartAreas["Predkość"].AxisY.TitleFont = new Font("Microsoft Sans Serif", 10);
                chart5.ChartAreas["Predkość"].AxisX.Interval = 20;

                chart6.ChartAreas["Ryzyko"].AxisX.Minimum = 0;
                chart6.ChartAreas["Ryzyko"].AxisY.Maximum = 1;
                chart6.ChartAreas["Ryzyko"].AxisX.Title = "x";
                chart6.ChartAreas["Ryzyko"].AxisY.Title = "μ";
                chart6.ChartAreas["Ryzyko"].AxisX.TitleFont = new Font("Microsoft Sans Serif", 10);
                chart6.ChartAreas["Ryzyko"].AxisY.TitleFont = new Font("Microsoft Sans Serif", 10);
                chart6.ChartAreas["Ryzyko"].AxisX.Interval = 2;
            }

            foreach (Series s in chart4.Series)
            {
                s.ChartArea = "Temperatura";
                s.BorderWidth = 4;
                s.ChartType = SeriesChartType.Line;
            }

            foreach (Series s in chart5.Series)
            {
                s.ChartArea = "Predkość";
                s.BorderWidth = 4;
                s.ChartType = SeriesChartType.Line;
            }

            foreach (Series s in chart6.Series)
            {
                s.ChartArea = "Ryzyko";
                s.BorderWidth = 4;
                s.ChartType = SeriesChartType.Line;
            }


            this.ResumeLayout(false);
        }
        public void ClearCharts()
        {
            chart4.Series.Clear();
            chart5.Series.Clear();
            chart6.Series.Clear();

            chart4.ChartAreas.RemoveAt(0);
            chart5.ChartAreas.RemoveAt(0);
            chart6.ChartAreas.RemoveAt(0);
          


            SetInitialCharts();
        }
    }
}
