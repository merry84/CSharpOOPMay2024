namespace VehiclesExtension
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*•	On the first three lines you will receive information about the vehicles in the format:
              	"Vehicle {initial fuel quantity} {liters per km} {tank capacity}"
              •	On the fourth line - the number of commands N that will be given on the next N lines
              •	On the next N lines - commands in format:
              	"Drive Car {distance}"
              	"Drive Truck {distance}"
              	"Drive Bus {distance}"
              	"DriveEmpty Bus {distance}"
              	"Refuel Car {liters}"
              	"Refuel Truck {liters}"
              	"Refuel Bus {liters}"
              */
            string[] carInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            double fuelQuantity = double.Parse(carInfo[1]);
            double fuelConsumption = double.Parse(carInfo[2]);
            double tankCapacity = double.Parse(carInfo[3]);

            Car car = new(fuelQuantity, fuelConsumption, tankCapacity);

            string[] truckInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            double truckFuelQuantity = double.Parse(truckInfo[1]);
            double truckFuelConsumption = double.Parse(truckInfo[2]);
            double truckTankCapacity = double.Parse(truckInfo[3]);

            Truck truck = new (truckFuelQuantity, truckFuelConsumption, truckTankCapacity);

            string[] busInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            double busFuelQuantity = double.Parse(busInfo[1]);
            double busFuelConsumption = double.Parse(busInfo[2]);
            double busTankCapacity = double.Parse(busInfo[3]);

            Bus bus = new(busFuelQuantity, busFuelConsumption, busTankCapacity);  

            int count = int.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                try
                {
                    string[] commands = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string action = commands[0];
                    string typeVehicle = commands[1];
                    if(action == "Drive")
                    {
                        double distance = double.Parse(commands[2]);
                        if(typeVehicle == "Car")
                        {
                            car.Drive(distance);
                        }
                        else if(typeVehicle == "Truck")
                        {
                            truck.Drive(distance);
                        }
                        else if(typeVehicle == "Bus")
                        {
                            bus.Drive(distance);
                        }
                    }
                    else if(action == "Refuel")
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
                        else if (typeVehicle == "Bus")
                        {
                            bus.Refuel(liters);
                        }
                    }
                    else if(action == "DriveEmpty")
                    {
                        double distance = double.Parse(commands[2]);
                        bus.DriveEmptyBus(distance);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            /*•	After the "End" command, print the remaining fuel for all vehicles, 
             * rounded to 2 digits after the floating point in the format:
         	"Car: {liters}"
         	"Truck: {liters}"
         	"Bus: {liters}"    */
            Console.WriteLine($"Car: {car.FuelQuantity:f2}");
            Console.WriteLine($"Truck: {truck.FuelQuantity:f2}");
            Console.WriteLine($"Bus: {bus.FuelQuantity:f2}");

        }
    }
}
