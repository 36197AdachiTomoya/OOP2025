using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02 {
    class YardConverter {
        private const double yard = 0.9144;

        //ヤードからメートル
        public static double ToMeter(int yard) {
            return yard * 0.9144;
        }

        //メートルからヤード
        public static double ToYard(int meter) {
            return meter / yard;
        }
    }
}
