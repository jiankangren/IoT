using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSensorApp.Models.Sensehat
{
    public class Compass : BaseSensehatSensor
    {
        public override string Name => "Compass";

        public override void GetValue()
        {
            const double halfCircle = Math.PI;
            const double fullCircle = Math.PI * 2;

            _sensehat.Sensors.ImuSensor.Update();

            if (_sensehat.Sensors.Pose.HasValue)
            {
                double northAngle = _sensehat.Sensors.Pose.Value.Z;
                if (northAngle < 0)
                {
                    northAngle += fullCircle;
                }

                northAngle = fullCircle - northAngle;
                double southAngle = northAngle + halfCircle;

                SensorValueUpdated(northAngle);

                //Point northPoint = GetPixelCoordinate(northAngle);
                //Point southPoint = GetPixelCoordinate(southAngle);

                //SenseHat.Display.Clear();
                //SenseHat.Display.Screen[(int)northPoint.X, (int)northPoint.Y] = northColor;
                //SenseHat.Display.Screen[(int)southPoint.X, (int)southPoint.Y] = southColor;
                //SenseHat.Display.Screen[3, 3] = centerColor;
                //SenseHat.Display.Screen[4, 3] = centerColor;
                //SenseHat.Display.Screen[3, 4] = centerColor;
                //SenseHat.Display.Screen[4, 4] = centerColor;
                //SenseHat.Display.Update();

                //if ((SetScreenText != null) && nextMainPageUpdate <= DateTime.Now)
                //{
                //    SetScreenText($"{northAngle / fullCircle * 360:0}");
                //    nextMainPageUpdate = DateTime.Now.Add(mainPageUpdateRate);
                //}
            }
        }
    }
}
