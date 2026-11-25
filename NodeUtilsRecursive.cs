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
            return lst; // placeholder
        }

        // 2 - Reverse
        // Target: Time O(n), Space O(n)
        public static Node<T> Reverse<T>(Node<T> lst)
        {
            return lst; // placeholder
        }

        // 3 - MergeSorted
        // Target: Time O(n + m), Space O(n + m)
        public static Node<int> MergeSorted(Node<int> a, Node<int> b)
        {
            return a; // placeholder
        }

        // 4 - RemoveDuplicates
        // Target: Time O(n), Space O(n)
        public static Node<int> RemoveDuplicates(Node<int> lst)
        {
            return lst; // placeholder
        }

        // 5 - LongestAscRun
        // Target: Time O(n) (required), Space O(n)
        public static int LongestAscRun(Node<int> lst)
        {
            return 0; // placeholder
        }

        // 6 - AddOne
        // Target: Time O(n), Space O(n), at most one new node
        public static Node<int> AddOne(Node<int> lst)
        {
            return lst; // placeholder
        }

        // 7 - AlternateSplit
        // Target: Time O(n), Space O(n)
        public static void AlternateSplit(Node<int> lst, out Node<int> odd, out Node<int> even)
        {
            odd = null;  // placeholder
            even = null; // placeholder
        }

        // 8 - RemoveNthFromEnd
        // Target: Time O(n) in one pass, Space O(n)
        public static Node<int> RemoveNthFromEnd(Node<int> lst, int n)
        {
            return lst; // placeholder
        }

        // 9 - IsPalindrome
        // Target: Time O(n) (required), Space O(n)
        public static bool IsPalindrome(Node<int> lst)
        {
            return false; // placeholder
        }

        // 10 - MergeSort
        // Target: Time O(n log n), Space O(n) (deepest recursion: MergeSorted)
        public static Node<int> MergeSort(Node<int> lst)
        {
            return lst; // placeholder
        }

        // 11 - MoveLast2First
        // Target: Time O(n), Space O(n)
        public static Node<T> MoveLast2First<T>(Node<T> ls)
        {
            return ls; // placeholder
        }
    }
}
