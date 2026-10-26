using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace LinkChain
{
    internal class Smell
    {
        private string essence_name;
        private int intensity;// from 1 to 10 - full num
        private int concentration_essence; //percentages - %

        public Smell(string essence_name, int intensity, int concentration_essence)
        {
            this.essence_name = essence_name;
            this.intensity = intensity;//as writen in project file - no need to check if amount of intensty is actually 1 to 10.
            this.concentration_essence = concentration_essence;//as writen in project file - no need to check if percentages of concertration is actually 1 to 100 percent.
        }
        public Smell(Smell other)
        {
            this.essence_name = other.essence_name;
            this.intensity = other.intensity;
            this.concentration_essence = other.concentration_essence;
        }

        public string GetEssemceName()
        {
            return this.essence_name;
        }
        public int GetIntensity()
        {
            return this.intensity;
        }
        public int GetConcentrationEssence()
        {
            return this.concentration_essence;
        }

        public bool IsSame(Smell other)
        {
            if ((other.essence_name == this.essence_name) && (other.intensity == this.intensity))
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return "Essence name: " + this.essence_name + "\n" +
                   "Intensity: " + this.intensity + "\n" +
                   "Concentration in essence: " + this.concentration_essence;
        }
    }
}
