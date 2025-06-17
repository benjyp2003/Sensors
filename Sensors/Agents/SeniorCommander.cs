using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sensors.Agents
{
    internal class SeniorCommander : Agent
    {
        protected override ISensor[] SensitiveSensors { get; set; }
        protected override ISensor[] SensorSlots { get; set; }

        public SeniorCommander(string name)
        {
            Name = name;
            Rank = Sensors.Rank.FootSoldier;
            AttackCounter = 0;

            // Initialize arrays
            SensitiveSensors = new ISensor[6];
            SensorSlots = new ISensor[6];

            // Get random sensors
            var randomSensors = SensorsVaulte.GetRandomSensors(4);
            if (randomSensors.Count > 0)
            {
                for (int i = 0; i < Math.Min(randomSensors.Count, 4); i++)
                {
                    SensitiveSensors[i] = randomSensors[i];
                }

            }
            else
            {
                Console.WriteLine($"Warning: No sensors available for {name}. Agent will have no sensitive sensors.");
            }
        }

        public override void TryMatchingSensor(ISensor sensor)
        {
            AttackCounter++;
            if (AttackCounter >= 10)
            {
                Console.WriteLine("You reached 10 attacks! \n");
                ClearAllSensetiveSensors();
                ClearAllSensorsSlot();
                return;
            }
            if (AttackCounter == 3)
            {
                RemoveRandomSensors(2);
            }

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
    }
}
