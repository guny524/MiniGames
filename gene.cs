using System;

namespace Gene
{
    enum Element {Length, }
    interface GeneProperty
    {
        public string[] GetElmt();
    }
    class HalfGene : GeneProperty
    {
        public string[] elmt;

        public HalfGene(string[] elmt)
        {
            this.elmt = elmt;
        }

        public string[] GetElmt()
        {
            return elmt;
        }
    }
    class GeneProperty
    {
        public int[] resultValue;
        public Gene()
        {

        }
    }
    class MainApp
    {
        public staic void Main()
        {
            
        }
    }
}
