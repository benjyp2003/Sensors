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
        public AudioSensor(string name)
        {
            Name = name;
            AddToVaulte();
        }

        public void Activate()
        {
            Console.WriteLine($"The sensor '{Name}' was activated. \n");
        }

        public void AddToVaulte()
        {
            SensorsVaulte.AddSensorToList(this);
            Console.WriteLine($"Added sensor '{Name}' to the vault. \n");
        }
    }
}
