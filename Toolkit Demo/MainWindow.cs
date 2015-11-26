using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toolkit_Demo
{
    public partial class MainWindow : Form
    {
        // Variable used for determining if the user is currently drawing on the canvas
        private Boolean isDrawing;

        /// <summary>
        /// Getter/Setter used for changing the pen colour
        /// </summary>
        private Color colour
        {
            // Simply return the colour panel's background colour
            get { return this.colourPanel.BackColor; }

            // Set the pen colour, then set the colour panel's background colour
            set
            {
                // Set the pen colour
                this.canvas.Colour = value;

                // Set the colour panel's background colour
                this.colourPanel.BackColor = value;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            // Initialize all of the components that you added using the designer
            InitializeComponent();

            // Set the default pen colour to black. This will trigger the setter above!
            this.colour = Color.Black;
            this.isDrawing = false;    // The user is not currently drawing
        }

        /// <summary>
        /// Event handler that is fired when the user clicks the Colour Picker button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colourPickerButton_Click(object sender, EventArgs e)
        {
            // Open the dialog, where the user can choose a colour. Store the 
            // result to be checked later
            DialogResult result = this.colorDialog.ShowDialog();

            // Check to see if the result is valid
            if (result == DialogResult.OK) {
                // Set the pen colour, and colour panel's background colour. This
                // fires the Setter at the top of the file
                this.colour = this.colorDialog.Color;
            }
        }

        /// <summary>
        /// Event handler for when the user clicks with ANY mouse button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            // Only handle left mouse clicks
            if (e.Button == MouseButtons.Left)
            {
                // Set the variable that watches if the user is currently drawing
                this.isDrawing = true;

                // Add a new shape to the canvas, at the point where the user clicked.
                // By default, your mouse coordinates are measured from the upper left 
                // hand corner OF THE WINDOW, not the canvas. So PointToClient converts
                // Those coordinates so that it is relative to the upper left hand corner
                // of the canvas
                this.canvas.AddNewShape(this.canvas.PointToClient(Cursor.Position));
            }
        }

        /// <summary>
        /// Event handler for when the user moves their mouse. This is fired whether the
        /// user is clicking, or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            // Only handle the event if the user is currently drawing (clicking down 
            // on the canvas)
            if (this.isDrawing)
            {
                // Update the current shape with the current coordinates of the mouse.
                // By default, your mouse coordinates are measured from the upper left 
                // hand corner OF THE WINDOW, not the canvas. So PointToClient converts
                // Those coordinates so that it is relative to the upper left hand corner
                // of the canvas
                this.canvas.UpdateCurrentShape(this.canvas.PointToClient(Cursor.Position));
            }
        }

        /// <summary>
        /// Event handler for when the user is already clicking down with any button, 
        /// and they release.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            // Turn off the variable that watches whethere or not the user is 
            // currently drawing
            this.isDrawing = false;
        }

        /// <summary>
        /// Event handler for when the cursor leaves the canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_MouseLeave(object sender, EventArgs e)
        {
            // Turn off the variable that watches whethere or not the user is 
            // currently drawing
            this.isDrawing = false;
        }

        /// <summary>
        /// Event handler for when the user clicks the Clear button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            // Clear all of the shapes from the canvas
            this.canvas.Clear();
        }
    }
}
