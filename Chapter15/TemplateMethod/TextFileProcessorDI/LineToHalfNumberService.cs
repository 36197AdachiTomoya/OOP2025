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
            str = line.Normalize(NormalizationForm.FormKD);
            
        }

        public void Terminate() {
            Console.WriteLine(str);
        }
    }
}
