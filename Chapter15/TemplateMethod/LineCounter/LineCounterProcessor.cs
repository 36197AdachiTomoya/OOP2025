using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFileProcessor;

namespace LineCounter {
    internal class LineCounterProcessor : TextProcessor{
        private int _count = 0;
        string text = string.Empty;



        protected override void Initialize(string fname) {
            Console.Write("入力：");
            text = Console.ReadLine() ?? string.Empty;
            _count = 0;
        } 

        protected override void Execute(string line) {
            if (line.Contains(text)) {
                _count++;
            }
        } 

        protected override void Terminate() => Console.WriteLine("{0}個", _count);
        
        
    }
}
