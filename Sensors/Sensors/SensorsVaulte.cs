using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    /// <summary>
    /// A Singlton class that contains all the available sensors,
    /// and functions the returns wanted sensors.
    /// </summary>
    internal class SensorsVaulte
    {
        static SensorsVaulte Instance = null;

        static public List<ISensor> Sensors = new List<ISensor>();

        SensorsVaulte() { }

        static public SensorsVaulte GetInstance()
        {
            if (Instance == null)
            { Instance = new SensorsVaulte(); }
            return Instance;
        }

        /// <summary>
        /// Adds a sensor to the sensor list.
        /// </summary>
        /// <param name="sensor"></param>
        public void AddSensorToList(ISensor sensor)
        {
            Sensors.Add(sensor);
        }

        
        /// <summary>
        /// Gets a givin amount of random sensors from the sensor list.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<ISensor> GetRandomSensors(int count)
        {
            Random random = new Random();
            List<ISensor> newSensors = new List<ISensor>();
            for (int i = 0; i < count; i++)
            {
                newSensors.Add(Sensors[random.Next(Sensors.Count-1)]);
            }
            return newSensors;
        }

        /// <summary>
        /// Deletes a givin sensor from the list.
        /// </summary>
        /// <param name="sensor"></param>
        public void DeleteSensor(ISensor sensor)
        {
            if (Sensors.Remove(sensor)) 
            {
                Console.WriteLine($"{sensor} was removed successfully");
            }
            else
            {
                Console.WriteLine($"{sensor} not found in list.");
            }
        }

        void ClearList()
        { Sensors.Clear(); }
    }
}
