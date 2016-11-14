using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iRacingSdkWrapper;
using System.Globalization;

namespace ZuppidashHost
{
    public class SDKManager
    {
        private iRacingSdkWrapper.SdkWrapper wrapper;
        private HostWindow hostWindow;
        private DataParser dataParser;

        public SDKManager(HostWindow hostWindow, DataParser dataParser)
        {
            this.hostWindow = hostWindow;
            this.dataParser = dataParser;
            
            wrapper = new SdkWrapper();
            wrapper.TelemetryUpdateFrequency = 10;
            wrapper.TelemetryUpdated += OnTelemetryUpdated;
            wrapper.SessionInfoUpdated += OnSessionInfoUpdated;
            wrapper.Connected += OnWrapperConnected;
            wrapper.Disconnected += OnWrapperDisconnected;
        }

        public void StartSDK()
        {
            wrapper.Start();
        }

        public void StopSDK()
        {
            wrapper.Stop();
        }

        private void OnSessionInfoUpdated(object sender, SdkWrapper.SessionInfoUpdatedEventArgs e)
        {
            YamlQuery query = query = e.SessionInfo["DriverInfo"]["DriverCarIdx"];
            int DriverCarIdx = int.Parse(query.GetValue(), NumberStyles.Any, CultureInfo.InvariantCulture);
            query = e.SessionInfo["DriverInfo"]["Drivers"]["CarIdx", DriverCarIdx]["CarPath"];
            string carTag = query.GetValue();

            query = e.SessionInfo["DriverInfo"]["DriverCarSLFirstRPM"];
            int firstRPM = int.Parse(query.GetValue(), NumberStyles.Any, CultureInfo.InvariantCulture);

            int shiftPoint = hostWindow.GetCustomShift(carTag);
            if (shiftPoint > 0)
            {
                dataParser.SetRPMRange(firstRPM, shiftPoint);
            }
            else
            {
                query = e.SessionInfo["DriverInfo"]["DriverCarSLBlinkRPM"];
                int lastRPM = int.Parse(query.GetValue(), NumberStyles.Any, CultureInfo.InvariantCulture);
                hostWindow.SetCustomShift(carTag, lastRPM);
                dataParser.SetRPMRange(firstRPM, lastRPM);
            }
            
           
           
        }
        private void OnTelemetryUpdated(object sender, SdkWrapper.TelemetryUpdatedEventArgs e)
        {
            TelemetryData teleData = new TelemetryData(e.UpdateTime,
                                                       e.TelemetryInfo.Speed.Value,
                                                       e.TelemetryInfo.RPM.Value,
                                                       e.TelemetryInfo.Gear.Value,
                                                       e.TelemetryInfo.ShiftIndicatorPct.Value);
            hostWindow.TelemetryRecieved(teleData);
        }

        private void OnWrapperConnected(object sender, EventArgs e)
        {
            hostWindow.StopClock();
        }

        private void OnWrapperDisconnected(object sender, EventArgs e)
        {
            hostWindow.StartClock();
        }
    }
}
