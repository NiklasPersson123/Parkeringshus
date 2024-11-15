using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringshus
{
    public class ParkingLot
    {
        private const double PricePerMinute = 1.5;
        private readonly List<Vehicle> vehicles;
        private readonly Dictionary<int, List<Motorcycle>> motorcycleSpots; //Hanterar delade parkerings platser

        public double TotalSpaces { get; }
        public double OccupiedSpaces { get; private set; }

        public ParkingLot(double totalSpaces = 15)
        {
            TotalSpaces = totalSpaces;
            OccupiedSpaces = 0;
            vehicles = new List<Vehicle>();
            motorcycleSpots = new Dictionary<int, List<Motorcycle>>();
        }

        public void ParkVehicle(Vehicle vehicle)
        {
            double requiredSpace = vehicle.ParkingSpace();
            if (OccupiedSpaces + requiredSpace <= TotalSpaces)
            {
                if (vehicle is Motorcycle mc)
                {
                    if (TryParkMotorcycle(mc))
                    {
                        return;
                    }
                }
                else
                {
                    vehicles.Add(vehicle); 
                    OccupiedSpaces += requiredSpace;
                    Console.WriteLine($"Vehicle {vehicle.RegistrationNumber} parked successfully.");
                }
            }
            else
            {
                Console.WriteLine("Insufficient space to park this vehicle.");
            }
        }

        private bool TryParkMotorcycle(Motorcycle motorcycle)
        {
            foreach (var spot in motorcycleSpots)
            {
                if (spot.Value.Count < 2) // Tittar om det finns en ledig MC-plats
                {
                    spot.Value.Add(motorcycle);
                    OccupiedSpaces += 0.5; // Bara MC påverkar delad plats
                    Console.WriteLine($"Motorcycle {motorcycle.RegistrationNumber} shares spot {spot.Key}.");
                    return true;
                }
            }

            // Om ingen delad plats är tillgänglig, skapa en ny
            int newSpot = motorcycleSpots.Count + 1;
            motorcycleSpots[newSpot] = new List<Motorcycle> { motorcycle };
            OccupiedSpaces += 0.5;
            Console.WriteLine($"Motorcycle {motorcycle.RegistrationNumber} parked at new spot {newSpot}.");
            return true;
        }

        public void RemoveVehicle(string registrationNumber)
        {
            var vehicle = vehicles.FirstOrDefault(v => v.RegistrationNumber == registrationNumber); //Lambdauttryck!!! som används för att specifiera sökvillkor i detta fall regnr
            if (vehicle != null)
            {
                double parkingDuration = (DateTime.Now - vehicle.ParkingTime).TotalMinutes;
                double cost = Math.Round(parkingDuration * PricePerMinute, 2); //Math.Round är en inbyggd funktion som avrundar
                vehicles.Remove(vehicle);
                OccupiedSpaces -= vehicle.ParkingSpace();
                Console.WriteLine($"Vehicle {registrationNumber} removed. Parking cost: {cost} kr.");
            }
            else
            {
                Console.WriteLine($"Vehicle with registration number {registrationNumber} not found.");
            }
        }

        public void DisplayParkedVehicles()
        {
            Console.WriteLine("Displaying all parked vehicles:");
            int spotNumber = 1; 
            foreach (var spot in motorcycleSpots)
            {
                Console.Write($"Spot {spotNumber}: ");
                for (int i = 0; i < spot.Value.Count; i++)
                {
                    var motorcycle = spot.Value[i];
                    Console.Write($"{motorcycle.RegistrationNumber} ({motorcycle.Color}, Brand: {motorcycle.Brand})");
                    if (i < spot.Value.Count - 1)
                    {
                        Console.Write(", ");
                    }
                }
                Console.WriteLine(); // Ny rad efter motorcyklar i denna plats
                spotNumber++; // Öka platsnumret efter en motorcykelplats
            }

            // Visa andra fordon som inte är motorcyklar (bilar och bussar)
            foreach (var vehicle in vehicles.Where(v => !(v is Motorcycle))) //"Where" filtrerar en sekvens av värden 
            {
                if (vehicle is Car car)
                {
                    string carType = car.IsElectric ? "Electric Car" : "Petrol Car";
                    Console.WriteLine($"Spot {spotNumber}: Car {vehicle.RegistrationNumber} - {vehicle.Color} ({carType})");
                    spotNumber++; // Öka platsnumret för bilen
                }
                else if (vehicle is Bus bus)
                {
                    Console.WriteLine($"Spot {spotNumber}-{spotNumber + 1}: Bus {vehicle.RegistrationNumber} - {vehicle.Color} (Passengers: {bus.PassengerCount})");
                    spotNumber += 2; // Bussar tar två platser
                }
            }
        }
    }
}
