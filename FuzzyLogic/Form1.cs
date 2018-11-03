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
            results = problemSolver.Solve(Convert.ToDouble(textBoxSpeed.Text), Convert.ToDouble(textBoxTemp.Text));

            textBox3.Text = "Wysokie na poziomie " + results[0].ToString();
            textBox4.Text = "Średnio wysokie na poziomie " + results[1].ToString();
            textBox5.Text = "Średnio wysokie na poziomie " + results[2].ToString();
            textBox6.Text = "Średnie na poziomie " + results[3].ToString();
        }


        public void SetInitialCharts()
        {
             this.SuspendLayout();
            // Series
            {
                chart4.Series.Add("Młody");
                chart4.Series.Add("Średni");
                chart4.Series.Add("Stary");
                chart5.Series.Add("Niska");
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
                chart4.ChartAreas.Add("Wiek");
                chart5.ChartAreas.Add("Predkość");
                chart6.ChartAreas.Add("Ryzyko");
            }

            // Axis
            {
                chart4.ChartAreas["Wiek"].AxisX.Minimum = 20;
                chart4.ChartAreas["Wiek"].AxisY.Maximum = 1;
                chart4.ChartAreas["Wiek"].AxisX.Title = "x";
                chart4.ChartAreas["Wiek"].AxisY.Title = "μ";

                chart5.ChartAreas["Predkość"].AxisX.Minimum = 20;
                chart5.ChartAreas["Predkość"].AxisY.Maximum = 1;
                chart5.ChartAreas["Predkość"].AxisX.Title = "x";
                chart5.ChartAreas["Predkość"].AxisY.Title = "μ";

                chart6.ChartAreas["Ryzyko"].AxisX.Minimum = 0;
                chart6.ChartAreas["Ryzyko"].AxisY.Maximum = 1;
                chart6.ChartAreas["Ryzyko"].AxisX.Title = "x";
                chart6.ChartAreas["Ryzyko"].AxisY.Title = "μ";
            }


            chart4.Series["Młody"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart4.Series["Młody"].ChartArea = "Wiek";

            chart4.Series["Średni"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart4.Series["Średni"].ChartArea = "Wiek";

            chart4.Series["Stary"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart4.Series["Stary"].ChartArea = "Wiek";


            chart5.Series["Niska"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart5.Series["Niska"].ChartArea = "Predkość";

            chart5.Series["Średnia"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart5.Series["Średnia"].ChartArea = "Predkość";

            chart5.Series["Duża"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart5.Series["Duża"].ChartArea = "Predkość";

            chart6.Series["Niskie"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart6.Series["Niskie"].ChartArea = "Ryzyko";

            chart6.Series["Śr_nisk"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart6.Series["Śr_nisk"].ChartArea = "Ryzyko";

            chart6.Series["Średnie"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart6.Series["Średnie"].ChartArea = "Ryzyko";

            chart6.Series["Śr_wys"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart6.Series["Śr_wys"].ChartArea = "Ryzyko";

            chart6.Series["Wysokie"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart6.Series["Wysokie"].ChartArea = "Ryzyko";

            this.ResumeLayout(false);
        }

    }
}
