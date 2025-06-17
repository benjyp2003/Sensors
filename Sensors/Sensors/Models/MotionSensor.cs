using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors.Agents
{
    internal class MotionSensor : ISensor
    {
        public string Name { get; }
        int ActivateCounter { get; set; }

        public MotionSensor(string name)
        {
            Name = name;
            AddToVaulte();
        }

        public void Activate()
        {
            ActivateCounter++;
            if (ActivateCounter == 3)
            {
                BreakSensor();
            }
            else
            {
                Console.WriteLine($"The sensor '{Name}' was activated. \n");
            }
        }

        void BreakSensor()
        {
            Console.WriteLine($"Sensor {Name} is Broken. \n");
            SensorsVaulte.DeleteSensor(this);
        }

        public void AddToVaulte()
        {
            SensorsVaulte.AddSensorToList(this);
            Console.WriteLine($"Added sensor '{Name}' to the vault. \n");
        }
    }
}
