using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    internal interface IAgent
    {
        string Name { get; }
        /// <summary>
        /// Agents rank 2 - 5, Where 5 is the highest rank and 2 is the lowest.
        /// </summary>
        int Rank { get; }
        ISensor[] SensitiveSensors { get; } 
        ISensor[] SensorsSlots { get; } 

    }
}
