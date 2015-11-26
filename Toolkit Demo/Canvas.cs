using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toolkit_Demo
{
    public partial class Canvas : Panel
    {
        // Store a list of shapes to draw on the canvas
        private List<Shape> shapes;

        // The current pen colour. Each new shape will be the same colour as the pen
        public Color Colour;

        // The current thickness that the shape should be
        public float Thickness;

        // The current drawing mode. I added this so that you can try to add your own
        // shapes to this program! Try adding a rectangle, or a circle! To add your 
        // own shape, make a new class that extends the Shape class, add your new shape 
        // to the ShapeType enum below, and then update the AddNewShape/UpdateCurrentShape 
        // at the bottom of the file
        public ShapeType Mode;

        /// <summary>
        /// A list of valid shapes. It is convention to only use upper case
        /// </summary>
        public enum ShapeType
        {
            FREE_HAND
            // If you want to add a new shape, add a new value here!
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Canvas()
        {
            // Initialize any other components that belong to the canvas
            InitializeComponent();

            this.shapes = new List<Shape>(); // Initialize the list of shapes
            this.Mode = ShapeType.FREE_HAND; // Default shape type is free hand mode

            // The thickess of the current shape. The default value is 3F (F converts 
            // integer to float type). I'll leave it to you to give the user the ability
            // to change this value
            this.Thickness = 1F;
 
            // This prevents flickering when you draw. Try commenting out this line to
            // see what happens!
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// Overriden event handler that redraws the graphics on the canvas
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            // Call the base class's implementation
            base.OnPaint(pe);

            // This makes the shapes nice and smooth. Try commenting out this line
            // to see what happens!
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Iterate through all of the shapes in the shapes list, drawing each one
            foreach (Shape shape in this.shapes)
            {
                // Create a new pen when this shape's colour, so that you could draw
                // the shape
                using (Pen pen = new Pen(shape.Colour, shape.Thickness))
                {
                    // Call this shape's draw method to display in on the screen
                    shape.Draw(pe.Graphics, pen);
                }
            }
        }

        /// <summary>
        /// Clear the canvas
        /// </summary>
        public void Clear()
        {
            this.shapes.Clear(); // Remove all of the shapes in the shapes list
            this.Invalidate(); // Update the canvas
        }

        /// <summary>
        /// Add a new shape to the canvas
        /// </summary>
        /// <param name="point">The start point of the new shape</param>
        public void AddNewShape(Point point)
        {
            // Create a new shape on the canvas, depending on what mode the user is in
            switch(this.Mode)
            {
                // If the current mode is set to free hand drawing
                case ShapeType.FREE_HAND:
                    // Add a new free hand line to the list of shapes, using the current
                    // colour and thickness the user has specified
                    this.shapes.Add(new FreeHand(point, this.Colour, this.Thickness));
                    break;
                // If you want to add more shapes, add more case statements here!
                default:
                    break;
            }

            this.Invalidate(); // Update the canvas
        }

        /// <summary>
        /// Update the current shape using the cursor's current position
        /// </summary>
        /// <param name="point"></param>
        public void UpdateCurrentShape(Point point)
        {
            // Get the most recent object from the list. That will always be 
            // the last shape that you added to the list
            this.shapes.Last().Update(point);
            this.Invalidate(); // Update the canvas
        }
    }
}
