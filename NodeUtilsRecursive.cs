using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkChain
{
    internal class NodeUtilsRecursive
    {
        // 1 - RemoveAll
        // Target: Time O(n), Space O(n)
        public static Node<int> RemoveAll(Node<int> lst, int x)
        {
            if (lst == null)
            { return null; }
            if (lst.GetValue() == x)
                return RemoveAll(lst.GetNext(), x);

            if (lst.GetNext().GetValue() == x)
            { lst.SetNext(lst.GetNext().GetNext()); }
            RemoveAll(lst.GetNext(), x);
            
            return lst;
        }

        // 2 - Reverse
        // Target: Time O(n), Space O(n)
        public static Node<T> Reverse<T>(Node<T> lst)
        {
            if (lst == null)
            { return null; }

            Node<T> next = lst.GetNext();
            if (next == null)
                return lst;

            Node<T> head = Reverse(next);
            next.SetNext(lst);
            lst.SetNext(null);     
            return head;
        }


        // 3 - MergeSorted
        // Target: Time O(n + m), Space O(n + m)
        public static Node<int> MergeSorted(Node<int> a, Node<int> b)
        {//assuming sorted

            if (a == null && b == null)
                return null;
            if (a == null)
                return b;
            if(b == null)
                return a;
            if (a.GetValue() <= b.GetValue())
            {
                a.SetNext(MergeSorted(a.GetNext(), b)); return a;
            }
            else
            {
                b.SetNext(MergeSorted(a, b.GetNext())); return b;
            }
        }

        // 4 - RemoveDuplicates
        // Target: Time O(n), Space O(n)
        public static Node<int> RemoveDuplicates(Node<int> lst)
        {
            if (lst == null || lst.GetNext() == null)
                return lst;

            if (lst.GetValue() == lst.GetNext().GetValue())
            {
                lst.SetNext(lst.GetNext().GetNext());
                return RemoveDuplicates(lst);
            }

            lst.SetNext(RemoveDuplicates(lst.GetNext()));
            return lst;
        }

        // 5 - LongestAscRun
        // Target: Time O(n) (required), Space O(n)
        public static int LongestAscRun(Node<int> lst)
        {
            if (lst == null)
                return 0;
            return LongestAscRun(lst, 1, 1);
        }
        private static int LongestAscRun(Node<int> lst, int current, int best)
        {
            Node<int> next = lst.GetNext();
            if (next == null)
                return best;

            if (lst.GetValue() < next.GetValue())
            {
                int newCurrent = current + 1;
                if (newCurrent > best)
                    best = newCurrent;
                return LongestAscRun(next, newCurrent, best);
            }

            return LongestAscRun(next, 1, best);
        }

        // 6 - AddOne
        // Target: Time O(n), Space O(n), at most one new node
        public static Node<int> AddOne(Node<int> lst)
        {
            int carry = AddOneHelper(lst);
            if (carry == 1)
                return new Node<int>(1, lst);
            return lst;
        }
        private static int AddOneHelper(Node<int> lst)
        {
            int carry;
            if (lst.GetNext() == null)
                carry = 1;
            else
                carry = AddOneHelper(lst.GetNext());

            int sum = lst.GetValue() + carry;
            if (sum == 10)
            {
                lst.SetValue(0);
                return 1;
            }
            lst.SetValue(sum);
            return 0;
        }

        // 7 - AlternateSplit
        // Target: Time O(n), Space O(n)
        public static void AlternateSplit(Node<int> lst, out Node<int> odd, out Node<int> even)
        {
            if (lst == null)
            {
                odd = null;
                even = null;
                return;
            }
            if (lst.GetNext() == null)
            {
                odd = lst;
                even = null;
                return;
            }
            Node<int> second = lst.GetNext();
            Node<int> rest = second.GetNext();

            AlternateSplit(rest, out odd, out even);
            lst.SetNext(odd);
            second.SetNext(even);
            odd = lst;
            even = second;
        }

        // 8 - RemoveNthFromEnd
        // Target: Time O(n) in one pass, Space O(n)
        public static Node<int> RemoveNthFromEnd(Node<int> lst, int n)
        {
            int fromEnd;
            return RemoveNthFromEnd(lst, n, out fromEnd);
        }
        private static Node<int> RemoveNthFromEnd(Node<int> lst, int n, out int fromEnd)
        {
            if (lst == null)
            {
                fromEnd = 0;
                return null;
            }

            lst.SetNext(RemoveNthFromEnd(lst.GetNext(), n, out fromEnd));
            fromEnd++;

            if (fromEnd == n)
                return lst.GetNext();
            return lst;
        }

        // 9 - IsPalindrome
        // Target: Time O(n) (required), Space O(n)
        public static bool IsPalindrome(Node<int> lst)
        {
            Node<int> left = lst;// starts at the first node
            return IsPalindrome(lst, ref left);
        }
        private static bool IsPalindrome(Node<int> right, ref Node<int> left)
        {
            if (right == null)
                return true;// nothing left to compare

            if (!IsPalindrome(right.GetNext(), ref left))
                return false;// a mismatch deeper in the list

            if (left.GetValue() != right.GetValue())
                return false;

            left = left.GetNext();// the next comparison uses the next node from the left
            return true;
        }

        // 10 - MergeSort
        // Target: Time O(n log n), Space O(n) (deepest recursion: MergeSorted)
        public static Node<int> MergeSort(Node<int> lst)
        {
            if (lst == null || lst.GetNext() == null)
                return lst;// 0 or 1 nodes: already sorted

            int len = Length(lst);
            Node<int> second = Split(lst, len / 2);// lst keeps the first half, second is the rest
            Node<int> first = MergeSort(lst);
            second = MergeSort(second);
            return MergeSorted(first, second);
        }
        // number of nodes in the list
        private static int Length(Node<int> lst)
        {
            if (lst == null)
                return 0;
            return 1 + Length(lst.GetNext());
        }
        // cuts the list after count nodes (count >= 1) and returns the head of the rest
        private static Node<int> Split(Node<int> lst, int count)
        {
            if (count == 1)
            {
                Node<int> rest = lst.GetNext();
                lst.SetNext(null);// the first half ends here
                return rest;
            }
            return Split(lst.GetNext(), count - 1);
        }

        // 11 - MoveLast2First
        // Target: Time O(n), Space O(n)
        public static Node<T> MoveLast2First<T>(Node<T> ls)
        {
            if (ls == null || ls.GetNext() == null)
                return ls; // empty or one node: nothing to move

            Node<T> head = MoveLast2First(ls.GetNext());// head = the old last node, then the rest without it

            ls.SetNext(head.GetNext());// 1st: ls takes over what followed head
            head.SetNext(ls);// 2nd: head now points to ls
            return head;// the last node is the new first node
        }

        //11 - InsertionSort
        //Time: O(n^2)
        public static Node<int> InsertionSortRecursive(Node<int> head)
        {
            if (head == null || !head.HasNext())
                return head;

            Node<int> sortedRest = InsertionSortRecursive(head.GetNext());
            return InsertSorted(sortedRest, head);
        }
        private static Node<int> InsertSorted(Node<int> head, Node<int> node)
        {
            if (head == null || node.GetValue() <= head.GetValue())
            {
                node.SetNext(head);
                return node;
            }

            head.SetNext(InsertSorted(head.GetNext(), node));
            return head;
        }
    }
}
