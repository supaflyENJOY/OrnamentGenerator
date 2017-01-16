using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vishivator {

    public class ColorO {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
    }

    public class OrnamentPlace {
        public float rotate { get; set; }
        public int width { get; set; }
        public PointF centerPos { get; set; }
    }

    public class OrnamentFounding {
        public string imageFile { get; set; }
        public string name { get; set; }
        public ColorO backgroundColor { get; set; }
        public ColorO redColor { get; set; }
        public ColorO blackColor { get; set; }
        public List<OrnamentPlace> ornamentPlaces { get; set; }
    }

    //

    public class Region {
        public float rotate { get; set; }
        public int width { get; set; }
        public List<PointF> dots { get; set; }
    }

    public class Shirt {
        public string imageFile { get; set; }
        public string name { get; set; }
        public List<Region> regions { get; set; }
    }
}
