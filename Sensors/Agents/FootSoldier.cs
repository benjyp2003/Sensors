using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    internal class FootSoldier : Agent
    {
        protected override ISensor[] SensitiveSensors { get; set; } 
        protected override ISensor[] SensorSlots { get; set; }

        public FootSoldier(string name) : base()
        {
            Name = name;
            Rank = Sensors.Rank.FootSoldier;
            SensitiveSensors = SensorsVaulte.GetRandomSensors(2).ToArray();
            SensorSlots = new ISensor[2];
        }

        

    }
}
