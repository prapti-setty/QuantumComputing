namespace Quantum.TriangleProblemProject
{
    partial class Window
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.button1 = new System.Windows.Forms.Button();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.findTriangleButton = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.edgeButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.quantumRadioButton = new System.Windows.Forms.RadioButton();
			this.bruteForceRadioButton = new System.Windows.Forms.RadioButton();
			this.resetButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.traceRadioButton = new System.Windows.Forms.RadioButton();
			this.btnGraph = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.updateGUILab = new System.Windows.Forms.Label();
			this.panel1 = new Quantum.TriangleProblemProject.DoubleBufferedPanel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(849, 4);
			this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(35, 32);
			this.button1.TabIndex = 15;
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// richTextBox1
			// 
			this.richTextBox1.BackColor = System.Drawing.SystemColors.Menu;
			this.richTextBox1.Location = new System.Drawing.Point(439, 364);
			this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(449, 175);
			this.richTextBox1.TabIndex = 14;
			this.richTextBox1.Text = "";
			this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
			// 
			// findTriangleButton
			// 
			this.findTriangleButton.Location = new System.Drawing.Point(13, 353);
			this.findTriangleButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.findTriangleButton.Name = "findTriangleButton";
			this.findTriangleButton.Size = new System.Drawing.Size(121, 38);
			this.findTriangleButton.TabIndex = 1;
			this.findTriangleButton.Text = "Find Triangle";
			this.findTriangleButton.UseVisualStyleBackColor = true;
			this.findTriangleButton.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(115, 63);
			this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(51, 46);
			this.button2.TabIndex = 2;
			this.button2.Text = "V";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label1.Location = new System.Drawing.Point(24, 75);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 20);
			this.label1.TabIndex = 3;
			this.label1.Text = "Vertex";
			// 
			// edgeButton
			// 
			this.edgeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.edgeButton.Location = new System.Drawing.Point(115, 116);
			this.edgeButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.edgeButton.Name = "edgeButton";
			this.edgeButton.Size = new System.Drawing.Size(49, 43);
			this.edgeButton.TabIndex = 4;
			this.edgeButton.Text = "E";
			this.edgeButton.UseVisualStyleBackColor = true;
			this.edgeButton.Click += new System.EventHandler(this.edgeButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label2.Location = new System.Drawing.Point(24, 127);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 20);
			this.label2.TabIndex = 5;
			this.label2.Text = "Edge";
			// 
			// quantumRadioButton
			// 
			this.quantumRadioButton.AutoSize = true;
			this.quantumRadioButton.Location = new System.Drawing.Point(27, 252);
			this.quantumRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.quantumRadioButton.Name = "quantumRadioButton";
			this.quantumRadioButton.Size = new System.Drawing.Size(87, 21);
			this.quantumRadioButton.TabIndex = 6;
			this.quantumRadioButton.TabStop = true;
			this.quantumRadioButton.Text = "Quantum";
			this.quantumRadioButton.UseVisualStyleBackColor = true;
			this.quantumRadioButton.CheckedChanged += new System.EventHandler(this.quantumRadioButton_CheckedChanged);
			// 
			// bruteForceRadioButton
			// 
			this.bruteForceRadioButton.AutoSize = true;
			this.bruteForceRadioButton.Location = new System.Drawing.Point(28, 281);
			this.bruteForceRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.bruteForceRadioButton.Name = "bruteForceRadioButton";
			this.bruteForceRadioButton.Size = new System.Drawing.Size(171, 21);
			this.bruteForceRadioButton.TabIndex = 7;
			this.bruteForceRadioButton.TabStop = true;
			this.bruteForceRadioButton.Text = "Classical - Brute Force";
			this.bruteForceRadioButton.UseVisualStyleBackColor = true;
			// 
			// resetButton
			// 
			this.resetButton.Location = new System.Drawing.Point(27, 166);
			this.resetButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.resetButton.Name = "resetButton";
			this.resetButton.Size = new System.Drawing.Size(137, 34);
			this.resetButton.TabIndex = 8;
			this.resetButton.Text = "Reset";
			this.resetButton.UseVisualStyleBackColor = true;
			this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label3.Location = new System.Drawing.Point(59, 32);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(50, 20);
			this.label3.TabIndex = 9;
			this.label3.Text = "Tools";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label4.Location = new System.Drawing.Point(59, 217);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 20);
			this.label4.TabIndex = 10;
			this.label4.Text = "Run";
			// 
			// traceRadioButton
			// 
			this.traceRadioButton.AutoSize = true;
			this.traceRadioButton.Location = new System.Drawing.Point(28, 310);
			this.traceRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.traceRadioButton.Name = "traceRadioButton";
			this.traceRadioButton.Size = new System.Drawing.Size(134, 21);
			this.traceRadioButton.TabIndex = 11;
			this.traceRadioButton.TabStop = true;
			this.traceRadioButton.Text = "Classical - Trace";
			this.traceRadioButton.UseVisualStyleBackColor = true;
			// 
			// btnGraph
			// 
			this.btnGraph.Location = new System.Drawing.Point(13, 398);
			this.btnGraph.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnGraph.Name = "btnGraph";
			this.btnGraph.Size = new System.Drawing.Size(121, 37);
			this.btnGraph.TabIndex = 12;
			this.btnGraph.Text = "Make Graph";
			this.btnGraph.UseVisualStyleBackColor = true;
			this.btnGraph.Click += new System.EventHandler(this.btnGraph_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(24, 487);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(66, 17);
			this.label6.TabIndex = 0;
			this.label6.Text = "Iterations";
			this.label6.Click += new System.EventHandler(this.label6_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(23, 533);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(61, 17);
			this.label7.TabIndex = 0;
			this.label7.Text = "Repeats";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(93, 484);
			this.textBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(113, 22);
			this.textBox3.TabIndex = 2;
			this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(93, 529);
			this.textBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(116, 22);
			this.textBox4.TabIndex = 2;
			this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.label5.Location = new System.Drawing.Point(25, 449);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(126, 18);
			this.label5.TabIndex = 0;
			this.label5.Text = "Quantum Settings";
			// 
			// updateGUILab
			// 
			this.updateGUILab.AutoSize = true;
			this.updateGUILab.Location = new System.Drawing.Point(1069, 9);
			this.updateGUILab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.updateGUILab.Name = "updateGUILab";
			this.updateGUILab.Size = new System.Drawing.Size(0, 17);
			this.updateGUILab.TabIndex = 13;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.richTextBox1);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Location = new System.Drawing.Point(223, 32);
			this.panel1.Margin = new System.Windows.Forms.Padding(4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(890, 541);
			this.panel1.TabIndex = 0;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick_1);
			// 
			// Window
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1129, 588);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.updateGUILab);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.btnGraph);
			this.Controls.Add(this.traceRadioButton);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.resetButton);
			this.Controls.Add(this.bruteForceRadioButton);
			this.Controls.Add(this.quantumRadioButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.edgeButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.findTriangleButton);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "Window";
			this.Text = "Triangle Finder";
			this.Load += new System.EventHandler(this.Window_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button findTriangleButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button edgeButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton quantumRadioButton;
        private System.Windows.Forms.RadioButton bruteForceRadioButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton traceRadioButton;
        private System.Windows.Forms.RadioButton StrassenRadioButton;
        private System.Windows.Forms.Button btnGraph;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label updateGUILab;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
		private DoubleBufferedPanel panel1;
	}
}