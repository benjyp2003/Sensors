using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    /// <summary>
    /// A Singleton class that contains all the available agents,
    /// and functions that returns wanted agents.
    /// </summary>
    internal class Prison 
    {
        static Prison _instance;
        List<Agent> _allNonExposedAgents;
        List<Agent> _exposedAgents;

        Prison()
        {
            _allNonExposedAgents = new List<Agent>();
            _exposedAgents = new List<Agent>();
        }

        public static Prison Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Prison();
                }
                return _instance;
            }
        }

       
        public void AddAgentToList(Agent agent)
        {
            _allNonExposedAgents.Add(agent);
        }

        public void AddAgentToExposedList(Agent agent)
        {
            DeleteAgentFromList(agent);
            _exposedAgents.Add(agent);
        }


        /// <summary>
        /// Gets a given amount of random agents from the agents list.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Agent> GetRandomAgents(int count)
        {
            try
            {
                if (_allNonExposedAgents == null || _allNonExposedAgents.Count == 0)
                {
                    Console.WriteLine("Error: Agents list is empty or null. \n");
                    return new List<Agent>();
                }

                Random random = new Random();
                List<Agent> newAgents = new List<Agent>();

                for (int i = 0; i < count; i++)
                {
                    newAgents.Add(_allNonExposedAgents[random.Next(_allNonExposedAgents.Count)]);
                }

                return newAgents;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e} in Prison.GetRandomAgents");
                return new List<Agent>();
            }
        }

        /// <summary>
        /// Gets an agent by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Agent GetAgentByName(string name)
        {
            foreach (Agent agent in _allNonExposedAgents)
            {
                if (agent.Name == name)
                {
                    return agent;
                }
            }
            return null;
        }

        /// <summary>
        /// Shows all the available agents.
        /// </summary>
        public void ShowAllNotExposedAgents()
        {
            Console.WriteLine("\nThe available Agents for investigation are: ");
            foreach (var agent in _allNonExposedAgents)
            {
                Console.WriteLine($"- Aent '{agent.Name}' ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Returns all of the types of agents in the agents list.
        /// </summary>
        /// <returns></returns>
        public HashSet<Type> GetAllAgentTypes()
        {
            HashSet<Type> typesOfAgents = new HashSet<Type>();
            for (int i = 0; i < _allNonExposedAgents.Count; i++)
            {
                typesOfAgents.Add(_allNonExposedAgents[i].GetType());
            }

            return typesOfAgents;
        }

        /// <summary>
        /// Deletes a given agent from the list.
        /// </summary>
        /// <param name="agent"></param>
        public void DeleteAgentFromList(Agent agent)
        {
            if (_allNonExposedAgents.Remove(agent))
            {
                Console.WriteLine($"{agent.Name} was removed successfully from the Non exposed list. \n");
            }
            else
            {
                Console.WriteLine($"{agent.Name} not found in list. \n");
            }
        }

        public void ClearList()
        {
            _allNonExposedAgents.Clear();
        }
    }
}