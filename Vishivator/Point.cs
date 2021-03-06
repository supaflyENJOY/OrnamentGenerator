﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vishivator {
    public class Point {
        public int X { get; set; }
        public int Y { get; set; }
        public int Color;
        public static bool operator ==(Point x, Point y) {
            return x.X == y.X && x.Y == y.Y;
        }
        public static bool operator !=(Point x, Point y) {
            return !(x == y);
        }

        public Point() {

        }
    }
}
