using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSO_odev
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        
         
        {
            int particles = (int)numericUpDown1.Value; // Parçacık Sayısı
            int iterations = (int)numericUpDown2.Value; // Jenerasyon Sayısı
            double c1 = (double)numericUpDown3.Value;
            double c2 = (double)numericUpDown4.Value;
            double w = 0.5; // İstersen formdan ekleyebilirsin

            double[] lower = new double[] { -5.0, -5.0 };
            double[] upper = new double[] { 5.0, 5.0 };

            PSO pso = new PSO(particles, iterations, c1, c2, w, lower, upper);
            pso.Initialize();
            pso.Run();
            var bestPos = pso.GetBestPosition();
            var bestFit = pso.GetBestFitness();

            label5.Text = $"Amaç Fonksiyonu Değeri: {bestFit:F5}\nX: {bestPos[0]:F5}, Y: {bestPos[1]:F5}";

            chart1.Series.Clear();
            chart1.Series.Add("Yakınsama");
            chart1.Series["Yakınsama"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            chart1.Series["Yakınsama"].Points.Clear();
            for (int i = 0; i < pso.Convergence.Count; i++)
            {
                chart1.Series["Yakınsama"].Points.AddXY(i + 1, pso.Convergence[i]);
            }
        
    }
    }
}
