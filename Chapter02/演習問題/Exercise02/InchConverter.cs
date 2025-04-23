using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02
{
    public static class InchConverter {
        private const double ratio = 0.0254;
        //インチからメートルを求める
        public static double ToMeter(double inch) {
            return inch * ratio;
        }

        public static double ToInch(int i) {
            return i / 0.0254;
        } 

    }
}
