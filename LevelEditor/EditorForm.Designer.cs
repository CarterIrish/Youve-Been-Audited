namespace LevelEditor
{
    partial class EditorForm
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
            buttonSky = new Button();
            buttonYellow = new Button();
            buttonMidnight = new Button();
            buttonWhite = new Button();
            buttonGreen = new Button();
            buttonSave = new Button();
            buttonGray = new Button();
            pictureBoxCurrentTile = new PictureBox();
            buttonLoad = new Button();
            labelTileSelector = new Label();
            labelCurrentTile = new Label();
            buttonPathing = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCurrentTile).BeginInit();
            SuspendLayout();
            // 
            // buttonSky
            // 
            buttonSky.BackColor = Color.SkyBlue;
            buttonSky.Location = new Point(42, 37);
            buttonSky.Name = "buttonSky";
            buttonSky.Size = new Size(24, 23);
            buttonSky.TabIndex = 4;
            buttonSky.UseVisualStyleBackColor = false;
            // 
            // buttonYellow
            // 
            buttonYellow.BackColor = Color.Gold;
            buttonYellow.Location = new Point(72, 37);
            buttonYellow.Name = "buttonYellow";
            buttonYellow.Size = new Size(24, 23);
            buttonYellow.TabIndex = 3;
            buttonYellow.UseVisualStyleBackColor = false;
            // 
            // buttonMidnight
            // 
            buttonMidnight.BackColor = Color.MidnightBlue;
            buttonMidnight.Location = new Point(12, 66);
            buttonMidnight.Name = "buttonMidnight";
            buttonMidnight.Size = new Size(24, 23);
            buttonMidnight.TabIndex = 2;
            buttonMidnight.UseVisualStyleBackColor = false;
            // 
            // buttonWhite
            // 
            buttonWhite.BackColor = Color.White;
            buttonWhite.Location = new Point(42, 66);
            buttonWhite.Name = "buttonWhite";
            buttonWhite.Size = new Size(24, 23);
            buttonWhite.TabIndex = 1;
            buttonWhite.UseVisualStyleBackColor = false;
            // 
            // buttonGreen
            // 
            buttonGreen.BackColor = Color.Green;
            buttonGreen.Location = new Point(12, 37);
            buttonGreen.Name = "buttonGreen";
            buttonGreen.Size = new Size(24, 23);
            buttonGreen.TabIndex = 0;
            buttonGreen.UseVisualStyleBackColor = false;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(12, 305);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(84, 77);
            buttonSave.TabIndex = 3;
            buttonSave.Text = "Save File";
            buttonSave.UseVisualStyleBackColor = true;
            // 
            // buttonGray
            // 
            buttonGray.BackColor = Color.DarkGray;
            buttonGray.Location = new Point(72, 66);
            buttonGray.Name = "buttonGray";
            buttonGray.Size = new Size(24, 23);
            buttonGray.TabIndex = 5;
            buttonGray.UseVisualStyleBackColor = false;
            // 
            // pictureBoxCurrentTile
            // 
            pictureBoxCurrentTile.Location = new Point(12, 153);
            pictureBoxCurrentTile.Name = "pictureBoxCurrentTile";
            pictureBoxCurrentTile.Size = new Size(84, 76);
            pictureBoxCurrentTile.TabIndex = 6;
            pictureBoxCurrentTile.TabStop = false;
            // 
            // buttonLoad
            // 
            buttonLoad.Location = new Point(12, 388);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(84, 77);
            buttonLoad.TabIndex = 7;
            buttonLoad.Text = "Load File";
            buttonLoad.UseVisualStyleBackColor = true;
            // 
            // labelTileSelector
            // 
            labelTileSelector.AutoSize = true;
            labelTileSelector.Location = new Point(12, 19);
            labelTileSelector.Name = "labelTileSelector";
            labelTileSelector.Size = new Size(70, 15);
            labelTileSelector.TabIndex = 8;
            labelTileSelector.Text = "Tile Selector";
            // 
            // labelCurrentTile
            // 
            labelCurrentTile.AutoSize = true;
            labelCurrentTile.Location = new Point(12, 123);
            labelCurrentTile.Name = "labelCurrentTile";
            labelCurrentTile.Size = new Size(71, 15);
            labelCurrentTile.TabIndex = 9;
            labelCurrentTile.Text = "Current Tile:";
            // 
            // buttonPathing
            // 
            buttonPathing.Location = new Point(122, 37);
            buttonPathing.Name = "buttonPathing";
            buttonPathing.Size = new Size(75, 23);
            buttonPathing.TabIndex = 10;
            buttonPathing.Text = "Pathing";
            buttonPathing.UseVisualStyleBackColor = true;
            // 
            // EditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1175, 527);
            Controls.Add(buttonPathing);
            Controls.Add(labelCurrentTile);
            Controls.Add(labelTileSelector);
            Controls.Add(buttonLoad);
            Controls.Add(pictureBoxCurrentTile);
            Controls.Add(buttonGray);
            Controls.Add(buttonSave);
            Controls.Add(buttonWhite);
            Controls.Add(buttonMidnight);
            Controls.Add(buttonYellow);
            Controls.Add(buttonSky);
            Controls.Add(buttonGreen);
            Name = "EditorForm";
            Text = "EditorForm";
            ((System.ComponentModel.ISupportInitialize)pictureBoxCurrentTile).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button buttonGreen;
        private Button buttonSky;
        private Button buttonYellow;
        private Button buttonMidnight;
        private Button buttonWhite;
        private Button buttonSave;
        private Button buttonGray;
        private PictureBox pictureBoxCurrentTile;
        private Button buttonLoad;
        private Label labelTileSelector;
        private Label labelCurrentTile;
        private Button buttonPathing;
    }
}