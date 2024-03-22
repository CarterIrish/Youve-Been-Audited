namespace LevelEditor
{
    /// <summary>
    /// Chase Collins
    /// Defines the initial windows form for creating or loading a Level Editor
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            buttonCreateMap.Click += CreateMap;
            buttonLoad.Click += LoadMap;
        }

        /// <summary>
        /// Creates a new map
        /// Checks to make sure the incoming dimensions are valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CreateMap(object sender, EventArgs e)
        {
            int width;
            int height;

            //if stuff in the text boxes aren't ints, game just doesn't create
            if (!int.TryParse(textBoxWidth.Text, out width) ||
            !int.TryParse(textBoxHeight.Text, out height))
            {
                return;
            }

            //Keeps track of all errors that happen due to invalid ranges for dimensions
            string errors = "";

            if (width < 10)
            {
                errors += "- Width too small. Minimum is 10.\n";
            }
            else if (width > 30)
            {
                errors += "- Width too large. Maximum is 30\n";
            }

            if (height < 10)
            {
                errors += "- Height too small. Minimum is 10.";
            }
            else if (height > 30)
            {
                errors += "- Height is too large. Maximum is 30.";
            }

            //shows message box if there are any errors
            if (errors != "")
            {
                MessageBox.Show(errors, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                EditorForm newForm = new EditorForm(width, height);
                newForm.Show();
            }

        }

        /// <summary>
        /// Loads a previously saved map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LoadMap(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open a level file.";
            ofd.Filter = "Level Files|*.level";
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                //LOAD FILE
                EditorForm newForm = new EditorForm(ofd.FileName);
                newForm.Show();
            }
            else
            {
                return;
            }
        }

    }
}