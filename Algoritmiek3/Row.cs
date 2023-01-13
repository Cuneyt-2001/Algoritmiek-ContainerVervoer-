using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Algoritmiek3.Side;

namespace Algoritmiek3
{
    public class Row
    {
        public const int C_WIDTH = 10, C_LENGTH = 10;
        public int x;
        public List<Stack> stacks;
        public Ship ship;

        public Row(int x, int shipWidth, int containerWidth, Ship ship)
        {
            this.x = x;
            this.ship = ship;
            stacks = new List<Stack>();

            for (int i = 0; i < shipWidth / containerWidth; i++)
            {
                var stackSide = FindStackSide(shipWidth, containerWidth, i);
                Stack stack = new Stack(i, stackSide);

                stacks.Add(stack);

            }

        }
        public StackSide FindStackSide(int shipWidth, int containerWidth, int stackIndex)
        {
            var midIndex = shipWidth / containerWidth / 2;
            var result = StackSide.Left;
            if (stackIndex < midIndex)
            {
                result = StackSide.Left;
            }
            else if(stackIndex > midIndex)
            {
                result = StackSide.Right;
            }
            else
            {
                result = StackSide.Mid;
            }
           

            return result;
        }

        public bool PlaceContainer(Container container)
        {
            foreach (var stack in stacks)
            {

                if (!IsTrueSide(container, stack.StackSide))
                {
                    continue;
                }

                if (stack.PlaceContainer(container))
                {
                    
                    return true;
                }

            }

            return false;

        }
        public void display()
        {

            Console.WriteLine("Row " + x);
            stacks.ForEach(stack =>
            {
                stack.display();
            });
        }

        public int TotalWeight()
        {
            int sum = 0;
            ship.rows.ForEach(row => row.stacks.ForEach(stack =>
               stack.containers.ForEach(container => sum += container.Weight)));
            return sum;
        }


        public bool IsTrueSide(Container c, StackSide stackSide)
        {
            var result = true;
            if (stackSide == StackSide.Right)
            {

                var sub = Math.Abs((RightSideWeight() + c.Weight) - LeftSideWeight());
                var totalWeight = TotalWeight() + c.Weight;

                if (sub > (totalWeight * 0.2))
                {
                    result = false;
                }
            }
            if (stackSide == StackSide.Left)
            {
                var sub = Math.Abs((LeftSideWeight() + c.Weight) - RightSideWeight());
                var totalWeight = TotalWeight() + c.Weight;


                if (sub > (totalWeight * 0.2))
                {
                    result = false;
                }
                return result;
            }

            if (stackSide == StackSide.Mid)
            {
                var left = LeftSideWeight();
                var right = RightSideWeight();

                var sub = Math.Abs((LeftSideWeight() + c.Weight / 2) - (RightSideWeight() + c.Weight / 2));

                var totalWeight = TotalWeight() + c.Weight;
                if (sub > (totalWeight * 0.2))
                {
                    result = false;
                }
            }
            return result;
        }

        public int LeftSideWeight()
        {
            var result = 0;
            var countofstackforrow = this.stacks.Count;
            int leftMaxIndex = (countofstackforrow / 2) - 1;
            var leftsStacks = new List<Stack>();
            foreach (var row in ship.rows)
            {
                leftsStacks.AddRange(row.stacks.Where(b => b.StackSide == StackSide.Left));
            }
            result = leftsStacks.Sum(a => a.containers.Sum(b => b.Weight));

            //
            if ((ship.ShipWidth / C_WIDTH) % 2 != 0)
            {
                var midStackIndex = ship.ShipWidth / C_WIDTH / 2;
                var midStackTotalWeight = 0;
                foreach (var row in ship.rows)
                {
                    midStackTotalWeight = row.stacks.Where(b => b.StackSide == StackSide.Mid).Sum(a => a.containers.Sum(b => b.Weight));
                    result = result + (midStackTotalWeight / 2);
                }
            }

            return result;

        }
        public int RightSideWeight()
        {
            var result = TotalWeight() - LeftSideWeight();
            return result;

        }


    }
}


