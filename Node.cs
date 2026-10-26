using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkChain
{
    public class Node<T>
    {
        private T value;
        private Node<T> next;

        public Node(T value)
        {
            this.value = value;
            this.next = null;
        }

        public Node(T value, Node<T> next)
        {
            this.value = value;
            this.next = next;
        }

        public T GetValue()
        {
            return this.value;
        }

        public Node<T> GetNext()
        {
            return this.next;
        }

        public bool HasNext()
        {
            return (this.next != null);
        }

        public void SetValue(T value)
        {
            this.value = value;
        }

        public void SetNext(Node<T> next)
        {
            this.next = next;
        }

        public override string ToString()
        {
            return this.value.ToString();
        }


        //For "Constructive Copying Action" project - === Part 1 ===
        /* */
        public Node(Node<T> node)//asuming not null and has end
        {
            this.value = node.value;
            this.next = null;

            Node<T> last = this;
            Node<T> current = node.next;

            while (current != null)
            {
                Node<T> copy = new Node<T>(current.value);
                last.next = copy;
                last = copy;
                current = current.next;
            }
        }
        /* */
    }
}//YNWA//
