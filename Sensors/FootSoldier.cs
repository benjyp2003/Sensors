using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    internal class FootSoldier : IAgent
    {
        public string Name { get; }

        public int Rank {  get; }

        public ISensor[] SensitiveSensors { get; }

        public ISensor[] SensorsSlots {  get; }

        public FootSoldier(string name)
        {
            Name = name;
            Rank = 2;
        }


    }
}
