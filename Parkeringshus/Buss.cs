using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringshus
{
    public class Bus : Vehicle
    {
        public int PassengerCount { get; private set; }

        public Bus(string color, int passengerCount) : base(color)
        {
            PassengerCount = passengerCount;
        }

        public override double ParkingSpace()
        {
            return 2;
        }
    }
}
