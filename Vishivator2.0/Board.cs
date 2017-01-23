using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vishivator {
    class Board {
        public List<Point> points;
        List<Letter> letters;
        int lastColor = 1;
        int min = 0;
        int maxValue = 0;
        string word;
        public Board(List<Letter> lt) {
            letters = lt;
            points = new List<Point>();
            word = "";
        }

        public int getMaxValue() {
            return maxValue;
        }

        public string getWord() {
            return word;
        }

        public void Clear() {
            points.Clear();
            word = "";
            min = 0;
            lastColor = 1;
            maxValue = 0;
        }

        public bool addLetter(char s) {
            var dat = letters.Where(x => x.getSymbol() == s);
            if (dat.Count() == 0) return false;
            Letter letter = dat.First();
            int bestV, bestD;
            for (bestV = min; bestV < 30; bestV++) {
                var addPoints = letter.getVPoints(0, bestV);
                bool good = true;
                for (int i = 0; i < addPoints.Count; i++) {
                    if (points.Where(x => addPoints[i] == x).Count() != 0) {
                        good = false;
                        break;
                    }
                }
                if (good == true) break;
            }
            for (bestD = min; bestD < 30; bestD++) {
                var addPoints = letter.getDPoints(bestD, bestD);
                bool good = true;
                for (int i = 0; i < addPoints.Count; i++) {
                    if (points.Where(x => addPoints[i] == x).Count() != 0) {
                        good = false;
                        break;
                    }
                }
                if (good == true) break;
            }
            if(bestV < bestD) {
                min = bestV;
                var addPoints = letter.getVPoints(0, bestV, lastColor);
                points.AddRange(addPoints);
            } else {
                min = bestD;
                var addPoints = letter.getDPoints(bestD, bestD, lastColor);
                points.AddRange(addPoints);
            }
            lastColor = 3 - lastColor;
            maxValue = Math.Max(maxValue,Math.Max(points.Select(x => x.X + 1).Max(), points.Select(x => x.Y + 1).Max()));
            return true;
        }

        public void setWord(string _word) {
            Clear();
            for(int i=0; i < _word.Length; i++) {
                if (addLetter(_word[i])) word += _word[i];
            }
        }
    }
}
