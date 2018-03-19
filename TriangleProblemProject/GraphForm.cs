using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quantum.TriangleProblemProject {
    public partial class GraphForm : Form {
        public PlotView View;

        public GraphForm() {
            InitializeComponent();
            View = new PlotView();
            View.Top = 0;
            View.Left = 0;
            View.Width = ClientSize.Width;
            View.Height = ClientSize.Height;
            View.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            Controls.Add(View);
        }

        private void GraphForm_Load(object sender, EventArgs e) {

        }
    }
}
