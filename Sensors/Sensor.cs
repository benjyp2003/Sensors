using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sensors
{
    internal class Sensor : ISensor
    {
        public string Name {  get; }
        public Sensor(string name)
        {
            Name = name;
        }

        public void Activate()
        {
            Console.WriteLine($"The sensor {Name} was activated.");
        }
    }
}
