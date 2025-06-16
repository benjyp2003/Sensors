using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    internal interface ISensor
    {
        string Name { get; }
        void Activate();

        void AddToVaulte();
    }
}
