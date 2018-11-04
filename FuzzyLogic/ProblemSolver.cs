using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using AForge;
using AForge.Fuzzy;
using AForge.Controls;
using System.Windows.Forms.DataVisualization.Charting;



namespace AForge.Fuzzy
{
    class ProblemSolver
    {
        public List<Rule> rules;
        public Database database;

        public LinguisticVariable lvTemperature;
        public LinguisticVariable Power;
        public LinguisticVariable Risk;

        public System.Windows.Forms.DataVisualization.Charting.Chart chart4;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart5;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart6;



        public ProblemSolver(System.Windows.Forms.DataVisualization.Charting.Chart chart4, System.Windows.Forms.DataVisualization.Charting.Chart chart5, System.Windows.Forms.DataVisualization.Charting.Chart chart6)
        {
            this.database = new Database();
            this.chart4 = chart4;
            this.chart5 = chart5;
            this.chart6 = chart6;

            SetInput1();
            SetInput2();
            SetOutput();
            SetRules();

        }

        public void SetInput1()
        {
            this.lvTemperature = new LinguisticVariable("Wiek", 20, 60);

            TrapezoidalFunction function1 = new TrapezoidalFunction(30, 40, TrapezoidalFunction.EdgeType.Right);
            FuzzySet set1 = new FuzzySet("młody", function1);
            TrapezoidalFunction function2 = new TrapezoidalFunction(30, 40, 50);
            FuzzySet set2 = new FuzzySet("średni", function2);
            TrapezoidalFunction function3 = new TrapezoidalFunction(40, 50, TrapezoidalFunction.EdgeType.Left);
            FuzzySet set3 = new FuzzySet("stary", function3);

            lvTemperature.AddLabel(set1);
            lvTemperature.AddLabel(set2);
            lvTemperature.AddLabel(set3);

            database.AddVariable(lvTemperature);

            int j = 0;
            double y1;
            double y2;
            double y3;
            for (float x = 20; x < 60; x += 0.25f, j++)
            {
                if (lvTemperature.GetLabelMembership("młody", x + 0.25f) + lvTemperature.GetLabelMembership("młody", x) > 0)
                {
                    y1 = lvTemperature.GetLabelMembership("młody", x);
                    chart4.Series["Młody"].Points.AddXY(x, y1);
                }
                if (lvTemperature.GetLabelMembership("średni", x + 0.25f) + lvTemperature.GetLabelMembership("średni", x) > 0)
                {
                    y2 = lvTemperature.GetLabelMembership("średni", x);
                    chart4.Series["Średni"].Points.AddXY(x, y2);
                }
                if (lvTemperature.GetLabelMembership("stary", x + 0.25f) + lvTemperature.GetLabelMembership("stary", x) > 0)
                {
                    y3 = lvTemperature.GetLabelMembership("stary", x);
                    chart4.Series["Stary"].Points.AddXY(x, y3);
                }      
            }
        }

        public void SetInput2()
        {
            this.Power = new LinguisticVariable("Moc_samochodu", 20, 220);

            TrapezoidalFunction function1 = new TrapezoidalFunction(70, 120, TrapezoidalFunction.EdgeType.Right);
            FuzzySet set1 = new FuzzySet("mała", function1);
            TrapezoidalFunction function2 = new TrapezoidalFunction(70, 120, 170);
            FuzzySet set2 = new FuzzySet("średnia", function2);
            TrapezoidalFunction function3 = new TrapezoidalFunction(120, 170, TrapezoidalFunction.EdgeType.Left);
            FuzzySet set3 = new FuzzySet("duża", function3);


            Power.AddLabel(set1);
            Power.AddLabel(set2);
            Power.AddLabel(set3);

            database.AddVariable(Power);

            double y1;
            double y2;
            double y3;
            int j = 0;
            for (float x = 20; x < 220; x += 0.7f, j++)
            {
                if (Power.GetLabelMembership("mała", x + 0.7f) + Power.GetLabelMembership("mała", x) > 0)
                {
                    y1 = Power.GetLabelMembership("mała", x);
                    chart5.Series["Niska"].Points.AddXY(x, y1);
                }

                if (Power.GetLabelMembership("średnia", x + 0.7f) + Power.GetLabelMembership("średnia", x) > 0)
                {
                    y2 = Power.GetLabelMembership("średnia", x);
                    chart5.Series["Średnia"].Points.AddXY(x, y2);
                }
                if (Power.GetLabelMembership("duża", x + 0.7f) + Power.GetLabelMembership("duża", x) > 0)
                {
                    y3 = Power.GetLabelMembership("duża", x);
                    chart5.Series["Duża"].Points.AddXY(x, y3);
                }
            }
        }

        public void SetOutput()
        {
            this.Risk = new LinguisticVariable("Ryzyko", 0, 30);

            TrapezoidalFunction function1 = new TrapezoidalFunction(5, 10, TrapezoidalFunction.EdgeType.Right);
            FuzzySet set1 = new FuzzySet("niskie", function1);
            TrapezoidalFunction function2 = new TrapezoidalFunction(5, 10, 15);
            FuzzySet set2 = new FuzzySet("średnio_niskie", function2);
            TrapezoidalFunction function3 = new TrapezoidalFunction(10, 15, 20);
            FuzzySet set3 = new FuzzySet("średnie", function3);
            TrapezoidalFunction function4 = new TrapezoidalFunction(15, 20, 25);
            FuzzySet set4 = new FuzzySet("średnio_wysokie", function4);
            TrapezoidalFunction function5 = new TrapezoidalFunction(20, 25, TrapezoidalFunction.EdgeType.Left);
            FuzzySet set5 = new FuzzySet("wysokie", function5);


            Risk.AddLabel(set1);
            Risk.AddLabel(set2);
            Risk.AddLabel(set3);
            Risk.AddLabel(set4);
            Risk.AddLabel(set5);

            database.AddVariable(Risk);

            int j = 0;
            double y1;
            double y2;
            double y3;
            double y4;
            double y5;
            for (float x = 0; x < 30 ; x += 0.05f, j++)
            {
                if (Risk.GetLabelMembership("niskie", x + 0.05f) + Risk.GetLabelMembership("niskie", x) > 0)
                {
                    y1 = Risk.GetLabelMembership("niskie", x);
                    chart6.Series["Niskie"].Points.AddXY(x, y1);
                }
                if (Risk.GetLabelMembership("średnio_niskie", x + 0.05f) + Risk.GetLabelMembership("średnio_niskie", x) > 0)
                {
                    y2 = Risk.GetLabelMembership("średnio_niskie", x);
                    chart6.Series["Śr_nisk"].Points.AddXY(x, y2);
                }

                if (Risk.GetLabelMembership("średnie", x + 0.05f) + Risk.GetLabelMembership("średnie", x) > 0)
                {
                    y3 = Risk.GetLabelMembership("średnie", x);
                    chart6.Series["Średnie"].Points.AddXY(x, y3);
                }

                if (Risk.GetLabelMembership("średnio_wysokie", x + 0.05f) + Risk.GetLabelMembership("średnio_wysokie", x) > 0)
                {
                    y4 = Risk.GetLabelMembership("średnio_wysokie", x);
                    chart6.Series["Śr_wys"].Points.AddXY(x, y4);
                }

                if (Risk.GetLabelMembership("wysokie", x + 0.05f) + Risk.GetLabelMembership("wysokie", x) > 0)
                {
                    y5 = Risk.GetLabelMembership("wysokie", x);
                    chart6.Series["Wysokie"].Points.AddXY(x, y5);
                }
            }
        }

        private void SetRules()
        {
            this.rules = new List<Rule>();
            Rule rule1 = new Rule(database, "Norm", "IF Wiek is młody Moc_samochodu is duża then Ryzyko is wysokie");
            Rule rule2 = new Rule(database, "Norm", "IF Wiek is młody Moc_samochodu is średnia then Ryzyko is średnio_wysokie");
            Rule rule3 = new Rule(database, "Norm", "IF Wiek is średni Moc_samochodu is duża then Ryzyko is średnio_wysokie");
            Rule rule4 = new Rule(database, "Norm", "IF Wiek is średni Moc_samochodu is średnia then Ryzyko is średnie");

            rules.Add(rule1);
            rules.Add(rule2);
            rules.Add(rule3);
            rules.Add(rule4);
        }

        public float[] Solve(double _speed, double _temp)
        {
            lvTemperature.NumericInput = (float)_temp;
            Power.NumericInput = (float)_speed;


            float[] result = new float[4];

            for (int i = 0; i < rules.Count; i++)
            {
                result[i] = rules[i].EvaluateFiringStrength();
            }

            markPoints(_speed, _temp);
            OutputArea(result);
            return result;
        }

        public void markPoints(double _speed, double _temp)
        {

            int index;

            foreach (Series s in chart4.Series)
            {
                foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint p in s.Points)
                {
                    if (p.XValue.Equals(_temp))
                    {
                        chart4.Invoke(new Action(() =>
                        {
                            index = s.Points.IndexOf(p);
                            s.Points[index].Color = System.Drawing.Color.PaleGreen;
                        }));
                    }
                }
            }

            foreach (Series s in chart5.Series)
            {
                foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint p in s.Points)
                {
                    if (p.XValue.Equals(_speed))
                    {
                        chart5.Invoke(new Action(() =>
                        {
                            index = s.Points.IndexOf(p);
                            s.Points[index].Color = System.Drawing.Color.Green; 
                        }));
                    }
                }
            }


        }
        public void OutputArea(float[] result)
        {

            List<List<DataPoint>> curve = new List<List<DataPoint>>();
            int j = 0;
            int series_number = -1;
            int buff = 0;
            foreach (float r in result)
            {
                switch (j)
                {
                    case 0:
                        series_number = 4;
                        break;
                    case 1:
                        series_number = 3;
                        break;
                    case 2:
                        series_number = 3;
                        break;
                    case 3:
                        series_number = 2;
                        break;
                }

                curve.Add(new List<DataPoint>());

                foreach (DataPoint d in chart6.Series[series_number].Points)
                {
                    curve[buff].Add(d);
                }
                List<DataPoint> list = curve[buff].ToList();
                buff++;


                int count = list.Count - 2;
                float y = r;

                for (int i = 0; i < count; i++)  // Calculate intersection point between the straight line and a line between (x0,y0) and (x1,y1) 
                {
                    double x0 = list[i + 0].XValue;
                    double y0 = list[i + 0].YValues[0];
                    double x1 = list[i + 1].XValue;
                    double y1 = list[i + 1].YValues[0];


                    if ((y0 > y && y1 < y) || (y0 < y && y1 > y))
                    {
                        double x = (y - y0) * (x1 - x0) / (y1 - y0) + x0;

                        list.Add(new DataPoint(x, y));
                    }
                }
                list.Sort((a, b) => System.Math.Sign(a.XValue - b.XValue));

                chart6.Series[series_number].Points.Clear();
                chart6.Series[series_number].ChartType = SeriesChartType.Range;
                //chart6.Series[series_number].Color = Color.Red;
                chart6.Series[series_number].BorderColor = Color.Cyan;
                chart6.ChartAreas[0].AxisX.Minimum = 0;
                chart6.ChartAreas[0].AxisX.Interval = 1;

                for (int i = 0; i < list.Count; i++)
                {
                    double xx = list[i].XValue;
                    double yy = list[i].YValues[0];
                    if (yy < y)
                    {
                        chart6.Series[series_number].Points.AddXY(xx, y, yy);
                    }
                    else
                    {
                        chart6.Series[series_number].Points.AddXY(xx, yy, yy);
                    }
                }

               //chart6.ChartAreas[0].AxisY.StripLines.Add(new StripLine { IntervalOffset = y, Interval = 0, BorderColor = Color.Orange, BorderWidth = 2 });

                list.Clear();
                j++;
            }

        }
    }
}

