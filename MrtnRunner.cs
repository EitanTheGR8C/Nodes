using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkChain
{
    internal class MrtnRunner
    {
        private string id;
        private int yearB;
        private Node<ItemMrtn> lst;


        public MrtnRunner(string id, int yearB, Node<ItemMrtn> lst)
        {
            this.id = id;
            this.yearB = yearB;
            this.lst = lst;
        }
        public MrtnRunner(string id, int yearB)
        {
            this.id = id;
            this.yearB = yearB;
            this.lst = null;
        }

        public Node<ItemMrtn> GetLst() { return this.lst; }
        public int GetYearB() { return this.yearB; }
        public string GetId() { return this.id; }

        public void SetLst(Node<ItemMrtn> lst)
        { this.lst = lst; }
        public void SetYearB(int yearB)
        { this.yearB = yearB; }
        public void SetId(string id)
        { this.id = id; }
    }
}
