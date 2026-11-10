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


        // Question 1a
        // Time: O(?), Space: O(?)   <-- fill in after you write it
        public static bool HasCycle(Node<int> chain)
        {
            // Create a List<Node<int>> for the nodes you already visited.
            // Walk the chain node by node.
            // Each step: if the current node is already in the list (Contains),
            // you were here before, so there is a cycle -> return true.
            // Otherwise add it to the list and move to the next node.
            // If you reach null, the chain is linear -> return false.
            return false;
        }

        // Question 1b - tortoise and hare, no collections allowed
        // Time: O(?), Space: O(?)   <-- fill in after you write it
        public static bool HasCycleFast(Node<int> chain)
        {
            // Two references starting at chain: slow and fast.
            // Loop: slow moves 1 step, fast moves 2 steps.
            // If fast reaches null (or its next is null), the chain is linear -> return false.
            // If slow == fast (same reference!), they met inside a cycle -> return true.
            return false;
        }

        // Question 2 - where does the cycle start
        // Time: O(?), Space: O(?)
        public static Node<int> CycleStart(Node<int> chain)
        {
            // Stage 1: tortoise and hare until they meet.
            // If fast hits null, the chain is linear -> return null.
            // Stage 2: move slow back to the start of the chain.
            // Move BOTH one step at a time until slow == fast.
            // Where they meet is the cycle start -> return it.
            // Required comment: explain in your own words why they meet at the cycle start.
            return null;
        }

        // Question 3a - number of distinct nodes
        // Time: O(?), Space: O(?)
        public static int Length(Node<int> chain)
        {
            // null -> 0.
            // Linear (check with HasCycleFast): count nodes until null.
            // Cyclic: return TailLength(chain) + CycleLength(chain).
            return 0;
        }

        // Question 3b
        // Time: O(?), Space: O(?)
        public static int CycleLength(Node<int> chain)
        {
            // No cycle -> 0.
            // Start from CycleStart(chain) and count steps
            // until you arrive back at it. That count is lambda.
            return 0;
        }

        // Time: O(?), Space: O(?)
        public static int TailLength(Node<int> chain)
        {
            // Linear: count all the nodes (that IS the tail).
            // Cyclic: count from chain until you reach CycleStart(chain). That's mu.
            return 0;
        }

        // Question 4 - print with the cycle in parentheses
        // Time: O(?), Space: O(?)  (hint: must stay O(1), so Console.Write, no long string)
        public static void PrintChain(Node<int> chain)
        {
            // null -> print an empty line and stop.
            // Find the cycle start with CycleStart (null means linear).
            // Walk the chain printing each value with Console.Write, "->" between nodes.
            // Right before printing the cycle-start node, print "(".
            // Cyclic: stop after the node whose GetNext() is the cycle start, then print ")".
            // Linear: stop at null.
            // Each node prints exactly once.
        }

        // Question 5 - disconnect the cycle
        // Time: O(?), Space: O(?)
        public static void BreakCycle(Node<int> chain)
        {
            // null or linear (HasCycleFast) -> do nothing.
            // Find the cycle start with CycleStart.
            // Walk until you find the node whose GetNext() IS the cycle start
            // (that's the last node of the cycle).
            // SetNext(null) on it. No other change allowed.
            // Test both edge cases: cycle starting at the first node,
            // and a last node pointing at itself.
        }

        // Question 6 (challenge) - middle node of a LINEAR chain
        // Time: O(?), Space: O(?)
        public static Node<int> Middle(Node<int> chain)
        {
            // null -> null.
            // Tortoise and hare again: slow 1 step, fast 2 steps.
            // When fast can't move two steps anymore, slow is at the middle.
            // Even length: you want the SECOND middle (length 6 -> node 4),
            // so pay attention to exactly when you stop.
            return null;
        }

    }
}
