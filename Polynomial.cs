using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkChain
{
    internal class Polynomial
    {
        public static Node<Monomial> PolynomialSort(Node<Monomial> poly)
        {
            // TODO: sort by exponent, descending
            return null;
        }

        public static Node<Monomial> ReduceSortedPolynomial(Node<Monomial> poly)
        {
            // TODO: combine equal exponents and remove zero terms
            return null;
        }

        public static string PolynomialToString(Node<Monomial> poly)
        {
            // TODO: format in the current list order
            return "";
        }

        public static void PrintPolynomial(Node<Monomial> poly)
        {
            // TODO: print the formatted polynomial and a new line
        }

        public static int CalcPolynomial(Node<Monomial> poly, int x)
        {
            // TODO: calculate the value at x
            return 0;
        }

        public static Node<Monomial> AddPolynomial(
            Node<Monomial> poly1, Node<Monomial> poly2)
        {
            // TODO: build the sum with new nodes and terms
            return null;
        }
    }
}
