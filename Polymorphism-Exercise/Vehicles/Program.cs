namespace Vehicles
{
    public class Program
    {
        static void Main()
        {
            /*•	On the first line – information about the car in the format:
             * "Car {fuel quantity} {liters per km}"
             •	On the second line – info about the truck in the format: "Truck {fuel quantity} {liters per km}"
             •	On the third line – the number of commands N that will be given on the next N lines
             •	On the next N lines – commands in the format:
             §	"Drive Car {distance}"
             §	"Drive Truck {distance}"
             §	"Refuel Car {liters}"
             §	"Refuel Truck {liters}"
             */
            string[] carInfo = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            double fuelQuantity = double.Parse(carInfo[1]);
            double fuelConsumption = double.Parse(carInfo[2]);
            Car car = new(fuelQuantity, fuelConsumption);

            string[] truckInfo = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            double truckFuelQuatity = double.Parse(truckInfo[1]);
            double truckFuelConsumption = double.Parse(truckInfo[2]);
            Truck truck = new(truckFuelQuatity, truckFuelConsumption);

            int countLine = int.Parse(Console.ReadLine());

            for (int i = 0; i < countLine; i++)
            {
                try
                {
                    string[] commands = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string action = commands[0];
                    string typeVehicle = commands[1];
                    if (action == "Drive")
                    {
                        double distane = double.Parse(commands[2]);
                        if (typeVehicle == "Car")
                        {
                            car.Drive(distane);
                        }
                        else if (typeVehicle == "Truck")
                        {
                            truck.Drive(distane);
                        }
                    }
                    else if (action == "Refuel")
                    {
                        double liters = double.Parse(commands[2]);
                        if (typeVehicle == "Car")
                        {
                            car.Refuel(liters);
                        }
                        else if (typeVehicle == "Truck")
                        {
                            truck.Refuel(liters);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine(car.ToString());
            Console.WriteLine(truck.ToString());
        }
    }
}
