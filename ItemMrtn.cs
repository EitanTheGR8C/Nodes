using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkChain
{
    internal class ItemMrtn//assuming correct inputs
    {
        private int year;
        private int score;

        public ItemMrtn(int year, int score)
        {
            this.year = year;
            this.score = score;
        }

        public int GetYear() { return this.year; }
        public int GetScore() { return this.score; }
        public void SetYear(int year) { this.year = year; }
        public void SetScore(int score) { this.score = score; }
    }
}
