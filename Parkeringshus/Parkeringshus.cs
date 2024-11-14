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
                if (vehicle is Motorcycle mc && TryParkMotorcycle(mc))
                {
                    return;
                }
                vehicles.Add(vehicle);
                OccupiedSpaces += requiredSpace;
                Console.WriteLine($"Vehicle {vehicle.RegistrationNumber} parked successfully.");
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
                if (spot.Value.Count < 2)
                {
                    spot.Value.Add(motorcycle);
                    vehicles.Add(motorcycle);
                    Console.WriteLine($"Motorcycle {motorcycle.RegistrationNumber} shares spot {spot.Key}.");
                    return true;
                }
            }

            int newSpot = motorcycleSpots.Count + 1;
            motorcycleSpots[newSpot] = new List<Motorcycle> { motorcycle };
            vehicles.Add(motorcycle);
            OccupiedSpaces += 0.5;
            Console.WriteLine($"Motorcycle {motorcycle.RegistrationNumber} parked at new spot {newSpot}.");
            return true;
        }

        public void RemoveVehicle(string registrationNumber)
        {
            var vehicle = vehicles.FirstOrDefault(v => v.RegistrationNumber == registrationNumber);
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
            int spotNumber = 1;
            foreach (var vehicle in vehicles)
            {
                if (vehicle is Car car)
                {
                    string carType = car.IsElectric ? "Electric Car" : "Petrol Car";
                    Console.WriteLine($"Spot {spotNumber}: Car {vehicle.RegistrationNumber} - {vehicle.Color} ({carType})");
                }
                else if (vehicle is Motorcycle motorcycle)
                {
                    Console.WriteLine($"Spot {spotNumber}: Motorcycle {vehicle.RegistrationNumber} - {vehicle.Color} (Brand: {motorcycle.Brand})");
                }
                else if (vehicle is Bus bus)
                {
                    Console.WriteLine($"Spot {spotNumber}-{spotNumber + 1}: Bus {vehicle.RegistrationNumber} - {vehicle.Color} (Passengers: {bus.PassengerCount})");
                    spotNumber++;
                }
                spotNumber++;
            }
        }
    }
}
