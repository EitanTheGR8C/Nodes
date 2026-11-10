using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkChain
{
    internal class ChainCycles
    {
        public static Node<int> BuildChain(int length)
        {
            if (length <= 0)
                return null;

            Node<int> first = new Node<int>(1);
            Node<int> current = first;
            for (int i = 2; i <= length; i++)
            {
                current.SetNext(new Node<int>(i));
                current = current.GetNext();
            }
            return first;
        }

        public static void MakeCycle(Node<int> chain, int n)
        {
            if (chain == null)
                return;

            Node<int> cycleStart = chain;
            for (int i = 1; i < n && cycleStart != null; i++)
            {
                cycleStart = cycleStart.GetNext();
            }
            if (cycleStart == null)
                return;

            Node<int> last = chain;
            while (last.HasNext())
            {
                last = last.GetNext();
            }
            last.SetNext(cycleStart);
        }

        //Explanation for N: n = repesents the number of distinc nodes in a chain -- same throught this class

        // Question 1a
        // Time O(n^2) - Contains rescans the list for every node. Space O(n) - the list can grow to n nodes.
        public static bool HasCycle(Node<int> chain)
        {
            List<Node<int>> visited = new List<Node<int>>();
            Node<int> current = chain;
            while (current != null)
            {
                if (visited.Contains(current))
                    return true;
                visited.Add(current);
                current = current.GetNext();
            }
            return false;
        }

        // Question 1b - tortoise and hare, no collections allowed
        // Time O(n) - each pointer passes every node at most a constant number of times. Space O(1) - only two pointers.
        public static bool HasCycleFast(Node<int> chain)
        {
            Node<int> slow = chain;
            Node<int> fast = chain;
            while (fast != null && fast.GetNext() != null)
            {
                slow = slow.GetNext();
                fast = fast.GetNext().GetNext();
                if (slow == fast)
                    return true;
            }
            return false;
        }

        // Question 2 - where does the cycle start
        // Time O(n) - one pass to meet, then one pass to walk back to the start. Space O(1) - only two pointers.
        // Why they meet at the cycle start: when stage 1 ends, fast walked exactly
        // twice as many steps as slow, so the difference between them is a whole
        // number of laps around the cycle. That means the distance from the chain
        // start to the cycle start equals the distance from the meeting point to
        // the cycle start (going around the cycle). Moving slow back to the start
        // and advancing both one step at a time, they arrive at the cycle start
        // together.
        public static Node<int> CycleStart(Node<int> chain)
        {
            if(chain == null)
                return null;
            Node<int> slow = chain;
            Node<int> fast = chain;

            slow = slow.GetNext();
            fast = fast.GetNext();
            if (fast != null)
                fast = fast.GetNext();
            if (slow == null || fast == null)
                return null;

            while (fast != slow)
            {
                if (fast == null)
                    return null;
                fast = fast.GetNext().GetNext();
                slow = slow.GetNext();
            }
            slow = chain;
            while (slow != fast)
            {
                slow = slow.GetNext();
                fast = fast.GetNext();
            }
            return fast;

        }

        // Question 3a - number of distinct nodes
        // Time O(n) - a constant number of passes over the chain. Space O(1) - counters only.
        public static int Length(Node<int> chain)
        {
            if (chain == null)
                return 0;

            if (!HasCycleFast(chain))
            {
                int count = 0;
                Node<int> current = chain;
                while (current != null)
                {
                    count++;
                    current = current.GetNext();
                }
                return count;
            }

            return TailLength(chain) + CycleLength(chain);
        }

        // Question 3b
        // Time O(n) - one walk around the cycle. Space O(1) - counters only.
        public static int CycleLength(Node<int> chain)
        {
            Node<int> current = CycleStart(chain);
            if( current == null )
                return 0;
            int cnt = 0;
            Node<int> start = current;
            current = current.GetNext();
            cnt++;
            while (start != current)
            {
                current = current.GetNext();
                cnt++;
            }
            return cnt;
        }

        // Time O(n) - one walk to the cycle start (or to null). Space O(1) - counters only.
        public static int TailLength(Node<int> chain)
        {
            // Linear: count all the nodes (that IS the tail).
            // Cyclic: count from chain until you reach CycleStart(chain). That's mu.
            if (chain == null)
                return 0;
            Node<int> start_circle = CycleStart(chain);
            int cnt = 0;
            while (chain != start_circle)
            {
                chain = chain.GetNext();
                cnt++;
            }    
            return cnt;
        }

        // Question 4 - print with the cycle in parentheses
        // Time O(n) - every node is printed exactly once. Space O(1) - Console.Write directly, no long string is built.
        public static void PrintChain(Node<int> chain)
        {
            if (chain == null)
            {
                Console.WriteLine();
                return;
            }

            Node<int> cycleStart = CycleStart(chain);
            Node<int> current = chain;

            while (current != null)
            {
                if (current == cycleStart)
                    Console.Write("(");

                Console.Write(current.GetValue());

                Node<int> next = current.GetNext();
                if (cycleStart != null && next == cycleStart)
                {
                    // last node of the cycle: close the bracket and stop
                    Console.Write(")");
                    Console.WriteLine();
                    return;
                }
                if (next != null)
                    Console.Write("->");
                current = next;
            }
            Console.WriteLine();

        }

        // Question 5 - disconnect the cycle
        // Time O(n) - finding the cycle start plus one walk around the cycle. Space O(1) - pointers only.
        public static void BreakCycle(Node<int> chain)
        {
            if (chain == null || !HasCycleFast(chain))
                return;
            Node<int> start = CycleStart(chain);
            Node<int> current = start;
            while (current.GetNext() != start)
            { current = current.GetNext(); }
            current.SetNext(null);
        }

        // Question 6 (challenge) - middle node of a LINEAR chain
        // Time O(n) - a single scan of the chain. Space O(1) - only two pointers.
        public static Node<int> Middle(Node<int> chain)//assuming linear
        {
            if(chain == null)
                return null;
            Node<int> fast = chain;
            Node<int> slow = chain;
            while (fast != null && fast.GetNext() != null)
            {
                slow = slow.GetNext();
                fast = fast.GetNext().GetNext();
            }
            return slow;
        }

    }
}
