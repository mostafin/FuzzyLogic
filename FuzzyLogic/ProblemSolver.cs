﻿using System;
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
        public LinguisticVariable Temp_opon;
        public LinguisticVariable Power;
        public LinguisticVariable Risk;

        public INorm norm;
        
        public System.Windows.Forms.DataVisualization.Charting.Chart chart4;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart5;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart6;

        public Random rnd = new Random(DateTime.Now.Millisecond);

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
            this.Temp_opon = new LinguisticVariable("Temperatura", 0, 180);

            TrapezoidalFunction function1 = new TrapezoidalFunction(30, 90, TrapezoidalFunction.EdgeType.Right);
            FuzzySet set1 = new FuzzySet("zimne", function1);
            TrapezoidalFunction function2 = new TrapezoidalFunction(70, 90, 110);
            FuzzySet set2 = new FuzzySet("średnie", function2);
            TrapezoidalFunction function3 = new TrapezoidalFunction(90, 150, TrapezoidalFunction.EdgeType.Left);
            FuzzySet set3 = new FuzzySet("gorące", function3);

            Temp_opon.AddLabel(set1);
            Temp_opon.AddLabel(set2);
            Temp_opon.AddLabel(set3);

            database.AddVariable(Temp_opon);

            double y1;
            double y2;
            double y3;
            for (float x = 0; x < 180; x += 0.5f)
            {
                if (Temp_opon.GetLabelMembership("zimne", x + 0.5f) + Temp_opon.GetLabelMembership("zimne", x) > 0)
                {
                    y1 = Temp_opon.GetLabelMembership("zimne", x);
                    chart4.Series["Zimne"].Points.AddXY(x, y1);
                }
                if (Temp_opon.GetLabelMembership("średnie", x + 0.5f) + Temp_opon.GetLabelMembership("średnie", x) > 0)
                {
                    y2 = Temp_opon.GetLabelMembership("średnie", x);
                    chart4.Series["Średnie"].Points.AddXY(x, y2);
                }
                if (Temp_opon.GetLabelMembership("gorące", x + 0.5f) + Temp_opon.GetLabelMembership("gorące", x) > 0)
                {
                    y3 = Temp_opon.GetLabelMembership("gorące", x);
                    chart4.Series["Gorące"].Points.AddXY(x, y3);
                }      
            }
        }

        public void SetInput2()
        {
            this.Power = new LinguisticVariable("Moc_samochodu", 20, 180);

            TrapezoidalFunction function1 = new TrapezoidalFunction(40, 100, TrapezoidalFunction.EdgeType.Right);
            FuzzySet set1 = new FuzzySet("mała", function1);
            TrapezoidalFunction function2 = new TrapezoidalFunction(70, 100, 130);
            FuzzySet set2 = new FuzzySet("średnia", function2);
            TrapezoidalFunction function3 = new TrapezoidalFunction(100, 160, TrapezoidalFunction.EdgeType.Left);
            FuzzySet set3 = new FuzzySet("duża", function3);


            Power.AddLabel(set1);
            Power.AddLabel(set2);
            Power.AddLabel(set3);

            database.AddVariable(Power);

            double y1;
            double y2;
            double y3;
            for (float x = 20; x < 180; x += 0.5f)
            {
                if (Power.GetLabelMembership("mała", x + 0.5f) + Power.GetLabelMembership("mała", x) > 0)
                {
                    y1 = Power.GetLabelMembership("mała", x);
                    chart5.Series["Mała"].Points.AddXY(x, y1);
                }

                if (Power.GetLabelMembership("średnia", x + 0.5f) + Power.GetLabelMembership("średnia", x) > 0)
                {
                    y2 = Power.GetLabelMembership("średnia", x);
                    chart5.Series["Średnia"].Points.AddXY(x, y2);
                }
                if (Power.GetLabelMembership("duża", x + 0.5f) + Power.GetLabelMembership("duża", x) > 0)
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

            double y1;
            double y2;
            double y3;
            double y4;
            double y5;
            for (float x = 0; x < 30 ; x += 0.05f)
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
            MinimumNorm minimum = new MinimumNorm();
           // minimum.Evaluate(rule1);
            this.rules = new List<Rule>();
            Rule rule1 = new Rule(database, "Test1", "IF Temperatura is zimne and Moc_samochodu is duża then Ryzyko is wysokie");
            Rule rule2 = new Rule(database, "Test2", "IF Temperatura is zimne and Moc_samochodu is średnia then Ryzyko is średnio_wysokie");
            Rule rule3 = new Rule(database, "Test3", "IF Temperatura is średnie and Moc_samochodu is duża then Ryzyko is średnio_wysokie");
            Rule rule4 = new Rule(database, "Test4", "IF Temperatura is średnie and Moc_samochodu is średnia then Ryzyko is niskie");

            Rule rule5 = new Rule(database, "Test5", "IF Temperatura is zimne and Moc_samochodu is mała then Ryzyko is średnio_niskie");
            Rule rule6 = new Rule(database, "Test6", "IF Temperatura is średnie and Moc_samochodu is mała then Ryzyko is niskie");
            Rule rule7 = new Rule(database, "Test7", "IF Temperatura is gorące and Moc_samochodu is mała then Ryzyko is średnio_niskie");

            Rule rule8 = new Rule(database, "Test8", "IF Temperatura is gorące and Moc_samochodu is średnia then Ryzyko is średnie");
            Rule rule9 = new Rule(database, "Test9", "IF Temperatura is gorące and Moc_samochodu is średnia then Ryzyko is wysokie");

            rules.Add(rule1);
            rules.Add(rule2);
            rules.Add(rule3);
            rules.Add(rule4);
        }

        public float[] Solve(double _speed, double _temp)
        {
            Temp_opon.NumericInput = (float)_temp;
            Power.NumericInput = (float)_speed;
  

            float[] result = new float[9];

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
                        index = s.Points.IndexOf(p);
                        s.Points[index].Color = System.Drawing.Color.PaleGreen;
                    }
                }
            }

            foreach (Series s in chart5.Series)
            {
                foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint p in s.Points)
                {
                    if (p.XValue.Equals(_speed))
                    {
                        index = s.Points.IndexOf(p);
                        s.Points[index].Color = System.Drawing.Color.PaleGreen;
                    }
                }
            }


        }

        public void OutputArea(float[] result)
        {
            int j = 0;
            int series_number = -1;
            List<Tuple<int,double,int>> list = new List<Tuple<int,double,int>>();
            List<Series> series = new List<Series>();
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
                        series_number = 0;
                        break;
                    case 4:
                        series_number = 1;
                        break;
                    case 5:
                        series_number = 0;
                        break;
                    case 6:
                        series_number = 1;
                        break;
                    case 7:
                        series_number = 2;
                        break;
                    case 8:
                        series_number = 4;
                        break;

                }

                list.Add(new Tuple<int, double,int>(series_number, r, j + 5));

                Series newSeries = new Series();
                series.Add(newSeries);

                foreach (DataPoint d in chart6.Series[series_number].Points)
                {

                    if (d.YValues[0] <= r)
                    {
                        newSeries.Points.AddXY(d.XValue, d.YValues[0]);
                    }
                    else
                    {
                        if (series_number == 0 || series_number == 4)
                        {
                            newSeries.Points.AddXY(d.XValue, r);
                        }
                    }
                }

                chart6.Series.Add(newSeries);
                chart6.Series[5 + j].ChartType = SeriesChartType.Range;
                chart6.Series[5 + j].IsVisibleInLegend = false;
                //chart6.Series[5 + j].Color = RandomColor();
                chart6.Series[5 + j].Color = Color.Crimson;

                j++;
            }

            foreach (Tuple<int,double,int> i in list)
            {
                foreach(Tuple<int, double,int> l in list)
                {
                    if (i.Item1 == l.Item1 && i.Item2 > l.Item2) ////  > - Min , < - Max
                    {
                        chart6.Series.RemoveAt(i.Item3);
                    }
                }
            }
        }
        public Color RandomColor()
        {
            return Color.FromArgb(rnd.Next(1,150), rnd.Next(1,150), rnd.Next(1,150));
        }
    }
}

