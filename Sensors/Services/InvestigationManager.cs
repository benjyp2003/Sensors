using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sensors
{
    internal static class InvestigationManager
    {
        public static void Run()
        {
            // Initialize some sensors
            AudioSensor sensor1 = new AudioSensor("Sony Microphone");
            AudioSensor sensor2 = new AudioSensor("Bose Microphone");

            // Initialize some agents
            FootSoldier agent1 = new FootSoldier("Yosuf");
            FootSoldier agent2 = new FootSoldier("Ahmed");

            HandleMenuChoice();
        }

        static void HandleMenuChoice()
        {
            while (true)
            {
                Console.Clear();
                Menu.ShowMenu();
                Console.WriteLine("Enter your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        SensorVaulteManager.HandleVaulteMenuChoice();
                        break;

                    case "2":
                        MatchSensors();
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

        static void MatchSensors()
        {
            Console.WriteLine("\nEnter the agent name you want to investigate: ");
            string agentName = Console.ReadLine();
            Agent agent = Prison.Instance.GetAgentByName(agentName);
            if (agent == null)
            {
                Console.WriteLine("Agent not found!");
                return;
            }
            while (!agent.IsExposed)
            {
                SensorsVaulte.ShowAllSensors();
                Console.WriteLine("Choose the sensor you want to match:");
                string name = Console.ReadLine();
                ISensor sensor = SensorsVaulte.GetSensorByName(name);
                if (sensor == null)
                {
                    Console.WriteLine("Sensor not found!");
                    continue;
                }

                agent.TryMatchingSensor(sensor);
            }
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
