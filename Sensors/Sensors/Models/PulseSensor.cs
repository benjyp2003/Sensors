using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    internal class PulseSensor : ISensor
    {
        public string Name { get; }
        int ActivateCounter { get; set; }

        public PulseSensor(string name)
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
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Pulse Sensor was activated 3 times.");
            Console.WriteLine($"Sensor {Name} is Breaking.. \n");
            SensorsVaulte.DeleteSensor(this);
            Console.ResetColor();

            Console.WriteLine($"Getting a new Sensor of type {this.GetType().Name} insted of the broken one...");
            Console.WriteLine("Enter a name for the new sensor: ");
            string sensorName = Console.ReadLine();

            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Type type = this.GetType();
                ISensor newSensor = (ISensor)Activator.CreateInstance(type, sensorName);
                Console.WriteLine($"Successfully created new pulse sensor: {sensorName} \n");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating sensor: {ex.Message}");
            }
        }

        public void AddToVaulte()
        {
            SensorsVaulte.AddSensorToList(this);
            Console.WriteLine($"Successfully Added sensor '{Name}' to the vault. \n");
        }
    }
}
