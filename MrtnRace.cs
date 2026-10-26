using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkChain
{
    internal class MrtnRace
    {
        private string country;
        private int yearMrtn;
        private Node<MrtnRunner> lstM;

        public MrtnRace(string country, int yearMrnt, Node<MrtnRunner> lstM)
        {
            this.country = country;
            this.yearMrtn = yearMrnt;
            this.lstM = lstM;
        }
        public MrtnRace(string country, int yearMrnt)
        {
            this.country = country;
            this.yearMrtn = yearMrnt;
            this.lstM = null;
        }

        public string GetCountry() { return this.country; }
        public int GetYearMrnt() { return this.yearMrtn; }
        public Node<MrtnRunner> GetLstM() { return this.lstM; }

        public void SetCountry(string country)
        { this.country = country; }
        public void SetYearMrnt(int yearMrtn)
        { this.yearMrtn = yearMrtn; }
        public void SetLstM(Node<MrtnRunner> lstM)
        { this.lstM = lstM; }


        public void AddResult(string id, int score)
        {
            Node<MrtnRunner> pos = this.lstM;
            while (pos != null && pos.GetValue().GetId() != id)
                pos = pos.GetNext();

            if (pos == null)
                return;//runner not found

            MrtnRunner runner = pos.GetValue();

            Node<ItemMrtn> newNode = new Node<ItemMrtn>(new ItemMrtn(this.yearMrtn, score));

            if (runner.GetLst() == null)
            {
                runner.SetLst(newNode);// first marathon: the list was empty
            }
            else
            {
                Node<ItemMrtn> last = runner.GetLst();
                while (last.GetNext() != null)
                    last = last.GetNext();
                last.SetNext(newNode);
            }
        }
    }
}
