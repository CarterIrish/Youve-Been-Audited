﻿namespace LevelEditor
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
            buttonMidnight = new Button();
            buttonGreen = new Button();
            buttonSave = new Button();
            pictureBoxCurrentTile = new PictureBox();
            buttonLoad = new Button();
            labelTileSelector = new Label();
            labelCurrentTile = new Label();
            buttonPathing = new Button();
            textBoxStartEnemies = new TextBox();
            labelStartEnemies = new Label();
            textBoxNumWaves = new TextBox();
            labelNumWaves = new Label();
            labelEnemyMultiplier = new Label();
            textBoxEnemyMultiplier = new TextBox();
            buttonPlayerSpawn = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCurrentTile).BeginInit();
            SuspendLayout();
            // 
            // buttonMidnight
            // 
            buttonMidnight.BackColor = Color.Black;
            buttonMidnight.Location = new Point(17, 110);
            buttonMidnight.Margin = new Padding(4, 5, 4, 5);
            buttonMidnight.Name = "buttonMidnight";
            buttonMidnight.Size = new Size(34, 38);
            buttonMidnight.TabIndex = 2;
            buttonMidnight.UseVisualStyleBackColor = false;
            // 
            // buttonGreen
            // 
            buttonGreen.BackColor = Color.Green;
            buttonGreen.Image = Properties.Resources.tile_wood_floor;
            buttonGreen.Location = new Point(17, 62);
            buttonGreen.Margin = new Padding(4, 5, 4, 5);
            buttonGreen.Name = "buttonGreen";
            buttonGreen.Size = new Size(34, 38);
            buttonGreen.TabIndex = 0;
            buttonGreen.UseVisualStyleBackColor = false;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(13, 542);
            buttonSave.Margin = new Padding(4, 5, 4, 5);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(120, 128);
            buttonSave.TabIndex = 3;
            buttonSave.Text = "Save File";
            buttonSave.UseVisualStyleBackColor = true;
            // 
            // pictureBoxCurrentTile
            // 
            pictureBoxCurrentTile.Location = new Point(17, 255);
            pictureBoxCurrentTile.Margin = new Padding(4, 5, 4, 5);
            pictureBoxCurrentTile.Name = "pictureBoxCurrentTile";
            pictureBoxCurrentTile.Size = new Size(120, 127);
            pictureBoxCurrentTile.TabIndex = 6;
            pictureBoxCurrentTile.TabStop = false;
            // 
            // buttonLoad
            // 
            buttonLoad.Location = new Point(13, 680);
            buttonLoad.Margin = new Padding(4, 5, 4, 5);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(120, 128);
            buttonLoad.TabIndex = 7;
            buttonLoad.Text = "Load File";
            buttonLoad.UseVisualStyleBackColor = true;
            // 
            // labelTileSelector
            // 
            labelTileSelector.AutoSize = true;
            labelTileSelector.Location = new Point(17, 32);
            labelTileSelector.Margin = new Padding(4, 0, 4, 0);
            labelTileSelector.Name = "labelTileSelector";
            labelTileSelector.Size = new Size(106, 25);
            labelTileSelector.TabIndex = 8;
            labelTileSelector.Text = "Tile Selector";
            // 
            // labelCurrentTile
            // 
            labelCurrentTile.AutoSize = true;
            labelCurrentTile.Location = new Point(17, 205);
            labelCurrentTile.Margin = new Padding(4, 0, 4, 0);
            labelCurrentTile.Name = "labelCurrentTile";
            labelCurrentTile.Size = new Size(105, 25);
            labelCurrentTile.TabIndex = 9;
            labelCurrentTile.Text = "Current Tile:";
            // 
            // buttonPathing
            // 
            buttonPathing.Location = new Point(172, 19);
            buttonPathing.Margin = new Padding(4, 5, 4, 5);
            buttonPathing.Name = "buttonPathing";
            buttonPathing.Size = new Size(107, 38);
            buttonPathing.TabIndex = 10;
            buttonPathing.Text = "Pathing";
            buttonPathing.UseVisualStyleBackColor = true;
            // 
            // textBoxStartEnemies
            // 
            textBoxStartEnemies.Location = new Point(769, 20);
            textBoxStartEnemies.Name = "textBoxStartEnemies";
            textBoxStartEnemies.Size = new Size(70, 31);
            textBoxStartEnemies.TabIndex = 11;
            // 
            // labelStartEnemies
            // 
            labelStartEnemies.AutoSize = true;
            labelStartEnemies.Location = new Point(627, 23);
            labelStartEnemies.Name = "labelStartEnemies";
            labelStartEnemies.Size = new Size(147, 25);
            labelStartEnemies.TabIndex = 12;
            labelStartEnemies.Text = "Starting Enemies:";
            // 
            // textBoxNumWaves
            // 
            textBoxNumWaves.Location = new Point(1081, 20);
            textBoxNumWaves.Name = "textBoxNumWaves";
            textBoxNumWaves.Size = new Size(70, 31);
            textBoxNumWaves.TabIndex = 13;
            // 
            // labelNumWaves
            // 
            labelNumWaves.AutoSize = true;
            labelNumWaves.Location = new Point(926, 23);
            labelNumWaves.Name = "labelNumWaves";
            labelNumWaves.Size = new Size(159, 25);
            labelNumWaves.TabIndex = 14;
            labelNumWaves.Text = "Number of Waves:";
            // 
            // labelEnemyMultiplier
            // 
            labelEnemyMultiplier.AutoSize = true;
            labelEnemyMultiplier.Location = new Point(1193, 26);
            labelEnemyMultiplier.Name = "labelEnemyMultiplier";
            labelEnemyMultiplier.Size = new Size(148, 25);
            labelEnemyMultiplier.TabIndex = 15;
            labelEnemyMultiplier.Text = "Enemy Multiplier:";
            // 
            // textBoxEnemyMultiplier
            // 
            textBoxEnemyMultiplier.Location = new Point(1337, 23);
            textBoxEnemyMultiplier.Name = "textBoxEnemyMultiplier";
            textBoxEnemyMultiplier.Size = new Size(70, 31);
            textBoxEnemyMultiplier.TabIndex = 16;
            // 
            // buttonPlayerSpawn
            // 
            buttonPlayerSpawn.Location = new Point(299, 21);
            buttonPlayerSpawn.Name = "buttonPlayerSpawn";
            buttonPlayerSpawn.Size = new Size(268, 34);
            buttonPlayerSpawn.TabIndex = 17;
            buttonPlayerSpawn.Text = "Player Spawn Placer";
            buttonPlayerSpawn.UseVisualStyleBackColor = true;
            // 
            // EditorForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1679, 878);
            Controls.Add(buttonPlayerSpawn);
            Controls.Add(textBoxEnemyMultiplier);
            Controls.Add(labelEnemyMultiplier);
            Controls.Add(labelNumWaves);
            Controls.Add(textBoxNumWaves);
            Controls.Add(labelStartEnemies);
            Controls.Add(textBoxStartEnemies);
            Controls.Add(buttonPathing);
            Controls.Add(labelCurrentTile);
            Controls.Add(labelTileSelector);
            Controls.Add(buttonLoad);
            Controls.Add(pictureBoxCurrentTile);
            Controls.Add(buttonSave);
            Controls.Add(buttonMidnight);
            Controls.Add(buttonGreen);
            Margin = new Padding(4, 5, 4, 5);
            Name = "EditorForm";
            Text = "EditorForm";
            ((System.ComponentModel.ISupportInitialize)pictureBoxCurrentTile).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button buttonGreen;
        private Button buttonMidnight;
        private Button buttonSave;
        private PictureBox pictureBoxCurrentTile;
        private Button buttonLoad;
        private Label labelTileSelector;
        private Label labelCurrentTile;
        private Button buttonPathing;
        private PictureBox pictureBox1;
        private TextBox textBoxStartEnemies;
        private Label labelStartEnemies;
        private TextBox textBoxNumWaves;
        private Label labelNumWaves;
        private Label labelEnemyMultiplier;
        private TextBox textBoxEnemyMultiplier;
        private Button buttonPlayerSpawn;
    }
}