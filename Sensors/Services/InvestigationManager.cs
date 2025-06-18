using Agents.Models;
using Sensors.Agents;
using Sensors.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sensors
{
    internal class InvestigationManager : SensorsVaulte
    {
        public static void Run()
        {
            // Initialize some sensors
            AudioSensor audioSensor1 = new AudioSensor("Sony Microphone");
            PulseSensor PulseSensor1 = new PulseSensor("Medtronic Heart monitor");
            MotionSensor motionSensor1 = new MotionSensor("Canon Movment Sensor");
            ThermalSensor thermalSensor1 = new ThermalSensor("Home made Heat Sensor");
            LightSensor lightSensor1 = new LightSensor("AMS Light Sensor");
            SignalSensor signalSensor1 = new SignalSensor("Hp Radio Sensor");
            MagneticSensor magneticSensor1 = new MagneticSensor("Magnetic Sensor");
            

            // Initialize some agents
            FootSoldier footAgent1 = new FootSoldier("Yosuf", "Iran");
            SquadLeader squadLeader1 = new SquadLeader("Ahmed", "Hamas");
            SeniorCommander seniorCommander1 = new SeniorCommander("Abdul", "Hizzballa");
            OrganizationLeader organizationLeader1 = new OrganizationLeader("Muhamad", "Hamas");

            HandleMenuChoice();
        }

        static void HandleMenuChoice()
        {
            while (true)
            {
                Console.Clear();
                Menu.ShowMenu();
                Console.WriteLine("Enter your choice: ");
                string choice = Console.ReadLine().Trim();
                switch (choice)
                {
                    case "1":
                        MatchSensors();
                        break;

                    case "2":
                        SensorVaulteManager.HandleVaulteMenuChoice();
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
            Agent agent;
            while (true)
            {

                Prison.Instance.ShowAllNotExposedAgents();
                Console.WriteLine("Enter the agent name you want to investigate: ");
                string agentName = Console.ReadLine().Trim();
                agent = Prison.Instance.GetAgentByName(agentName);

                if (agent == null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Agent not found!");
                    Console.ResetColor();
                    return;
                }

                if (CheckIfNotAllFootSoldiersExposed(agent))
                {
                    Console.ForegroundColor= ConsoleColor.DarkRed;
                    Console.WriteLine($"\nYou cant investigate a high rank agent before you exposed the Foot Soldiers! \n");
                    Console.ResetColor();
                }
                else
                { break; }
            }

            // Runs Until you matched all the sensores and the agent is exposed.
            while (!agent.IsExposed)
            {
                ShowAllSensors();
                Console.Write("Choose the sensor to match (enter number): ");
                var input = Console.ReadLine().Trim();

                if (!int.TryParse(input, out int choice) ||
                    choice < 1 || choice > GetAllSensors().Count)
                {
                    Console.WriteLine("Invalid choice – please enter a valid number.\n");
                    continue;
                }

                ISensor sensor = GetAllSensors()[choice - 1];
                agent.TryMatchingSensor(sensor);
            }
            Prison.Instance.AddAgentToExposedList(agent);

        }


        /// <summary>
        /// Returns true if the agent is a high rank , and there are still foot soldiers to expose.
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        static bool CheckIfNotAllFootSoldiersExposed(Agent agent)
        {
            Type type = agent.GetType();
            return (type.Name != "FootSoldier" && Prison.Instance.GetAllAgentTypes().Any(t => t.Name != "FootSoldier"));
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
