using System;
using System.Collections.Generic;
using System.Configuration;
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

        //Time: O(n).
        //Space: O(1).
        public static Node<Monomial> ReduceSortedPolynomial(Node<Monomial> poly)
        {//assuming sorted
            Node<Monomial> p = poly;

            // Combine terms with the same exponent.
            while (p != null)
            {
                while (p.GetNext() != null && p.GetValue().GetExponent() == p.GetNext().GetValue().GetExponent())
                {
                    int newFactor = p.GetValue().GetFactor() + p.GetNext().GetValue().GetFactor();

                    p.GetValue().SetFactor(newFactor);
                    p.SetNext(p.GetNext().GetNext());
                }

                p = p.GetNext();
            }

            // Remove zero terms from the beginning.
            while (poly != null && poly.GetValue().GetFactor() == 0)
            {
                poly = poly.GetNext();
            }

            // Remove zero terms from the rest of the list.
            p = poly;
            while (p != null && p.GetNext() != null)
            {
                if (p.GetNext().GetValue().GetFactor() == 0)
                {
                    p.SetNext(p.GetNext().GetNext());
                }
                else
                {
                    p = p.GetNext();
                }
            }

            return poly;
        }

        public static string PolynomialToString(Node<Monomial> poly)
        {
            if (poly == null)
                return "f(x)=0";
            string strReturn = "f(x)=";
            while (poly != null)
            {
                strReturn += poly.GetValue().ToString();
                poly = poly.GetNext();
            }
            return strReturn;
        }

        public static void PrintPolynomial(Node<Monomial> poly)
        {
            Console.WriteLine(PolynomialToString(poly));
        }

        public static int CalcPolynomial(Node<Monomial> poly, int x)
        {
            if (poly == null)
                return 0;
            int answer = 0;
            while (poly != null)
            {
                answer += poly.GetValue().GetFactor() * ((int)Math.Pow(x,poly.GetValue().GetExponent()));
                poly = poly.GetNext();
            }

            return answer;
        }

        public static Node<Monomial> AddPolynomial(Node<Monomial> poly1, Node<Monomial> poly2)
        {
            poly1 = PolynomialSort(poly1);
            poly2 = PolynomialSort(poly2);
            poly1 = ReduceSortedPolynomial(poly1);
            poly2 = ReduceSortedPolynomial(poly2);
            if (poly1 == null)
                return poly2;
            if (poly2 == null)
                return poly1;

            Node<Monomial> result = null;
            Node<Monomial> last = null;

            while (poly1 != null || poly2 != null)
            {
                int factor;
                int exponent;

                if (poly2 == null || (poly1 != null && poly1.GetValue().GetExponent() > poly2.GetValue().GetExponent()))
                {
                    factor = poly1.GetValue().GetFactor();
                    exponent = poly1.GetValue().GetExponent();
                    poly1 = poly1.GetNext();
                }
                else if (poly1 == null || poly2.GetValue().GetExponent() > poly1.GetValue().GetExponent())
                {
                    factor = poly2.GetValue().GetFactor();
                    exponent = poly2.GetValue().GetExponent();
                    poly2 = poly2.GetNext();
                }
                else
                {
                    factor = poly1.GetValue().GetFactor() +
                             poly2.GetValue().GetFactor();
                    exponent = poly1.GetValue().GetExponent();
                    poly1 = poly1.GetNext();
                    poly2 = poly2.GetNext();
                }

                if (factor != 0)
                {
                    Monomial term = new Monomial(factor, exponent);
                    Node<Monomial> node = new Node<Monomial>(term);

                    if (result == null)
                        result = node;
                    else
                        last.SetNext(node);

                    last = node;
                }
            }

            return result;
        }
    }
}
