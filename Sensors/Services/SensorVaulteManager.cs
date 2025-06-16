using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    internal static class SensorVaulteManager
    {
        public static void HandleVaulteMenuChoice()
        {
            while (true)
            {
                Console.Clear();
                Menu.ShowVaulteMenu();
                Console.WriteLine("Enter your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CreateNewSensor();
                        break;

                    case "2":
                        DeleteSensor();
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("Not a valid option! \n");
                        break;
                }
                char breakChar = ExitOption();
                if (breakChar == 'e' || breakChar == 'E')
                {
                    break;
                }
                Console.Clear();
            }
        }

        static void CreateNewSensor()
        {
            ShowTypes();
            Type type = ChooseType();
            if (type == null) return;
            
            string name = GetName();
            try
            {
                ISensor newSensor = (ISensor)Activator.CreateInstance(type, name);
                Console.WriteLine($"\nSuccessfully created new sensor: {name} of type {type.Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError creating sensor: {ex.Message}");
            }
        }

        static void ShowTypes()
        {
            Type[] typesOfSensors = SensorsVaulte.GetAllSensorTypes().ToArray();
            Console.WriteLine("Sensor types: ");
            for (int i = 0; i < typesOfSensors.Length; i++)
            {
                Console.WriteLine($"{i+1}: {typesOfSensors[i]}");
            }
        }

        static Type ChooseType()
        {
            try
            {
                Type[] typesOfSensors = SensorsVaulte.GetAllSensorTypes().ToArray();
                Console.WriteLine("Choose what type of sensor you want: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                for (int i = 1; i < typesOfSensors.Length+1; i++)
                {
                    if (choice == i)
                    {
                        return typesOfSensors[i-1];
                    }
                }
                Console.WriteLine("No types available.");
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"Error: {ex}, at SensorVaulteManager.ChooseType"); 
            }
            return null;
        }

        static void DeleteSensor()
        {
            string name = GetName();
            foreach (var sensor in SensorsVaulte.Sensors)
            {
                if (sensor.Name == name)
                {
                    SensorsVaulte.DeleteSensor(sensor);
                    return;
                }
            }
            Console.WriteLine("No matching sensor to delete.");
        }

        static string GetName()
        {
            Console.WriteLine("Enter name of the sensor: ");
            string name = Console.ReadLine();
            return name;
        }

        static char ExitOption()
        {
            Console.WriteLine();
            Console.WriteLine("┌──────────────────────────────────────────────┐");
            Console.WriteLine("│       Press any key  (or 'E' to exit):       |");
            Console.WriteLine("└──────────────────────────────────────────────┘");
            char key = Console.ReadKey().KeyChar;
            Console.WriteLine(); // Add a new line after the key press
            Console.ForegroundColor = ConsoleColor.White;
            return key;
        }
    }
}
