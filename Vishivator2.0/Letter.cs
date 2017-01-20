using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vishivator {
    class Letter {
        char Symbol;
        string[] VerticalPoints;
        string[] DiagonalPoints;
        public Letter() {
        }
        public Letter(char s, string[] vp, string[] dp) {
            Symbol = s;
            VerticalPoints = vp;
            DiagonalPoints = dp;
        }
        public char getSymbol() {
            return Symbol;
        }

        public List<Point> getVPoints(int offset_x = 0, int offset_y = 0, int color=0) {
            var lp = new List<Point>();
            for (int i = 0; i < VerticalPoints.Length; i++) {
                for (int j = 0; j < VerticalPoints[i].Length; j++) {
                    if (VerticalPoints[i][j] == '1') {
                        int x = j + offset_x-VerticalPoints[i].Length/2;
                        int y = VerticalPoints.Length-1-i + offset_y;
                        if(x < 0) {
                            int t = x;
                            x = y;
                            y = -t-1;
                        }
                        lp.Add(new Point { X = x, Y = y, Color = color });
                    }
                }
            }
            return lp;
        }
        public List<Point> getDPoints(int offset_x = 0, int offset_y = 0, int color = 0) {
            var lp = new List<Point>();
            for (int i = 0; i < DiagonalPoints.Length; i++) {
                for (int j = 0; j < DiagonalPoints[i].Length; j++) {
                    if (DiagonalPoints[i][j] == '1') {
                        lp.Add(new Point { X = j + offset_x, Y = DiagonalPoints.Length - 1 - i + offset_y, Color = color });
                    }
                }
            }
            return lp;
        }
    }
}
