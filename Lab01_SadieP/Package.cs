using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



    }
}
