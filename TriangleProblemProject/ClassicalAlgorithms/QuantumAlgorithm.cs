using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.TriangleProblemProject.ClassicalAlgorithms {
    public class QuantumAlgorithm : IAlgorithm {
        private QuantumSimulator _simulator;
        public string Name => "Quantum";

        public QuantumAlgorithm()
        {
            _simulator = new QuantumSimulator();
        }

        public bool Run(int[,] mat)
        {
            var testVals = getTriangle(mat);
            var (valOne, valTwo, valThree) = testVals;
            return (valOne != -1);

        }

        public (int, int, int) getTriangle(int[,] mat)
        {

            return getTriangle(mat, 0, 0);
        }
        public (int, int, int) getTriangle(int[,] mat, int iterations, int repeats)
        {
            QArray<QArray<long>> inputArray = Window.arrToQArray(mat);
            var res = findTriangleNew.Run(_simulator, inputArray, iterations, repeats).Result;
            var (retOne, retTwo, retThree) = res;
            return ((int)retOne, (int)retTwo, (int)retThree);
        }
    }
}
