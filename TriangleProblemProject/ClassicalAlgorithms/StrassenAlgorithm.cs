using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Quantum.TriangleProblemProject.ClassicalAlgorithms
{
    /// <summary>
    /// A classical algorithm for solving triangle finding.
    /// 
    /// Having each algorithm in a class allows each algorithm to easily be passed
    /// to the same testing methods, using this interface. Also, recursive algorithms
    /// can make use of class fields
    /// 
    /// This is efficient for dense graphs, with time complexity of O(n^2.373 or O(n^log2(7)), a mild improvement over standard matrix multication.
    /// </summary>
    class StrassenAlgorithm : IAlgorithm
    {
        /// <summary>
        /// Name of the algorithm.
        /// </summary>

        string IAlgorithm.Name => "Strassens Algorithm";

        /// <summary>
        /// Run the algorithm. Return whether there is a triangle in the graph.
        /// </summary>
        /// <param name="mat">The adjacency matrix of the graph.</param>
        public bool Run(int[,] mat)
        {
            //int boolResult = 0;
            int trace = 0;
            int nodes = mat.GetLength(0);
            // if nodes ==2 then its a standard trival matrix multiplication scenario (or trace algorithm)
            if (nodes == 2)
            {
                int[,] a2 = MatrixMultiply(nodes, mat, mat);
                int[,] a3 = MatrixMultiply(nodes, a2, mat);
                for (int i = 0; i < nodes; i++)
                {
                    trace += a3[i, i];
                }
                return trace != 0;
            }
            else
            {
                // Find the Highest Square Matrix of size 2^n * 2^n, where n == nodes
                double logOfNodes = Math.Log(2.0, nodes);
                int squareMatrixDimension = (int)Math.Pow(2.0, Math.Ceiling(logOfNodes));
                int[,] resizedMatrix = new int[squareMatrixDimension, squareMatrixDimension];
                for (int col = 0; col < nodes; col++)
                {
                    for (int row = 0; row < nodes; row++)
                    {
                        resizedMatrix[row, col] = mat[row, col];
                    }
                }
                int[,] mat1 = StrassensAlgorithm(mat, mat);
                int[,] mat2 = StrassensAlgorithm(mat1, mat);
                for (int i = 0; i < nodes; i++)
                {
                    trace += mat2[i, i];
                }
                return trace != 0;
            }
        }
        //Strassens Algorithm as an independent method
        public int[,] StrassensAlgorithm(int[,] A, int[,] B)
        {
            // DO NOT INPUT MATRICES OF SIZE <= 2
            if (A.GetLength(0) == 2)
            {
                return MatrixMultiply(2, A, B);
            }
            else
            {
                // initialisation of new matrices
                int halfLength = A.GetLength(0) / 2;
                int[,] a11 = new int[halfLength, halfLength];
                int[,] a12 = new int[halfLength, halfLength];
                int[,] a21 = new int[halfLength, halfLength];
                int[,] a22 = new int[halfLength, halfLength];

                int[,] b11 = new int[halfLength, halfLength];
                int[,] b12 = new int[halfLength, halfLength];
                int[,] b21 = new int[halfLength, halfLength];
                int[,] b22 = new int[halfLength, halfLength];

                int[,] aResult = new int[halfLength, halfLength];
                int[,] bResult = new int[halfLength, halfLength];
                //dividing the matrix into four n/2*n/2 sub-matrices  (crucial for algorithm)
                for (int i = 0; i < halfLength; i++)
                {
                    for (int j = 0; j < halfLength; j++)
                    {
                        a11[i, j] = A[i, j]; // top left
                        a12[i, j] = A[i, (j + halfLength)]; // top right
                        a21[i, j] = A[(i + halfLength), j]; // bottom left
                        a22[i, j] = A[(i + halfLength), (j + halfLength)]; // bottom right

                        b11[i, j] = B[i, j]; // top left
                        b12[i, j] = B[i, (j + halfLength)]; // top right
                        b21[i, j] = B[(i + halfLength), j]; // bottom left
                        b22[i, j] = B[(i + halfLength), (j + halfLength)]; // bottom right
                    }
                }
                // next comes a lot of arithmetic (algebra behind Strassen's detailed (and visualised) here https://arxiv.org/abs/1708.08083 )
                // essentially we create 7 assignments (labeled p1,p2...p7)
                aResult = MatrixAddition(a11, a22);
                bResult = MatrixAddition(b11, b22);
                int[,] p1 = StrassensAlgorithm(aResult, bResult);
                // p1 = (a11+a22) * (b11+b22)
                aResult = MatrixAddition(a21, a22); // a21 + a22
                int[,] p2 = StrassensAlgorithm(aResult, b11); // p2 = (a21+a22) * (b11)
                bResult = MatrixSubtraction(b12, b22); // b12 - b22
                int[,] p3 = StrassensAlgorithm(a11, bResult);
                // p3 = (a11) * (b12 - b22)
                bResult = StrassensAlgorithm(b21, b11); // b21 - b11
                int[,] p4 = StrassensAlgorithm(a22, bResult);
                // p4 = (a22) * (b21 - b11)
                aResult = MatrixAddition(a11, a12); // a11 + a12
                int[,] p5 = StrassensAlgorithm(aResult, b22);
                // p5 = (a11+a12) * (b22)
                aResult = MatrixSubtraction(a21, a11); // a21 - a11
                bResult = MatrixAddition(b11, b12); // b11 + b12
                int[,] p6 = StrassensAlgorithm(aResult, bResult);
                // p6 = (a21-a11) * (b11+b12)
                aResult = MatrixSubtraction(a12, a22); // a12 - a22
                bResult = MatrixAddition(b21, b22); // b21 + b22
                int[,] p7 = StrassensAlgorithm(aResult, bResult);
                // p7 = (a12-a22) * (b21+b22)
                // calculating c21, c21, c11 e c22:
                int[,] c12 = MatrixAddition(p3, p5); // c12 = p3 + p5
                int[,] c21 = MatrixAddition(p2, p4); // c21 = p2 + p4
                aResult = MatrixAddition(p1, p4); // p1 + p4
                bResult = MatrixAddition(aResult, p7); // p1 + p4 + p7
                int[,] c11 = MatrixSubtraction(bResult, p5);
                // c11 = p1 + p4 - p5 + p7
                aResult = MatrixAddition(p1, p3); // p1 + p3
                bResult = MatrixAddition(aResult, p6); // p1 + p3 + p6
                int[,] c22 = MatrixSubtraction(bResult, p2);
                // c22 = p1 + p3 - p2 + p6
                // Grouping the results obtained in a single matrix:
                int[,] C = new int[A.GetLength(0), A.GetLength(0)];
                for (int i = 0; i < halfLength; i++) {
                    for (int j = 0; j < halfLength; j++) {
                        C[i,j] = c11[i,j];
                        C[i,j + halfLength] = c12[i,j];
                        C[i + halfLength,j] = c21[i,j];
                        C[i + halfLength,j + halfLength] = c22[i,j];
                    }
                }
                return C;
            }
    }
        //public int[,] createSubmatrix
        // standard ikj multiplication of matrices
        int[,] MatrixMultiply(int size, int[,] mat1, int[,] mat2)
        {
            int[,] result = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int value = 0;
                    for (int k = 0; k < size; k++)
                    {
                        value += mat1[i, k] * mat2[k, j];
                    }

                    result[i, j] = value;
                }
            }

            return result;
        }
        int[,] MatrixAddition(int[,] mat1, int[,] mat2)
        {
            int n = mat1.GetLength(0);
            int[,] mat3 = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    mat3[i, j] = mat1[i, j] + mat2[i, j];
                }
            }
            return mat3;
        }
        int[,] MatrixSubtraction(int[,] mat1, int[,] mat2)
        {
            int n = mat1.GetLength(0);
            int[,] mat3 = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    mat3[i, j] = mat1[i, j] + mat2[i, j];
                }
            }
            return mat3;
        }
        bool IAlgorithm.Run(int[,] mat)
        {
            return Run(mat);
        }

        public (int, int, int) getTriangle(int[,] mat)
        {
           return new BruteForceAlgorithm().getTriangle(mat);
        }
    }
}

