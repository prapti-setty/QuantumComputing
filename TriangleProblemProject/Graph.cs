//Graph class by Isaac Walker

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Quantum.TriangleProblemProject
{
    class Graph
    {
        private int[,] adjMatrix;
        public List<Vertex> points;
        public static readonly int SET = 1;
        public static readonly int NOTSET = 0;
        public Graph()
        {
            points = new List<Vertex>();
            adjMatrix = new int[0, 0];
            initialiseMat(adjMatrix);
        }
        private void initialiseMat(int[,] mat)
        {
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                    mat[i, j] = NOTSET;
            }

        }
        public void addVertex(String v, Brush color, int x, int y)
        {
            points.Add(new Vertex(v, color, x, y));
            int[,] newMatrix = new int[adjMatrix.GetLength(0) + 1, adjMatrix.GetLength(1) + 1];
            initialiseMat(newMatrix);
            for (int i = 0; i < adjMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < adjMatrix.GetLength(0); j++)
                {
                    newMatrix[i, j] = adjMatrix[i, j];
                }
            }
            adjMatrix = newMatrix;
        }
		public void removeVertex(int index) {
			points.RemoveAt(index);
            int[,] newMatrix = new int[adjMatrix.GetLength(0) - 1, adjMatrix.GetLength(1) - 1];
            for (int i = 0; i < newMatrix.GetLength(0); i++)
            {
				int i2 = i;
				if (i2 >= index) {
					i2++;
				}

                for (int j = 0; j < newMatrix.GetLength(1); j++)
                {
					int j2 = j;
					if (j2 >= index) {
						j2++;
					}

                    newMatrix[i, j] = adjMatrix[i2, j2];
                }
            }
			adjMatrix = newMatrix;
		}
        private int getIndexOfV(String v)
        {
            for (int i = 0; i < points.Count; i++)
                if (v.Equals(points[i].idt))
                    return i;
            return -1;
        }
        public string getNextName()
        {
            char c;
            if (points.Count > 0)
            {
                char[] arr = points[points.Count - 1].idt.ToCharArray();
                c = ++arr[0];
            }
            else
            {
                c = 'a';
            }
            return Char.ToString(c);
        }

        public void setEdge(int v1, int v2, int set)
        {
            adjMatrix[v1, v2] = set;
            adjMatrix[v2, v1] = set;
        }

		public int getEdge(int v1, int v2) {
			return adjMatrix[v1, v2];
		}

        /*  public void printGraph()
          {
              Console.Write("V ");
              for (int i = 0; i < vertices.Length; i++)
                  Console.Write(vertices[i] + " ");
              Console.WriteLine("");
              for (int i = 0; i < adjMatrix.GetLength(0); i++)
              {
                  Console.Write(vertices[i] + " ");
                  for (int j = 0; j < adjMatrix.GetLength(1); j++)
                      Console.Write(adjMatrix[i, j]
                          + " ");
                  Console.WriteLine("");
              }
              Console.ReadLine();
          }*/

        public int[,] getAdjMat()
        {
            return adjMatrix;
        }
        public class Vertex
        {
            public String idt;
            public Brush brush;
            public int x, y;
            public Vertex(String idt, Brush brush, int x, int y)
            {
                this.idt = idt;
                this.brush = brush;
                this.x = x;
                this.y = y;
            }
            public string getName()
            {
                return idt;
            }


        }
        //classical implementation of triangle finding algorithm
        public (int, int, int) findTriangleBruteForce()
        {
            for (int i = 0; i < adjMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < adjMatrix.GetLength(0); j++)
                {

                }
            }
            return (1, 2, 3);
        }
    }
}

