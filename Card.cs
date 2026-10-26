using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkChain
{
    internal class Card
    {
        private int num;
        private string color;

        public Card(int num, string color) //assuming correct in put (same assumtion for setters)
        {
            this.num = num;
            this.color = color;
        }


        public int GetNum()
        {
            return this.num;
        }
        public string GetColor()
        {
            return this.color;
        }
        public void SetNum(int new_num)
        {
            this.num = new_num;
        }
        public void SetColor(string new_color)
        {
            this.color =new_color;
        }

    }
}
