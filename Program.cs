using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkChain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //TestUtils.RunAll(); //from first part of the project
            //TestPerfume(); // for perfume 
            //TestGroupCards(); // for cards
            //TestMarathon(); //for marathon
            //Test_Node(); //for constructive copying action
            TestCircular(); // for circular list
        }


        //Circular Linked List
        #region Circular Linked List

        public static void TestCircular()
        {
            Node<int> lst;
            Node<int> pos;
            string v;

            //Q1 MakeCircular
            lst = new Node<int>(1);
            lst.SetNext(new Node<int>(2));
            lst.GetNext().SetNext(new Node<int>(3));
            lst = NodesCircles.MakeCircular(lst);
            bool q1 = NodesCircles.IsCircleChain(lst);
            Check("Q1 MakeCircular", q1);

            //Q2 BreakCircle
            lst = NodesCircles.BreakCircle(NodesCircles.BuildCircle(4, 5, 6, 7));
            pos = lst;
            while (pos.HasNext())
                pos = pos.GetNext();
            bool q2 = pos.GetValue() == 7;
            Check("Q2 BreakCircle", q2);

            //Q3 PrintCircle - בדיקה ידנית: צריך להדפיס -> 1 -> 2 -> 3 -> Loop
            NodesCircles.PrintCircle(NodesCircles.BuildCircle(1, 2, 3));

            //Q4 IsCircleChain
            bool q4 = NodesCircles.IsCircleChain(NodesCircles.BuildCircle(1, 2))
                && !NodesCircles.IsCircleChain(NodesCircles.BreakCircle(NodesCircles.BuildCircle(1, 2)));
            Check("Q4 IsCircleChain", q4);

            //Q5 CountCircleChain
            bool q5 = NodesCircles.CountCircleChain(NodesCircles.BuildCircle(6, 2, 10, 9, 4, 20)) == 6;
            Check("Q5 CountCircleChain", q5);

            //Q6 SumCircle
            bool q6 = NodesCircles.SumCircle(NodesCircles.BuildCircle(6, 2, 10, 9, 4, 20)) == 51;
            Check("Q6 SumCircle", q6);

            //Q7 ContainsCircle - כולל ערך בחוליה האחרונה
            lst = NodesCircles.BuildCircle(6, 2, 10, 9, 4, 20);
            bool q7 = NodesCircles.ContainsCircle(lst, 20) && !NodesCircles.ContainsCircle(lst, 99);
            Check("Q7 ContainsCircle", q7);

            //Q8 AddFirst
            lst = NodesCircles.AddFirst(NodesCircles.BuildCircle(2, 3, 4), 1);
            pos = lst;
            v = "";
            while (pos.GetNext() != lst)
            {
                v += pos.GetValue() + ",";
                pos = pos.GetNext();
            }
            v += pos.GetValue();
            bool q8 = v == "1,2,3,4" && pos.GetNext() == lst;
            Check("Q8 AddFirst", q8);

            //Q9 AddLast
            lst = NodesCircles.AddLast(NodesCircles.BuildCircle(1, 2, 3), 4);
            pos = lst;
            v = "";
            while (pos.GetNext() != lst)
            {
                v += pos.GetValue() + ",";
                pos = pos.GetNext();
            }
            v += pos.GetValue();
            bool q9 = v == "1,2,3,4" && pos.GetNext() == lst;
            Check("Q9 AddLast", q9);

            //Q10 AddSumsBetween
            lst = NodesCircles.AddSumsBetween(NodesCircles.BuildCircle(1, 2, 3));
            pos = lst;
            v = "";
            while (pos.GetNext() != lst)
            {
                v += pos.GetValue() + ",";
                pos = pos.GetNext();
            }
            v += pos.GetValue();
            bool q10 = v == "1,3,2,5,3,4";
            Check("Q10 AddSumsBetween", q10);

            //Q11 AddBeforeEven
            lst = NodesCircles.AddBeforeEven(NodesCircles.BuildCircle(3, 4, 7));
            pos = lst;
            v = "";
            while (pos.GetNext() != lst)
            {
                v += pos.GetValue() + ",";
                pos = pos.GetNext();
            }
            v += pos.GetValue();
            bool q11 = v == "3,3,4,7";
            Check("Q11 AddBeforeEven", q11);

            //Q12 RemoveFirst
            lst = NodesCircles.RemoveFirst(NodesCircles.BuildCircle(1, 2, 3, 4));
            pos = lst;
            v = "";
            while (pos.GetNext() != lst)
            {
                v += pos.GetValue() + ",";
                pos = pos.GetNext();
            }
            v += pos.GetValue();
            bool q12 = v == "2,3,4";
            Check("Q12 RemoveFirst", q12);

            //Q13 RemoveLast
            lst = NodesCircles.RemoveLast(NodesCircles.BuildCircle(1, 2, 3, 4));
            pos = lst;
            v = "";
            while (pos.GetNext() != lst)
            {
                v += pos.GetValue() + ",";
                pos = pos.GetNext();
            }
            v += pos.GetValue();
            bool q13 = v == "1,2,3";
            Check("Q13 RemoveLast", q13);

            //Q14 RemoveEvens
            lst = NodesCircles.RemoveEvens(NodesCircles.BuildCircle(4, 2, 5, 6, 7, 8));
            pos = lst;
            v = "";
            while (pos.GetNext() != lst)
            {
                v += pos.GetValue() + ",";
                pos = pos.GetNext();
            }
            v += pos.GetValue();
            bool q14 = v == "5,7";
            Check("Q14 RemoveEvens", q14);

            //ALL - בדיקה מחוברת של כמה פעולות ביחד + כל הבדיקות הקודמות
            lst = NodesCircles.BuildCircle(2, 3, 4);
            lst = NodesCircles.AddFirst(lst, 1);
            lst = NodesCircles.AddLast(lst, 5);
            lst = NodesCircles.RemoveEvens(lst);
            pos = lst;
            v = "";
            while (pos.GetNext() != lst)
            {
                v += pos.GetValue() + ",";
                pos = pos.GetNext();
            }
            v += pos.GetValue();
            bool all = q1 && q2 && q4 && q5 && q6 && q7 && q8 && q9 && q10 && q11 && q12 && q13 && q14
                && v == "1,3,5" && NodesCircles.IsCircleChain(lst);
            Check("ALL", all);
        }




        #endregion Circular Linked List
        //Circular Linked List


        //Constructive Copying Action
        #region Constructive Copying Action
        // === part 2 ===
        public static void Test_Node()
        {
            int[] a = { 4, 5, 10, 13, 17, 20, 21 };
            Node<int> ls = NodeUtils.CreateListFromArray(a);
            NodeUtils.PrintList(ls);
            Node<int> ls2 = new Node<int>(ls);
            Console.WriteLine();

            // Stages 1 + 2:
            Check("same values", SameValues(ls, ls2));
            Check("different head", !ReferenceEquals(ls, ls2));
            Check("no shared nodes", NoSharedNodes(ls, ls2));

            // Stage 3:
            Node<int> p = ls;// skip 5th node
            for (int i = 1; i < 4; i++)
                p = p.GetNext();
            p.SetNext(p.GetNext().GetNext());

            int any_value = 99;//change 3rd value
            Node<int> q = ls.GetNext().GetNext();
            q.SetValue(any_value);

            Check("copy unaffected by changes to original", SameValues(ls2, NodeUtils.CreateListFromArray(a)));


            // Stage 4:
            any_value = 25;

            Node<int> orig = NodeUtils.CreateListFromArray(a);//create new list
            Node<int> copy = new Node<int>(orig);

            Node<int> loop_copy = copy;//add new node to end
            while (loop_copy.HasNext())
                loop_copy = loop_copy.GetNext();
            loop_copy.SetNext(new Node<int>(any_value));

            any_value = 1;//change head's value
            copy.SetValue(any_value);

            Check("original unaffected by changes to copy", SameValues(orig, NodeUtils.CreateListFromArray(a)));

            // Stage 5a:
            Node<int> one = new Node<int>(123);//creating two identical nodes
            Node<int> oneCopy = new Node<int>(one);

            Check("one node: same values", SameValues(one, oneCopy));//check same values / different head / no shared 
            Check("one node: different head", !ReferenceEquals(one, oneCopy));
            Check("one node: no shared nodes", NoSharedNodes(one, oneCopy));

            any_value = 321;// change oneCopy and check that one didn't change 
            oneCopy.SetValue(any_value);
            Check("one node: original unaffected", one.GetValue() == 123);

            // Stage 5b:
            Node<int> ls3 = new Node<int>(ls2);//copy of the copy
            Check("copy of copy: same values", SameValues(ls2, ls3));//check same values / different head / no shared 
            Check("copy of copy: different head", !ReferenceEquals(ls2, ls3));
            Check("copy of copy: no shared nodes", NoSharedNodes(ls2, ls3));

            Console.WriteLine();
            NodeUtils.PrintList(ls);
            NodeUtils.PrintList(ls2);
        }
        public static void Check(string title, bool condition)
        {
            Console.WriteLine(title + ": " + (condition ? "OK" : "FAIL"));
        }
        public static bool SameValues(Node<int> a, Node<int> b)
        {
            bool same_value = true;
            while (a != null || b != null)
            {
                if (a == null || b == null || a.GetValue() != b.GetValue())
                {
                    same_value = false;
                    break;
                }
                a = a.GetNext();
                b = b.GetNext();
            }
            return same_value;
        }
        public static bool NoSharedNodes(Node<int> a, Node<int> b)
        {
            while (a != null && b != null)
            {
                if (ReferenceEquals(a, b))
                    return false;// same object -> shared node

                a = a.GetNext();
                b = b.GetNext();
            }

            return true;
        }

        // === part 3 ===
        // --- What would happen if the copy constructor were written this way ---
        /*  
         *  value = node.value;
         *  next = node.next;
         * 
         * This constructor creates only ONE new node (the head of ls2) and copies its value.
         * But next = node.next makes the new head point to the exact same second node
         * that the head of ls points to. This is a shallow copy:
         * only the head is new, and the rest of the chain is shared between both lists.
         * 
         * Checks that would FAIL:
         * 
         * 1. "no shared nodes" (item 2) - FAILS.
         *     The second node of ls and of ls2 is the same object,
         *     so ReferenceEquals returns true.
         * 
         * 2. Changing the original (item 3) - FAILS.
         *    Deleting the 5th node and changing the 3rd node's value happen on shared nodes,
         *    so ls2 changes too and no longer equals the original array.
         *    
         * 3. Changing the copy (item 4) - FAILS.
         *    The last node is shared, so adding a node at the end of the copy
         *    also adds it to the original chain.
         *    (Changing only the head's value would not affect the original,
         *    because the head is a separate node.)
         *    
         * 4. Copy of a copy (item 5b) - the "no shared nodes" check FAILS, for the same reason.
         * 
         * 
         * Checks that would still PASS:
         * - "same values" - the values are identical.
         * - "different head" - the head of ls2 is a new object.
         * - One-node chain (item 5a) - there are no nodes after the head
         *   (next is null), so there is nothing to share.
         *   
         *   Summary: the checks fail because the copy is shallow. Only the head is copied
         *            and the rest is shared. The correct constructor must create a new node for
         *            every node in the original (a deep copy of the chain), like the loop in part A.
         */


        #endregion Constructive Copying Action
        //Constructive Copying Action


        //Marathon
        #region Mrtn
        static int CountConsecutiveRunners(MrtnRace ThisRace)//Gets list of runners from a race (MrtnRace ThisRace), and returns how many runners have been running consecutivly since starting to run (at least 3 years)
        {
            int cnt = 0;
            Node<MrtnRunner> pos = ThisRace.GetLstM();

            while (pos != null)
            {
                Node<ItemMrtn> item = pos.GetValue().GetLst();
                int marathons = 0;
                bool consecutive = true;

                while (item != null)
                {
                    marathons++;
                    // compare this year with the next one
                    if (item.GetNext() != null &&
                        item.GetNext().GetValue().GetYear() != item.GetValue().GetYear() + 1)
                    {
                        consecutive = false;   // a year was skipped
                    }
                    item = item.GetNext();
                }

                if (marathons >= 3 && consecutive)
                    cnt++;

                pos = pos.GetNext();
            }
            return cnt;
        }
        static Node<ItemMrtn> BuildItems(int[] years)// builds a list of ItemMrtn from years (score 0, not important here)
        {
            Node<ItemMrtn> head = null;
            Node<ItemMrtn> tail = null;
            foreach (int y in years)
            {
                Node<ItemMrtn> node = new Node<ItemMrtn>(new ItemMrtn(y, 0));
                if (head == null)
                    head = node;
                else
                    tail.SetNext(node);
                tail = node;
            }
            return head;
        }

        static void AddRunner(MrtnRace race, MrtnRunner runner)// adds a runner at the start of the race's runner list
        {
            Node<MrtnRunner> node = new Node<MrtnRunner>(runner);
            node.SetNext(race.GetLstM());
            race.SetLstM(node);
        }

        static void PrintYears(MrtnRunner runner)
        {
            string s = "";
            Node<ItemMrtn> pos = runner.GetLst();
            while (pos != null)
            {
                s += pos.GetValue().GetYear() + " ";
                pos = pos.GetNext();
            }
            Console.WriteLine(s);
        }

        static void TestMarathon()
        {
            // Test 1: AddResult to a runner with 2010, 2011 in the 2012 marathon
            // expected: 2010 2011 2012
            MrtnRace race1 = new MrtnRace("Israel", 2012);
            MrtnRunner r1 = new MrtnRunner("r1", 1990, BuildItems(new int[] { 2010, 2011 }));
            AddRunner(race1, r1);
            race1.AddResult("r1", 100);
            PrintYears(r1);

            // Test 2: AddResult to a runner with an empty list in the 2012 marathon
            // expected: 2012
            MrtnRace race2 = new MrtnRace("Israel", 2012);
            MrtnRunner r2 = new MrtnRunner("r2", 1995);
            AddRunner(race2, r2);
            race2.AddResult("r2", 100);
            PrintYears(r2);

            // Test 3: CountConsecutiveRunners
            // A (2010-2012), B (2009-2012), C (2008-2010), D (2008-2010 and 2012, skip), E (2012 only)
            // expected: 3 (A, B and C)
            MrtnRace race3 = new MrtnRace("Israel", 2012);
            AddRunner(race3, new MrtnRunner("A", 1990, BuildItems(new int[] { 2010, 2011, 2012 })));
            AddRunner(race3, new MrtnRunner("B", 1991, BuildItems(new int[] { 2009, 2010, 2011, 2012 })));
            AddRunner(race3, new MrtnRunner("C", 1992, BuildItems(new int[] { 2008, 2009, 2010 })));
            AddRunner(race3, new MrtnRunner("D", 1993, BuildItems(new int[] { 2008, 2009, 2010, 2012 })));
            AddRunner(race3, new MrtnRunner("E", 1994, BuildItems(new int[] { 2012 })));
            Console.WriteLine("Consecutive runners: " + CountConsecutiveRunners(race3));

            // Test 4: a marathon with no runners
            // expected: 0
            MrtnRace race4 = new MrtnRace("Israel", 2012);
            Console.WriteLine("Consecutive runners (no runners): " + CountConsecutiveRunners(race4));
        }
        #endregion Mrtn
        //Marathon


        // --- Cards ---
        #region Cards
        static void GroupCards(Node<Card> L)
        {
            int count = 0;
            Node<Card> pos = L;
            Node<Card> last = null;

            while (pos != null)
            {
                count++;
                last = pos;
                Node<Card> prev = last;
                Node<Card> cur = last.GetNext();

                while (cur != null)
                {
                    if (cur.GetValue().GetNum() == pos.GetValue().GetNum())
                    {
                        if (prev == last)
                        {
                            last = cur;
                            prev = cur;
                            cur = cur.GetNext();
                        }
                        else
                        {
                            prev.SetNext(cur.GetNext());
                            cur.SetNext(last.GetNext());
                            last.SetNext(cur);
                            last = cur;
                            cur = prev.GetNext();
                        }
                    }
                    else
                    {
                        prev = cur;
                        cur = cur.GetNext();
                    }
                }

                pos = last.GetNext();
            }

            last.SetNext(new Node<Card>(new Card(count, "White")));
        }

        static Node<Card> BuildList(int[] nums)
        {
            Node<Card> head = null;
            Node<Card> tail = null;
            foreach (int n in nums)
            {
                Node<Card> node = new Node<Card>(new Card(n, "Red"));
                if (head == null)
                    head = node;
                else
                    tail.SetNext(node);
                tail = node;
            }
            return head;
        }

        // prints the original list as numbers only, like [2,1,2,3,1]
        static void PrintBefore(Node<Card> list)
        {
            string s = "[";
            while (list != null)
            {
                s += list.GetValue().GetNum();
                if (list.GetNext() != null)
                    s += ",";
                list = list.GetNext();
            }
            Console.Write(s + "]");
        }

        // prints the result like [2,2,1,1,3], [3,White]
        static void PrintAfter(Node<Card> list)
        {
            string s = "[";
            while (list.GetNext() != null)
            {
                s += list.GetValue().GetNum();
                if (list.GetNext().GetNext() != null)
                    s += ",";
                list = list.GetNext();
            }
            s += "], [" + list.GetValue().GetNum() + "," + list.GetValue().GetColor() + "]";
            Console.WriteLine(s);
        }

        static void RunTestCards(int[] nums)
        {
            Node<Card> l = BuildList(nums);
            PrintBefore(l);
            Console.Write(" --> ");
            GroupCards(l);
            PrintAfter(l);
        }

        static void TestGroupCards()
        {
            RunTestCards(new int[] { 5 });
            RunTestCards(new int[] { 1, 1, 1 });
            RunTestCards(new int[] { 1, 2, 3 });
            RunTestCards(new int[] { 1, 1, 4 });
            RunTestCards(new int[] { 1, 2, 1, 2 });
            RunTestCards(new int[] { 2, 1, 2, 3, 1 });
        }
        #endregion Cards
        // --- Cards ---


        // --- Perfume ---
        #region Perfume
        static void TestPerfume()
        {
            // p1: 6 smells, percents sum to 100 (uses AddSmell with a Smell)
            Perfume p1 = new Perfume("p1", "Dior");
            p1.AddSmell(new Smell("s1", 5, 20));
            p1.AddSmell(new Smell("s2", 5, 10));
            p1.AddSmell(new Smell("s3", 5, 10));
            p1.AddSmell(new Smell("s4", 5, 25));
            p1.AddSmell(new Smell("s5", 5, 25));
            p1.AddSmell(new Smell("s6", 5, 10));

            // p2: 6 smells, percents sum to 110 (uses AddSmell with name, strength, percent)
            // s3, s4, s6 have the same name as in p1 but a different strength
            Perfume p2 = new Perfume("p2", "Chanel");
            p2.AddSmell("s2", 5, 10);
            p2.AddSmell("s5", 5, 25);
            p2.AddSmell("s3", 6, 10);
            p2.AddSmell("s4", 8, 25);
            p2.AddSmell("s6", 3, 10);
            p2.AddSmell("s7", 5, 30);

            // p3: sums to 90
            Perfume p3 = new Perfume("p3", "Gucci");
            p3.AddSmell("a", 4, 40);
            p3.AddSmell("b", 7, 30);
            p3.AddSmell("c", 2, 20);

            // p4: no smells
            Perfume p4 = new Perfume("p4", "Armani");

            // ToString
            Console.WriteLine(p1);
            Console.WriteLine(p2);
            Console.WriteLine(p3);
            Console.WriteLine(p4);

            // Is100Percent: expected true, false, false, false
            Console.WriteLine("p1 Is100Percent: " + p1.Is100Percent());
            Console.WriteLine("p2 Is100Percent: " + p2.Is100Percent());
            Console.WriteLine("p3 Is100Percent: " + p3.Is100Percent());
            Console.WriteLine("p4 Is100Percent: " + p4.Is100Percent());

            // CommonSmells(p1, p2): expected only s2 and s5
            Console.WriteLine("Common p1,p2:");
            PrintSmells(CommonSmells(p1, p2));

            // CommonSmells(p1, p4): expected null
            Console.WriteLine("Common p1,p4: " + (CommonSmells(p1, p4) == null ? "null" : "not null"));

            // CommonSmells(p1, p1): expected all 6 smells (copies)
            Console.WriteLine("Common p1,p1:");
            PrintSmells(CommonSmells(p1, p1));

            // p1 and p2 must not have changed
            Console.WriteLine(p1);
            Console.WriteLine(p2);
        }

        static Node<Smell> CommonSmells(Perfume p1, Perfume p2)
        {
            Node<Smell> head = null;   // first node of the new list
            Node<Smell> tail = null;   // last node of the new list
            Node<Smell> pos1 = p1.GetSmells();

            while (pos1 != null)
            {
                Node<Smell> pos2 = p2.GetSmells();
                bool found = false;
                while (pos2 != null && !found)
                {
                    if (pos1.GetValue().IsSame(pos2.GetValue()))
                        found = true;
                    pos2 = pos2.GetNext();
                }

                if (found)
                {
                    Node<Smell> newNode = new Node<Smell>(new Smell(pos1.GetValue()));
                    if (head == null)
                        head = newNode;
                    else
                        tail.SetNext(newNode);
                    tail = newNode;
                }

                pos1 = pos1.GetNext();
            }
            return head;
        }

        static void PrintSmells(Node<Smell> list)
        {
            while (list != null)
            {
                Console.WriteLine(list.GetValue());
                list = list.GetNext();
            }
        }
        #endregion Perfume
        // --- Perfume ---

    }
}//YNWA//
