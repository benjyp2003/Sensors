using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    internal class FootSoldier : Agent
    {

        public FootSoldier(string name) 
        {
            Name = name;
            Rank = Sensors.Rank.FootSoldier;
            SensitiveSensors = SensorsVaulte.GetRandomSensors(2).ToArray();
            SensitiveSensors = new ISensor[2];
        }

        

    }
}
