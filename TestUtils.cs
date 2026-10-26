using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkChain
{
    /// <summary>
    /// פעולות בדיקה לפעולות שבקובץ NodeUtils.cs
    /// כל בדיקה בנויה לפי המתכונת הנדרשת:
    /// יצירת הרשימה עם CreateListFromArray -> הדפסת הרשימה -> הפעלת הפעולה הנבדקת -> הדפסת התוצאה.
    /// בכל בדיקה 3 קלטים שונים ומשמעותיים (התחלה / אמצע / סוף).
    /// </summary>
    internal class TestUtils
    {
        // ============================================================
        // תרגיל 1 - CompareList & CompareListR
        // ============================================================
        public static void Test_CompareList()
        {
            Console.WriteLine("===== Test_CompareList =====");

            // test 1: identical lists
            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3, 4 });
            Node<int> l2 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3, 4 });
            Console.Write("list 1: "); NodeUtils.PrintList(l1);
            Console.Write("list 2: "); NodeUtils.PrintList(l2);
            Console.WriteLine($"CompareList : {NodeUtils.CompareList(l1, l2)}");
            Console.WriteLine($"CompareListR: {NodeUtils.CompareListR(l1, l2)}");

            // test 2: different lengths
            Node<int> l3 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3 });
            Node<int> l4 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3, 4 });
            Console.Write("list 3: "); NodeUtils.PrintList(l3);
            Console.Write("list 4: "); NodeUtils.PrintList(l4);
            Console.WriteLine($"CompareList : {NodeUtils.CompareList(l3, l4)}");
            Console.WriteLine($"CompareListR: {NodeUtils.CompareListR(l3, l4)}");

            // test 3: same length, value mismatch at end
            Node<int> l5 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3, 5 });
            Node<int> l6 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3, 9 });
            Console.Write("list 5: "); NodeUtils.PrintList(l5);
            Console.Write("list 6: "); NodeUtils.PrintList(l6);
            Console.WriteLine($"CompareList : {NodeUtils.CompareList(l5, l6)}");
            Console.WriteLine($"CompareListR: {NodeUtils.CompareListR(l5, l6)}");

            // test 4: both empty
            Node<int> l7 = null;
            Node<int> l8 = null;
            Console.WriteLine($"CompareList(null, null) : {NodeUtils.CompareList(l7, l8)}");
            Console.WriteLine($"CompareListR(null, null): {NodeUtils.CompareListR(l7, l8)}");

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 2 - CountList & CountListR
        // ============================================================
        public static void Test_CountList()
        {
            Console.WriteLine("===== Test_CountList =====");

            // test 1: standard list
            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { 10, 20, 30, 40, 50 });
            Console.Write("list 1: "); NodeUtils.PrintList(l1);
            Console.WriteLine($"CountList : {NodeUtils.CountList(l1)}");
            Console.WriteLine($"CountListR: {NodeUtils.CountListR(l1)}");

            // test 2: single element
            Node<int> l2 = NodeUtils.CreateListFromArray(new int[] { 99 });
            Console.Write("list 2: "); NodeUtils.PrintList(l2);
            Console.WriteLine($"CountList : {NodeUtils.CountList(l2)}");
            Console.WriteLine($"CountListR: {NodeUtils.CountListR(l2)}");

            // test 3: empty list
            Node<int> l3 = null;
            Console.WriteLine($"CountList(null) : {NodeUtils.CountList(l3)}");
            Console.WriteLine($"CountListR(null): {NodeUtils.CountListR(l3)}");

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 3 - SumList & SumListR
        // ============================================================
        public static void Test_SumList()
        {
            Console.WriteLine("===== Test_SumList =====");

            // test 1: positive numbers
            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { 5, 10, 15, 20 });
            Console.Write("list 1: "); NodeUtils.PrintList(l1);
            Console.WriteLine($"SumList : {NodeUtils.SumList(l1)}");
            Console.WriteLine($"SumListR: {NodeUtils.SumListR(l1)}");

            // test 2: mix of positive and negative
            Node<int> l2 = NodeUtils.CreateListFromArray(new int[] { -5, 10, -2, 7 });
            Console.Write("list 2: "); NodeUtils.PrintList(l2);
            Console.WriteLine($"SumList : {NodeUtils.SumList(l2)}");
            Console.WriteLine($"SumListR: {NodeUtils.SumListR(l2)}");

            // test 3: single element
            Node<int> l3 = NodeUtils.CreateListFromArray(new int[] { 42 });
            Console.Write("list 3: "); NodeUtils.PrintList(l3);
            Console.WriteLine($"SumList : {NodeUtils.SumList(l3)}");
            Console.WriteLine($"SumListR: {NodeUtils.SumListR(l3)}");

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 4 - IsExist & IsExistR
        // ============================================================
        public static void Test_IsExist()
        {
            Console.WriteLine("===== Test_IsExist =====");

            int[] a = { 10, 13, 16, 20, 35, 50, 54 };
            int[] b = { 20, 21, 60 };

            Console.WriteLine("list a: ");
            Node<int> ls = NodeUtils.CreateListFromArray(a);
            NodeUtils.PrintList(ls);

            Console.WriteLine($"IsExist(20)  {NodeUtils.IsExist(ls, 20)}");
            Console.WriteLine($"IsExist(21)  {NodeUtils.IsExist(ls, 21)}");
            Console.WriteLine($"IsExist(60)  {NodeUtils.IsExist(ls, 60)}");
            Console.WriteLine($"IsExistR(10) {NodeUtils.IsExistR(ls, 10)}");
            Console.WriteLine($"IsExistR(55) {NodeUtils.IsExistR(ls, 55)}");
            Console.WriteLine($"IsExistR(16) {NodeUtils.IsExistR(ls, 16)}");

            Console.WriteLine("list b: ");
            Node<int> ls2 = NodeUtils.CreateListFromArray(b);
            NodeUtils.PrintList(ls2);
            Console.WriteLine($"IsExist(20)  {NodeUtils.IsExist(ls2, 20)}");
            Console.WriteLine($"IsExist(21)  {NodeUtils.IsExist(ls2, 21)}");
            Console.WriteLine($"IsExist(60)  {NodeUtils.IsExist(ls2, 60)}");
            Console.WriteLine($"IsExistR(10) {NodeUtils.IsExistR(ls2, 10)}");
            Console.WriteLine($"IsExistR(55) {NodeUtils.IsExistR(ls2, 55)}");
            Console.WriteLine($"IsExistR(16) {NodeUtils.IsExistR(ls2, 16)}");


            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 5 - MaxList & MaxListR
        // ============================================================
        public static void Test_MaxList()
        {
            Console.WriteLine("===== Test_MaxList =====");

            // test 1: max at start
            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { 99, 10, 20, 30 });
            Console.Write("max at start : "); NodeUtils.PrintList(l1);
            Console.WriteLine($"MaxList : {NodeUtils.MaxList(l1)}");
            Console.WriteLine($"MaxListR: {NodeUtils.MaxListR(l1)}");

            // test 2: max in middle
            Node<int> l2 = NodeUtils.CreateListFromArray(new int[] { 10, 20, 88, 5, 12 });
            Console.Write("max in middle: "); NodeUtils.PrintList(l2);
            Console.WriteLine($"MaxList : {NodeUtils.MaxList(l2)}");
            Console.WriteLine($"MaxListR: {NodeUtils.MaxListR(l2)}");

            // test 3: max at end
            Node<int> l3 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3, 100 });
            Console.Write("max at end   : "); NodeUtils.PrintList(l3);
            Console.WriteLine($"MaxList : {NodeUtils.MaxList(l3)}");
            Console.WriteLine($"MaxListR: {NodeUtils.MaxListR(l3)}");

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 6 - MakePositive & MakePositiveR
        // ============================================================
        public static void Test_MakePositive()
        {
            Console.WriteLine("===== Test_MakePositive =====");

            int[] a1 = { -3, -8, 5, -2, 7, 4 };
            Node<int> ls1 = NodeUtils.CreateListFromArray(a1);
            Console.Write("before: "); NodeUtils.PrintList(ls1);
            Console.WriteLine();
            NodeUtils.MakePositive(ls1);
            Console.Write("after : "); NodeUtils.PrintList(ls1);
            NodeUtils.MakePositiveR(ls1);
            Console.Write("afterR: "); NodeUtils.PrintList(ls1);

            int[] a2 = { 6, 3, 9, -1, -4 };
            Node<int> ls2 = NodeUtils.CreateListFromArray(a2);
            Console.Write("before: "); NodeUtils.PrintList(ls2);
            Console.WriteLine();
            NodeUtils.MakePositiveR(ls2);
            Console.Write("after : "); NodeUtils.PrintList(ls2);
            NodeUtils.MakePositiveR(ls2);
            Console.Write("afterR: "); NodeUtils.PrintList(ls2);

            int[] a3 = { 1, 2, 3 };
            Node<int> ls3 = NodeUtils.CreateListFromArray(a3);
            Console.Write("before: "); NodeUtils.PrintList(ls3);
            Console.WriteLine();
            NodeUtils.MakePositive(ls3);
            Console.Write("after : "); NodeUtils.PrintList(ls3);
            NodeUtils.MakePositiveR(ls3);
            Console.Write("afterR: "); NodeUtils.PrintList(ls3);

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 7 - GetElement & GetElementR
        // ============================================================
        public static void Test_GetElement()
        {
            Console.WriteLine("===== Test_GetElement =====");

            int[] a1 = { 4, 7, 2, 9, 5, 1 };
            Node<int> ls1 = NodeUtils.CreateListFromArray(a1);
            Console.Write("list1: "); NodeUtils.PrintList(ls1);
            PrintNode("GetElement(1)  ", NodeUtils.GetElement(ls1, 1));
            PrintNode("GetElement(4)  ", NodeUtils.GetElement(ls1, 4));
            PrintNode("GetElement(6)  ", NodeUtils.GetElement(ls1, 6));
            PrintNode("GetElement(7)  ", NodeUtils.GetElement(ls1, 7));
            PrintNode("GetElementR(3) ", NodeUtils.GetElementR(ls1, 3));

            int[] a2 = { 42 };
            Node<int> ls2 = NodeUtils.CreateListFromArray(a2);
            Console.Write("list2: "); NodeUtils.PrintList(ls2);
            PrintNode("GetElement(1)  ", NodeUtils.GetElement(ls2, 1));
            PrintNode("GetElementR(2) ", NodeUtils.GetElementR(ls2, 2));

            string[] a3 = { "aa", "bb", "cc", "dd" };
            Node<string> ls3 = NodeUtils.CreateListFromArray(a3);
            Console.Write("list3: "); NodeUtils.PrintList(ls3);
            Node<string> res = NodeUtils.GetElementR(ls3, 4);
            Console.WriteLine("GetElementR(4) " + (res == null ? "null" : res.GetValue()));

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 8 - IsSorted & IsSortedR
        // ============================================================
        public static void Test_IsSorted()
        {
            Console.WriteLine("===== Test_IsSorted =====");

            // test 1: sorted with duplicates
            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { 2, 5, 5, 8, 12 });
            Console.Write("sorted list  : "); NodeUtils.PrintList(l1);
            Console.WriteLine($"IsSorted : {NodeUtils.IsSorted(l1)}");
            Console.WriteLine($"IsSortedR: {NodeUtils.IsSortedR(l1)}");

            // test 2: unsorted in middle
            Node<int> l2 = NodeUtils.CreateListFromArray(new int[] { 2, 5, 3, 8, 12 });
            Console.Write("unsorted mid : "); NodeUtils.PrintList(l2);
            Console.WriteLine($"IsSorted : {NodeUtils.IsSorted(l2)}");
            Console.WriteLine($"IsSortedR: {NodeUtils.IsSortedR(l2)}");

            // test 3: unsorted at end
            Node<int> l3 = NodeUtils.CreateListFromArray(new int[] { 2, 4, 6, 8, 5 });
            Console.Write("unsorted end : "); NodeUtils.PrintList(l3);
            Console.WriteLine($"IsSorted : {NodeUtils.IsSorted(l3)}");
            Console.WriteLine($"IsSortedR: {NodeUtils.IsSortedR(l3)}");

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 9 - CountSequences
        // ============================================================
        public static void Test_CountSequences()
        {
            Console.WriteLine("===== Test_CountSequences =====");

            // test 1: sequences at start and middle
            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { 4, 4, 5, 6, 6, 3, 4, 4, 4, 22, 3 });
            Console.Write("list 1: "); NodeUtils.PrintList(l1);
            Console.WriteLine($"CountSequences(4): {NodeUtils.CountSequences(l1, 4)}"); // should be 2

            // test 2: sequence at end
            Node<int> l2 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3, 5, 5, 5 });
            Console.Write("list 2: "); NodeUtils.PrintList(l2);
            Console.WriteLine($"CountSequences(5): {NodeUtils.CountSequences(l2, 5)}"); // should be 1

            // test 3: value does not exist
            Node<int> l3 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3, 4 });
            Console.Write("list 3: "); NodeUtils.PrintList(l3);
            Console.WriteLine($"CountSequences(9): {NodeUtils.CountSequences(l3, 9)}"); // should be 0

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 10 - PrintBetween
        // ============================================================
        public static void Test_PrintBetween()
        {
            Console.WriteLine("===== Test_PrintBetween =====");

            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { 10, 20, 30, 40, 50, 60 });
            Console.Write("full list: "); NodeUtils.PrintList(l1);

            // test 1: start to middle (1 to 3)
            Console.WriteLine("PrintBetween(1, 3):");
            NodeUtils.PrintBetween(l1, 1, 3);

            // test 2: middle to end (3 to 6)
            Console.WriteLine("PrintBetween(3, 6):");
            NodeUtils.PrintBetween(l1, 3, 6);

            // test 3: single element position (2 to 2)
            Console.WriteLine("PrintBetween(2, 2):");
            NodeUtils.PrintBetween(l1, 2, 2);

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 11 - RemoveDuplicates
        // ============================================================
        public static void Test_RemoveDuplicates()
        {
            Console.WriteLine("===== Test_RemoveDuplicates =====");

            // test 1: duplicates at the start
            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { 3, 3, 5, 8, 2 });
            Console.Write("list 1: "); NodeUtils.PrintList(l1);
            Console.Write("result: "); NodeUtils.PrintList(NodeUtils.RemoveDuplicates(l1));
            Console.Write("original still: "); NodeUtils.PrintList(l1);

            // test 2: duplicates in the middle
            Node<int> l2 = NodeUtils.CreateListFromArray(new int[] { 1, 4, 4, 4, 9 });
            Console.Write("list 2: "); NodeUtils.PrintList(l2);
            Console.Write("result: "); NodeUtils.PrintList(NodeUtils.RemoveDuplicates(l2));

            // test 3: duplicates at the end
            Node<int> l3 = NodeUtils.CreateListFromArray(new int[] { 7, 2, 5, 5, 5 });
            Console.Write("list 3: "); NodeUtils.PrintList(l3);
            Console.Write("result: "); NodeUtils.PrintList(NodeUtils.RemoveDuplicates(l3));

            // test 4: empty list
            Console.Write("RemoveDuplicates(null): "); NodeUtils.PrintList(NodeUtils.RemoveDuplicates(null));

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 12 - IsBalanced
        // ============================================================
        public static void Test_IsBalanced()
        {
            Console.WriteLine("===== Test_IsBalanced =====");

            // test 1: balanced (2 below avg, 2 above avg)
            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3, 4, 5 });
            Console.Write("list 1: "); NodeUtils.PrintList(l1);
            Console.WriteLine($"IsBalanced: {NodeUtils.IsBalanced(l1)}"); // avg=3, below:1,2 above:4,5 -> true

            // test 2: unbalanced, more values below the average
            Node<int> l2 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3, 100 });
            Console.Write("list 2: "); NodeUtils.PrintList(l2);
            Console.WriteLine($"IsBalanced: {NodeUtils.IsBalanced(l2)}"); // avg=26.5, below:1,2,3 above:100 -> false

            // test 3: unbalanced, more values above the average
            Node<int> l3 = NodeUtils.CreateListFromArray(new int[] { 1, 50, 60, 70 });
            Console.Write("list 3: "); NodeUtils.PrintList(l3);
            Console.WriteLine($"IsBalanced: {NodeUtils.IsBalanced(l3)}"); // avg=45.25, below:1 above:50,60,70 -> false

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 13 - RemoveMax
        // ============================================================
        public static void Test_RemoveMax()
        {
            Console.WriteLine("===== Test_RemoveMax =====");

            // test 1: max at the start (head changes)
            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { 9, 2, 4, 1 });
            Console.Write("list 1: "); NodeUtils.PrintList(l1);
            Console.Write("result: "); NodeUtils.PrintList(NodeUtils.RemoveMax(l1));

            // test 2: max in the middle
            Node<int> l2 = NodeUtils.CreateListFromArray(new int[] { 5, 8, 3, 1 });
            Console.Write("list 2: "); NodeUtils.PrintList(l2);
            Console.Write("result: "); NodeUtils.PrintList(NodeUtils.RemoveMax(l2));

            // test 3: max at the end
            Node<int> l3 = NodeUtils.CreateListFromArray(new int[] { 3, 1, 2, 9 });
            Console.Write("list 3: "); NodeUtils.PrintList(l3);
            Console.Write("result: "); NodeUtils.PrintList(NodeUtils.RemoveMax(l3));

            // test 4: max appears more than once - only the first occurrence is removed
            Node<int> l4 = NodeUtils.CreateListFromArray(new int[] { 5, 8, 3, 8, 1 });
            Console.Write("list 4: "); NodeUtils.PrintList(l4);
            Console.Write("result: "); NodeUtils.PrintList(NodeUtils.RemoveMax(l4)); // expect 5,3,8,1

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 14 - Insert2SortedList
        // ============================================================
        public static void Test_Insert2SortedList()
        {
            Console.WriteLine("===== Test_Insert2SortedList =====");

            // test 1: insert at the start (new smallest)
            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { 3, 5, 8 });
            Console.Write("list 1: "); NodeUtils.PrintList(l1);
            Console.Write("insert(0): "); NodeUtils.PrintList(NodeUtils.Insert2SortedList(l1, 0));

            // test 2: insert in the middle
            Node<int> l2 = NodeUtils.CreateListFromArray(new int[] { 1, 3, 5 });
            Console.Write("list 2: "); NodeUtils.PrintList(l2);
            Console.Write("insert(4): "); NodeUtils.PrintList(NodeUtils.Insert2SortedList(l2, 4));

            // test 3: insert at the end (new biggest)
            Node<int> l3 = NodeUtils.CreateListFromArray(new int[] { 1, 3, 5 });
            Console.Write("list 3: "); NodeUtils.PrintList(l3);
            Console.Write("insert(10): "); NodeUtils.PrintList(NodeUtils.Insert2SortedList(l3, 10));

            // test 4: insert into an empty list
            Console.Write("insert(7) into null: "); NodeUtils.PrintList(NodeUtils.Insert2SortedList(null, 7));

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 15 - SortList
        // ============================================================
        public static void Test_SortList()
        {
            Console.WriteLine("===== Test_SortList =====");

            // test 1: reverse sorted
            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { 5, 4, 3, 2, 1 });
            Console.Write("list 1: "); NodeUtils.PrintList(l1);
            Console.Write("sorted: "); NodeUtils.PrintList(NodeUtils.SortList(l1));

            // test 2: random order with duplicates
            Node<int> l2 = NodeUtils.CreateListFromArray(new int[] { 5, 2, 8, 2, 1 });
            Console.Write("list 2: "); NodeUtils.PrintList(l2);
            Console.Write("sorted: "); NodeUtils.PrintList(NodeUtils.SortList(l2));

            // test 3: already sorted
            Node<int> l3 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3 });
            Console.Write("list 3: "); NodeUtils.PrintList(l3);
            Console.Write("sorted: "); NodeUtils.PrintList(NodeUtils.SortList(l3));

            // test 4: empty list
            Console.Write("SortList(null): "); NodeUtils.PrintList(NodeUtils.SortList(null));

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 16 - ReverseList
        // ============================================================
        public static void Test_ReverseList()
        {
            Console.WriteLine("===== Test_ReverseList =====");

            // test 1: several elements
            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { 4, 7, 2, 9, 5 });
            Console.Write("list 1: "); NodeUtils.PrintList(l1);
            Console.Write("reversed: "); NodeUtils.PrintList(NodeUtils.ReverseList(l1));

            // test 2: two elements
            Node<int> l2 = NodeUtils.CreateListFromArray(new int[] { 1, 2 });
            Console.Write("list 2: "); NodeUtils.PrintList(l2);
            Console.Write("reversed: "); NodeUtils.PrintList(NodeUtils.ReverseList(l2));

            // test 3: single element
            Node<int> l3 = NodeUtils.CreateListFromArray(new int[] { 42 });
            Console.Write("list 3: "); NodeUtils.PrintList(l3);
            Console.Write("reversed: "); NodeUtils.PrintList(NodeUtils.ReverseList(l3));

            // test 4: empty list
            Console.Write("ReverseList(null): "); NodeUtils.PrintList(NodeUtils.ReverseList<int>(null));

            // test 5: generic - works on strings too
            Node<string> l5 = NodeUtils.CreateListFromArray(new string[] { "aa", "bb", "cc" });
            Console.Write("list 5: "); NodeUtils.PrintList(l5);
            Console.Write("reversed: "); NodeUtils.PrintList(NodeUtils.ReverseList(l5));

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 17 - MaxNegativeSeq
        // ============================================================
        public static void Test_MaxNegativeSeq()
        {
            Console.WriteLine("===== Test_MaxNegativeSeq =====");

            // test 1: negative sequence at the start
            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { -1, -2, -3, 4, 5 });
            Console.Write("list 1: "); NodeUtils.PrintList(l1);
            Console.WriteLine($"MaxNegativeSeq: {NodeUtils.MaxNegativeSeq(l1)}"); // expect 3

            // test 2: two negative sequences in the middle
            Node<int> l2 = NodeUtils.CreateListFromArray(new int[] { 3, -1, -5, -2, 4, -6, -6, -6, 2 });
            Console.Write("list 2: "); NodeUtils.PrintList(l2);
            Console.WriteLine($"MaxNegativeSeq: {NodeUtils.MaxNegativeSeq(l2)}"); // expect 3

            // test 3: no negative numbers at all
            Node<int> l3 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3, 4 });
            Console.Write("list 3: "); NodeUtils.PrintList(l3);
            Console.WriteLine($"MaxNegativeSeq: {NodeUtils.MaxNegativeSeq(l3)}"); // expect 0

            // test 4: empty list
            Console.WriteLine($"MaxNegativeSeq(null): {NodeUtils.MaxNegativeSeq(null)}"); // expect 0

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 18 - MergeLists
        // ============================================================
        public static void Test_MergeLists()
        {
            Console.WriteLine("===== Test_MergeLists =====");

            // test 1: interleaved values
            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { 1, 4, 7 });
            Node<int> l2 = NodeUtils.CreateListFromArray(new int[] { 2, 3, 8 });
            Console.Write("list 1: "); NodeUtils.PrintList(l1);
            Console.Write("list 2: "); NodeUtils.PrintList(l2);
            Console.Write("merged: "); NodeUtils.PrintList(NodeUtils.MergeLists(l1, l2));
            Console.Write("list 1 still: "); NodeUtils.PrintList(l1); // must stay unchanged
            Console.Write("list 2 still: "); NodeUtils.PrintList(l2); // must stay unchanged

            // test 2: one list entirely smaller than the other
            Node<int> l3 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3 });
            Node<int> l4 = NodeUtils.CreateListFromArray(new int[] { 10, 20 });
            Console.Write("list 3: "); NodeUtils.PrintList(l3);
            Console.Write("list 4: "); NodeUtils.PrintList(l4);
            Console.Write("merged: "); NodeUtils.PrintList(NodeUtils.MergeLists(l3, l4));

            // test 3: shared values across both lists
            Node<int> l5 = NodeUtils.CreateListFromArray(new int[] { 1, 3, 5 });
            Node<int> l6 = NodeUtils.CreateListFromArray(new int[] { 3, 5, 7 });
            Console.Write("list 5: "); NodeUtils.PrintList(l5);
            Console.Write("list 6: "); NodeUtils.PrintList(l6);
            Console.Write("merged: "); NodeUtils.PrintList(NodeUtils.MergeLists(l5, l6));

            // test 4: one list empty
            Node<int> l7 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3 });
            Console.Write("list 7: "); NodeUtils.PrintList(l7);
            Console.Write("merged with null: "); NodeUtils.PrintList(NodeUtils.MergeLists(l7, null));
            Console.Write("list 7 still: "); NodeUtils.PrintList(l7); // must stay unchanged

            // test 5: both lists empty
            Console.Write("merged null,null: "); NodeUtils.PrintList(NodeUtils.MergeLists(null, null));

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 19 - MergeLists2
        // ============================================================
        public static void Test_MergeLists2()
        {
            Console.WriteLine("===== Test_MergeLists2 =====");
            // note: per the exercise, lst1/lst2 are no longer valid after the call,
            // so (unlike Test_MergeLists) they are not printed again afterwards.

            // test 1: interleaved values
            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { 1, 4, 7 });
            Node<int> l2 = NodeUtils.CreateListFromArray(new int[] { 2, 3, 8 });
            Console.Write("list 1: "); NodeUtils.PrintList(l1);
            Console.Write("list 2: "); NodeUtils.PrintList(l2);
            Console.Write("merged: "); NodeUtils.PrintList(NodeUtils.MergeLists2(l1, l2));

            // test 2: one list entirely smaller than the other
            Node<int> l3 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3 });
            Node<int> l4 = NodeUtils.CreateListFromArray(new int[] { 10, 20 });
            Console.Write("list 3: "); NodeUtils.PrintList(l3);
            Console.Write("list 4: "); NodeUtils.PrintList(l4);
            Console.Write("merged: "); NodeUtils.PrintList(NodeUtils.MergeLists2(l3, l4));

            // test 3: shared values across both lists
            Node<int> l5 = NodeUtils.CreateListFromArray(new int[] { 1, 3, 5 });
            Node<int> l6 = NodeUtils.CreateListFromArray(new int[] { 3, 5, 7 });
            Console.Write("list 5: "); NodeUtils.PrintList(l5);
            Console.Write("list 6: "); NodeUtils.PrintList(l6);
            Console.Write("merged: "); NodeUtils.PrintList(NodeUtils.MergeLists2(l5, l6));

            // test 4: one list empty
            Node<int> l7 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3 });
            Console.Write("list 7: "); NodeUtils.PrintList(l7);
            Console.Write("merged with null: "); NodeUtils.PrintList(NodeUtils.MergeLists2(l7, null));

            // test 5: both lists empty
            Console.Write("merged null,null: "); NodeUtils.PrintList(NodeUtils.MergeLists2(null, null));

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 20 - InsertAfterValue
        // ============================================================
        public static void Test_InsertAfterValue()
        {
            Console.WriteLine("===== Test_InsertAfterValue =====");

            // test 1: value at the start
            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { 3, 5, 8, 1 });
            Console.Write("before: "); NodeUtils.PrintList(l1);
            NodeUtils.InsertAfterValue(l1, 3);
            Console.Write("after : "); NodeUtils.PrintList(l1); // expect 3,4,5,8,1

            // test 2: value in the middle
            Node<int> l2 = NodeUtils.CreateListFromArray(new int[] { 1, 5, 8, 2 });
            Console.Write("before: "); NodeUtils.PrintList(l2);
            NodeUtils.InsertAfterValue(l2, 8);
            Console.Write("after : "); NodeUtils.PrintList(l2); // expect 1,5,8,9,2

            // test 3: value at the end
            Node<int> l3 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3 });
            Console.Write("before: "); NodeUtils.PrintList(l3);
            NodeUtils.InsertAfterValue(l3, 3);
            Console.Write("after : "); NodeUtils.PrintList(l3); // expect 1,2,3,4

            // test 4: value appears more than once - only the first occurrence is handled
            Node<int> l4 = NodeUtils.CreateListFromArray(new int[] { 1, 3, 5, 3, 7 });
            Console.Write("before: "); NodeUtils.PrintList(l4);
            NodeUtils.InsertAfterValue(l4, 3);
            Console.Write("after : "); NodeUtils.PrintList(l4); // expect 1,3,4,5,3,7

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 21 - Intersection
        // ============================================================
        public static void Test_Intersection()
        {
            Console.WriteLine("===== Test_Intersection =====");

            // test 1: partial overlap
            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { 1, 3, 5, 7 });
            Node<int> l2 = NodeUtils.CreateListFromArray(new int[] { 3, 5, 9 });
            Console.Write("list 1: "); NodeUtils.PrintList(l1);
            Console.Write("list 2: "); NodeUtils.PrintList(l2);
            Console.Write("intersection: "); NodeUtils.PrintList(NodeUtils.Intersection(l1, l2)); // expect 3,5

            // test 2: no overlap
            Node<int> l3 = NodeUtils.CreateListFromArray(new int[] { 1, 3, 5 });
            Node<int> l4 = NodeUtils.CreateListFromArray(new int[] { 2, 4, 6 });
            Console.Write("list 3: "); NodeUtils.PrintList(l3);
            Console.Write("list 4: "); NodeUtils.PrintList(l4);
            Console.Write("intersection: "); NodeUtils.PrintList(NodeUtils.Intersection(l3, l4)); // expect empty

            // test 3: duplicates inside the lists - each shared value appears once
            Node<int> l5 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 2, 2, 3 });
            Node<int> l6 = NodeUtils.CreateListFromArray(new int[] { 2, 2, 4 });
            Console.Write("list 5: "); NodeUtils.PrintList(l5);
            Console.Write("list 6: "); NodeUtils.PrintList(l6);
            Console.Write("intersection: "); NodeUtils.PrintList(NodeUtils.Intersection(l5, l6)); // expect 2

            // test 4: one list empty
            Node<int> l7 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3 });
            Console.Write("list 7: "); NodeUtils.PrintList(l7);
            Console.Write("intersection with null: "); NodeUtils.PrintList(NodeUtils.Intersection(l7, null));

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 22 - RemoveMiddle
        // ============================================================
        public static void Test_RemoveMiddle()
        {
            Console.WriteLine("===== Test_RemoveMiddle =====");

            // test 1: single element - list becomes empty
            Node<int> l1 = NodeUtils.CreateListFromArray(new int[] { 7 });
            Console.Write("list 1: "); NodeUtils.PrintList(l1);
            Console.Write("result: "); NodeUtils.PrintList(NodeUtils.RemoveMiddle(l1));

            // test 2: two elements - the bigger of the two is removed
            Node<int> l2 = NodeUtils.CreateListFromArray(new int[] { 3, 9 });
            Console.Write("list 2: "); NodeUtils.PrintList(l2);
            Console.Write("result: "); NodeUtils.PrintList(NodeUtils.RemoveMiddle(l2)); // expect 3

            // test 3: even length - the bigger of the two middle values is removed
            Node<int> l3 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3, 4, 5, 6 });
            Console.Write("list 3: "); NodeUtils.PrintList(l3);
            Console.Write("result: "); NodeUtils.PrintList(NodeUtils.RemoveMiddle(l3)); // expect 1,2,3,5,6

            // test 4: odd length - first and last are removed
            Node<int> l4 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3, 4, 5 });
            Console.Write("list 4: "); NodeUtils.PrintList(l4);
            Console.Write("result: "); NodeUtils.PrintList(NodeUtils.RemoveMiddle(l4)); // expect 2,3,4

            Console.WriteLine();
        }

        // ============================================================
        // תרגיל 23 - RemoveCommon
        // ============================================================
        public static void Test_RemoveCommon()
        {
            Console.WriteLine("===== Test_RemoveCommon =====");

            // test 1: the example from the assignment sheet
            Node<int> c1 = NodeUtils.CreateListFromArray(new int[] { 1, 5, 3, 20, 15 });
            Node<int> c2 = NodeUtils.CreateListFromArray(new int[] { 4, 8, 15, 20, 1 });
            Console.Write("chain1: "); NodeUtils.PrintList(c1);
            Console.Write("chain2: "); NodeUtils.PrintList(c2);
            Console.Write("result: "); NodeUtils.PrintList(NodeUtils.RemoveCommon(c1, c2)); // expect 5,3,3
            Console.Write("chain2 still: "); NodeUtils.PrintList(c2); // must stay unchanged

            // test 2: no shared values at all
            Node<int> c3 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3 });
            Node<int> c4 = NodeUtils.CreateListFromArray(new int[] { 4, 5, 6 });
            Console.Write("chain3: "); NodeUtils.PrintList(c3);
            Console.Write("chain4: "); NodeUtils.PrintList(c4);
            Console.Write("result: "); NodeUtils.PrintList(NodeUtils.RemoveCommon(c3, c4)); // expect 1,2,3,0

            // test 3: chains are identical - everything gets removed
            Node<int> c5 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3 });
            Node<int> c6 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3 });
            Console.Write("chain5: "); NodeUtils.PrintList(c5);
            Console.Write("chain6: "); NodeUtils.PrintList(c6);
            Console.Write("result: "); NodeUtils.PrintList(NodeUtils.RemoveCommon(c5, c6)); // expect just 3

            // test 4: chain1 is empty
            Node<int> c7 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3 });
            Console.Write("chain7: "); NodeUtils.PrintList(c7);
            Console.Write("RemoveCommon(null, chain7): "); NodeUtils.PrintList(NodeUtils.RemoveCommon(null, c7)); // expect just 0

            // test 5: chain2 is empty
            Node<int> c8 = NodeUtils.CreateListFromArray(new int[] { 1, 2, 3 });
            Console.Write("chain8: "); NodeUtils.PrintList(c8);
            Console.Write("RemoveCommon(chain8, null): "); NodeUtils.PrintList(NodeUtils.RemoveCommon(c8, null)); // expect 1,2,3,0

            Console.WriteLine();
        }

        // ============================================================
        // Run All Tests
        // ============================================================
        public static void RunAll()
        {
            Test_CompareList();
            Test_CountList();
            Test_SumList();
            Test_IsExist();
            Test_MaxList();
            Test_MakePositive();
            Test_GetElement();
            Test_IsSorted();
            Test_CountSequences();
            Test_PrintBetween();
            Test_RemoveDuplicates();
            Test_IsBalanced();
            Test_RemoveMax();
            Test_Insert2SortedList();
            Test_SortList();
            Test_ReverseList();
            Test_MaxNegativeSeq();
            Test_MergeLists();
            Test_MergeLists2();
            Test_InsertAfterValue();
            Test_Intersection();
            Test_RemoveMiddle();
            Test_RemoveCommon();
        }

        private static void PrintNode(string title, Node<int> n)
        {
            Console.WriteLine(title + (n == null ? "null" : n.GetValue().ToString()));
        }
    }
}