using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sensors;

namespace Agents.Models
{
    internal class FootSoldier : Agent
    {
        protected override ISensor[] SensitiveSensors { get; set; } 
        protected override ISensor[] SensorSlots { get; set; }

        public FootSoldier(string name, string affiliation) : base()
        {
            Name = name;
            Rank = Sensors.Rank.FootSoldier;
            Affiliation = affiliation;
            
            // Initialize arrays
            SensitiveSensors = new ISensor[2];
            SensorSlots = new ISensor[2];
            
            // Get random sensors
            var randomSensors = SensorsVaulte.GetRandomSensors(2);
            if (randomSensors.Count > 0)
            {
                for (int i = 0; i < Math.Min(randomSensors.Count, 2); i++)
                {
                    SensitiveSensors[i] = randomSensors[i];
                }

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Warning: No sensors available for {name}. Agent will have no sensitive sensors.");
                Console.ResetColor();
            }
        }

        

    }
}
