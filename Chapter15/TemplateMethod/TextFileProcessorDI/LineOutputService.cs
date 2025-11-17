using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    public class LineOutputService : ITextFileService {
        private int _count;
        public void Initialize(string fname) {
            _count = 0;
        }

        public void Execute(string line) {
            _count++;
            if(_count <= 20) {
                Console.WriteLine(_count + line);
            }
        }

        public void Terminate() {
            
        }
    }
}
