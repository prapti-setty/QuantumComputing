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

using Microsoft.Quantum.Simulation.Simulators;
using Quantum.TriangleProblemProject.ClassicalAlgorithms;

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
            MessageBox.Show(getMString(getHMatrix(1)), "Result");
        }
        private int power(int i,int j)
        {
            int r = 1;
            while (j > 0)
            {
                r = r * i;
                j = j - 1;
            }
            return r;
        }
        private string getMString(int[,] inp)
        {
            string str = "";
            for (int i = 0; i < inp.GetLength(0); i++)
            {
                for (int j = 0; j < inp.GetLength(0); j++)
                    str = str + " " + inp[i, j];
                str = str + "\n";
            }
            return str;
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
            if (quantumRadioButton.Checked)
            {
                getTriangleInGraph(g.getAdjMat());
            }
            else if (bruteForceRadioButton.Checked)
            {
                runBruteForce(g.getAdjMat());
            } else {
                runTrace(g.getAdjMat());
            }

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
        private int[,] getHMatrix(int input)
        {
            int size = power(2, input);
            int[,] returnArr = new int[size, size];
            if(input == 1)
            {
                return baseMatrix;
            }
            else
            {
                int[,] quadrantMat = getHMatrix(input -1);
                for(int i =0;i<returnArr.GetLength(0)/2;i++)
                {
                    for(int j=0;j<returnArr.GetLength(0)/2;j++)
                    {
                        returnArr[i, j] = quadrantMat[i, j];
                        returnArr[i + quadrantMat.GetLength(0), j] = quadrantMat[i, j];
                        returnArr[i, j + quadrantMat.GetLength(0)] = quadrantMat[i, j];
                        returnArr[i + quadrantMat.GetLength(0), j + quadrantMat.GetLength(0)] = quadrantMat[i, j] * -1;
                    }
                }
            }
            return returnArr;
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
        //Converts a 2d array to a usable qarray
        static QArray<QArray<long>> arrToQArray(int[,] input)
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
