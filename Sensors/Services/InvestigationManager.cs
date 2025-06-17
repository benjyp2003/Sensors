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
            FootSoldier footAgent2 = new FootSoldier("Natzer", "Hamas");
            SquadLeader squadLeader1 = new SquadLeader("Ahmed", "Hamas");
            SeniorCommander seniorCommander1 = new SeniorCommander("Abdul", "Hizzballa");

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
            Prison.Instance.ShowAllAgents();
            Console.WriteLine("Enter the agent name you want to investigate: ");
            string agentName = Console.ReadLine();
            Agent agent = Prison.Instance.GetAgentByName(agentName);
            if (agent == null)
            {
                Console.WriteLine("Agent not found!");
                return;
            }
            while (!agent.IsExposed)
            {
                ShowAllSensors();
                Console.WriteLine("Choose the sensor you want to match: (Enter the sensors name)");
                string name = Console.ReadLine();
                Console.WriteLine();
                ISensor sensor = GetSensorByName(name);
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
