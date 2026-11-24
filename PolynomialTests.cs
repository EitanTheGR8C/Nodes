using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkChain
{
    // Comprehensive tests for the polynomial task.
    // Requires: Node<T>, Monomial, Polynomial (the student's / reference solution).
    // Usage:  PolynomialTests.RunAll();   // prints a summary, returns true if everything passed
    public static class PolynomialTests
    {
        private static int passed = 0;
        private static int failed = 0;

        // ------------------------------------------------------------------ helpers

        // Build(f1, e1, f2, e2, ...) -> list of Monomials (fresh nodes, fresh objects)
        private static Node<Monomial> Build(params int[] pairs)
        {
            Node<Monomial> head = null;
            Node<Monomial> tail = null;
            for (int i = 0; i < pairs.Length; i += 2)
            {
                Node<Monomial> node = new Node<Monomial>(new Monomial(pairs[i], pairs[i + 1]));
                if (head == null)
                    head = node;
                else
                    tail.SetNext(node);
                tail = node;
            }
            return head;
        }

        // Raw dump that does not depend on Monomial.ToString: "(5,4)->(2,1)" or "null"
        // Protects against infinite loops / cycles (a broken list makes the test fail, not hang).
        private static string Dump(Node<Monomial> list)
        {
            if (list == null)
                return "null";
            StringBuilder sb = new StringBuilder();
            int count = 0;
            while (list != null)
            {
                if (++count > 1000)
                    return sb + "...(list too long / cycle)";
                if (sb.Length > 0)
                    sb.Append("->");
                if (list.GetValue() == null)
                    sb.Append("(NULL-VALUE)");
                else
                    sb.Append("(" + list.GetValue().GetFactor() + "," + list.GetValue().GetExponent() + ")");
                list = list.GetNext();
            }
            return sb.ToString();
        }

        private static List<string> Pairs(Node<Monomial> list)
        {
            List<string> res = new List<string>();
            int count = 0;
            while (list != null && count++ < 1000)
            {
                res.Add(list.GetValue().GetFactor() + "," + list.GetValue().GetExponent());
                list = list.GetNext();
            }
            res.Sort(StringComparer.Ordinal);
            return res;
        }

        private static bool IsSortedDesc(Node<Monomial> list)
        {
            int count = 0;
            while (list != null && list.HasNext())
            {
                if (count++ > 1000) return false;
                if (list.GetValue().GetExponent() < list.GetNext().GetValue().GetExponent())
                    return false;
                list = list.GetNext();
            }
            return true;
        }

        private static void Check(string name, string expected, string actual)
        {
            if (expected == actual)
                passed++;
            else
            {
                failed++;
                Console.WriteLine("FAIL: " + name);
                Console.WriteLine("   expected: " + expected);
                Console.WriteLine("   actual:   " + actual);
            }
        }

        private static void CheckInt(string name, long expected, long actual)
        {
            Check(name, expected.ToString(), actual.ToString());
        }

        private static void CheckTrue(string name, bool condition)
        {
            Check(name, "True", condition.ToString());
        }

        // Runs one test and turns an exception into a failure instead of aborting everything.
        private static void Run(string name, Action test)
        {
            try
            {
                test();
            }
            catch (Exception e)
            {
                failed++;
                Console.WriteLine("FAIL: " + name + " threw " + e.GetType().Name + ": " + e.Message);
            }
        }

        // Reference model: a polynomial as an array of coefficients, index = exponent (0..6)
        private const int MaxExp = 6;

        private static int[] ToCoefficients(int[] pairs)
        {
            int[] c = new int[MaxExp + 1];
            for (int i = 0; i < pairs.Length; i += 2)
                c[pairs[i + 1]] += pairs[i];
            return c;
        }

        private static string CoefficientsToDump(int[] c)
        {
            StringBuilder sb = new StringBuilder();
            for (int e = MaxExp; e >= 0; e--)
            {
                if (c[e] == 0) continue;
                if (sb.Length > 0) sb.Append("->");
                sb.Append("(" + c[e] + "," + e + ")");
            }
            return sb.Length == 0 ? "null" : sb.ToString();
        }

        private static long PowLong(long x, int n)
        {
            long r = 1;
            for (int i = 0; i < n; i++) r *= x;
            return r;
        }

        private static long EvalPairs(int[] pairs, int x)
        {
            long sum = 0;
            for (int i = 0; i < pairs.Length; i += 2)
                sum += (long)pairs[i] * PowLong(x, pairs[i + 1]);
            return sum;
        }

        private static int[] RandomPairs(Random rnd, int maxLen)
        {
            int len = rnd.Next(0, maxLen + 1);
            int[] pairs = new int[len * 2];
            for (int i = 0; i < len; i++)
            {
                pairs[2 * i] = rnd.Next(-10, 11);      // factor in [-10, 10], zero allowed
                pairs[2 * i + 1] = rnd.Next(0, MaxExp + 1);   // exponent in [0, 6]
            }
            return pairs;
        }

        // ------------------------------------------------------------------ Monomial

        private static void Test_Monomial_ToString()
        {
            int[][] cases = new int[][] {
            // factor, exponent
            new int[] { 5, 4 }, new int[] { -5, 4 }, new int[] { 12, 6 }, new int[] { -10, 2 },
            new int[] { 1, 4 }, new int[] { -1, 4 }, new int[] { 1, 2 }, new int[] { -1, 2 },
            new int[] { 1, 1 }, new int[] { -1, 1 }, new int[] { 3, 1 }, new int[] { -3, 1 },
            new int[] { 10, 1 }, new int[] { -4, 1 },
            new int[] { 3, 0 }, new int[] { -4, 0 }, new int[] { 1, 0 }, new int[] { -1, 0 },
            new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 5 }
        };
            string[] expected = new string[] {
            "+5x^4", "-5x^4", "+12x^6", "-10x^2",
            "+x^4", "-x^4", "+x^2", "-x^2",
            "+x", "-x", "+3x", "-3x",
            "+10x", "-4x",
            "+3", "-4", "+1", "-1",
            "+0", "+0", "+0"
        };
            for (int i = 0; i < cases.Length; i++)
                Check("Monomial(" + cases[i][0] + "," + cases[i][1] + ").ToString()",
                      expected[i], new Monomial(cases[i][0], cases[i][1]).ToString());
        }

        private static void Test_Monomial_GetSet()
        {
            Monomial m = new Monomial(-2, 4);
            CheckInt("GetFactor", -2, m.GetFactor());
            CheckInt("GetExponent", 4, m.GetExponent());
            m.SetFactor(7);
            m.SetExponent(0);
            CheckInt("SetFactor", 7, m.GetFactor());
            CheckInt("SetExponent", 0, m.GetExponent());
            Check("ToString after set", "+7", m.ToString());
        }

        // ------------------------------------------------------------------ Sort

        private static void Test_Sort()
        {
            Check("sort null", "null", Dump(Polynomial.PolynomialSort(null)));
            Check("sort single", "(3,2)", Dump(Polynomial.PolynomialSort(Build(3, 2))));
            Check("sort already sorted", "(1,5)->(2,3)->(3,0)",
                  Dump(Polynomial.PolynomialSort(Build(1, 5, 2, 3, 3, 0))));
            Check("sort reversed", "(1,5)->(2,3)->(3,0)",
                  Dump(Polynomial.PolynomialSort(Build(3, 0, 2, 3, 1, 5))));
            Check("sort unsorted distinct", "(4,6)->(3,4)->(2,3)->(-1,2)->(5,1)->(7,0)",
                  Dump(Polynomial.PolynomialSort(Build(5, 1, 3, 4, 7, 0, 2, 3, -1, 2, 4, 6))));
            Check("sort max is last", "(9,6)->(1,2)->(1,1)",
                  Dump(Polynomial.PolynomialSort(Build(1, 1, 1, 2, 9, 6))));
            Check("sort max is first", "(9,6)->(1,2)->(1,1)",
                  Dump(Polynomial.PolynomialSort(Build(9, 6, 1, 2, 1, 1))));
            Check("sort two elements", "(1,1)->(2,0)",
                  Dump(Polynomial.PolynomialSort(Build(2, 0, 1, 1))));
            Check("sort keeps zero factors (does not reduce)", "(0,3)->(5,0)",
                  Dump(Polynomial.PolynomialSort(Build(5, 0, 0, 3))));
            Check("sort gap exponents", "(1,6)->(2,0)",
                  Dump(Polynomial.PolynomialSort(Build(2, 0, 1, 6))));

            // duplicates: order among equal exponents is not specified -> check properties only
            int[] pairs = { 5, 4, 3, 1, -2, 3, -7, 1, 1, 3, 1, 4, 5, 0, 1, 3, 7, 1 };
            Node<Monomial> input = Build(pairs);
            List<string> before = Pairs(input);
            Node<Monomial> sorted = Polynomial.PolynomialSort(input);
            CheckTrue("sort duplicates: descending", IsSortedDesc(sorted));
            Check("sort duplicates: same elements", string.Join(";", before.ToArray()),
                  string.Join(";", Pairs(sorted).ToArray()));
            CheckInt("sort duplicates: same length", 9, Dump(sorted).Split(new string[] { "->" }, StringSplitOptions.None).Length);

            Node<Monomial> same = Polynomial.PolynomialSort(Build(1, 2, 2, 2, 3, 2, 4, 2));
            CheckTrue("sort all same exponent: descending", IsSortedDesc(same));
            Check("sort all same exponent: same elements", "1,2;2,2;3,2;4,2",
                  string.Join(";", Pairs(same).ToArray()));
        }

        private static void Test_Sort_Random()
        {
            Random rnd = new Random(12345);
            for (int t = 0; t < 300; t++)
            {
                int[] pairs = RandomPairs(rnd, 50);
                Node<Monomial> input = Build(pairs);
                List<string> before = Pairs(input);
                Node<Monomial> sorted = Polynomial.PolynomialSort(input);
                if (!IsSortedDesc(sorted) ||
                    string.Join(";", before.ToArray()) != string.Join(";", Pairs(sorted).ToArray()))
                {
                    failed++;
                    Console.WriteLine("FAIL: random sort #" + t + " input=" + Dump(Build(pairs)) + " got=" + Dump(sorted));
                    return;
                }
            }
            passed++;
        }

        // ------------------------------------------------------------------ Reduce

        private static void Test_Reduce()
        {
            Check("reduce null", "null", Dump(Polynomial.ReduceSortedPolynomial(null)));
            Check("reduce single", "(3,2)", Dump(Polynomial.ReduceSortedPolynomial(Build(3, 2))));
            Check("reduce single zero factor", "null", Dump(Polynomial.ReduceSortedPolynomial(Build(0, 2))));
            Check("reduce no duplicates", "(3,4)->(2,2)->(1,0)",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(3, 4, 2, 2, 1, 0))));

            // the example from the task: the LAST group has two elements (classic NullReference bug)
            Check("reduce task example", "(7,4)->(-1,1)->(-7,0)",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(5, 4, 2, 4, 2, 1, -3, 1, -9, 0, 2, 0))));

            Check("reduce first group only", "(3,4)->(1,2)",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(1, 4, 2, 4, 1, 2))));
            Check("reduce last group only", "(1,4)->(5,0)",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(1, 4, 2, 0, 3, 0))));
            Check("reduce middle group only", "(1,4)->(5,2)->(1,0)",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(1, 4, 2, 2, 3, 2, 1, 0))));
            Check("reduce all same exponent", "(10,3)",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(1, 3, 2, 3, 3, 3, 4, 3))));
            Check("reduce three in a group", "(6,2)",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(1, 2, 2, 2, 3, 2))));

            // zeros
            Check("reduce all cancel -> null", "null",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(3, 2, -3, 2))));
            Check("reduce all cancel, 3 elements -> null", "null",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(1, 3, 2, 3, -3, 3))));
            Check("reduce several groups all cancel -> null", "null",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(2, 3, -2, 3, 1, 1, -1, 1, 4, 0, -4, 0))));
            Check("reduce cancel at head", "(1,2)->(1,0)",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(2, 3, -2, 3, 1, 2, 1, 0))));
            Check("reduce cancel in the middle", "(5,4)->(1,0)",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(5, 4, 2, 2, -2, 2, 1, 0))));
            Check("reduce cancel at the end", "(5,4)",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(5, 4, 2, 0, -2, 0))));
            Check("reduce zero factor at head", "(1,2)",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(0, 5, 1, 2))));
            Check("reduce zero factor in the middle", "(1,5)->(1,0)",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(1, 5, 0, 3, 1, 0))));
            Check("reduce zero factor at the end", "(1,5)",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(1, 5, 0, 0))));
            Check("reduce only zero factors -> null", "null",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(0, 5, 0, 3, 0, 0))));
            Check("reduce zero factors in a group stay harmless", "(2,3)",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(0, 3, 2, 3, 0, 3))));
            Check("reduce negative sum", "(-4,1)",
                  Dump(Polynomial.ReduceSortedPolynomial(Build(-1, 1, -3, 1))));
        }

        private static void Test_Reduce_Random()
        {
            Random rnd = new Random(777);
            for (int t = 0; t < 300; t++)
            {
                int[] pairs = RandomPairs(rnd, 50);
                Node<Monomial> sorted = Polynomial.PolynomialSort(Build(pairs));
                string actual = Dump(Polynomial.ReduceSortedPolynomial(sorted));
                string expected = CoefficientsToDump(ToCoefficients(pairs));
                if (actual != expected)
                {
                    failed++;
                    Console.WriteLine("FAIL: random reduce #" + t + " input=" + Dump(Build(pairs)));
                    Console.WriteLine("   expected: " + expected);
                    Console.WriteLine("   actual:   " + actual);
                    return;
                }
            }
            passed++;
        }

        // ------------------------------------------------------------------ PolynomialToString / Print

        private static void Test_ToStringAndPrint()
        {
            Check("string null", "f(x)=0", Polynomial.PolynomialToString(null));
            Check("string task example", "f(x)=+5x^4+2x^2-3x-9",
                  Polynomial.PolynomialToString(Build(5, 4, 2, 2, -3, 1, -9, 0)));
            Check("string negative first", "f(x)=-x^2", Polynomial.PolynomialToString(Build(-1, 2)));
            Check("string constant", "f(x)=+7", Polynomial.PolynomialToString(Build(7, 0)));
            Check("string negative constant", "f(x)=-7", Polynomial.PolynomialToString(Build(-7, 0)));
            Check("string x", "f(x)=+x", Polynomial.PolynomialToString(Build(1, 1)));
            Check("string x-1", "f(x)=+x-1", Polynomial.PolynomialToString(Build(1, 1, -1, 0)));
            Check("string coefficient 1 and -1 inside", "f(x)=+x^3-x^2+x-1",
                  Polynomial.PolynomialToString(Build(1, 3, -1, 2, 1, 1, -1, 0)));
            Check("string no spaces and no *", "False",
                  (Polynomial.PolynomialToString(Build(5, 4, 2, 2, -3, 1, -9, 0)).Contains(" ") ||
                   Polynomial.PolynomialToString(Build(5, 4, 2, 2, -3, 1, -9, 0)).Contains("*")).ToString());
            Check("string does not modify the list", "(5,4)->(2,2)",
                  Dump(KeepAfter(Build(5, 4, 2, 2))));

            // PrintPolynomial prints exactly the string, followed by a new line
            TextWriter oldOut = Console.Out;
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            try { Polynomial.PrintPolynomial(Build(1, 1, -1, 0)); }
            finally { Console.SetOut(oldOut); }
            Check("print", "f(x)=+x-1" + Environment.NewLine, sw.ToString());
        }

        private static Node<Monomial> KeepAfter(Node<Monomial> list)
        {
            Polynomial.PolynomialToString(list);
            return list;
        }

        // ------------------------------------------------------------------ Calc

        private static void Test_Calc()
        {
            CheckInt("calc null x=5", 0, Polynomial.CalcPolynomial(null, 5));
            CheckInt("calc null x=0", 0, Polynomial.CalcPolynomial(null, 0));
            CheckInt("calc constant x=5", 7, Polynomial.CalcPolynomial(Build(7, 0), 5));
            CheckInt("calc constant x=0", 7, Polynomial.CalcPolynomial(Build(7, 0), 0));
            CheckInt("calc constant x=-3", 7, Polynomial.CalcPolynomial(Build(7, 0), -3));
            CheckInt("calc 3x, x=0", 0, Polynomial.CalcPolynomial(Build(3, 1), 0));
            CheckInt("calc 3x, x=-2", -6, Polynomial.CalcPolynomial(Build(3, 1), -2));
            CheckInt("calc x^2, x=-3", 9, Polynomial.CalcPolynomial(Build(1, 2), -3));
            CheckInt("calc x^3, x=-3", -27, Polynomial.CalcPolynomial(Build(1, 3), -3));
            CheckInt("calc 5+3x^2, x=0 (0^0 must be 1)", 5, Polynomial.CalcPolynomial(Build(3, 2, 5, 0), 0));
            CheckInt("calc 0x^3, x=7", 0, Polynomial.CalcPolynomial(Build(0, 3), 7));

            Node<Monomial> p = Build(6, 4, 3, 1, 5, 0);   // 6x^4+3x+5
            CheckInt("calc example x=3", 500, Polynomial.CalcPolynomial(p, 3));
            CheckInt("calc example x=-1", 8, Polynomial.CalcPolynomial(p, -1));
            CheckInt("calc example x=0", 5, Polynomial.CalcPolynomial(p, 0));
            CheckInt("calc example x=1", 14, Polynomial.CalcPolynomial(p, 1));
            CheckInt("calc example x=10", 60035, Polynomial.CalcPolynomial(p, 10));
            CheckInt("calc example x=-10", 59975, Polynomial.CalcPolynomial(p, -10));

            // unsorted + unreduced + zero factors: same polynomial, same value
            Node<Monomial> messy = Build(5, 4, 3, 1, -2, 3, -7, 1, 1, 3, 1, 4, 5, 0, 1, 3, 7, 1, 0, 6);
            CheckInt("calc messy list x=3", 500, Polynomial.CalcPolynomial(messy, 3));
            CheckInt("calc does not modify the list", 500, Polynomial.CalcPolynomial(messy, 3));

            // limits of the allowed range: no overflow, exact value
            int[] big = new int[100];
            for (int i = 0; i < 50; i++) { big[2 * i] = 10; big[2 * i + 1] = 6; }
            CheckInt("calc max range x=10", 500000000L, Polynomial.CalcPolynomial(Build(big), 10));
            CheckInt("calc max range x=-10", 500000000L, Polynomial.CalcPolynomial(Build(big), -10));
            int[] bigNeg = new int[100];
            for (int i = 0; i < 50; i++) { bigNeg[2 * i] = -10; bigNeg[2 * i + 1] = 5; }
            CheckInt("calc max range negative odd", 50L * 10 * 100000, Polynomial.CalcPolynomial(Build(bigNeg), -10));

            // exactness for every x in [-10, 10] and every exponent in [0, 6]
            for (int x = -10; x <= 10; x++)
                for (int e = 0; e <= MaxExp; e++)
                    CheckInt("calc x^e exact x=" + x + " e=" + e, PowLong(x, e), Polynomial.CalcPolynomial(Build(1, e), x));
        }

        private static void Test_Calc_Random()
        {
            Random rnd = new Random(2024);
            for (int t = 0; t < 300; t++)
            {
                int[] pairs = RandomPairs(rnd, 50);
                int x = rnd.Next(-10, 11);
                long expected = EvalPairs(pairs, x);
                long actual = Polynomial.CalcPolynomial(Build(pairs), x);
                if (expected != actual)
                {
                    failed++;
                    Console.WriteLine("FAIL: random calc #" + t + " x=" + x + " expected " + expected + " actual " + actual);
                    return;
                }
            }
            passed++;
        }

        // ------------------------------------------------------------------ Add

        private static void Test_Add()
        {
            Check("add null+null", "null", Dump(Polynomial.AddPolynomial(null, null)));
            Check("add null+p", "(6,4)->(3,1)->(5,0)",
                  Dump(Polynomial.AddPolynomial(null, Build(5, 4, 3, 1, 1, 4, 5, 0))));
            Check("add p+null", "(6,4)->(3,1)->(5,0)",
                  Dump(Polynomial.AddPolynomial(Build(5, 4, 3, 1, 1, 4, 5, 0), null)));
            Check("add zero polynomial (only zero factors) + null", "null",
                  Dump(Polynomial.AddPolynomial(Build(0, 3, 0, 1), null)));

            // the example of the task
            Check("add task example", "(7,4)->(5,2)->(1,1)->(10,0)",
                  Dump(Polynomial.AddPolynomial(
                      Build(5, 4, 3, 1, -2, 3, -7, 1, 1, 3, 1, 4, 5, 0, 1, 3, 7, 1),
                      Build(5, 2, 3, 1, -2, 3, -7, 1, 1, 3, 1, 4, 5, 0, 1, 3, 2, 1))));

            Check("add p + (-p) = null", "null",
                  Dump(Polynomial.AddPolynomial(Build(3, 4, -2, 1, 5, 0), Build(-3, 4, 2, 1, -5, 0))));
            Check("add partial cancellation", "(4,2)->(1,0)",
                  Dump(Polynomial.AddPolynomial(Build(3, 2, 4, 1, 1, 0), Build(1, 2, -4, 1))));
            Check("add cancel at head", "(1,2)->(1,0)",
                  Dump(Polynomial.AddPolynomial(Build(1, 3, 1, 2), Build(-1, 3, 1, 0))));
            Check("add cancel at tail", "(1,3)",
                  Dump(Polynomial.AddPolynomial(Build(1, 3, 1, 0), Build(-1, 0))));
            Check("add interleaved exponents", "(1,5)->(1,4)->(1,3)->(1,2)->(1,1)->(1,0)",
                  Dump(Polynomial.AddPolynomial(Build(1, 5, 1, 3, 1, 1), Build(1, 4, 1, 2, 1, 0))));
            Check("add interleaved, unsorted input", "(1,5)->(1,4)->(1,3)->(1,2)->(1,1)->(1,0)",
                  Dump(Polynomial.AddPolynomial(Build(1, 1, 1, 5, 1, 3), Build(1, 0, 1, 4, 1, 2))));
            Check("add first much longer", "(1,6)->(1,5)->(1,4)->(3,0)",
                  Dump(Polynomial.AddPolynomial(Build(1, 6, 1, 5, 1, 4, 1, 0), Build(2, 0))));
            Check("add second much longer", "(1,6)->(1,5)->(1,4)->(3,0)",
                  Dump(Polynomial.AddPolynomial(Build(2, 0), Build(1, 6, 1, 5, 1, 4, 1, 0))));
            Check("add p+p = 2p", "(4,3)->(2,1)->(-6,0)",
                  Dump(Polynomial.AddPolynomial(Build(2, 3, 1, 1, -3, 0), Build(2, 3, 1, 1, -3, 0))));
            Check("add constants only", "(5,0)",
                  Dump(Polynomial.AddPolynomial(Build(2, 0), Build(3, 0))));
            Check("add with duplicates inside each list", "(6,2)->(2,1)",
                  Dump(Polynomial.AddPolynomial(Build(1, 2, 2, 2, 1, 1), Build(3, 2, 1, 1))));
            Check("add with zero factors inside", "(1,3)->(1,0)",
                  Dump(Polynomial.AddPolynomial(Build(0, 5, 1, 3), Build(1, 0, 0, 2))));

            // the result must be a NEW list: new nodes and new Monomial objects
            Node<Monomial> a = Build(2, 3, 1, 1, 4, 0);
            Node<Monomial> b = Build(1, 3, 2, 2, 1, 0);
            List<object> inputObjects = new List<object>();
            for (Node<Monomial> n = a; n != null; n = n.GetNext()) { inputObjects.Add(n); inputObjects.Add(n.GetValue()); }
            for (Node<Monomial> n = b; n != null; n = n.GetNext()) { inputObjects.Add(n); inputObjects.Add(n.GetValue()); }
            Node<Monomial> sum = Polynomial.AddPolynomial(a, b);
            Check("add result values", "(3,3)->(2,2)->(1,1)->(5,0)", Dump(sum));
            bool shared = false;
            for (Node<Monomial> n = sum; n != null; n = n.GetNext())
                foreach (object o in inputObjects)
                    if (ReferenceEquals(o, n) || ReferenceEquals(o, n.GetValue()))
                        shared = true;
            CheckTrue("add result shares no node / Monomial with the inputs", !shared);

            // the result must not be affected by what happens to a second call
            Node<Monomial> r1 = Polynomial.AddPolynomial(Build(1, 1), Build(1, 0));
            Node<Monomial> r2 = Polynomial.AddPolynomial(Build(5, 1), Build(5, 0));
            Check("add two results are independent (1)", "(1,1)->(1,0)", Dump(r1));
            Check("add two results are independent (2)", "(5,1)->(5,0)", Dump(r2));
        }

        private static void Test_Add_Random()
        {
            Random rnd = new Random(31337);
            for (int t = 0; t < 300; t++)
            {
                int[] pa = RandomPairs(rnd, 50);
                int[] pb = RandomPairs(rnd, 50);
                int[] ca = ToCoefficients(pa);
                int[] cb = ToCoefficients(pb);
                int[] sum = new int[MaxExp + 1];
                for (int e = 0; e <= MaxExp; e++) sum[e] = ca[e] + cb[e];
                string expected = CoefficientsToDump(sum);

                string ab = Dump(Polynomial.AddPolynomial(Build(pa), Build(pb)));
                string ba = Dump(Polynomial.AddPolynomial(Build(pb), Build(pa)));
                if (ab != expected || ba != expected)
                {
                    failed++;
                    Console.WriteLine("FAIL: random add #" + t);
                    Console.WriteLine("   a        = " + Dump(Build(pa)));
                    Console.WriteLine("   b        = " + Dump(Build(pb)));
                    Console.WriteLine("   expected = " + expected);
                    Console.WriteLine("   a+b      = " + ab);
                    Console.WriteLine("   b+a      = " + ba);
                    return;
                }
                // value check: (a+b)(x) = a(x)+b(x)
                int x = rnd.Next(-10, 11);
                long viaList = Polynomial.CalcPolynomial(Polynomial.AddPolynomial(Build(pa), Build(pb)), x);
                if (viaList != EvalPairs(pa, x) + EvalPairs(pb, x))
                {
                    failed++;
                    Console.WriteLine("FAIL: random add value #" + t + " x=" + x);
                    return;
                }
            }
            passed++;
        }

        // ------------------------------------------------------------------ the two examples of the task

        public static void Test_Polynomial()
        {
            Console.WriteLine($"--- {(new StackTrace()).GetFrame(0).GetMethod()} ---");
            Monomial[] a = {
            new Monomial(5,4),
            new Monomial(3,1),
            new Monomial(-2,3),
            new Monomial(-7,1),
            new Monomial(1,3),
            new Monomial(1,4),
            new Monomial(5,0),
            new Monomial(1,3),
            new Monomial(7,1)
        };
            Node<Monomial> poly = NodeUtils.CreateListFromArray(a);
            Polynomial.PrintPolynomial(poly);
            poly = Polynomial.PolynomialSort(poly);
            Polynomial.PrintPolynomial(poly);
            poly = Polynomial.ReduceSortedPolynomial(poly);
            Polynomial.PrintPolynomial(poly);
            int res = Polynomial.CalcPolynomial(poly, 3);
            Console.WriteLine($"f({3}) = {res}");
        }

        public static void Test_Polynomial2()
        {
            Console.WriteLine($"--- {(new StackTrace()).GetFrame(0).GetMethod()} ---");
            Monomial[] a = {
            new Monomial(5,4),
            new Monomial(3,1),
            new Monomial(-2,3),
            new Monomial(-7,1),
            new Monomial(1,3),
            new Monomial(1,4),
            new Monomial(5,0),
            new Monomial(1,3),
            new Monomial(7,1)
        };
            Monomial[] b = {
            new Monomial(5,2),
            new Monomial(3,1),
            new Monomial(-2,3),
            new Monomial(-7,1),
            new Monomial(1,3),
            new Monomial(1,4),
            new Monomial(5,0),
            new Monomial(1,3),
            new Monomial(2,1)
        };
            Node<Monomial> poly1 = NodeUtils.CreateListFromArray(a);
            Node<Monomial> poly2 = NodeUtils.CreateListFromArray(b);
            Polynomial.PrintPolynomial(poly1);
            Polynomial.PrintPolynomial(poly2);

            Node<Monomial> addPoly = Polynomial.AddPolynomial(poly1, poly2);
            Polynomial.PrintPolynomial(addPoly);
        }

        private static string Capture(Action action)
        {
            TextWriter oldOut = Console.Out;
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            try { action(); }
            finally { Console.SetOut(oldOut); }
            return sw.ToString();
        }

        private static string[] LinesWithoutHeader(string text)
        {
            string[] lines = text.Replace("\r", "").TrimEnd('\n').Split('\n');
            List<string> res = new List<string>(lines);
            res.RemoveAt(0);   // the "--- Void Test_X() ---" header depends on the runtime
            return res.ToArray();
        }

        private static void Test_TaskExamples()
        {
            string[] out1 = LinesWithoutHeader(Capture(Test_Polynomial));
            CheckInt("Test_Polynomial: number of lines", 4, out1.Length);
            Check("Test_Polynomial: line 1 (original)", "f(x)=+5x^4+3x-2x^3-7x+x^3+x^4+5+x^3+7x", out1[0]);
            CheckTrue("Test_Polynomial: line 2 starts with +5x^4 or +x^4 (sorted)", out1[1].StartsWith("f(x)=+5x^4+x^4") || out1[1].StartsWith("f(x)=+x^4+5x^4"));
            Check("Test_Polynomial: line 3 (reduced)", "f(x)=+6x^4+3x+5", out1[2]);
            Check("Test_Polynomial: line 4 (value)", "f(3) = 500", out1[3]);

            string[] out2 = LinesWithoutHeader(Capture(Test_Polynomial2));
            CheckInt("Test_Polynomial2: number of lines", 3, out2.Length);
            Check("Test_Polynomial2: line 1", "f(x)=+5x^4+3x-2x^3-7x+x^3+x^4+5+x^3+7x", out2[0]);
            Check("Test_Polynomial2: line 2", "f(x)=+5x^2+3x-2x^3-7x+x^3+x^4+5+x^3+2x", out2[1]);
            Check("Test_Polynomial2: line 3 (sum)", "f(x)=+7x^4+5x^2+x+10", out2[2]);
        }

        // ------------------------------------------------------------------ entry point

        public static bool RunAll()
        {
            passed = 0;
            failed = 0;
            Run("Monomial.ToString", Test_Monomial_ToString);
            Run("Monomial get/set", Test_Monomial_GetSet);
            Run("PolynomialSort", Test_Sort);
            Run("PolynomialSort (random)", Test_Sort_Random);
            Run("ReduceSortedPolynomial", Test_Reduce);
            Run("ReduceSortedPolynomial (random)", Test_Reduce_Random);
            Run("PolynomialToString / PrintPolynomial", Test_ToStringAndPrint);
            Run("CalcPolynomial", Test_Calc);
            Run("CalcPolynomial (random)", Test_Calc_Random);
            Run("AddPolynomial", Test_Add);
            Run("AddPolynomial (random)", Test_Add_Random);
            Run("Task examples", Test_TaskExamples);

            Console.WriteLine();
            Console.WriteLine("Passed checks: " + passed + ", failed checks: " + failed);
            Console.WriteLine(failed == 0 ? "ALL TESTS PASSED" : "SOME TESTS FAILED");
            return failed == 0;
        }
    }
}
