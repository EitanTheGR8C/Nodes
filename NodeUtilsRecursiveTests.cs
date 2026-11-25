using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkChain
{
    // Tests for NodeUtilsRecursive: exercises 1-11 and InsertionSortRecursive.
    // (12 and 13 are not included: they have no given signatures yet.)
    // Requires: Node<T> and NodeUtilsRecursive with the signatures from the exercise sheet.
    // Usage:  NodeUtilsRecursiveTests.RunAll();   // prints a summary, returns true if everything passed
    public static class NodeUtilsRecursiveTests
    {
        private static int passed = 0;
        private static int failed = 0;

        // ------------------------------------------------------------------ helpers

        // Build(a, b, c, ...) -> list of fresh nodes, null for no values
        private static Node<T> Build<T>(params T[] values)
        {
            Node<T> head = null;
            Node<T> tail = null;
            for (int i = 0; i < values.Length; i++)
            {
                Node<T> node = new Node<T>(values[i]);
                if (head == null)
                    head = node;
                else
                    tail.SetNext(node);
                tail = node;
            }
            return head;
        }

        // All nodes of a list, in order. Stops after 1001 nodes, so a broken list (cycle) fails instead of hanging.
        private static List<Node<T>> Nodes<T>(Node<T> list)
        {
            List<Node<T>> res = new List<Node<T>>();
            while (list != null && res.Count <= 1000)
            {
                res.Add(list);
                list = list.GetNext();
            }
            return res;
        }

        private static string DumpNodes<T>(List<Node<T>> nodes)
        {
            if (nodes.Count == 0)
                return "null";
            if (nodes.Count > 1000)
                return "...(list too long / cycle)";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < nodes.Count; i++)
            {
                if (i > 0)
                    sb.Append("->");
                sb.Append(nodes[i].GetValue());
            }
            return sb.ToString();
        }

        // "4->2->3" or "null"
        private static string Dump<T>(Node<T> list)
        {
            return DumpNodes(Nodes(list));
        }

        private static string DumpValues<T>(IEnumerable<T> values)
        {
            StringBuilder sb = new StringBuilder();
            foreach (T v in values)
            {
                if (sb.Length > 0)
                    sb.Append("->");
                sb.Append(v);
            }
            return sb.Length == 0 ? "null" : sb.ToString();
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

        private static void CheckTrue(string name, bool condition)
        {
            Check(name, "True", condition.ToString());
        }

        // The result must be exactly these nodes (the same node objects) in this order.
        // This also proves that no new nodes were created and that the links are right.
        private static void CheckSameNodes<T>(string name, List<Node<T>> expected, Node<T> actual)
        {
            List<Node<T>> act = Nodes(actual);
            bool same = act.Count == expected.Count;
            for (int i = 0; same && i < act.Count; i++)
                if (!ReferenceEquals(act[i], expected[i]))
                    same = false;
            if (same)
                passed++;
            else
            {
                failed++;
                Console.WriteLine("FAIL: " + name + " (wrong nodes or wrong order, or a new node was created)");
                Console.WriteLine("   expected: " + DumpNodes(expected));
                Console.WriteLine("   actual:   " + DumpNodes(act));
            }
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

        // Runs one test case; an exception counts as a failure of that case only (the other cases still run).
        // To keep the output short, only the first 3 exceptions of each exercise are printed.
        private static Dictionary<string, int> exceptionCount = new Dictionary<string, int>();

        private static void Case(string exercise, Action test)
        {
            try
            {
                test();
            }
            catch (Exception e)
            {
                failed++;
                int n;
                exceptionCount.TryGetValue(exercise, out n);
                exceptionCount[exercise] = n + 1;
                if (n < 3)
                    Console.WriteLine("FAIL: " + exercise + " threw " + e.GetType().Name + ": " + e.Message);
                else if (n == 3)
                    Console.WriteLine("   (more exceptions in " + exercise + " are counted but not printed)");
            }
        }

        private static int[] RandomArray(Random rnd, int maxLen, int lo, int hi)
        {
            int[] a = new int[rnd.Next(0, maxLen + 1)];
            for (int i = 0; i < a.Length; i++)
                a[i] = rnd.Next(lo, hi + 1);
            return a;
        }

        // ------------------------------------------------------------------ 1 - RemoveAll

        private static void CheckRemoveAll(int[] values, int x)
        {
            Node<int> list = Build(values);
            List<Node<int>> orig = Nodes(list);
            List<Node<int>> expected = new List<Node<int>>();
            foreach (Node<int> n in orig)
                if (n.GetValue() != x)
                    expected.Add(n);
            Node<int> result = NodeUtilsRecursive.RemoveAll(list, x);
            CheckSameNodes("RemoveAll " + DumpValues(values) + ", x=" + x, expected, result);
        }

        private static void Test_RemoveAll()
        {
            Case("RemoveAll", () => CheckRemoveAll(new int[] { 3, 5, 3, 3, 7 }, 3));
            Case("RemoveAll", () => CheckRemoveAll(new int[] { 4, 4 }, 4));
            Case("RemoveAll", () => CheckRemoveAll(new int[0], 4));
            Case("RemoveAll", () => CheckRemoveAll(new int[] { 4 }, 4));
            Case("RemoveAll", () => CheckRemoveAll(new int[] { 4 }, 5));
            Case("RemoveAll", () => CheckRemoveAll(new int[] { 1, 2, 3 }, 9));
            Case("RemoveAll", () => CheckRemoveAll(new int[] { 2, 1, 2 }, 2));
            Random rnd = new Random(1);
            for (int t = 0; t < 500; t++)
                Case("RemoveAll", () => CheckRemoveAll(RandomArray(rnd, 10, 0, 3), rnd.Next(0, 4)));
        }

        // ------------------------------------------------------------------ 2 - Reverse

        private static void CheckReverse<T>(T[] values)
        {
            Node<T> list = Build(values);
            List<Node<T>> expected = Nodes(list);
            expected.Reverse();
            Node<T> result = NodeUtilsRecursive.Reverse(list);
            CheckSameNodes("Reverse " + DumpValues(values), expected, result);
        }

        private static void Test_Reverse()
        {
            Case("Reverse", () => CheckReverse(new int[0]));
            Case("Reverse", () => CheckReverse(new int[] { 1 }));
            Case("Reverse", () => CheckReverse(new int[] { 1, 2 }));
            Case("Reverse", () => CheckReverse(new int[] { 1, 2, 3, 4 }));
            Case("Reverse", () => CheckReverse(new string[] { "a", "b", "c" }));   // generic: strings too
            Random rnd = new Random(2);
            for (int t = 0; t < 300; t++)
                Case("Reverse", () => CheckReverse(RandomArray(rnd, 10, -5, 5)));
        }

        // ------------------------------------------------------------------ 3 - MergeSorted

        private static void CheckMergeSorted(int[] a, int[] b)
        {
            Node<int> la = Build(a);
            Node<int> lb = Build(b);
            // expected order: by value; equal values: the node from the first list comes first
            List<Node<int>> all = new List<Node<int>>();
            Dictionary<Node<int>, int> key = new Dictionary<Node<int>, int>();
            int k = 0;
            foreach (Node<int> n in Nodes(la)) { all.Add(n); key[n] = k++; }
            foreach (Node<int> n in Nodes(lb)) { all.Add(n); key[n] = k++; }
            all.Sort((x, y) =>
            {
                int c = x.GetValue().CompareTo(y.GetValue());
                return c != 0 ? c : key[x].CompareTo(key[y]);
            });
            Node<int> result = NodeUtilsRecursive.MergeSorted(la, lb);
            CheckSameNodes("MergeSorted a=" + DumpValues(a) + " b=" + DumpValues(b), all, result);
        }

        private static void Test_MergeSorted()
        {
            Case("MergeSorted", () => CheckMergeSorted(new int[] { 1, 4, 6 }, new int[] { 2, 4, 9, 10 }));
            Case("MergeSorted", () => CheckMergeSorted(new int[0], new int[0]));
            Case("MergeSorted", () => CheckMergeSorted(new int[0], new int[] { 1, 2 }));
            Case("MergeSorted", () => CheckMergeSorted(new int[] { 1, 2 }, new int[0]));
            Case("MergeSorted", () => CheckMergeSorted(new int[] { 5 }, new int[] { 5 }));
            Case("MergeSorted", () => CheckMergeSorted(new int[] { 1, 2, 3 }, new int[] { 7, 8 }));
            Case("MergeSorted", () => CheckMergeSorted(new int[] { 7, 8 }, new int[] { 1, 2, 3 }));
            Case("MergeSorted", () => CheckMergeSorted(new int[] { 2, 2, 2 }, new int[] { 2, 2 }));
            Random rnd = new Random(3);
            for (int t = 0; t < 500; t++)
            {
                int[] a = RandomArray(rnd, 8, -3, 3);
                int[] b = RandomArray(rnd, 8, -3, 3);
                Array.Sort(a);
                Array.Sort(b);
                Case("MergeSorted", () => CheckMergeSorted(a, b));
            }
        }

        // ------------------------------------------------------------------ 4 - RemoveDuplicates

        private static void CheckRemoveDuplicates(int[] sortedValues)
        {
            Node<int> list = Build(sortedValues);
            List<Node<int>> orig = Nodes(list);
            List<Node<int>> expected = new List<Node<int>>();   // the first node of every value
            for (int i = 0; i < orig.Count; i++)
                if (i == 0 || orig[i].GetValue() != orig[i - 1].GetValue())
                    expected.Add(orig[i]);
            Node<int> result = NodeUtilsRecursive.RemoveDuplicates(list);
            CheckSameNodes("RemoveDuplicates " + DumpValues(sortedValues), expected, result);
        }

        private static void Test_RemoveDuplicates()
        {
            Case("RemoveDuplicates", () => CheckRemoveDuplicates(new int[] { 1, 1, 2, 3, 3, 3, 5 }));
            Case("RemoveDuplicates", () => CheckRemoveDuplicates(new int[0]));
            Case("RemoveDuplicates", () => CheckRemoveDuplicates(new int[] { 4 }));
            Case("RemoveDuplicates", () => CheckRemoveDuplicates(new int[] { 4, 4 }));
            Case("RemoveDuplicates", () => CheckRemoveDuplicates(new int[] { 1, 2, 3 }));
            Case("RemoveDuplicates", () => CheckRemoveDuplicates(new int[] { 2, 2, 2, 2 }));
            Random rnd = new Random(4);
            for (int t = 0; t < 500; t++)
            {
                int[] a = RandomArray(rnd, 12, -3, 3);
                Array.Sort(a);
                Case("RemoveDuplicates", () => CheckRemoveDuplicates(a));
            }
        }

        // ------------------------------------------------------------------ 5 - LongestAscRun

        private static int LongestAscRunModel(int[] a)
        {
            if (a.Length == 0)
                return 0;
            int best = 1;
            int cur = 1;
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] > a[i - 1])
                    cur++;
                else
                    cur = 1;
                if (cur > best)
                    best = cur;
            }
            return best;
        }

        private static void CheckLongestAscRun(int[] values)
        {
            Node<int> list = Build(values);
            int result = NodeUtilsRecursive.LongestAscRun(list);
            Check("LongestAscRun " + DumpValues(values), LongestAscRunModel(values).ToString(), result.ToString());
        }

        private static void Test_LongestAscRun()
        {
            Case("LongestAscRun", () => CheckLongestAscRun(new int[] { 4, 5, 9, 2, 3, 4, 8, 1 }));
            Case("LongestAscRun", () => CheckLongestAscRun(new int[] { 7, 7, 7 }));
            Case("LongestAscRun", () => CheckLongestAscRun(new int[0]));
            Case("LongestAscRun", () => CheckLongestAscRun(new int[] { 5 }));
            Case("LongestAscRun", () => CheckLongestAscRun(new int[] { 1, 2, 3, 4, 5 }));
            Case("LongestAscRun", () => CheckLongestAscRun(new int[] { 5, 4, 3, 2, 1 }));
            Case("LongestAscRun", () => CheckLongestAscRun(new int[] { 1, 2, 2, 3 }));
            Random rnd = new Random(5);
            for (int t = 0; t < 500; t++)
                Case("LongestAscRun", () => CheckLongestAscRun(RandomArray(rnd, 12, 0, 5)));
        }

        // ------------------------------------------------------------------ 6 - AddOne

        private static void CheckAddOne(int[] digits)
        {
            // model: school addition on the digit array
            List<int> expected = new List<int>(digits);
            int i = expected.Count - 1;
            while (i >= 0 && expected[i] == 9)
            {
                expected[i] = 0;
                i--;
            }
            if (i >= 0)
                expected[i]++;
            else
                expected.Insert(0, 1);

            Node<int> list = Build(digits);
            List<Node<int>> orig = Nodes(list);
            Node<int> result = NodeUtilsRecursive.AddOne(list);
            string name = "AddOne " + DumpValues(digits);
            Check(name, DumpValues(expected), Dump(result));

            // at most one new node is allowed (the extra digit, like 99 -> 100)
            int newNodes = 0;
            List<Node<int>> res = Nodes(result);
            foreach (Node<int> n in res)
                if (!orig.Contains(n))
                    newNodes++;
            CheckTrue(name + " (at most one new node)", newNodes <= 1);
        }

        private static void Test_AddOne()
        {
            Case("AddOne", () => CheckAddOne(new int[] { 1, 2, 9 }));
            Case("AddOne", () => CheckAddOne(new int[] { 9, 9 }));
            Case("AddOne", () => CheckAddOne(new int[] { 0 }));
            Case("AddOne", () => CheckAddOne(new int[] { 9 }));
            Case("AddOne", () => CheckAddOne(new int[] { 1, 2, 8 }));
            Case("AddOne", () => CheckAddOne(new int[] { 9, 9, 9, 9 }));
            Case("AddOne", () => CheckAddOne(new int[] { 1, 0, 9, 9 }));
            Random rnd = new Random(6);
            for (int t = 0; t < 500; t++)
            {
                int len = rnd.Next(1, 9);
                int[] d = new int[len];
                for (int i = 0; i < len; i++)
                    d[i] = rnd.Next(0, 10);
                if (len > 1 && d[0] == 0)
                    d[0] = rnd.Next(1, 10);       // no leading zeros
                if (rnd.Next(0, 4) == 0)
                    for (int i = Math.Max(1, len - 3); i < len; i++)
                        d[i] = 9;                  // many carries
                Case("AddOne", () => CheckAddOne(d));
            }
        }

        // ------------------------------------------------------------------ 7 - AlternateSplit

        private static void CheckAlternateSplit(int[] values)
        {
            Node<int> list = Build(values);
            List<Node<int>> orig = Nodes(list);
            List<Node<int>> expectedOdd = new List<Node<int>>();    // places 1, 3, 5, ...
            List<Node<int>> expectedEven = new List<Node<int>>();   // places 2, 4, 6, ...
            for (int i = 0; i < orig.Count; i++)
            {
                if (i % 2 == 0)
                    expectedOdd.Add(orig[i]);
                else
                    expectedEven.Add(orig[i]);
            }
            Node<int> odd;
            Node<int> even;
            NodeUtilsRecursive.AlternateSplit(list, out odd, out even);
            CheckSameNodes("AlternateSplit odd " + DumpValues(values), expectedOdd, odd);
            CheckSameNodes("AlternateSplit even " + DumpValues(values), expectedEven, even);
        }

        private static void Test_AlternateSplit()
        {
            Case("AlternateSplit", () => CheckAlternateSplit(new int[] { 1, 2, 3, 4, 5 }));
            Case("AlternateSplit", () => CheckAlternateSplit(new int[] { 1, 2, 3, 4 }));
            Case("AlternateSplit", () => CheckAlternateSplit(new int[0]));
            Case("AlternateSplit", () => CheckAlternateSplit(new int[] { 9 }));
            Case("AlternateSplit", () => CheckAlternateSplit(new int[] { 9, 8 }));
            Random rnd = new Random(7);
            for (int t = 0; t < 300; t++)
                Case("AlternateSplit", () => CheckAlternateSplit(RandomArray(rnd, 10, 0, 9)));
        }

        // ------------------------------------------------------------------ 8 - RemoveNthFromEnd

        private static void CheckRemoveNthFromEnd(int[] values, int n)
        {
            Node<int> list = Build(values);
            List<Node<int>> orig = Nodes(list);
            List<Node<int>> expected = new List<Node<int>>(orig);
            expected.RemoveAt(orig.Count - n);                    // n = 1 is the last node
            Node<int> result = NodeUtilsRecursive.RemoveNthFromEnd(list, n);
            CheckSameNodes("RemoveNthFromEnd " + DumpValues(values) + ", n=" + n, expected, result);
        }

        private static void Test_RemoveNthFromEnd()
        {
            Case("RemoveNthFromEnd", () => CheckRemoveNthFromEnd(new int[] { 1, 2, 3, 4, 5 }, 2));
            Case("RemoveNthFromEnd", () => CheckRemoveNthFromEnd(new int[] { 1, 2, 3, 4, 5 }, 5));
            Case("RemoveNthFromEnd", () => CheckRemoveNthFromEnd(new int[] { 1, 2, 3, 4, 5 }, 1));
            Case("RemoveNthFromEnd", () => CheckRemoveNthFromEnd(new int[] { 7 }, 1));
            Case("RemoveNthFromEnd", () => CheckRemoveNthFromEnd(new int[] { 7, 8 }, 1));
            Case("RemoveNthFromEnd", () => CheckRemoveNthFromEnd(new int[] { 7, 8 }, 2));
            Random rnd = new Random(8);
            for (int t = 0; t < 200; t++)
            {
                int[] a = RandomArray(rnd, 9, 0, 5);
                for (int n = 1; n <= a.Length; n++)               // the exercise guarantees 1 <= n <= length
                    Case("RemoveNthFromEnd", () => CheckRemoveNthFromEnd(a, n));
            }
        }

        // ------------------------------------------------------------------ 9 - IsPalindrome

        private static void CheckIsPalindrome(int[] values)
        {
            bool expected = true;
            for (int i = 0; i < values.Length / 2; i++)
                if (values[i] != values[values.Length - 1 - i])
                    expected = false;

            Node<int> list = Build(values);
            List<Node<int>> orig = Nodes(list);
            bool result = NodeUtilsRecursive.IsPalindrome(list);
            string name = "IsPalindrome " + DumpValues(values);
            Check(name, expected.ToString(), result.ToString());
            CheckSameNodes(name + " (the list must not change)", orig, list);
            Check(name + " (values must not change)", DumpValues(values), Dump(list));
        }

        private static void Test_IsPalindrome()
        {
            Case("IsPalindrome", () => CheckIsPalindrome(new int[] { 1, 2, 3, 2, 1 }));
            Case("IsPalindrome", () => CheckIsPalindrome(new int[] { 1, 2, 2, 1 }));
            Case("IsPalindrome", () => CheckIsPalindrome(new int[] { 1, 2, 3 }));
            Case("IsPalindrome", () => CheckIsPalindrome(new int[0]));
            Case("IsPalindrome", () => CheckIsPalindrome(new int[] { 5 }));
            Case("IsPalindrome", () => CheckIsPalindrome(new int[] { 5, 5 }));
            Case("IsPalindrome", () => CheckIsPalindrome(new int[] { 5, 6 }));
            Case("IsPalindrome", () => CheckIsPalindrome(new int[] { 1, 2, 3, 3, 1 }));
            Random rnd = new Random(9);
            for (int t = 0; t < 400; t++)
            {
                int[] a = RandomArray(rnd, 7, 0, 2);
                Case("IsPalindrome", () => CheckIsPalindrome(a));
                // a real palindrome built from the same values
                int[] p = new int[a.Length * 2 + (t % 2)];
                for (int i = 0; i < a.Length; i++)
                {
                    p[i] = a[i];
                    p[p.Length - 1 - i] = a[i];
                }
                if (t % 2 == 1)
                    p[a.Length] = rnd.Next(0, 3);
                Case("IsPalindrome", () => CheckIsPalindrome(p));
            }
        }

        // ------------------------------------------------------------------ 10 - MergeSort

        private static void CheckMergeSort(int[] values)
        {
            Node<int> list = Build(values);
            List<Node<int>> orig = Nodes(list);
            Node<int> result = NodeUtilsRecursive.MergeSort(list);

            int[] sorted = (int[])values.Clone();
            Array.Sort(sorted);
            string name = "MergeSort " + DumpValues(values);
            Check(name, DumpValues(sorted), Dump(result));

            // no new nodes, none lost (equal values may sit in any order)
            List<Node<int>> res = Nodes(result);
            HashSet<Node<int>> origSet = new HashSet<Node<int>>(orig);
            HashSet<Node<int>> seen = new HashSet<Node<int>>();
            bool ok = res.Count == orig.Count;
            foreach (Node<int> n in res)
                if (!origSet.Contains(n) || !seen.Add(n))
                    ok = false;
            CheckTrue(name + " (same nodes, none new, none lost)", ok);
        }

        private static void Test_MergeSort()
        {
            Case("MergeSort", () => CheckMergeSort(new int[] { 5, 2, 9, 1, 5, 3 }));
            Case("MergeSort", () => CheckMergeSort(new int[0]));
            Case("MergeSort", () => CheckMergeSort(new int[] { 4 }));
            Case("MergeSort", () => CheckMergeSort(new int[] { 2, 1 }));
            Case("MergeSort", () => CheckMergeSort(new int[] { 1, 2, 3, 4 }));
            Case("MergeSort", () => CheckMergeSort(new int[] { 4, 3, 2, 1 }));
            Case("MergeSort", () => CheckMergeSort(new int[] { 4, 2, 3, 1 }));
            Case("MergeSort", () => CheckMergeSort(new int[] { 3, 3, 3 }));
            Random rnd = new Random(10);
            for (int t = 0; t < 500; t++)
                Case("MergeSort", () => CheckMergeSort(RandomArray(rnd, 14, -5, 5)));
        }

        // ------------------------------------------------------------------ 11 - MoveLast2First

        private static void CheckMoveLast2First<T>(T[] values)
        {
            Node<T> list = Build(values);
            List<Node<T>> orig = Nodes(list);
            List<Node<T>> expected = new List<Node<T>>(orig);
            if (expected.Count > 1)
            {
                Node<T> last = expected[expected.Count - 1];
                expected.RemoveAt(expected.Count - 1);
                expected.Insert(0, last);
            }
            Node<T> result = NodeUtilsRecursive.MoveLast2First(list);
            CheckSameNodes("MoveLast2First " + DumpValues(values), expected, result);
        }

        private static void Test_MoveLast2First()
        {
            Case("MoveLast2First", () => CheckMoveLast2First(new int[0]));
            Case("MoveLast2First", () => CheckMoveLast2First(new int[] { 1 }));
            Case("MoveLast2First", () => CheckMoveLast2First(new int[] { 1, 2 }));
            Case("MoveLast2First", () => CheckMoveLast2First(new int[] { 1, 2, 3, 4 }));
            Case("MoveLast2First", () => CheckMoveLast2First(new string[] { "a", "b", "c" }));   // generic: strings too
            Random rnd = new Random(11);
            for (int t = 0; t < 300; t++)
                Case("MoveLast2First", () => CheckMoveLast2First(RandomArray(rnd, 10, 0, 9)));
        }

        // ------------------------------------------------------------------ InsertionSortRecursive

        private static void CheckInsertionSortRecursive(int[] values)
        {
            Node<int> list = Build(values);
            List<Node<int>> orig = Nodes(list);
            Node<int> result = NodeUtilsRecursive.InsertionSortRecursive(list);

            int[] sorted = (int[])values.Clone();
            Array.Sort(sorted);
            string name = "InsertionSortRecursive " + DumpValues(values);
            Check(name, DumpValues(sorted), Dump(result));

            // no new nodes, none lost (equal values may sit in any order)
            List<Node<int>> res = Nodes(result);
            HashSet<Node<int>> origSet = new HashSet<Node<int>>(orig);
            HashSet<Node<int>> seen = new HashSet<Node<int>>();
            bool ok = res.Count == orig.Count;
            foreach (Node<int> n in res)
                if (!origSet.Contains(n) || !seen.Add(n))
                    ok = false;
            CheckTrue(name + " (same nodes, none new, none lost)", ok);
        }

        private static void Test_InsertionSortRecursive()
        {
            Case("InsertionSortRecursive", () => CheckInsertionSortRecursive(new int[] { 5, 2, 9, 1, 5, 3 }));
            Case("InsertionSortRecursive", () => CheckInsertionSortRecursive(new int[0]));
            Case("InsertionSortRecursive", () => CheckInsertionSortRecursive(new int[] { 4 }));
            Case("InsertionSortRecursive", () => CheckInsertionSortRecursive(new int[] { 2, 1 }));
            Case("InsertionSortRecursive", () => CheckInsertionSortRecursive(new int[] { 1, 2, 3, 4 }));
            Case("InsertionSortRecursive", () => CheckInsertionSortRecursive(new int[] { 4, 3, 2, 1 }));
            Case("InsertionSortRecursive", () => CheckInsertionSortRecursive(new int[] { 4, 2, 3, 1 }));
            Case("InsertionSortRecursive", () => CheckInsertionSortRecursive(new int[] { 3, 3, 3 }));
            Random rnd = new Random(12);
            for (int t = 0; t < 500; t++)
                Case("InsertionSortRecursive", () => CheckInsertionSortRecursive(RandomArray(rnd, 14, -5, 5)));
        }

        // ------------------------------------------------------------------ run everything

        public static bool RunAll()
        {
            passed = 0;
            failed = 0;
            exceptionCount.Clear();
            Run("1 - RemoveAll", Test_RemoveAll);
            Run("2 - Reverse", Test_Reverse);
            Run("3 - MergeSorted", Test_MergeSorted);
            Run("4 - RemoveDuplicates", Test_RemoveDuplicates);
            Run("5 - LongestAscRun", Test_LongestAscRun);
            Run("6 - AddOne", Test_AddOne);
            Run("7 - AlternateSplit", Test_AlternateSplit);
            Run("8 - RemoveNthFromEnd", Test_RemoveNthFromEnd);
            Run("9 - IsPalindrome", Test_IsPalindrome);
            Run("10 - MergeSort", Test_MergeSort);
            Run("11 - MoveLast2First", Test_MoveLast2First);
            Run("InsertionSortRecursive", Test_InsertionSortRecursive);

            Console.WriteLine();
            Console.WriteLine("Passed checks: " + passed + ", failed checks: " + failed);
            Console.WriteLine(failed == 0 ? "ALL TESTS PASSED" : "SOME TESTS FAILED");
            return failed == 0;
        }
    }

}
