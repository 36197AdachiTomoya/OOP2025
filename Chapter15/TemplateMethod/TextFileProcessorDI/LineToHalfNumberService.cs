using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    internal class LineToHalfNumberService : ITextFileService {
        private int _count;
        private string? str;
        public void Initialize(string fname) {
            _count = 0;
        }

        public void Execute(string line) {
            _count++;
            str = new string(
                line.Select(c =>
            (c >= '０' && c <= '９') ? (char)('0' + (c - '０'))   
                : c                          
            ).ToArray()
            );

            Console.WriteLine(str);
        }

        public void Terminate() {
            
        }
    }
}
