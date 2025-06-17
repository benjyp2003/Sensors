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

        public string Affiliation { get; set; }

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
            Console.WriteLine();
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

        /// <summary>
        /// Checks if the givin sensor is a special type sensor.
        /// </summary>
        /// <param name="sensor"></param>
        void SpecialSensorActions(ISensor sensor)
        {
            switch (sensor.GetType().Name)
            {
                case "ThermalSensor":
                    ShowSensitiveSensor();
                    break;

                case "MagneticSensor":
                    AttackCounter -= 6;
                    break;

                case "SignalSensor":
                    ShowInfo(1);
                    break;

                case "LightSensor":
                    ShowInfo(2);
                    break;

            }
        }

        void ShowSensitiveSensor()
        {
            Random random = new Random();
            int randomIndex = random.Next(0, SensitiveSensors.Length);
            Console.WriteLine($"Thermal sensor revealed the sensitive sensor - '{SensitiveSensors[randomIndex].Name}' \n");
        }

        void ShowInfo(int numberOfFieldsToShow)
        {
            switch (numberOfFieldsToShow)
            {
                case 1:
                    Console.WriteLine($"Signal Sensor revealed agents Rank '{this.Rank}'");
                    break;

                case 2:
                    Console.WriteLine($"Light Sensor revealed Agents - \n" +
                                      $"Rank: '{this.Rank}' \n" +
                                      $"Affiliation: '{this.Affiliation}'");
                    break;
            }
        }

        /// <summary>
        /// Remove a givin amount of random sensors.
        /// </summary>
        /// <param name="num"></param>
        protected virtual void RemoveRandomSensors(int num)
        {
            try
            {
                Random random = new Random();
                List<int> nonNullIndexes = new List<int>();
                Console.WriteLine($"You tried 3 Times, Removing {num} random Sensor");

                for (int i = 0; i < SensorSlots.Length; i++)
                {
                    if (SensorSlots[i] != null)
                    { nonNullIndexes.Add(i); }
                }
                if (nonNullIndexes.Count == 0)
                {
                    Console.WriteLine("No Sensors to Remove.\n");
                    return;
                }
                for (int i = 0; i < num; i++)
                {
                    int rand = random.Next(nonNullIndexes.Count);
                    int slotIndex = nonNullIndexes[rand];
                    ISensor sensorToDelete = SensorSlots[slotIndex];

                    Console.WriteLine($"Removing sensor '{sensorToDelete.Name}'..\n");
                    SensorSlots[slotIndex] = null; 
                    successfulMatches--;

                    nonNullIndexes.RemoveAt(rand); // Optionally avoid duplicate removal
                    if (nonNullIndexes.Count == 0)
                    { 
                        if (i < num)
                        { Console.WriteLine("No more Sensors to delete. \n"); }
                        break; 
                    }
                }
            }
            catch (Exception ex)
            { Console.WriteLine($"{ex}"); }
        }

        protected virtual void ResetAllSensetiveSensors()
        {
            Array.Clear(SensitiveSensors, 0, SensitiveSensors.Length);
            SensitiveSensors = SensorsVaulte.GetRandomSensors(2).ToArray();
            Console.WriteLine("Reseted the SensitiveSensors array. \n");
        }

        protected virtual void ClearAllSensorsSlot()
        {
            Array.Clear(SensorSlots, 0, SensorSlots.Length);
            Console.WriteLine("Cleard the SensorsSlot array. \n");
            successfulMatches = 0;
        }

        protected void ResetAttackCount()
        {
            AttackCounter = 0;
        }

    }
}
