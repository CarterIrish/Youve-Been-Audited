using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelEditor
{
    /// <summary>
    /// Chase Collins
    /// Defines the form for editing a level
    /// </summary>
    public partial class EditorForm : Form
    {
        //dimensions of level
        private int width;
        private int height;

        //dimensions of a single tile
        private int tileLength;

        //2d array to hold tiles
        PictureBox[,] map;

        Color currentColor;

        //Is the user in "pathing" mode and can add points to the enemy path
        bool isPathing;

        //Enemy path
        List<Point> enemyPath;

        bool isSaved;

        /// <summary>
        /// Creates and EditorForm with a blank canvas
        /// </summary>
        /// <param name="width">width of the level</param>
        /// <param name="height">height of the level</param>
        public EditorForm(int width, int height)
            : this()
        {

            this.width = width;
            this.height = height;
            tileLength = 450 / height;
            map = new PictureBox[height, width];
            enemyPath = new List<Point>();
            

            //Creates the level canvas in the correct dimensions
            CreateCanvas(width, height);
        }

        /// <summary>
        /// Creates an EditorForm from a pre existing file
        /// </summary>
        /// <param name="fileName">file to load</param>
        public EditorForm(string fileName)
            : this()
        {
            Load(fileName);
        }

        /// <summary>
        /// Creates an EditorForm without a canvas
        /// </summary>
        public EditorForm()
        {
            InitializeComponent();

            isPathing = false;
            currentColor = Color.White;

            //initializes the buttons' click events
            buttonGreen.Click += SelectColor;
            buttonMidnight.Click += SelectColor;
            buttonYellow.Click += SelectColor;
            buttonSky.Click += SelectColor;
            buttonGray.Click += SelectColor;
            buttonWhite.Click += SelectColor;

            buttonSave.Click += SaveFile;
            buttonLoad.Click += LoadFile;

            buttonPathing.Click += PathingMode;

            this.FormClosing += UnsavedChanges;

            isSaved = true;
        }

        /// <summary>
        /// Creates a new canvas with the specified width and height
        /// </summary>
        /// <param name="width">width of canvas</param>
        /// <param name="height">height of canvas</param>
        void CreateCanvas(int width, int height)
        {
            map = new PictureBox[height, width];
            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    map[r, c] = new PictureBox();
                    this.Controls.Add(map[r, c]);
                    map[r, c].Size = new Size(tileLength, tileLength);
                    map[r, c].Location = new Point((c * tileLength) + 115, (r * tileLength) + 100);
                    map[r, c].Click += PaintFloor;
                    map[r, c].BackColor = Color.White;
                }
            }

        }

        /// <summary>
        /// Changes the color of a tile on the map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PaintFloor(object sender, EventArgs e)
        {
            PictureBox tile = (PictureBox)sender;
            tile.BackColor = currentColor;

            //Puts a "*" whenever the file has unsaved changes and changes isSaved to false
            if (isSaved)
            {
                this.Text += "*";
                isSaved = false;
            }
        }

        void PathingMode(object sender, EventArgs e)
        {
            if (isPathing)
            {
                isPathing = false;
                foreach (PictureBox p in map)
                {
                    p.Click -= AddPoint;
                    p.Click += PaintFloor;
                    p.Paint -= PaintPoint;
                }

            }
            else if (!isPathing)
            {
                foreach (PictureBox p in map)
                {
                    p.Click += AddPoint;
                    p.Click -= PaintFloor;
                    p.Paint += PaintPoint;
                }
                isPathing = true;
            }
        }

        /// <summary>
        /// Paints a point in the enemy path
        /// </summary>
        void AddPoint(object sender, EventArgs e)
        {
            PictureBox tile = (PictureBox)sender;
            Point pointLocation = new Point(tile.Location.X + (tile.Width / 2), tile.Location.Y + (tile.Height / 2));
            enemyPath.Add(pointLocation);
            tile.Refresh();
        }

        /// <summary>
        /// Paints a line connecting two points in an enemy path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       /* void PaintLine(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            e.Graphics.DrawLine(pen, 20, 10, 300, 100);
        }*/

        void PaintPoint(object sender, PaintEventArgs e)
        {
            PictureBox tile = (PictureBox)sender;
            SolidBrush myBrush = new SolidBrush(Color.Red);

            Point pointLocation = new Point((tile.Width / 2), (tile.Height / 2));

            e.Graphics.FillEllipse(myBrush, new Rectangle(pointLocation, new Size(15, 15)));
        }

        /// <summary>
        /// Changes the current color that is being drawn with
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SelectColor(object sender, EventArgs e)
        {
            Button color = (Button)sender;
            currentColor = color.BackColor;
            pictureBoxCurrentTile.BackColor = currentColor;

        }

        /// <summary>
        /// Saves the current file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SaveFile(object sender, EventArgs e)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "Level Files|*.level";
            file.Title = "Save a level file.";
            DialogResult result = file.ShowDialog();

            if (result == DialogResult.OK)
            {
                StreamWriter output = new StreamWriter(file.FileName);
                output.WriteLine(width + "," + height); //first line specifies width and height

                //writes a letter to represent each color square on the map
                for (int i = 0; i < height; i++)
                {
                    for (int k = 0; k < width; k++)
                    {
                        if (map[i, k].BackColor == Color.White)
                        {
                            output.Write("w");
                        }
                        else if (map[i, k].BackColor == Color.MidnightBlue)
                        {
                            output.Write("m");
                        }
                        else if (map[i, k].BackColor == Color.Green)
                        {
                            output.Write("g");
                        }
                        else if (map[i, k].BackColor == Color.Gold)
                        {
                            output.Write("y");
                        }
                        else if (map[i, k].BackColor == Color.DarkGray)
                        {
                            output.Write("d");
                        }
                        else if (map[i, k].BackColor == Color.SkyBlue)
                        {
                            output.Write("s");
                        }
                    }
                    output.Write("\n");
                }
                output.Close();
            }

            //If there were unsaved changes, removes the "*" and sets isSaved to true
            if (!isSaved)
            {
                this.Text = this.Text.Substring(0, this.Text.Length - 1);
                isSaved = true;
            }

            MessageBox.Show("File saved successfully", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Brings up the load dialog for the user to choose what file to load
        /// </summary>
        /// <returns>a string containing the name of the file to load</returns>
        string LoadDialog()
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Open a level file.";
            file.Filter = "Level Files|*.level";
            DialogResult result = file.ShowDialog();
            if (result == DialogResult.OK)
            {
                return file.FileName;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Loads a file into the canvas
        /// </summary>
        void Load(string fileName)
        {
            /*OpenFileDialog file = new OpenFileDialog();
            file.Title = "Open a level file.";
            file.Filter = "Level Files|*.level";
            DialogResult result = file.ShowDialog();*/
            if (fileName != null)
            {
                //reads the dimensions of the canvas
                StreamReader input = new StreamReader(fileName);
                string[] dimensions;
                int height;
                int width;
                try
                {
                    dimensions = input.ReadLine().Split(",");
                    width = int.Parse(dimensions[0]);
                    height = int.Parse(dimensions[1]);
                }
                catch (Exception ex)
                {
                    //Shows an error message if loading fails
                    MessageBox.Show("Error: Could not load file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                //disposes the current canvas
                for (int i = 0; i < this.height; i++)
                {
                    for (int k = 0; k < this.width; k++)
                    {
                        map[i, k].Dispose();
                    }
                }

                //sets the form's dimensions to the new dimensions that were read in the file
                this.width = width;
                this.height = height;
                tileLength = 450 / height;

                CreateCanvas(width, height);

                //Reads the file and creates the saved canvas tile by tile
                for (int i = 0; i < height; i++)
                {
                    for (int k = 0; k < width; k++)
                    {
                        switch (input.Read())
                        {
                            case 'w':
                                map[i, k].BackColor = Color.White;
                                break;

                            case 'g':
                                map[i, k].BackColor = Color.Green;
                                break;

                            case 'm':
                                map[i, k].BackColor = Color.MidnightBlue;
                                break;

                            case 'y':
                                map[i, k].BackColor = Color.Gold;
                                break;

                            case 'd':
                                map[i, k].BackColor = Color.DarkGray;
                                break;

                            case 's':
                                map[i, k].BackColor = Color.SkyBlue;
                                break;
                        }
                    }
                    input.ReadLine();
                }
                input.Close();
            }

            //Renames the title bar to the name of the file that was loaded
            this.Text = fileName.Substring(fileName.LastIndexOf("\\") + 1);

            MessageBox.Show("File loaded successfully", "File loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Event for the load file button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LoadFile(object sender, EventArgs e)
        {
            Load(LoadDialog());
        }

        /// <summary>
        /// Asks the user what to do if there are unsaved changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UnsavedChanges(object sender, FormClosingEventArgs e)
        {
            if (!isSaved)
            {
                DialogResult wannaSave = MessageBox.Show("There are unsaved changes. Are you sure you want to quit?", "Unsaved Changes",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (wannaSave != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

    }
}
