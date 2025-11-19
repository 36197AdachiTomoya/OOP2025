using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    internal class LineToHalfNumberService : ITextFileService {
        private int _count;
        public void Initialize(string fname) {
            _count = 0;
        }

        public void Execute(string line) {
            _count++;
            string str = line.Normalize(NormalizationForm.FormKD);
            Console.WriteLine(str);
        }

        public void Terminate() {
            
        }
    }
}
