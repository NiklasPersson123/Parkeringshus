using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringshus
{
    public abstract class Vehicle
    {
        public string RegistrationNumber { get; private set; }
        public string Color { get; private set; }
        public DateTime ParkingTime { get; private set; }

        protected Vehicle(string color)
        {
            RegistrationNumber = GenerateRegistrationNumber();
            Color = color;
            ParkingTime = DateTime.Now;
        }

        public abstract double ParkingSpace();

        private static string GenerateRegistrationNumber()
        {
            Random random = new Random();
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digits = "0123456789";
            return new string(Enumerable.Range(0, 3).Select(_ => letters[random.Next(letters.Length)]).ToArray()) +
                   new string(Enumerable.Range(0, 3).Select(_ => digits[random.Next(digits.Length)]).ToArray());
        }
    }
}
