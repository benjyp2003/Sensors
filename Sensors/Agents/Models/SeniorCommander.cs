using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Sensors;

namespace Agents.Models
{
    internal class SeniorCommander : Agent
    {
        protected override Type[] SensitiveSensors { get; set; }
        protected override ISensor[] SensorSlots { get; set; }

        public SeniorCommander(string name, string affiliation) : base()
        {
            Name = name;
            Rank = Sensors.Rank.SeniorCommander;
            Affiliation = affiliation;
            AttackCounter = 0;

            // Initialize arrays
            SensitiveSensors = new Type[6];
            SensorSlots = new ISensor[6];

            // Get random sensors
            var randomSensors = SensorsVaulte.GetRandomSensorsType(6);
            if (randomSensors.Count > 0)
            {
                for (int i = 0; i < Math.Min(randomSensors.Count, 6); i++)
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
            SpecialAttackCounter++;
           
            if (SpecialAttackCounter == 3)
            {
                RemoveRandomSensors(2);
                SpecialAttackCounter = 0;
            }

            // check if you tried 10 times
            if (CheckAttackCounter()) { return; }

            // Check if all slots are filled
            if (CheckIfSlotsAreFull()) { return; }
            

            // Check each SensitiveSensor
            for (int i = 0; i < SensitiveSensors.Length; i++)
            {
                // check if the type in the sensitive arr matches the givin sensor type
                if (SensitiveSensors[i] != null && SensitiveSensors[i].Name == sensor.GetType().Name)
                {
                    // Check if the slot is not already filled
                    if (SensorSlots[i] == null)
                    {
                        AttackCounter--;  // if you guess right the sensitive sensor it wont count as a turn (for the main attack count).

                        Console.ForegroundColor = ConsoleColor.Green;
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
                        Console.ResetColor();
                        return;
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Sensor {sensor.Name} did not match or its slot is already filled. \n");
            Console.ResetColor();
        }

        protected override void ResetAllSensetiveSensors()
        {
            Array.Clear(SensitiveSensors, 0, SensitiveSensors.Length);
            SensitiveSensors = SensorsVaulte.GetRandomSensorsType(6).ToArray();
            Console.WriteLine("Reseted the SensitiveSensors array.");
        }
    }
}
