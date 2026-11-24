using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkChain
{
    internal class Polynomial
    {
        //Time: O(n^2)
        //Space: O(1)
        public static Node<Monomial> PolynomialSort(Node<Monomial> poly)
        {
            Node<Monomial> p = poly;

            while (p != null)
            {
                Node<Monomial> biggest = p;
                Node<Monomial> q = p.GetNext();

                while (q != null)
                {
                    if (q.GetValue().GetExponent() > biggest.GetValue().GetExponent())
                    {
                        biggest = q;
                    }

                    q = q.GetNext();
                }

                Monomial temp = p.GetValue();
                p.SetValue(biggest.GetValue());
                biggest.SetValue(temp);

                p = p.GetNext();
            }

            return poly;
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
