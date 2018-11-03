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
            for (float x = 20; x < 60; x += 0.25f, j++)
            {
                double y1 = lvTemperature.GetLabelMembership("młody", x);
                double y2 = lvTemperature.GetLabelMembership("średni", x);
                double y3 = lvTemperature.GetLabelMembership("stary", x);


                chart4.Series["Młody"].Points.AddXY(x, y1);
                chart4.Series["Średni"].Points.AddXY(x, y2);
                chart4.Series["Stary"].Points.AddXY(x, y3);
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

            int j = 0;
            for (float x = 20; x < 220; x += 0.7f, j++)
            {
                double y1 = Power.GetLabelMembership("mała", x);
                double y2 = Power.GetLabelMembership("średnia", x);
                double y3 = Power.GetLabelMembership("duża", x);

                chart5.Series["Niska"].Points.AddXY(x, y1);
                chart5.Series["Średnia"].Points.AddXY(x, y2);
                chart5.Series["Duża"].Points.AddXY(x, y3);
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
            for (float x = 0; x < 30; x += 0.05f, j++)
            {
                double y1 = Risk.GetLabelMembership("niskie", x);
                double y2 = Risk.GetLabelMembership("średnio_niskie", x);
                double y3 = Risk.GetLabelMembership("średnie", x);
                double y4 = Risk.GetLabelMembership("średnio_wysokie", x);
                double y5 = Risk.GetLabelMembership("wysokie", x);



                chart6.Series["Niskie"].Points.AddXY(x, y1);
                chart6.Series["Śr_nisk"].Points.AddXY(x, y2);
                chart6.Series["Średnie"].Points.AddXY(x, y3);
                chart6.Series["Śr_wys"].Points.AddXY(x, y4);
                chart6.Series["Wysokie"].Points.AddXY(x, y5);
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
                            s.Points[index].Color = System.Drawing.Color.DeepPink;
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
                            s.Points[index].Color = System.Drawing.Color.DeepPink;
                        }));
                    }
                }
            }
        }
        
    }
}

