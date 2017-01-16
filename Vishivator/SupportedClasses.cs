using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vishivator {
    
    public class OrnamentPlace {
        public float rotate { get; set; }
        public int width { get; set; }
        public PointF centerPos { get; set; }
    }

    public class OrnamentFounding {
        public string imageFile { get; set; }
        public string name { get; set; }
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
