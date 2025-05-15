using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSO_odev
{
    public class PSO
    {
        public int ParticleCount { get; set; }
        public int Iterations { get; set; }
        public int Dimension { get; set; } = 2;

        public double C1 { get; set; }
        public double C2 { get; set; }
        public double W { get; set; }

        public double[] LowerBounds { get; set; }
        public double[] UpperBounds { get; set; }

        private List<Particle> swarm;
        private double[] globalBestPosition;
        private double globalBestFitness;

        private Random rand = new Random();

        public List<double> Convergence { get; private set; } = new List<double>();

        public PSO(int particleCount, int iterations, double c1, double c2, double w, double[] lowerBounds, double[] upperBounds)
        {
            ParticleCount = particleCount;
            Iterations = iterations;
            C1 = c1;
            C2 = c2;
            W = w;
            LowerBounds = lowerBounds;
            UpperBounds = upperBounds;

            swarm = new List<Particle>();
            globalBestFitness = double.MaxValue;
            globalBestPosition = new double[Dimension];
        }

        public void Initialize()
        {
            for (int i = 0; i < ParticleCount; i++)
            {
                Particle p = new Particle(Dimension);
                for (int d = 0; d < Dimension; d++)
                {
                    p.Position[d] = LowerBounds[d] + rand.NextDouble() * (UpperBounds[d] - LowerBounds[d]);
                    p.Velocity[d] = 0;
                    p.BestPosition[d] = p.Position[d];
                }

                p.BestFitness = TestFunctions.SixHumpCamelBack(p.Position);
                if (p.BestFitness < globalBestFitness)
                {
                    globalBestFitness = p.BestFitness;
                    Array.Copy(p.Position, globalBestPosition, Dimension);
                }

                swarm.Add(p);
            }
        }

        public void Run()
        {
            for (int iter = 0; iter < Iterations; iter++)
            {
                foreach (var p in swarm)
                {
                    for (int d = 0; d < Dimension; d++)
                    {
                        double r1 = rand.NextDouble();
                        double r2 = rand.NextDouble();

                        p.Velocity[d] = W * p.Velocity[d] +
                                        C1 * r1 * (p.BestPosition[d] - p.Position[d]) +
                                        C2 * r2 * (globalBestPosition[d] - p.Position[d]);

                        p.Position[d] += p.Velocity[d];

                        // Boundaries
                        p.Position[d] = Math.Max(LowerBounds[d], Math.Min(UpperBounds[d], p.Position[d]));
                    }

                    double fitness = TestFunctions.SixHumpCamelBack(p.Position);
                    if (fitness < p.BestFitness)
                    {
                        p.BestFitness = fitness;
                        Array.Copy(p.Position, p.BestPosition, Dimension);
                    }

                    if (fitness < globalBestFitness)
                    {
                        globalBestFitness = fitness;
                        Array.Copy(p.Position, globalBestPosition, Dimension);
                    }
                }

                Convergence.Add(globalBestFitness);
            }
        }

        public double[] GetBestPosition() => globalBestPosition;
        public double GetBestFitness() => globalBestFitness;
    }

}
