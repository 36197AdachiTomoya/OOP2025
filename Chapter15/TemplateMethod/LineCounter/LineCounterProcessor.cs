using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFileProcessor;

namespace LineCounter {
    internal class LineCounterProcessor : TextProcessor{
        private int _count = 0;

        protected override void Initialize(string fname) => _count = 0;

        protected override void Execute(string line) {
            if (line.Contains("山田太郎")) {
                _count++;
            }
        } 

        protected override void Terminate() => Console.WriteLine("{0}個", _count);
        
    }
}
