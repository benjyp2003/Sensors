using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    internal abstract class Agent 
    {
        public string Name { get; set; }
        /// <summary>
        /// Agents rank 2 - 5, Where 5 is the highest rank and 2 is the lowest.
        /// </summary>
        public Rank Rank { get; set; }
        protected abstract ISensor[] SensitiveSensors { get; set; }
        protected abstract ISensor[] SensorSlots { get; set; }
        
        protected int successfulMatches = 0;
        public bool IsExposed => successfulMatches >= SensitiveSensors.Length;

        protected Agent()
        {
            Prison.Instance.AddAgentToList(this);
        }

        /// <summary>
        /// Gets a sensor and trys to match it to the SensitiveSensors array.
        /// if manages to match activates the sensor.
        /// </summary>
        /// <param name="sensor"></param>
        public virtual void TryMatchingSensor(ISensor sensor)
        {
            // Check if all slots are filled
            if (successfulMatches >= SensitiveSensors.Length)
            {
                Console.WriteLine("All sensor slots are already filled. \n");
                return;
            }
            
            // Check each SensitiveSensor
            for (int i = 0; i < SensitiveSensors.Length; i++)
            {
                if (SensitiveSensors[i] != null && SensitiveSensors[i].Name == sensor.Name)
                {
                    // Check if the slot is not already filled
                    if (SensorSlots[i] == null)
                    {
                        SensorSlots[i] = sensor;
                        successfulMatches++;
                        Console.WriteLine($"\nSensor {sensor.Name} matched and placed in slot {i}. \n");

                        ActivateSensor(sensor); // Activate the sensor

                        // Print progress
                        if (successfulMatches < SensitiveSensors.Length)
                        {
                            Console.WriteLine($"{successfulMatches}/{SensitiveSensors.Length} Found \n");
                        }
                        else
                        {
                            Console.WriteLine($"{successfulMatches}/{SensitiveSensors.Length} found, Agent Exposed! \n");
                        }

                        return;
                    }
                }
            }

            Console.WriteLine($"Sensor {sensor.Name} did not match or its slot is already filled. \n");
        }

        // Activates the sensor
        protected virtual void ActivateSensor(ISensor sensor)
        {
            sensor.Activate();
        }
    }
}
