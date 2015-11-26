using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit_Demo
{
    class FreeHand : Shape
    {
        // A list of points that will make up our line
        public List<Point> Points;

        /// <summary>
        /// Constructor for creating a new free hand line
        /// </summary>
        /// <param name="startPoint">The starting point of the line</param>
        /// <param name="colour">The colour of the line</param>
        public FreeHand(Point startPoint, Color colour, float thickness) 
            : base(colour, thickness) // Call the base class's constructor
        {
            // Initialize the list of points with the start point
            this.Points = new List<Point>(new Point[] { startPoint });
        }

        /// <summary>
        /// Override the Draw method that is defined in the Shape class
        /// </summary>
        /// <param name="g">The graphics object for displaying graphics on the screen</param>
        /// <param name="pen">The pen that will be used to draw the shape</param>
        public override void Draw(Graphics g, Pen pen)
        {
            // Check to see if there is only 1 point in the list. DrawLines
            // requires at least two points!
            if (this.Points.Count > 2)
            {
                // Draw the free hand line using the list of points that make up the shape
                // Use a lambda expression to return every second item in the list, to 
                // improve performance
                g.DrawLines(pen, this.Points.Where((x, i) => i % 2 == 0).ToArray());
            }
            // This case is when there is only 1 
            else
            {
                // using statement is used to prevent memory leaks!
                // Create a new brush to draw your point
                using (Brush brush = new SolidBrush(this.Colour))
                {
                    // Draw one single pixel on the screen
                    g.FillRectangle(brush, this.Points.First().X, this.Points.First().Y, 1, 1);
                }
            }
        }

        /// <summary>
        /// Update the shape with the current point. For this shape, all we have
        /// to do is add the point to the list
        /// </summary>
        /// <param name="point"></param>
        public override void Update(Point point)
        {
            // Add the point to the list
            this.Points.Add(point);
        }
    }
}
