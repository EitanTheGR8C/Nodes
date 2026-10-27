using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkChain
{
    internal class NodesCircles
    {


        //have it in Program as well
        public static Node<int> BuildCircle(params int[] values)
        {
            if (values.Length == 0) return null;
            Node<int> lst = new Node<int>(values[0]);
            Node<int> last = lst;
            for (int i = 1; i < values.Length; i++)
            {
                Node<int> node = new Node<int>(values[i]);
                last.SetNext(node);
                last = node;
            }
            last.SetNext(lst);// סגירת המעגל
            return lst;
        }




        //Q1
        public static Node<int> MakeCircular(Node<int> lst)//asuming not circle
        {
            if (lst == null)
                return null;
            Node<int> head = lst;
            while (lst.HasNext())
            {
                lst = lst.GetNext();
            }
            lst.SetNext(head);

            return head;
        }

        //Q2
        public static Node<int> BreakCircle(Node<int> lst)//asuming not null and is already circle
        {
            Node<int> head = lst;
            while (lst.GetNext() != head)
            {
                lst = lst.GetNext();
            }
            lst.SetNext(null);
            return head;
        }

        //Q3
        public static void PrintCircle<T>(Node<T> lst)
        {//two way: 1. call BreakCircle and print normal way. 2. Like BreakCircle but with printing only
            //we will do the second way (but both work)
            if (lst == null)
            {
                Console.WriteLine("Empty list!");
                return;
            }
            Node<T> head = lst;
            string s = "";
            while (lst.GetNext() != head)
            {
                s += $"-> {lst.GetValue()} ";
                lst = lst.GetNext();
            }
            s += "-> Loop";
            Console.WriteLine(s);
        }

        //Q4
        public static bool IsCircleChain(Node<int> lst)//true if circle otherwise false
        {
            if (lst == null)
                return false;
            Node<int> head = lst;

            while (true)
            {
                lst = lst.GetNext();

                if (lst == null)
                    return false;
                if (lst == head)
                    return true;
            }
        }

        //Q5
        public static int CountCircleChain(Node<int> lst)//asuming circle
        {
            if (lst == null)
                return 0;
            int cnt = 0;
            Node<int> head = lst;
            while (lst.GetNext() != head)
            {
                lst = lst.GetNext();
                cnt++;
            }
            cnt++;

            return cnt;
        }

        //Q6
        public static int SumCircle(Node<int> lst)
        {
            if (lst == null)
                return 0;
            int sum = 0;
            Node<int> head = lst;
            while (lst.GetNext() != head)
            {
                sum += lst.GetValue();
                lst = lst.GetNext();
            }
            sum += lst.GetValue();
            return sum;
        }

        //Q7
        public static bool ContainsCircle(Node<int> lst, int value)
        {
            if (lst == null)
                return false;
            Node<int> head = lst;
            if (lst.GetValue() == value)
                return true;
            lst = lst.GetNext();
            while (lst != head)
            {
                if (lst.GetValue() == value)
                    return true;
                lst = lst.GetNext();
            }
            return false;
        }


        //Q8
        public static Node<int> AddFirst(Node<int> lst, int num)
        {
            Node<int> ToAdd = new Node<int>(num);

            if (lst == null)
            {
                ToAdd.SetNext(ToAdd);
                return ToAdd;
            }
                
            ToAdd.SetNext(lst);
            Node<int> head = lst;
            while (lst.GetNext() != head)
            {
                lst = lst.GetNext();
            }
            lst.SetNext(ToAdd);
            return ToAdd;

        }

        //Q9
        public static Node<int> AddLast(Node<int> lst, int num)
        {
            Node<int> ToAdd = new Node<int>(num);
            if (lst == null)
            {
                ToAdd.SetNext(ToAdd);
                return ToAdd;
            }
            Node<int> head = lst;
            ToAdd.SetNext(head);
            while (lst.GetNext() != head )
            {
                lst = lst.GetNext();
            }
            lst.SetNext(ToAdd);
            return head;
        }

        //Q10
        public static Node<int> AddSumsBetween(Node<int> lst)//asuming at least 2 nodes in the list.
        {
            Node<int> head = lst;
            while (lst.GetNext() != head)
            {
                int sum1 = lst.GetValue();
                sum1 += lst.GetNext().GetValue();
                Node<int> ToAdd1 = new Node<int>(sum1, lst.GetNext());

                lst.SetNext(ToAdd1);

                lst = lst.GetNext().GetNext();

            }
            int sum = lst.GetValue();
            sum += head.GetValue();

            Node<int> ToAdd = new Node<int>(sum, head);
            lst.SetNext(ToAdd);

            return head;
        }

        //Q11
        public static Node<int> AddBeforeEven(Node<int> lst)
        {
            if (lst == null) return null;
            Node<int> head = lst;
            
            while (lst.GetNext() != head)
            {
                
                if (lst.GetNext().GetValue() % 2 == 0)
                {
                    Node<int> ToAdd = new Node<int>(lst.GetNext().GetValue() - 1, lst.GetNext());
                    lst.SetNext(ToAdd);
                    lst = lst.GetNext();
                }
                lst = lst.GetNext();
            }
            if (head.GetValue() % 2 == 0)
            {
                Node<int> ToAdd = new Node<int>(head.GetValue() - 1, head);
                lst.SetNext(ToAdd);
                head = ToAdd;
            }
            
            return head;
        }

        //Q12
        public static Node<int> RemoveFirst(Node<int> lst)
        {
            if(lst == null) return null;
            if(lst.GetNext() == lst) return null;

            Node<int> head = lst;
            while (lst.GetNext() != head)
            { lst = lst.GetNext(); }
            head = head.GetNext();
            lst.SetNext(head);

            return head;
        }

        //Q13
        public static Node<int> RemoveLast(Node<int> lst)
        {
            if( lst == null) return null;
            if( lst.GetNext() == lst) return null;
            if (lst.GetNext().GetNext() == lst)
            {
                lst.SetNext(lst);
                return lst;
            }
            Node<int> head = lst;
            while (lst.GetNext().GetNext() != head)
            {
                lst = lst.GetNext();
            }
            lst.SetNext(head);
            return head;
        }

        //Q14
        public static Node<int> RemoveEvens(Node<int> lst)
        {
            while (lst != null && lst.GetValue() % 2 == 0)
            {
                lst = RemoveFirst(lst);
            }
            if (lst == null)
                return null;

            Node<int> head = lst;
            Node<int> prev = lst;
            Node<int> pos = lst.GetNext();
            while (pos != head)
            {
                if (pos.GetValue() % 2 == 0)
                    prev.SetNext(pos.GetNext());
                else
                    prev = pos;
                pos = pos.GetNext();
            }
            return head;
        }
    }
}
