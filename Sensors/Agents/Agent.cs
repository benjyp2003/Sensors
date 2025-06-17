using Sensors.Sensors;
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

        protected string Affiliation { get; set; }

        protected int AttackCounter {  get; set; }

        protected abstract ISensor[] SensitiveSensors { get; set; }
        protected abstract ISensor[] SensorSlots { get; set; }
        
        protected int successfulMatches = 0;
        public bool IsExposed => successfulMatches >= SensitiveSensors.Length;

        protected Agent()
        {
            Prison.Instance.AddAgentToList(this);
            AttackCounter = 0;
        }

        /// <summary>
        /// Gets a sensor and trys to match it to the SensitiveSensors array.
        /// if manages to match activates the sensor.
        /// </summary>
        /// <param name="sensor"></param>
        public virtual void TryMatchingSensor(ISensor sensor)
        {
            if (IsExposed)
            {
                Console.WriteLine($"Agent {this.Name} Already exposed! \n");
                return;
            }

            AttackCounter++;
            if (AttackCounter >= 10)
            {
                Console.WriteLine("You reached 10 attacks! \n");
                ResetAllSensetiveSensors();
                ClearAllSensorsSlot();
                return;
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

        /// <summary>
        /// Activates the sensor
        /// </summary>
        /// <param name="sensor"></param>
        protected virtual void ActivateSensor(ISensor sensor)
        {
            sensor.Activate();
            SpecialSensorActions(sensor);
        }

        void SpecialSensorActions(ISensor sensor)
        {
            switch (sensor.GetType().Name)
            {
                case "ThermalSensor":
                    ShowSensitiveSensor();
                    break;

                case 
            }
        }

        void ShowSensitiveSensor()
        {
            Random random = new Random();
            int randomIndex = random.Next(0, SensitiveSensors.Length);
            Console.WriteLine($"Thermal sensor revealed the sensitive sensor - '{SensitiveSensors[randomIndex].Name}' \n");
        }


        /// <summary>
        /// Remove a givin amount of random sensors.
        /// </summary>
        /// <param name="num"></param>
        protected virtual void RemoveRandomSensors(int num)
        {
            Random random = new Random();
            for (int i = 0; i < num; i++)
            {
                int rand = random.Next(SensorSlots.Length);
                ISensor sensorToDelete = SensorSlots[rand];
                Console.WriteLine($"Removing sensor '{sensorToDelete.Name}'.. \n");
                SensorSlots[rand] = null;
                successfulMatches--;
            }
        }

        protected virtual void ResetAllSensetiveSensors()
        {
            Array.Clear(SensitiveSensors, 0, SensitiveSensors.Length);
            SensitiveSensors = SensorsVaulte.GetRandomSensors(2).ToArray();
            Console.WriteLine("Reseted the SensitiveSensors array.");
        }

        protected virtual void ClearAllSensorsSlot()
        {
            Array.Clear(SensorSlots, 0, SensorSlots.Length);
            Console.WriteLine("Cleard the SensorsSlot array.");
            successfulMatches = 0;
        }

    }
}
