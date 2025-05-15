using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSO_odev
{
    public static class TestFunctions
    {
        // Six-Hump Camel Back Fonksiyonu
        public static double SixHumpCamelBack(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];
            double term1 = (4 - 2.1 * Math.Pow(x1, 2) + (Math.Pow(x1, 4) / 3)) * Math.Pow(x1, 2);
            double term2 = x1 * x2;
            double term3 = (-4 + 4 * Math.Pow(x2, 2)) * Math.Pow(x2, 2);

            return term1 + term2 + term3;
        }
    }

}
