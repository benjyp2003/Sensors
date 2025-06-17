using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    /// <summary>
    /// A Static class that contains all the available sensors,
    /// and functions that returns wanted sensors.
    /// </summary>
    internal class SensorsVaulte
    {

        static protected List<ISensor> Sensors = new List<ISensor>();


        /// <summary>
        /// Adds a sensor to the sensor list.
        /// </summary>
        /// <param name="sensor"></param>
        public static void AddSensorToList(ISensor sensor)
        {
            if (sensor == null)
            {
                Console.WriteLine("Error: Cannot add null sensor to the vault.");
                return;
            }
            
            if (Sensors.Any(s => s.Name == sensor.Name))
            {
                Console.WriteLine($"Error: A sensor with name '{sensor.Name}' already exists in the vault.");
                return;
            }

            Sensors.Add(sensor);
            Console.WriteLine($"Successfully added sensor '{sensor.Name}' to the vault.");
        }


        /// <summary>
        /// Gets a givin amount of random sensors from the sensor list.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<ISensor> GetRandomSensors(int count)
        {
            try
            {
                if (Sensors == null || Sensors.Count == 0)
                {
                    Console.WriteLine("Error: Sensors list is empty or null. \n");
                    return new List<ISensor>();
                }

                Random random = new Random();
                List<ISensor> newSensors = new List<ISensor>();

                for (int i = 0; i < count; i++)
                {
                    newSensors.Add(Sensors[random.Next(Sensors.Count)]);
                }

                return newSensors;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex} in SensorVault.GetRandomSensors");
                return new List<ISensor>();
            }
        }

        public static ISensor GetSensorByName(string name)
        {
            foreach (ISensor sensor in Sensors)
            {
                if (sensor.Name == name)
                {  return sensor; }
            }
            return null;
        }

        public static List<ISensor> GetAllSensors()
        {
            return Sensors;
        }

        /// <summary>
        /// Show all the available Sensors.
        /// </summary>
        public static void ShowAllSensors()
        {
            Console.WriteLine("\nThe available Sensors are: ");
            foreach (var sensor in Sensors)
            {
                Console.WriteLine($"'{sensor.Name}' of type {sensor.GetType().Name}");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Returns all of the types of sensors in the sensor list.
        /// </summary>
        /// <returns></returns>
        protected static HashSet<Type> GetAllSensorTypes()
        {
            HashSet<Type> typesOfSensors = new HashSet<Type>();
            for (int i = 0; i < SensorsVaulte.Sensors.Count; i++)
            {
                typesOfSensors.Add(SensorsVaulte.Sensors[i].GetType());
            }

            return typesOfSensors;
        }

        /// <summary>
        /// Deletes a givin sensor from the list.
        /// </summary>
        /// <param name="sensor"></param>
        public static void DeleteSensor(ISensor sensor)
        {
            if (Sensors.Remove(sensor)) 
            {
                Console.WriteLine($"{sensor} was removed successfully from the Sensor List. \n");
            }
            else
            {
                Console.WriteLine($"{sensor} not found in list.");
            }
        }

        static void ClearList()
        { Sensors.Clear(); }
    }
}
