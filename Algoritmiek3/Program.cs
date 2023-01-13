using static Algoritmiek3.Side;

namespace Algoritmiek3
{
    public class program
    {
        public const int C_WIDTH = 10, C_LENGTH = 10;
        static void Main(string[] args)
        {

            Ship ship = new Ship(30, 50, 150);
            int Wamount = ship.ShipWidth / C_WIDTH, Lamount = ship.ShipLenght / C_LENGTH;


            List<Container> containersToPlace = new List<Container>();

            Random rnd = new Random();


            int containerCount = 50;
            for (int i = 0; i < containerCount; i++)
            {
                containersToPlace.Add(new Container(rnd.Next(10, 26), (Type)(rnd.Next(0, 4))));
            }

            int countofcold = containersToPlace.Where(c => c.type == Type.ColdContainers).Count();
            int countofcoldandvaluable = containersToPlace.Where(c => c.type == Type.ColdandValuableContainers).Count();
            int regular = containersToPlace.Where(c => c.type == Type.RegularContiners).Count();
            int valuable = containersToPlace.Where(c => c.type == Type.ValuableContainers).Count();

            var placedContainers = new List<Container>();
            var unPlacedContainers = new List<Container>();


            containersToPlace = containersToPlace.OrderBy(x => (byte)x.type).ThenByDescending(a => a.Weight).ToList();
            containersToPlace.ForEach(container =>
            {
                var result = ship.PlaceContainer(container);
                if (result == true)
                {
                    placedContainers.Add(container);
                }
                else
                {
                    unPlacedContainers.Add(container);
                }
            });

            //Display
            var rows = ship.rows;
            rows.ForEach(r => r.stacks.ForEach(s => s.containers.ForEach(c => Console.WriteLine(

                     ))));

            var result = ship.IsSafe();
            var totalstackcount = rows.Sum(a => a.stacks.Count);
            var totalcontainers = rows.Sum(a => a.stacks.Sum(b => b.containers.Count));
            var totalWeight = rows.Sum(a => a.stacks.Sum(b => b.containers.Sum(c => c.Weight)));



            var midWeight = rows.Sum(a => a.stacks.Where(x => x.StackSide == StackSide.Mid).Sum(b => b.containers.Sum(c => c.Weight))) + ship.ShipWidth;
            var rightWeight = rows.Sum(a => a.stacks.Where(x => x.StackSide == StackSide.Right).Sum(b => b.containers.Sum(c => c.Weight))) + ship.ShipWidth + midWeight / 2;
            var leftWeight = rows.Sum(a => a.stacks.Where(x => x.StackSide == StackSide.Left).Sum(b => b.containers.Sum(c => c.Weight))) + ship.ShipWidth + midWeight / 2;


            void SendConsole()
            {
                if (result == true)
                {
                    Console.WriteLine("Ship is safe");
                }
                else
                {
                    Console.WriteLine("Ship is not safe");
                }
                int sum = 0;
                ship.rows.ForEach(row =>
                row.stacks.ForEach(stack =>
                stack.containers.ForEach(container => sum += container.Weight)));

                Console.WriteLine("TotalHeavy: " + sum);
                Console.WriteLine("ShipWeight: " + ship.ShipWeight);
                Console.WriteLine("Stacks: " + totalstackcount);
                Console.WriteLine("PlacedContainers " + totalcontainers);
                Console.WriteLine("Cold " + countofcold);
                Console.WriteLine("coldandvaluable " + countofcoldandvaluable);
                Console.WriteLine("regular " + regular);
                Console.WriteLine("valuable " + valuable);

                Console.WriteLine("Total Weight: " + totalWeight);
                Console.WriteLine("Right Weight: " + rightWeight);
                Console.WriteLine("Left Weight: " + leftWeight);
                Console.WriteLine("Left Weight: " + ((totalWeight * 0.20) >= Math.Abs(leftWeight - rightWeight)));
            }
            SendConsole();  
            ship.display();
        }

    }











}
