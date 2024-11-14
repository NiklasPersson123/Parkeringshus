using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringshus
{
    public class Motorcycle : Vehicle
    {
        public string Brand { get; private set; }

        public Motorcycle(string color, string brand) : base(color)
        {
            Brand = brand;
        }

        public override double ParkingSpace()
        {
            return 0.5;
        }
    }
}
