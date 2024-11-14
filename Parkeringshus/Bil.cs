using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkeringshus
{
    public class Car : Vehicle
    {
        public bool IsElectric { get; private set; }

        public Car(string color, bool isElectric) : base(color)
        {
            IsElectric = isElectric;
        }

        public override double ParkingSpace()
        {
            return 1;
        }
    }
}
