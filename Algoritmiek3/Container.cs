using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmiek3
{
    public class Container
    {
        public int Weight;
        public Type type;
        public bool IsPlaced;

        public Container(int Weight, Type type)
        {
            this.Weight = Weight;
            this.type = type;
            this.IsPlaced = false;

        }
        public void display()
        {
            Console.WriteLine("  " + this.type.ToString() + " - " + this.Weight.ToString());
        }
    }
    public enum Type
    {
        ColdContainers,
        ColdandValuableContainers,
        RegularContiners,
        ValuableContainers
    }




}

