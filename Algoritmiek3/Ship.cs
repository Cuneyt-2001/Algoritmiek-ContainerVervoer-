using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Algoritmiek3.Side;

namespace Algoritmiek3
{
    public class Ship
    {
        public const int C_WIDTH = 10, C_LENGTH = 10;
        public int ShipWidth;
        public int ShipLenght;
        public int ShipWeight;
        public List<Row> rows;
        public Ship(int width, int lenght, int ShipWeight)
        {
            
            this.ShipWidth = width;
            this.ShipLenght = lenght;
            rows = new List<Row>();
            this.ShipWeight = ShipWeight;
            init();
        }
        public int init()
        {
            int result = 0;
            for (int i = 0; i < ShipWidth / C_LENGTH; i++)
            {
                CreateRow(i);
                result++;
            }
            return result;
        }
        public int CreateRow(int x)
        {
            Row row = new Row(x ,ShipWidth, C_WIDTH, this);

            rows.Add(row);
            return rows.Count;
        }
       
        public bool PlaceContainer(Container container)
        {
            if (container.Weight > 30 || container.Weight < 0)
            {

                return false;

            }

            foreach (var row in rows)
            {


                if (container.type == Type.ValuableContainers || container.type == Type.ColdandValuableContainers)
                {
                    if (!(row == rows.Last() || row == rows.First()))
                    {

                        continue;
                    }
                }

                if ((container.type == Type.ColdContainers || container.type == Type.ColdandValuableContainers) && row.x != 0)
                {
                    Console.WriteLine("it is not placed because cold must be at first row " + container.type.ToString() + "row index: " + row.x);

                    return false;
                }

                if (row.PlaceContainer(container))
                {

                    return true;
                }
            }

            return false;

        }
   

        public bool IsSafe()
        {
            int sum = 0;
            rows.ForEach(row =>
            row.stacks.ForEach(stack =>
            stack.containers.ForEach(container => sum += container.Weight)));
            return (sum >= this.ShipWeight / 2);
        }
        public void display()
        {
            Console.WriteLine("Ship: ");

            rows.ForEach(row =>
            {
                row.display();
            });
        }


    }
}
