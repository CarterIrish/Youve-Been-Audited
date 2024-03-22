namespace LevelEditor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            buttonCreateMap = new Button();
            textBoxHeight = new TextBox();
            textBoxWidth = new TextBox();
            labelHeight = new Label();
            labelWidth = new Label();
            labelNewMap = new Label();
            buttonLoad = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.Control;
            groupBox1.Controls.Add(buttonCreateMap);
            groupBox1.Controls.Add(textBoxHeight);
            groupBox1.Controls.Add(textBoxWidth);
            groupBox1.Controls.Add(labelHeight);
            groupBox1.Controls.Add(labelWidth);
            groupBox1.Controls.Add(labelNewMap);
            groupBox1.Location = new Point(40, 74);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 198);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // buttonCreateMap
            // 
            buttonCreateMap.Location = new Point(38, 117);
            buttonCreateMap.Name = "buttonCreateMap";
            buttonCreateMap.Size = new Size(129, 46);
            buttonCreateMap.TabIndex = 7;
            buttonCreateMap.Text = "Create Map";
            buttonCreateMap.UseVisualStyleBackColor = true;
            // 
            // textBoxHeight
            // 
            textBoxHeight.Location = new Point(74, 68);
            textBoxHeight.Name = "textBoxHeight";
            textBoxHeight.Size = new Size(100, 23);
            textBoxHeight.TabIndex = 6;
            // 
            // textBoxWidth
            // 
            textBoxWidth.Location = new Point(74, 31);
            textBoxWidth.Name = "textBoxWidth";
            textBoxWidth.Size = new Size(100, 23);
            textBoxWidth.TabIndex = 5;
            // 
            // labelHeight
            // 
            labelHeight.AutoSize = true;
            labelHeight.Location = new Point(25, 71);
            labelHeight.Name = "labelHeight";
            labelHeight.Size = new Size(43, 15);
            labelHeight.TabIndex = 4;
            labelHeight.Text = "Height";
            // 
            // labelWidth
            // 
            labelWidth.AutoSize = true;
            labelWidth.Location = new Point(25, 34);
            labelWidth.Name = "labelWidth";
            labelWidth.Size = new Size(39, 15);
            labelWidth.TabIndex = 3;
            labelWidth.Text = "Width";
            // 
            // labelNewMap
            // 
            labelNewMap.AutoSize = true;
            labelNewMap.Location = new Point(0, 0);
            labelNewMap.Name = "labelNewMap";
            labelNewMap.Size = new Size(95, 15);
            labelNewMap.TabIndex = 2;
            labelNewMap.Text = "Create New Map";
            // 
            // buttonLoad
            // 
            buttonLoad.Location = new Point(78, 12);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(129, 46);
            buttonLoad.TabIndex = 1;
            buttonLoad.Text = "Load Map";
            buttonLoad.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(281, 299);
            Controls.Add(buttonLoad);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Level Editor";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button buttonLoad;
        private Label labelNewMap;
        private TextBox textBoxHeight;
        private TextBox textBoxWidth;
        private Label labelHeight;
        private Label labelWidth;
        private Button buttonCreateMap;
    }
}