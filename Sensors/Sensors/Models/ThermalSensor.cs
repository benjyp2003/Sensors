using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors.Sensors
{
    internal class ThermalSensor : ISensor 
    {
        public string Name { get; }

        public ThermalSensor(string name)
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
            Console.WriteLine($"Successfully Added sensor '{Name}' to the vault. \n");
        }
    }
}
