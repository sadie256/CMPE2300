using GDIDrawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab01_SadieP
{
    internal class Package
    {
        private static Random _rnd = new Random();

        private List<Point> _lines;

        private int _id;

        public int Id { get { return _id; } }

        public Color _color { get; set; }

        public Point Location { get; set; }

        /// <summary>
        /// Parses a string to initialize the package’s ID, color, and polygon points.
        /// Throws exceptions on bad input or parsing errors
        /// </summary>
        /// <param name="packageString">string describing the package parameters</param>
        public Package(string packageString)
        {

        }

        /// <summary>
        /// checks if this package intersects with another package
        /// </summary>
        /// <param name="package">the package this package is being checked against</param>
        /// <returns>true if they intersect, false if they don't</returns>
        public bool Intersects (Package package)
        {
            for (int i = 0; i < _lines.Count; i++)
            {
                Point aStart = _lines[i]; //the start of the line segment being checked
                Point aEnd; //end of the line segment being checked

                //if the start is the last point of _lines, set the end point to the first point of _lines
                if (i == _lines.Count  - 1)
                    aEnd = _lines[0];
                else
                    aEnd = _lines[i + 1];

                for (int j = 0; j < package._lines.Count; j++)
                {
                    Point bStart = package._lines[i]; //the start of the line segment being checked
                    Point bEnd; //end of the line segment being checked

                    //if the start is the last point of package._lines, set the end point to the first point of package._lines
                    if (j == package._lines.Count - 1)
                        bEnd = package._lines[0];
                    else
                        bEnd = package._lines[j + 1];
                    
                    //if the lines intersect, return true because the package intersect
                    if (LineIntersects(aStart, aEnd, bStart, bEnd))
                        return true;
                }
            }
            //if every line in this package doesn't intersect with every line in the given package, return false because they don't intersect
            return false;
        }



        /* CITATION ICA04:
         *
         * Code for this method was copied from ICA04 and tweaked to fit this method
         * 
         * Original code below:
         * 
         *  // Denominators for the 'u_a' and 'u_b' parameters (derived from parametric equations)
         *  double denominator = (line._end.Y - line._start.Y) * (_end.X - _start.X) - (line._end.X - line._start.X) * (_end.Y - _start.Y);
         *
         *  // If the denominator is 0, the lines are parallel or collinear
         *  if (Math.Abs(denominator) < double.Epsilon)
         *  {
         *      // For parallel lines, you may want additional checks for collinear overlap
         *      return false;
         *  }
         *
         *  double numerator1 = (line._end.X - line._start.X) * (_start.Y - line._start.Y) - (line._end.Y - line._start.Y) * (_start.X - line._start.X);
         *  double numerator2 = (_end.X - _start.X) * (_start.Y - line._start.Y) - (_end.Y - _start.Y) * (_start.X - line._start.X);
         *
         *  // Calculate the parametric values for the intersection point
         *  double u_a = numerator1 / denominator;
         *  double u_b = numerator2 / denominator;
         *
         *  // The segments intersect only if both 'u_a' and 'u_b' are between 0 and 1 (inclusive of endpoints)
         *  if (u_a >= 0 && u_a <= 1 && u_b >= 0 && u_b <= 1)
         *  {
         *      return true; // The segments intersect
         *  }
         *
         *  return false; // No intersection within the segments
         */

        /// <summary>
        /// checks if two lines, represented by start and end points, intersect
        /// </summary>
        /// <param name="LineAStart">Start of line A</param>
        /// <param name="LineAEnd">End of Line A</param>
        /// <param name="LineBStart">Start of Line B</param>
        /// <param name="LineBEnd">end of line B</param>
        /// <returns>true if the lines intersect</returns>
        static private bool LineIntersects(Point LineAStart, Point LineAEnd, Point LineBStart, Point LineBEnd)
        {
            // Denominators for the 'u_a' and 'u_b' parameters (derived from parametric equations)
            double denominator = (LineBEnd.Y - LineBStart.Y) * (LineAEnd.X - LineAStart.X) - (LineBEnd.X - LineBStart.X) * (LineAEnd.Y - LineAStart.Y);

            // If the denominator is 0, the lines are parallel or collinear
            if (Math.Abs(denominator) < double.Epsilon)
            {
                // For parallel lines, you may want additional checks for collinear overlap
                return false;
            }

            double numerator1 = (LineBEnd.X - LineBStart.X) * (LineAStart.Y - LineBStart.Y) - (LineBEnd.Y - LineBStart.Y) * (LineAStart.X - LineBStart.X);
            double numerator2 = (LineAEnd.X - LineAStart.X) * (LineAStart.Y - LineBStart.Y) - (LineAEnd.Y - LineAStart.Y) * (LineAStart.X - LineBStart.X);

            // Calculate the parametric values for the intersection point
            double u_a = numerator1 / denominator;
            double u_b = numerator2 / denominator;

            // The segments intersect only if both 'u_a' and 'u_b' are between 0 and 1 (inclusive of endpoints)
            if (u_a >= 0 && u_a <= 1 && u_b >= 0 && u_b <= 1)
            {
                return true; // The segments intersect
            }

            return false; // No intersection within the segments
        }

        /// <summary>
        /// draws this package on the given CDrawing canvas using the package's color and lines
        /// </summary>
        /// <param name="Canvas">the CDrawer object the package is being drawn on</param>
        public void Draw(CDrawer Canvas)
        {
            for (int i = 0; i < _lines.Count; i++)
            {
                Point start = _lines[i]; //the start of the line segment being drawn
                Point end; //end of the line segment being drawn

                //if the start is the last point of _lines, set the end point to the first point of _lines
                if (i == _lines.Count - 1)
                    end = _lines[0];
                else
                    end = _lines[i + 1];

                //add the line segment to the canvas with the package's color
                Canvas.AddLine(start.X, start.Y, end.X, end.Y, _color);
            }
        }
    }
}
