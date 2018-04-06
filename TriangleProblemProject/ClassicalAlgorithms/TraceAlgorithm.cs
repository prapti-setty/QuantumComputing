using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.TriangleProblemProject.ClassicalAlgorithms {
    /// <summary>
    /// A graph with adjacency matrix A has a triangle if and only if trace(A^3) is non-zero.
    /// Runtime for this algorithim is O(n^3)

    /// </summary>
    public class TraceAlgorithm : IClassicalAlgorithm {
        public string Name => "Trace of Matrix";

        public bool Run(int[,] mat) {
            int nodes = mat.GetLength(0);
            int[,] a2 = Multiply(nodes, mat, mat);
            int[,] a3 = Multiply(nodes, a2, mat);

            int trace = 0;
            for (int i = 0; i < nodes; i++) {
                trace += a3[i, i];
            }

            return trace != 0;
        }
        public (int,int,int) getTriangle(int[,] mat)
        {
            return new BruteForceAlgorithm().getTriangle(mat);
        }
        private int[,] Multiply(int size, int[,] mat1, int[,] mat2) {
            int[,] result = new int[size, size];
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    int value = 0;
                    for (int k = 0; k < size; k++) {
                        value += mat1[i, k] * mat2[k, j];
                    }

                    result[i, j] = value;
                }
            }

            return result;
        }
    }
}
