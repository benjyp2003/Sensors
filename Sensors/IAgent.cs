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
        string Rank { get; }
        ISensor[] SensitiveSensors { get; } 
        ISensor[] SensorsSlots { get; } 

    }
}
