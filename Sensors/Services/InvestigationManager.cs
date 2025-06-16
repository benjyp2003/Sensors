using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    internal class InvestigationManager
    {
        static SensorsVaulte sensorsVaulte = SensorsVaulte.GetInstance();
        public void TryMatchingSensors()
        {
            ISensor soundSensor = new AudioSensor("Sony Microphone");
            sensorsVaulte.AddSensorToList(soundSensor);
            sensorsVaulte.AddSensorToList(soundSensor);


        }
    }
}
