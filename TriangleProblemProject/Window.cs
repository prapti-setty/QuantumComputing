
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Quantum.Simulation.Core;
using System.Diagnostics;

using Microsoft.Quantum.Simulation.Simulators;
using Quantum.TriangleProblemProject.ClassicalAlgorithms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot.Axes;
using System.Timers;

namespace Quantum.TriangleProblemProject
{
    public partial class Window : Form
    {
        private int iterations = 0;
        private int repeats = 0;
        private int currentTriangleA, currentTriangleB, currentTriangleC = -1;
		private List<int> selectedVertices = new List<int>();
        private static readonly int CIRCLE_SIZE = 16;
        private static readonly int OFFSET_SIZE = 3;
        Random randGen;
        Graph g;
        public Window()
        {
            randGen = new Random();
            InitializeComponent();
            g = new Graph();
            quantumRadioButton.Checked = true;
            g.addVertex("a", Brushes.Red, 56, 56);
            textBox3.Text = "0";
            textBox4.Text = "0";
            richTextBox1.ReadOnly = true;
        }
        private void addSkewedVertex(int center, int x, int y, Brush b)
        {
            if (Math.Abs(y - center) > 30) y = center + Math.Sign(y-center) *(int)(( center) * (0.5 + x / (6.0 * center)));
             g.addVertex("", b, x, y);
        }
        private void drawSquare(int startx,int startY, Brush b)
        {
           // g.addVertex("", b, startx, startY);
            addSkewedVertex(140, startx, startY, b);
          //  g.addVertex("", b, startx + 100, startY + 100);
            addSkewedVertex(140, startx + 100, startY +  100, b);
          //  g.addVertex("", b, startx + 100, startY );
               addSkewedVertex(140,startx +100, startY,b);
            g.setEdge(g.getAdjMat().GetLength(0) - 1, g.getAdjMat().GetLength(0) - 3, 1);
          //  g.addVertex("", b, startx, startY+100);
            addSkewedVertex(140, startx , startY + 100, b);
            g.setEdge(g.getAdjMat().GetLength(0)-3, g.getAdjMat().GetLength(0) - 2,1);
            g.setEdge(g.getAdjMat().GetLength(0) - 1, g.getAdjMat().GetLength(0) - 3, 1);
            g.setEdge(g.getAdjMat().GetLength(0) - 1, g.getAdjMat().GetLength(0) - 4, 1);
        }
        private void drawMicrosotSymbol()
        {
            drawSquare(30, 30, new SolidBrush(Color.FromArgb(246, 83, 20)));
            drawSquare(150, 30, new SolidBrush(Color.FromArgb(124, 181, 0)));
            drawSquare(30, 150, new SolidBrush(Color.FromArgb(0, 161, 241)));
            drawSquare(150, 150, new SolidBrush(Color.FromArgb(255, 187, 0)));
        }
        private void runQuantum(int[,] adjMat)
        {

            writeMessage("Running Quantum Algorithm");
            var resCount = 0;
            var runNum = 1;
            var bad = 0;
            QuantumSimulator simulator = new QuantumSimulator();

            QuantumAlgorithm quantumAlgorithm = new QuantumAlgorithm(simulator);
            for (int i = 0; i < runNum; i++)
            {
                var resOne = quantumAlgorithm.getTriangle(adjMat, iterations, repeats);
                var (one, two, three) = resOne;
                if (one >= 0 && two >= 0 && three >= 0 && one != two && two != three && one != three)
                    resCount++;
                if (!(one != two && two != three && one != three) && one >= 0 && two >= 0 && three >= 0)
                {
                    bad++;
                    Console.WriteLine(one + " " + two + " " + three);
                    Console.ReadLine();

                }
                // This code would be for if we could calcuate the probability correctly
                /*
                double probability = calculateProbability(iterations, repeats, adjMat);
                setTriangle(one, two, three);
                writeMessage("Finding with probability: " + probability);
                */
                if (one == -1)
                    writeMessage("No Triangle Found");
                else
                    writeMessage("Triangle Found at: " + g.points[one].getName() + ", " + g.points[two].getName()
                        + ", " + g.points[three].getName());
                Refresh();
            }
        }
        private void writeMessage(string str)
        {
            richTextBox1.Text = richTextBox1.Text + "=> " + str + "...\n";
        }
        void displayTriangle(int one, int two, int three)
        {


        }
        String arrString(int[] inp)
        {
            String str = "";
            for (int i = 0; i < inp.Length; i++)
                str = str + inp[i];
            return str;
        }
        private void Window_Load(object sender, EventArgs e)
        {
            setIcon();
            setMicroIcon();
        }

        private void runBruteForce(int[,] adjMat)
        {
            writeMessage("Running Brute Force");
            BruteForceAlgorithm algorithm = new BruteForceAlgorithm();
            var result = algorithm.getTriangle(adjMat);
            var (one, two, three) = result;
            setTriangle(one, two, three);
            if (one == -1)
                writeMessage("No Triangle Found");
            else
                writeMessage("Triangle Found at: " + g.points[one].getName() + ", " + g.points[two].getName()
                    + ", " + g.points[three].getName());
            Refresh();
        }

        private void runTrace(int[,] adjMat)
        {
            writeMessage("Running Trace");
            TraceAlgorithm algorithm = new TraceAlgorithm();
            bool result = algorithm.Run(adjMat);
            if (result == false)
                writeMessage("No Triangle Found");
            else
                writeMessage("Triangle Found!");
            Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
        }

        //Find Triangle button clicked
        private void button1_Click(object sender, EventArgs e)
        {
            // Create new stopwatch
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            richTextBox1.Text = "";
            if (quantumRadioButton.Checked)
            {
                runQuantum(g.getAdjMat());
            }
            else if (bruteForceRadioButton.Checked)
            {
                runBruteForce(g.getAdjMat());
            }
            else
            {
                runTrace(g.getAdjMat());
            }
            writeMessage("Time Elapsed: " + stopwatch.Elapsed);
            stopwatch.Stop();

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
         
        }
        private Brush getRandColor()
        {
            int rand = randGen.Next(0, 6);
            switch (rand)
            {
                case (0):
                    return Brushes.Red;
                case (1):
                    return Brushes.Green;
                case (2):
                    return Brushes.Blue;
                case (3):
                    return Brushes.Black;
                case (4):
                    return Brushes.Brown;
                default:
                    return Brushes.Yellow;
            }


        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < g.getAdjMat().GetLength(0); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (g.getAdjMat()[i, j] == 1)
                    {
                        Pen curPen = Pens.Black;
                        if ((i == currentTriangleA || i == currentTriangleB || i == currentTriangleC) &&
                                (j == currentTriangleA || j == currentTriangleB || j == currentTriangleC))
                        {
                            curPen = new Pen(Brushes.Red);
                            curPen.Width = 4.0F;
                        }
                        e.Graphics.DrawLine(curPen, g.points[i].x + (CIRCLE_SIZE / 2), g.points[i].y
                      + (CIRCLE_SIZE / 2), g.points[j].x + (CIRCLE_SIZE / 2), g.points[j].y + (CIRCLE_SIZE / 2));
                    }


                }
            }
            for (int i = 0; i < g.points.Count; i++)
            {
                e.Graphics.FillEllipse(g.points[i].brush, new Rectangle(g.points[i].x, g.points[i].y, CIRCLE_SIZE, CIRCLE_SIZE));
                e.Graphics.DrawString(g.points[i].idt, new Font(FontFamily.GenericSansSerif,
            9.0F, FontStyle.Bold), Brushes.Black, g.points[i].x + (CIRCLE_SIZE / 2), g.points[i].y + (CIRCLE_SIZE + OFFSET_SIZE));

            }

			foreach (int selectedVertex in selectedVertices) {
				e.Graphics.DrawEllipse(Pens.Black, new Rectangle(g.points[selectedVertex].x - OFFSET_SIZE,
					g.points[selectedVertex].y - OFFSET_SIZE, CIRCLE_SIZE + (2 * OFFSET_SIZE), CIRCLE_SIZE + (2 * OFFSET_SIZE)));
			}
        }

        private void panel1_MouseClick_1(object sender, MouseEventArgs e)
        {
            int newSel = GetVertexAtPos(e.X, e.Y);
            if (newSel >= 0)
            {
                if (ModifierKeys == Keys.Shift) {
					if (selectedVertices.Contains(newSel)) {
						// If already selected, unselect.
						selectedVertices.Remove(newSel);
					} else {
						selectedVertices.Add(newSel);
						// Maxixmum of 2 vertices selected. Remove oldest.
						if (selectedVertices.Count > 2) {
							selectedVertices.RemoveAt(0);
						}
					}
				} else {
					// Set this as only selected vertex.
					selectedVertices.Clear();
					selectedVertices.Add(newSel);
				}
            }
            else
            {
				if (ModifierKeys != Keys.Shift) {
					selectedVertices.Clear();
				}
            }

            Refresh();

        }

		private void panel1_MouseDoubleClick(object sender, MouseEventArgs e) {
			g.addVertex(g.getNextName(), getRandColor(), e.X, e.Y);
			selectedVertices.Clear();
			selectedVertices.Add(g.points.Count - 1);
            Refresh();
		}

        private int GetVertexAtPos(int x, int y)
        {
            for (int i = 0; i < g.points.Count; i++)
            {
                Rectangle rect = new Rectangle(g.points[i].x, g.points[i].y, CIRCLE_SIZE, CIRCLE_SIZE);
                if (rect.Contains(x, y))
                    return i;
            }

            return -1;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
			selectedVertices.Clear();
            g = new Graph();
            Refresh();
        }

        private class AlgorithmResults
        {
            public Func<IAlgorithm> AlgorithmConstructor;
            public long MaxTime;
            public List<long> MaxTimes = new List<long>();
            public List<long> Times = new List<long>();
			// How many matrices to run this algorithm with.
			public int MatrixCount;

            public AlgorithmResults(int matrixCount, Func<IAlgorithm> algorithmConstructor)
            {
				MatrixCount = matrixCount;
                AlgorithmConstructor = algorithmConstructor;
            }

            public void AddNewTime()
            {
                Times.Add(0);
                MaxTimes.Add(0);
            }

            public void AddTime(long time)
            {
                Times[Times.Count - 1] += time;
                MaxTimes[MaxTimes.Count - 1] = Math.Max(MaxTimes[MaxTimes.Count - 1], time);
                MaxTime = Math.Max(MaxTime, Times[Times.Count - 1]);
            }
        }

		bool IncludeQuantumInGraph = false;

        private void btnGraph_Click(object sender, EventArgs e)
        {
            var myModel = new PlotModel
            {
                Title = "Results",
            };
            myModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Vertex Count", MinorStep = 1, MajorStep = 1 });
            myModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Time Taken (Relative)" });

            int matrixCount = 10000;
            int minVertices = 3;
            int maxVertices = 8;
            int verticesGap = 1;
            QuantumSimulator simulator = new QuantumSimulator();
            List<AlgorithmResults> algorithmResults = new List<AlgorithmResults> {
                // Repeat brute force a bunch of times, else its times are too small.
                new AlgorithmResults(matrixCount, () => new BruteForceAlgorithm()),
                new AlgorithmResults(matrixCount, () => new TraceAlgorithm()),
                new AlgorithmResults(100, () => new QuantumAlgorithm(simulator)),
            };

			// Don't include quantum, the first time we run the graph.
			if (!IncludeQuantumInGraph) {
				IncludeQuantumInGraph = true;
				algorithmResults.RemoveAt(algorithmResults.Count - 1);
			}

            for (int i = minVertices; i <= maxVertices; i += verticesGap)
            {
                foreach (AlgorithmResults result in algorithmResults)
                {
                    result.AddNewTime();
                }

                for (int matrixI = 0; matrixI < matrixCount; matrixI++)
                {
                    int[,] matrix = new int[i, i];
                    for (int j = 0; j < i; j++)
                    {
                        for (int k = j + 1; k < i; k++)
                        {
                            if (randGen.NextDouble() < 0.2)
                            {
                                matrix[j, k] = 1;
                                matrix[k, j] = 1;
                            }
                        }
                    }

                    // Run each algorithm on this matrix.
                    foreach (AlgorithmResults result in algorithmResults)
                    {
						if (matrixI < result.MatrixCount) {
							IAlgorithm algorithm = result.AlgorithmConstructor();
							Stopwatch watch = Stopwatch.StartNew();
							algorithm.Run(matrix);
							result.AddTime(watch.ElapsedTicks);
						}
                    }
                }
            }

            foreach (var result in algorithmResults)
            {
                myModel.Series.Add(new FunctionSeries((x =>
                {
                    int timeIndex = (int)(x - minVertices) / verticesGap;
                    return (double)result.Times[timeIndex] / result.MaxTime;
                }), minVertices, maxVertices, (double)verticesGap, result.AlgorithmConstructor().Name));
            }
            GraphForm graphForm = new GraphForm();
            graphForm.View.Model = myModel;
            graphForm.Show();
        }
        static int[] qarrToArr(QArray<long> arr)
        {
            int[] retArr = new int[arr.Count];
            for (int i = 0; i < retArr.Length; i++)
                retArr[i] = (int)arr[i];
            return retArr;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void setTriangle(long a, long b, long c)
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            // Set the Interval to 1 second.
            currentTriangleA = (int)a;
            currentTriangleB = (int)b;
            currentTriangleC = (int)c;
            Refresh();
            aTimer.Interval = 5000;
            aTimer.Enabled = true;
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            currentTriangleA = -1;
            currentTriangleB = -1;
            currentTriangleC = -1;
            this.Invoke((MethodInvoker)delegate
            {
                updateGUILab.Text = "";
            }); ;
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox3.Text, out iterations) == true)
            {
                iterations = Int32.Parse(textBox3.Text);
                textBox3.Text = "" + iterations;
            }
            else
            {
                iterations = 0;
                textBox3.Text = "" + 0;
            }

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox4.Text, out repeats) == true)
            {
                repeats = Int32.Parse(textBox4.Text);
                textBox4.Text = "" + repeats;
            }
            else
            {
                repeats = 0;
                textBox4.Text = "" + 0;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void quantumRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
			selectedVertices.Clear();
            g = new Graph();
            drawMicrosotSymbol();
            Refresh();
        }

        //Converts a 2d array to a usable qarray
        public static QArray<QArray<long>> arrToQArray(int[,] input)
        {
            QArray<QArray<long>> returnArray = new QArray<QArray<long>>();
            for (int i = 0; i < input.GetLength(0); i++)
            {
                QArray<long> innerArray = new QArray<long>();
                for (int j = 0; j < input.GetLength(1); j++)
                    innerArray.Add(input[i, j]);
                returnArray.Add(innerArray);
            }

            return returnArray;
        }

		private void setIcon()
        {
            string dir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            string filename = System.IO.Path.Combine(dir, @"Quantum.png");
            Bitmap b = (Bitmap)Image.FromFile(filename);
            IntPtr pIcon = b.GetHicon();
            Icon i = Icon.FromHandle(pIcon);
            this.Icon = i;
            i.Dispose();
        }
        private void setMicroIcon()
        {
            string dir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            string filename = System.IO.Path.Combine(dir, @"micro.png");
            button1.BackgroundImage = Image.FromFile(filename);
            button1.BackgroundImageLayout = ImageLayout.Stretch;
         //   IntPtr pIcon = b.GetHicon();
          //  Icon i = Icon.FromHandle(pIcon);
           // this.Icon = i;
           // button1.Image = b;
            
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.E)
            {
				ToggleEdge();
                Refresh();
                return true;
            } else if (keyData == Keys.Delete) {
				// Delete vertex
				if (selectedVertices.Count == 1) {
					g.removeVertex(selectedVertices[0]);
					selectedVertices.Clear();
				}
				Refresh();
				return true;
			}
            return base.ProcessCmdKey(ref msg, keyData);
        }

		private void ToggleEdge() {
			if (selectedVertices.Count == 2) {
				int value = g.getEdge(selectedVertices[0], selectedVertices[1]);
				g.setEdge(selectedVertices[0], selectedVertices[1], 1 - value);
			}
		}


        //We were unable to work out a probability of finding a triangle that correctly matches the outputs we got in testing.
        //However, what we do know is that increasing the repeats increases the probability, 
        //and increasing the iterations changes the probability on a sinusoidal wave
        //With more time, we would be able to get this working
        /*
        private static double calculateProbability(int iteration, int repeat, int[,] adjMat)
        {
            // (1 - (P(E) * (1-P(V) / x) ^ n) / x) ^ n | where x is the number of possible hits, and n is the number of times the search is repeated
            int size = (int)Math.Pow(2, getNumOfQubitsUsed(adjMat.GetLength(0)));
            int[] vertexValues = getAdjacencyValues(adjMat);
            int[] edgesList = getAllEdges(adjMat);
            int edgesSize = (int)Math.Pow(2, getNumOfQubitsUsed(edgesList.Length));
            int vertexSize = (int)Math.Pow(2, getNumOfQubitsUsed(vertexValues.Length));
            if (edgesSize <= 0)
            {
                return 1;
            }
            int edgesSum = 0;
            for (int i = 0; i < edgesList.Length; i++)
            {
                edgesSum += edgesList[i];
            }
            if (iteration == 0 && repeat == 0)
            {
                repeat = (size > edgesSum) ? size : edgesSum;
                iteration = (int)Math.Sqrt(edgesSum) - 1;
                if (iteration == 0)
                {
                    iteration = 1;
                }
            }
            double chanceOfFindingAnEdge = Math.Pow(
                Math.Sin((2.0 * (double)iteration + 1.0)
                        * Math.Asin(Math.Sqrt(edgesSum) / Math.Sqrt(edgesSize))),
                2.0);

            double chanceOfFindingEachEdge = 1
                    - Math.Pow((1 - (chanceOfFindingAnEdge / edgesSum)), repeat);

            double chanceOfFindingAVertex = 0;
            for (int i = 0; i < vertexValues.Length; i++)
            {
                chanceOfFindingAVertex += (edgesList.Length - (i + 1)) * Math.Pow(
                        Math.Sin((2.0 * (double)iteration + 1.0) * Math
                                .Asin(Math.Sqrt(vertexValues[i]) / Math.Sqrt(vertexSize))),
                        2.0);

            }

            chanceOfFindingAVertex = chanceOfFindingAVertex / edgesSize;

            double chanceOfFindingEachVertex = 1
                    - Math.Pow((1 - (chanceOfFindingAVertex / vertexValues.Length)), repeat);

            double chanceOfFindingAnEdgeAndAVertex = chanceOfFindingAnEdge * chanceOfFindingAVertex;

            double chanceOfFindingEachVertexAndEachEdge = chanceOfFindingEachVertex * chanceOfFindingEachEdge;


            double chanceOfFindingAnEdgeAndEachVertex = chanceOfFindingEachVertex * chanceOfFindingAnEdge;
            double chanceOfFindingEachEdgeAndEachVertex = 1 - Math.Pow(1 - (chanceOfFindingAnEdgeAndEachVertex / edgesSum), repeat);

            Console.WriteLine("Chance of finding an edge: " + chanceOfFindingAnEdge + "/n");
            Console.WriteLine("Chance of finding each edge: " + chanceOfFindingEachEdge + "/n");
            Console.WriteLine("Chance of finding a vertex: " + chanceOfFindingAVertex + "/n");
            Console.WriteLine("Chance of finding edge and vertex: " + chanceOfFindingAnEdgeAndAVertex + "/n");
            Console.WriteLine("Chance of finding each vertex: " + chanceOfFindingEachVertex + "/n");
            Console.WriteLine("Chance of finding each edge and vertex: " + chanceOfFindingEachVertexAndEachEdge + "/n");

            return chanceOfFindingAnEdgeAndEachVertex;

        }
        private static int[] getAllEdges(int[,] adjMat)
        {
            int x = adjMat.GetLength(0);
            int[] retArr = new int[((adjMat.GetLength(0) * adjMat.GetLength(0)) - adjMat.GetLength(0)) / 2];     // nC2, or (n^2 - n) / 2
            int retArrIndex = 0;

            for (int i = 0; i < adjMat.GetLength(0); i++)
            {
                for (int j = i + 1; j < adjMat.GetLength(0); j++)
                {
                    retArr[retArrIndex] = adjMat[i, j];
                    retArrIndex = retArrIndex + 1;
                }
            }

            return retArr;
        }

        private static int[] getAdjacencyValues(int[,] adjMat)
        {
            int[] ret = new int[adjMat.GetLength(0)];
            for (int i = 0; i < adjMat.GetLength(0); i++)
            {
                int sum = 0;
                for (int j = 0; j < adjMat.GetLength(0); j++)
                {
                    sum += adjMat[i, j];
                }
                ret[i] = sum;
            }
            return ret;
        }

        private static int getNumOfQubitsUsed(int num)
        {
            int count = 1;
            int res = 0;
            while (count < num)
            {
                count = count * 2;
                res += 1;
            }
            return res;
        }
        */
    }
}
