using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.TriangleProblemProject.ClassicalAlgorithms {
    /// <summary>
    /// A classical algorithm for solving triangle finding.
    /// 
    /// Having each algorithm in a class allows each algorithm to easily be passed
    /// to the same testing methods, using this interface. Also, recursive algorithms
    /// can make use of class fields
    /// </summary>
    interface IClassicalAlgorithm {
        /// <summary>
        /// Name of the algorithm.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Run the algorithm. Return whether there is a triangle in the graph.
        /// </summary>
        /// <param name="mat">The adjacency matrix of the graph.</param>
        bool Run(int[,] mat);
    }
}
