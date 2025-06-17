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
        List<Agent> _allAgents;

        Prison()
        {
            _allAgents = new List<Agent>();
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

        /// <summary>
        /// Adds an agent to the agents list.
        /// </summary>
        /// <param name="agent"></param>
        public void AddAgentToList(Agent agent)
        {
            _allAgents.Add(agent);
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
                if (_allAgents == null || _allAgents.Count == 0)
                {
                    Console.WriteLine("Error: Agents list is empty or null. \n");
                    return new List<Agent>();
                }

                Random random = new Random();
                List<Agent> newAgents = new List<Agent>();

                for (int i = 0; i < count; i++)
                {
                    newAgents.Add(_allAgents[random.Next(_allAgents.Count)]);
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
            foreach (Agent agent in _allAgents)
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
        public void ShowAllAgents()
        {
            Console.WriteLine("The available Agents are: ");
            foreach (var agent in _allAgents)
            {
                Console.WriteLine($"'{agent.Name}' of type {agent.GetType().Name} with rank {agent.Rank}");
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
            for (int i = 0; i < _allAgents.Count; i++)
            {
                typesOfAgents.Add(_allAgents[i].GetType());
            }

            return typesOfAgents;
        }

        /// <summary>
        /// Deletes a given agent from the list.
        /// </summary>
        /// <param name="agent"></param>
        public void DeleteAgent(Agent agent)
        {
            if (_allAgents.Remove(agent))
            {
                Console.WriteLine($"{agent.Name} was removed successfully");
            }
            else
            {
                Console.WriteLine($"{agent.Name} not found in list.");
            }
        }

        public void ClearList()
        {
            _allAgents.Clear();
        }
    }
}