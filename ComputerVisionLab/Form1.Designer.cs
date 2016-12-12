namespace ComputerVisionLab
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.effectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeDetectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dilateAndErodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayscaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findContoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.metroTrackBar1 = new MetroFramework.Controls.MetroTrackBar();
            this.metroTrackBar2 = new MetroFramework.Controls.MetroTrackBar();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioRed = new MetroFramework.Controls.MetroRadioButton();
            this.radioGreen = new MetroFramework.Controls.MetroRadioButton();
            this.radioBlue = new MetroFramework.Controls.MetroRadioButton();
            this.radioGray = new MetroFramework.Controls.MetroRadioButton();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.effectsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(643, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.openFileToolStripMenuItem.Text = "Open File...";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // effectsToolStripMenuItem
            // 
            this.effectsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.edgeDetectionToolStripMenuItem,
            this.dilateAndErodeToolStripMenuItem,
            this.grayscaleToolStripMenuItem,
            this.findContoursToolStripMenuItem});
            this.effectsToolStripMenuItem.Name = "effectsToolStripMenuItem";
            this.effectsToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.effectsToolStripMenuItem.Text = "Effects";
            // 
            // edgeDetectionToolStripMenuItem
            // 
            this.edgeDetectionToolStripMenuItem.Name = "edgeDetectionToolStripMenuItem";
            this.edgeDetectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.edgeDetectionToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.edgeDetectionToolStripMenuItem.Text = "Edge Detection";
            this.edgeDetectionToolStripMenuItem.Click += new System.EventHandler(this.edgeDetectionToolStripMenuItem_Click);
            // 
            // dilateAndErodeToolStripMenuItem
            // 
            this.dilateAndErodeToolStripMenuItem.Name = "dilateAndErodeToolStripMenuItem";
            this.dilateAndErodeToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.dilateAndErodeToolStripMenuItem.Text = "Dilate and Erode";
            this.dilateAndErodeToolStripMenuItem.Click += new System.EventHandler(this.dilateAndErodeToolStripMenuItem_Click);
            // 
            // grayscaleToolStripMenuItem
            // 
            this.grayscaleToolStripMenuItem.Name = "grayscaleToolStripMenuItem";
            this.grayscaleToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.grayscaleToolStripMenuItem.Text = "Grayscale";
            this.grayscaleToolStripMenuItem.Click += new System.EventHandler(this.grayscaleToolStripMenuItem_Click);
            // 
            // findContoursToolStripMenuItem
            // 
            this.findContoursToolStripMenuItem.Name = "findContoursToolStripMenuItem";
            this.findContoursToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.findContoursToolStripMenuItem.Text = "Find Contours";
            this.findContoursToolStripMenuItem.Click += new System.EventHandler(this.findContoursToolStripMenuItem_Click);
            // 
            // imageBox1
            // 
            this.imageBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox1.Location = new System.Drawing.Point(13, 154);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(618, 400);
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // metroTrackBar1
            // 
            this.metroTrackBar1.BackColor = System.Drawing.Color.Transparent;
            this.metroTrackBar1.CustomBackground = false;
            this.metroTrackBar1.LargeChange = ((uint)(5u));
            this.metroTrackBar1.Location = new System.Drawing.Point(12, 50);
            this.metroTrackBar1.Maximum = 255;
            this.metroTrackBar1.Minimum = 0;
            this.metroTrackBar1.MouseWheelBarPartitions = 10;
            this.metroTrackBar1.Name = "metroTrackBar1";
            this.metroTrackBar1.Size = new System.Drawing.Size(264, 23);
            this.metroTrackBar1.SmallChange = ((uint)(1u));
            this.metroTrackBar1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTrackBar1.StyleManager = null;
            this.metroTrackBar1.TabIndex = 3;
            this.metroTrackBar1.Text = "metroTrackBar1";
            this.metroTrackBar1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTrackBar1.Value = 20;
            this.metroTrackBar1.ValueChanged += new System.EventHandler(this.metroTrackBar1_ValueChanged);
            this.metroTrackBar1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.metroTrackBar1_MouseUp);
            // 
            // metroTrackBar2
            // 
            this.metroTrackBar2.BackColor = System.Drawing.Color.Transparent;
            this.metroTrackBar2.CustomBackground = false;
            this.metroTrackBar2.LargeChange = ((uint)(5u));
            this.metroTrackBar2.Location = new System.Drawing.Point(12, 107);
            this.metroTrackBar2.Maximum = 255;
            this.metroTrackBar2.Minimum = 0;
            this.metroTrackBar2.MouseWheelBarPartitions = 10;
            this.metroTrackBar2.Name = "metroTrackBar2";
            this.metroTrackBar2.Size = new System.Drawing.Size(264, 23);
            this.metroTrackBar2.SmallChange = ((uint)(1u));
            this.metroTrackBar2.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTrackBar2.StyleManager = null;
            this.metroTrackBar2.TabIndex = 4;
            this.metroTrackBar2.Text = "metroTrackBar2";
            this.metroTrackBar2.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTrackBar2.Value = 70;
            this.metroTrackBar2.ValueChanged += new System.EventHandler(this.metroTrackBar2_ValueChanged);
            this.metroTrackBar2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.metroTrackBar2_MouseUp);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.CustomBackground = false;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.metroLabel1.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabel1.Location = new System.Drawing.Point(12, 28);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(105, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel1.StyleManager = null;
            this.metroLabel1.TabIndex = 5;
            this.metroLabel1.Text = "Lower threshold:";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel1.UseStyleColors = false;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.CustomBackground = false;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.metroLabel2.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabel2.Location = new System.Drawing.Point(12, 85);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(107, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel2.StyleManager = null;
            this.metroLabel2.TabIndex = 5;
            this.metroLabel2.Text = "Upper threshold:";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel2.UseStyleColors = false;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.CustomBackground = false;
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.metroLabel3.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabel3.Location = new System.Drawing.Point(282, 50);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(30, 19);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel3.StyleManager = null;
            this.metroLabel3.TabIndex = 5;
            this.metroLabel3.Text = "255";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel3.UseStyleColors = false;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.CustomBackground = false;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.metroLabel4.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabel4.Location = new System.Drawing.Point(282, 107);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(30, 19);
            this.metroLabel4.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel4.StyleManager = null;
            this.metroLabel4.TabIndex = 5;
            this.metroLabel4.Text = "255";
            this.metroLabel4.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel4.UseStyleColors = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(26, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Astrocytes:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(108, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "0";
            // 
            // radioRed
            // 
            this.radioRed.AutoSize = true;
            this.radioRed.CustomBackground = false;
            this.radioRed.FontSize = MetroFramework.MetroLinkSize.Small;
            this.radioRed.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.radioRed.Location = new System.Drawing.Point(337, 45);
            this.radioRed.Name = "radioRed";
            this.radioRed.Size = new System.Drawing.Size(43, 15);
            this.radioRed.Style = MetroFramework.MetroColorStyle.Red;
            this.radioRed.StyleManager = null;
            this.radioRed.TabIndex = 10;
            this.radioRed.Text = "Red";
            this.radioRed.Theme = MetroFramework.MetroThemeStyle.Light;
            this.radioRed.UseStyleColors = true;
            this.radioRed.UseVisualStyleBackColor = true;
            // 
            // radioGreen
            // 
            this.radioGreen.AutoSize = true;
            this.radioGreen.CustomBackground = false;
            this.radioGreen.FontSize = MetroFramework.MetroLinkSize.Small;
            this.radioGreen.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.radioGreen.Location = new System.Drawing.Point(337, 67);
            this.radioGreen.Name = "radioGreen";
            this.radioGreen.Size = new System.Drawing.Size(54, 15);
            this.radioGreen.Style = MetroFramework.MetroColorStyle.Green;
            this.radioGreen.StyleManager = null;
            this.radioGreen.TabIndex = 11;
            this.radioGreen.Text = "Green";
            this.radioGreen.Theme = MetroFramework.MetroThemeStyle.Light;
            this.radioGreen.UseStyleColors = true;
            this.radioGreen.UseVisualStyleBackColor = true;
            // 
            // radioBlue
            // 
            this.radioBlue.AutoSize = true;
            this.radioBlue.CustomBackground = false;
            this.radioBlue.FontSize = MetroFramework.MetroLinkSize.Small;
            this.radioBlue.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.radioBlue.Location = new System.Drawing.Point(337, 89);
            this.radioBlue.Name = "radioBlue";
            this.radioBlue.Size = new System.Drawing.Size(46, 15);
            this.radioBlue.Style = MetroFramework.MetroColorStyle.Blue;
            this.radioBlue.StyleManager = null;
            this.radioBlue.TabIndex = 12;
            this.radioBlue.Text = "Blue";
            this.radioBlue.Theme = MetroFramework.MetroThemeStyle.Light;
            this.radioBlue.UseStyleColors = true;
            this.radioBlue.UseVisualStyleBackColor = true;
            // 
            // radioGray
            // 
            this.radioGray.AutoSize = true;
            this.radioGray.Checked = true;
            this.radioGray.CustomBackground = false;
            this.radioGray.FontSize = MetroFramework.MetroLinkSize.Small;
            this.radioGray.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.radioGray.Location = new System.Drawing.Point(337, 111);
            this.radioGray.Name = "radioGray";
            this.radioGray.Size = new System.Drawing.Size(47, 15);
            this.radioGray.Style = MetroFramework.MetroColorStyle.Black;
            this.radioGray.StyleManager = null;
            this.radioGray.TabIndex = 13;
            this.radioGray.TabStop = true;
            this.radioGray.Text = "Gray";
            this.radioGray.Theme = MetroFramework.MetroThemeStyle.Light;
            this.radioGray.UseStyleColors = true;
            this.radioGray.UseVisualStyleBackColor = true;
            this.radioGray.CheckedChanged += new System.EventHandler(this.radioGray_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(643, 569);
            this.Controls.Add(this.radioGray);
            this.Controls.Add(this.radioBlue);
            this.Controls.Add(this.radioGreen);
            this.Controls.Add(this.radioRed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.metroTrackBar2);
            this.Controls.Add(this.metroTrackBar1);
            this.Controls.Add(this.imageBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "DetermineAstrocytes";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem effectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edgeDetectionToolStripMenuItem;
        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private MetroFramework.Controls.MetroTrackBar metroTrackBar1;
        private MetroFramework.Controls.MetroTrackBar metroTrackBar2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private System.Windows.Forms.ToolStripMenuItem dilateAndErodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayscaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findContoursToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroRadioButton radioRed;
        private MetroFramework.Controls.MetroRadioButton radioGreen;
        private MetroFramework.Controls.MetroRadioButton radioBlue;
        private MetroFramework.Controls.MetroRadioButton radioGray;
    }
}

