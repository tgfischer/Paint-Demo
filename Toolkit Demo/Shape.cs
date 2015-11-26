using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Toolkit_Demo
{
    /// <summary>
    /// Abstract class that each shape will extend. I added this is case you want
    /// to add your own shapes to this program. Try adding a rectangle, or a circle!
    /// To add your own shape, make a new class that extends this class, add your new
    /// shape to the ShapeType enum in Canvas.cs, and then update the 
    /// AddNewShape/UpdateCurrentShape methods in Canvas.cs
    /// </summary>
    abstract class Shape
    {
        // The colour of the shape. No getter/setter since there isn't
        // anything special that you have to do when you get/set the 
        // variable
        public Color Colour;

        public float Thickness;

        /// <summary>
        /// Constructor for creating a new shape
        /// </summary>
        /// <param name="colour">The colour of the shape</param>
        public Shape(Color colour, float thickness)
        {
            // Set the colour of the shape
            this.Colour = colour;

            // Set the thickness of the shape's lines
            this.Thickness = thickness;
        }

        /// <summary>
        /// Any shapes that extend this class MUST have a Draw method.
        /// Otherwise how are we supposed to draw the shape? Each shape
        /// will be drawn differently, so implement this in the extension
        /// class
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        public abstract void Draw(Graphics g, Pen pen);

        /// <summary>
        /// Update the shape using the cursor's current position. Again, 
        /// every shape has to implement this method!
        /// </summary>
        /// <param name="point">The cursor's current position</param>
        public abstract void Update(Point point);
    }
}
