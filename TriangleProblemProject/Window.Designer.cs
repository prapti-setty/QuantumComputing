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
            this.panel1 = new System.Windows.Forms.Panel();
            this.findTriangleButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.edgeButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.quantumRadioButton = new System.Windows.Forms.RadioButton();
            this.classicalRadioButton = new System.Windows.Forms.RadioButton();
            this.resetButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Location = new System.Drawing.Point(128, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(381, 281);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick_1);
            // 
            // findTriangleButton
            // 
            this.findTriangleButton.Location = new System.Drawing.Point(12, 260);
            this.findTriangleButton.Name = "findTriangleButton";
            this.findTriangleButton.Size = new System.Drawing.Size(91, 31);
            this.findTriangleButton.TabIndex = 1;
            this.findTriangleButton.Text = "Find Triangle";
            this.findTriangleButton.UseVisualStyleBackColor = true;
            this.findTriangleButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(72, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(38, 37);
            this.button2.TabIndex = 2;
            this.button2.Text = "V";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(18, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Vertex";
            // 
            // edgeButton
            // 
            this.edgeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.edgeButton.Location = new System.Drawing.Point(72, 94);
            this.edgeButton.Name = "edgeButton";
            this.edgeButton.Size = new System.Drawing.Size(37, 35);
            this.edgeButton.TabIndex = 4;
            this.edgeButton.Text = "E";
            this.edgeButton.UseVisualStyleBackColor = true;
            this.edgeButton.Click += new System.EventHandler(this.edgeButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(18, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Edge";
            // 
            // quantumRadioButton
            // 
            this.quantumRadioButton.AutoSize = true;
            this.quantumRadioButton.Location = new System.Drawing.Point(20, 205);
            this.quantumRadioButton.Name = "quantumRadioButton";
            this.quantumRadioButton.Size = new System.Drawing.Size(68, 17);
            this.quantumRadioButton.TabIndex = 6;
            this.quantumRadioButton.TabStop = true;
            this.quantumRadioButton.Text = "Quantum";
            this.quantumRadioButton.UseVisualStyleBackColor = true;
            this.quantumRadioButton.CheckedChanged += new System.EventHandler(this.quantumRadioButton_CheckedChanged);
            // 
            // classicalRadioButton
            // 
            this.classicalRadioButton.AutoSize = true;
            this.classicalRadioButton.Location = new System.Drawing.Point(21, 228);
            this.classicalRadioButton.Name = "classicalRadioButton";
            this.classicalRadioButton.Size = new System.Drawing.Size(66, 17);
            this.classicalRadioButton.TabIndex = 7;
            this.classicalRadioButton.TabStop = true;
            this.classicalRadioButton.Text = "Classical";
            this.classicalRadioButton.UseVisualStyleBackColor = true;
            this.classicalRadioButton.CheckedChanged += new System.EventHandler(this.classicalRadioButton_CheckedChanged);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(20, 135);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(89, 23);
            this.resetButton.TabIndex = 8;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(44, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Tools";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(44, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Run";
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 316);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.classicalRadioButton);
            this.Controls.Add(this.quantumRadioButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edgeButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.findTriangleButton);
            this.Controls.Add(this.panel1);
            this.Name = "Window";
            this.Text = "Triangle Finder";
            this.Load += new System.EventHandler(this.Window_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button findTriangleButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button edgeButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton quantumRadioButton;
        private System.Windows.Forms.RadioButton classicalRadioButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}