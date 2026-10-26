using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkChain
{
    internal class Perfume
    {
        private string perfume_name;
        private string manufacturer;
        private Node<Smell> smells;

        public Perfume(string perfume_name, string manufacturer)
        {
            this.perfume_name = perfume_name;
            this.manufacturer = manufacturer;
            this.smells = null;

        }

        public string GetPerfumeName()
        { return this.perfume_name; }
        public string GetManufacturer()
        { return this.manufacturer; }
        public Node<Smell> GetSmells()
        { return this.smells; }

        public void AddSmell(Smell smell)
        {
            this.smells = new Node<Smell>(smell, this.smells);
        }
        public void AddSmell(string essence_name, int intensity, int concentration_essence)
        {
            Smell new_smell = new Smell(essence_name, intensity, concentration_essence);
            this.smells = new Node<Smell>(new_smell, this.smells);
        }

        public bool Is100Percent()
        {
            int cocentration_cnt = 0;
            Node<Smell> concentrations = this.smells;
            while (concentrations != null)
            {
                cocentration_cnt += concentrations.GetValue().GetConcentrationEssence();
                concentrations = concentrations.GetNext();
            }
            if(cocentration_cnt == 100)
                return true;
            return false;
        }

        /* Code for CommonSmells (from main class):
         * 
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
         *
         */

        public override string ToString()
        {
            string str = "Perfume: " + this.perfume_name + ", company: " + this.manufacturer + "\nSmells:\n";
            Node<Smell> pos = this.smells;
            while (pos != null)
            {
                str += pos.GetValue().ToString() + "\n";
                pos = pos.GetNext();
            }
            return str;
        }
    }
}
