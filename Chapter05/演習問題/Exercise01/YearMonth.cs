using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01{
    public record YearMonth{

        //5.1.1
        public readonly int Year;
        public readonly int Month;

       public YearMonth(int year, int month) {
            Year = year;
            Month = month;
        }

        //5.1.2
        public bool Is21stCentury => Year >= 2001 && Year <= 2100;

        //5.1.3
        public YearMonth AddOneMonth() {
            YearMonth rtn;
            if(Year == 12) {
                rtn = new YearMonth(Year + 1, 1);
            } else {
                rtn = new YearMonth(Year, Month + 1);
            }
            return rtn;
        }

        //5.1.4
        public override string ToString() => Year + "年" + Month + "月";
            
        
    }
}
