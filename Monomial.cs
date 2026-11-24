using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkChain
{
    internal class Monomial
    {
        private int factor;
        private int exponent;

        public Monomial(int factor, int exponent)
        {
            this.factor = factor;
            this.exponent = exponent;
        }
        public int GetFactor()
        { return this.factor; }
        public void SetFactor(int factor)
        { this.factor = factor; }
        public int GetExponent()
        { return this.exponent; }

        public void SetExponent(int exponent)
        { this.exponent = exponent; }
        public override string ToString()
        {
            if (this.factor == 0)
                return "+0";
            int number = this.factor;
            string sign = "+";
            if (number < 0)
            {
                sign = "-";
                number = -number;
            }
            if (this.exponent == 0)
                return sign + number;
            string result = sign;
            if (number != 1)
                result += number;
            result += "x";
            if (this.exponent != 1)
                result += "^" + this.exponent;
            return result;
        }
    }
}
