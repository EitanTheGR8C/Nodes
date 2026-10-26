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
            if(lst == null)
                Console.WriteLine("Empty list!");
            Node<T> head = lst;
            string s = "";
            while (lst.GetNext() != head)
            {
                s += $"-> {lst.GetValue()} ";
                lst = lst.GetNext();
            }
            s += "-> Loop";
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

            return sum;
        }

        //Q7
        public static bool ContainsCircle(Node<int> lst, int value)
        {
            return false;
        }


    }
}
