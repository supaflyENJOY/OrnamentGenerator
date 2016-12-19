using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vishivator {
    struct Point {
        public int X;
        public int Y;
        public int Color;
        public static bool operator ==(Point x, Point y) {
            return x.X == y.X && x.Y == y.Y;
        }
        public static bool operator !=(Point x, Point y) {
            return !(x == y);
        }
    }
}
