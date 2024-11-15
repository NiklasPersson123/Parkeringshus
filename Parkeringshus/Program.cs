namespace Parkeringshus
{
    internal class Program
    {
        static void Main()
        {
            ParkingLot parkingLot = new ParkingLot();
            while (true)
            {
                Console.WriteLine("\nParking System Menu");
                Console.WriteLine("1. Park a Vehicle");
                Console.WriteLine("2. Remove a Vehicle");
                Console.WriteLine("3. Display Parked Vehicles");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            AddVehicleMenu(parkingLot);
                            
                            break;
                        case 2:
                            
                            Console.Write("Enter registration number: ");
                            string regNumber = Console.ReadLine().ToUpper();
                            parkingLot.RemoveVehicle(regNumber);
                            
                            break;
                        case 3:
                            Console.Clear();
                            parkingLot.DisplayParkedVehicles();
                            
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine("Exiting the program.");
                            return;
                        default:
                            
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        static void AddVehicleMenu(ParkingLot parkingLot)
        {
            Console.WriteLine("\nChoose Vehicle Type:");
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Motorcycle");
            Console.WriteLine("3. Bus");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.Write("Enter vehicle color: ");
                string color = Console.ReadLine();
                
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("Is the car electric (yes/no)? ");
                        bool isElectric = Console.ReadLine().Trim().ToLower() == "yes";
                        parkingLot.ParkVehicle(new Car(color, isElectric));
                        
                        break;
                    case 2:
                        Console.Clear();
                        Console.Write("Enter motorcycle brand: ");
                        string brand = Console.ReadLine();
                        parkingLot.ParkVehicle(new Motorcycle(color, brand));
                        
                        break;
                    case 3:
                        Console.Clear();
                        Console.Write("Enter number of passengers: ");
                        if (int.TryParse(Console.ReadLine(), out int passengerCount))
                        {
                            parkingLot.ParkVehicle(new Bus(color, passengerCount));
                        }
                        else
                        {
                            Console.WriteLine("Invalid input for passengers.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid vehicle type.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }
}
