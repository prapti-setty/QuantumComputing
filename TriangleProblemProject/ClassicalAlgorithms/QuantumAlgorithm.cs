using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.TriangleProblemProject.ClassicalAlgorithms {
    public class QuantumAlgorithm : IClassicalAlgorithm {
        private QuantumSimulator _simulator;
        public string Name => "Quantum";

        public QuantumAlgorithm() {
            _simulator = new QuantumSimulator();
        }

        public bool Run(int[,] mat) {
            QArray<QArray<long>> inputArray = Window.arrToQArray(mat);
          //  var res = decipher.Run(_simulator, inputArray).Result;
            return true;
        }
    }
}
