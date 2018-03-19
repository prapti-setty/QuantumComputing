//Window.cs by Isaac Walker
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

namespace Quantum.TriangleProblemProject
{
    public partial class Window : Form
    {
        private Boolean isSelected = false;
        private int selectedVertex, ctrlVertex = -1;
        int[,] baseMatrix = { { 1, 1 }, { 1, -1 } };
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
        }
        private void Window_Load(object sender, EventArgs e)
        {

        }
        private void getTriangleInGraph(int[,] adjMat)
        {
            QArray<QArray<long>> inputArray = arrToQArray(adjMat);
            QuantumSimulator sim = new QuantumSimulator();
            var res = decipher.Run(sim, inputArray).Result;
            int acRes = (int)res;
            MessageBox.Show(acRes.ToString(),"Result");
        }

        private void runBruteForce(int[,] adjMat) {
            BruteForceAlgorithm algorithm = new BruteForceAlgorithm();
            bool result = algorithm.Run(adjMat);
            MessageBox.Show(result.ToString());
        }

        private void runTrace(int[,] adjMat) {
            TraceAlgorithm algorithm = new TraceAlgorithm();
            bool result = algorithm.Run(adjMat);
            MessageBox.Show(result.ToString());
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
            if (quantumRadioButton.Checked)
            {
                getTriangleInGraph(g.getAdjMat());
                Console.WriteLine("Quantum Algorithm");
            }
            else if (bruteForceRadioButton.Checked)
            {
                runBruteForce(g.getAdjMat());
                Console.WriteLine("Brute Force Algorithm");
            } else {
                runTrace(g.getAdjMat());
                Console.WriteLine("Trace Algorithm");
            }
            stopwatch.Stop();
            Console.WriteLine("Time elapsed (ms): {0}", stopwatch.Elapsed.TotalMilliseconds);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //   Refresh();

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
                        e.Graphics.DrawLine(Pens.Black, g.points[i].x + 5, g.points[i].y + 5, g.points[j].x + 5, g.points[j].y + 5);

                }
            }
            for (int i = 0; i < g.points.Count; i++)
            {
                e.Graphics.FillEllipse(g.points[i].brush, new Rectangle(g.points[i].x, g.points[i].y, 10, 10));
                e.Graphics.DrawString(g.points[i].idt, new Font(FontFamily.GenericSansSerif,
            9.0F, FontStyle.Bold), Brushes.Black, g.points[i].x + 8, g.points[i].y + 18);

            }
            if (selectedVertex >= 0)
                e.Graphics.DrawEllipse(Pens.Black, new Rectangle(g.points[selectedVertex].x - 2,
                    g.points[selectedVertex].y - 2, 14, 14));
            if (ctrlVertex >= 0)
                e.Graphics.DrawEllipse(Pens.Black, new Rectangle(g.points[ctrlVertex].x - 2,
                    g.points[ctrlVertex].y - 2, 14, 14));
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

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.E)
                if (ctrlVertex >= 0 && selectedVertex >= 0)
                    g.setEdge(ctrlVertex, selectedVertex, 1);
            Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.E || e.KeyData == Keys.E || e.KeyValue == (char)Keys.E)
                if (ctrlVertex >= 0 && selectedVertex >= 0)
                    g.setEdge(ctrlVertex, selectedVertex, 1);
            Refresh();

        }

        private int setSelectedVertex(int x, int y)
        {
            for (int i = 0; i < g.points.Count; i++)
            {
                Rectangle rect = new Rectangle(g.points[i].x, g.points[i].y, 10, 10);
                if (rect.Contains(x, y))
                    return i;
            }

            return -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isSelected = true;
        }

        private void quantumRadioButton_CheckedChanged(object sender, EventArgs e)
        {
       
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            selectedVertex = -1;
            ctrlVertex = -1;
            g = new Graph();
            Refresh();
        }

        private class AlgorithmResults {
            public int Repetitions = 1;
            public IClassicalAlgorithm Algorithm;
            public double MaxTime;
            public List<double> Times = new List<double>();

            public AlgorithmResults(IClassicalAlgorithm algorithm) {
                Algorithm = algorithm;
            }

            public void AddTime(double time) {
                Times.Add(time);
                MaxTime = Math.Max(MaxTime, time);
            }
        }

        private void btnGraph_Click(object sender, EventArgs e) {
            var myModel = new PlotModel { Title = "Results" };
            Dictionary<int, int[,]> matrices = new Dictionary<int, int[,]>();
            int minVertices = 100;
            int maxVertices = 500;
            int verticesGap = 100;
            for (int i = minVertices; i <= maxVertices; i += verticesGap) {
                int[,] matrix = new int[i, i];
                for (int j = 0; j < i; j++) {
                    for (int k = j + 1; k < i; k++) {
                        if (randGen.NextDouble() < 0.2) {
                            matrix[j, k] = 1;
                            matrix[k, j] = 1;
                        }
                    }
                }

                matrices[i] = matrix;
            }

            List<AlgorithmResults> algorithms = new List<AlgorithmResults> {
                // Repeat brute force a bunch of times, else its times are too small.
                new AlgorithmResults(new BruteForceAlgorithm()) { Repetitions = 10000 },
                new AlgorithmResults(new TraceAlgorithm()),
                new AlgorithmResults(new QuantumAlgorithm()),
            };

            foreach (var algorithm in algorithms) {
                for (int vertices = minVertices; vertices <= maxVertices; vertices += verticesGap) {
                    var matrix = matrices[vertices];
                    Stopwatch watch = Stopwatch.StartNew();
                    for (int i = 0; i < algorithm.Repetitions; i++) {
                        algorithm.Algorithm.Run(matrix);
                    }
                    algorithm.AddTime(watch.ElapsedMilliseconds);
                }
            }

            foreach (var algorithm in algorithms) {
                myModel.Series.Add(new FunctionSeries((x => {
                    int timeIndex = (int)(x - minVertices) / verticesGap;
                    Console.WriteLine(algorithm.MaxTime);
                    return algorithm.Times[timeIndex] / algorithm.MaxTime;
                }), minVertices, maxVertices, (double)verticesGap, algorithm.Algorithm.Name));
            }
            GraphForm graphForm = new GraphForm();
            graphForm.View.Model = myModel;
            graphForm.Show();
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
    }

}
