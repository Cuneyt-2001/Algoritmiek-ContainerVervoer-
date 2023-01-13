using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Algoritmiek3.Side;

namespace Algoritmiek3
{
    public class Stack
    {
        public int Y;
        public StackSide StackSide;

        public List<Container> containers;

        public Stack(int y ,StackSide stackSide)
        {
            this.Y = y;
            containers = new List<Container>();
            this.StackSide = stackSide;
        }
        public bool PlaceContainer(Container container)
        {
            if (container.IsPlaced == true)
            {
                return false;
            }
           

            if (this.containers.Count() > 0 && (this.containers.Sum(a => a.Weight) - this.containers.First().Weight + container.Weight) > 120)
            {
                Console.WriteLine("The totalweight is more than 120 ton. Weight: " + this.containers.Sum(a => a.Weight) + " New Container : " + container.Weight);

                return false;
            }
            if (this.containers.Count() > 0 && (this.containers.Last().type.ToString().Contains("Valuable")))
            {
                Console.WriteLine("it is not placed because the last container is valuable " + this.containers.Last().type.ToString());

                return false;
            }

            containers.Add(container);
            container.IsPlaced = true;
            return true;

        }
        public void display()
        {
            Console.WriteLine("Stack " + Y);
            containers.ForEach(container =>
            {
                container.display();
            });
            if (containers.Count > 0)
            {
                Console.WriteLine("Total Heavy: " + Convert.ToString(containers.Sum(a => a.Weight)));
            }
        }



    }
}
