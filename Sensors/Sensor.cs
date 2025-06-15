using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    internal class Sensor
    {
        public string Name {  get; }
        string Action { get; }  
        public Sensor(string name, string action)
        {
            Name = name;
            Action = action;
        }
    }
}
