
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
        private Boolean isSelected = false;
        private int selectedVertex, ctrlVertex = -1;
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
            g.addVertex("a", getRandColor(), 45, 45);
            g.addVertex("b", getRandColor(), 130, 90);
            g.addVertex("c", getRandColor(), 45, 113);
            g.addVertex("d", getRandColor(), 90, 160);
            textBox3.Text = "0";
            textBox4.Text = "0";
            richTextBox1.ReadOnly = true;
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

        private void button3_Click(object sender, EventArgs e)
        {
            isSelected = true;
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
            if (selectedVertex >= 0)
                e.Graphics.DrawEllipse(Pens.Black, new Rectangle(g.points[selectedVertex].x - OFFSET_SIZE,
                    g.points[selectedVertex].y - OFFSET_SIZE, CIRCLE_SIZE + (2 * OFFSET_SIZE), CIRCLE_SIZE + (2 * OFFSET_SIZE)));
            if (ctrlVertex >= 0)
                e.Graphics.DrawEllipse(Pens.Black, new Rectangle(g.points[ctrlVertex].x - OFFSET_SIZE,
                    g.points[ctrlVertex].y - OFFSET_SIZE, CIRCLE_SIZE + (2 * OFFSET_SIZE), CIRCLE_SIZE + (2 * OFFSET_SIZE)));
        }

        private void panel1_MouseClick_1(object sender, MouseEventArgs e)
        {
            int newSel = setSelectedVertex(e.X, e.Y);
            if (isSelected)
            {
                g.addVertex(g.getNextName(), getRandColor(), e.X, e.Y);
                isSelected = false;
                selectedVertex = g.points.Count - 1;
            }
            else if (newSel >= 0)
            {
                if (Control.ModifierKeys == Keys.Shift && selectedVertex >= 0)
                    ctrlVertex = newSel;
                else if (Control.ModifierKeys == Keys.Shift && selectedVertex < 0)
                    selectedVertex = newSel;
                else
                    selectedVertex = newSel;
            }
            else
            {
                selectedVertex = newSel;
                ctrlVertex = newSel;
            }

            Refresh();

        }
        private void edgeButton_Click(object sender, EventArgs e)
        {
            if (ctrlVertex >= 0 && selectedVertex >= 0)
                g.setEdge(ctrlVertex, selectedVertex, Graph.SET);
            Refresh();
        }

        private int setSelectedVertex(int x, int y)
        {
            for (int i = 0; i < g.points.Count; i++)
            {
                Rectangle rect = new Rectangle(g.points[i].x, g.points[i].y, CIRCLE_SIZE, CIRCLE_SIZE);
                if (rect.Contains(x, y))
                    return i;
            }

            return -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isSelected = true;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            selectedVertex = -1;
            ctrlVertex = -1;
            g = new Graph();
            Refresh();
        }

        private class AlgorithmResults
        {
            public Func<IAlgorithm> AlgorithmConstructor;
            public long MaxTime;
            public List<long> MaxTimes = new List<long>();
            public List<long> Times = new List<long>();

            public AlgorithmResults(Func<IAlgorithm> algorithmConstructor)
            {
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

        private void btnGraph_Click(object sender, EventArgs e)
        {
            var myModel = new PlotModel
            {
                Title = "Results",
            };
            myModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Vertex Count", MinorStep = 1, MajorStep = 1 });
            myModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Time Taken (Relative)" });

            int matrixCount = 10;
            int minVertices = 3;
            int maxVertices = 8;
            int verticesGap = 1;
            QuantumSimulator simulator = new QuantumSimulator();
            List<AlgorithmResults> algorithmResults = new List<AlgorithmResults> {
                // Repeat brute force a bunch of times, else its times are too small.
                new AlgorithmResults(() => new BruteForceAlgorithm()),
                new AlgorithmResults(() => new TraceAlgorithm()),
                new AlgorithmResults(() => new QuantumAlgorithm(simulator)),
            };

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
                        IAlgorithm algorithm = result.AlgorithmConstructor();
                        Stopwatch watch = Stopwatch.StartNew();
                        algorithm.Run(matrix);
                        result.AddTime(watch.ElapsedTicks);
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


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.E)
            {
                if (selectedVertex != -1 && ctrlVertex != -1)
                    g.setEdge(selectedVertex, ctrlVertex, Graph.SET);
                Refresh();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
