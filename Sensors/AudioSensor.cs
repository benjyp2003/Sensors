using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sensors
{
    internal class AudioSensor : ISensor
    {
        public string Name {  get; }
        public AudioSensor()
        {
            Name = "Sony Microphone";
        }

        public void Activate()
        {
            Console.WriteLine($"The sensor {Name} was activated.");
        }
    }
}
