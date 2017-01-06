using System;
using iRacingSdkWrapper;

namespace ZuppidashHost
{
    public class IRacingConnection
    {
        private iRacingSdkWrapper.SdkWrapper wrapper;
        private HostWindow hostWindow;
        private DataParser dataParser;
        private DisplayConnection displayConnection;

        public IRacingConnection(HostWindow window, DisplayConnection displayConnection)
        {
            this.hostWindow = window;
            this.displayConnection = displayConnection;

            wrapper = new SdkWrapper();           
            wrapper.TelemetryUpdated += OnTelemetryUpdated;
            wrapper.SessionInfoUpdated += OnSessionInfoUpdated;
            wrapper.Connected += OnWrapperConnected;
            wrapper.Disconnected += OnWrapperDisconnected;
        }

        public void Open(int updateFrequency)
        {
            wrapper.TelemetryUpdateFrequency = updateFrequency;
            wrapper.Start();
        }

        public void Close()
        {
            wrapper.Stop();
        }
       
        private void OnSessionInfoUpdated(object sender, SdkWrapper.SessionInfoUpdatedEventArgs e)
        {
            dataParser.ParseSession(e);           
        }

        private void OnTelemetryUpdated(object sender, SdkWrapper.TelemetryUpdatedEventArgs e)
        {
            
            float TCValue = 99;
            try
            {
                var tcVar = wrapper.GetTelemetryValue<float>("dcTractionControl");
                TCValue = tcVar.Value;
                if (TCValue < 0)
                {
                    TCValue = 99;
                }
            }
            catch{}

            float BBValue = 99;
            try
            {
                var bbVar = wrapper.GetTelemetryValue<float>("dcBrakeBias");
                BBValue = bbVar.Value;
                if (BBValue < 10)
                {
                    BBValue = 99;
                }
            }
            catch {}

            dataParser.ParseTelemetry(e, (int)TCValue, Math.Round(BBValue, 2));

            displayConnection.SendMessage(dataParser.GetDisplayString(), dataParser.GetLedAmount(), dataParser.GetDotBin());
        }

        private void OnWrapperConnected(object sender, EventArgs e)
        {
            hostWindow.IracingStarted();
            dataParser = new DataParser();
        }

        private void OnWrapperDisconnected(object sender, EventArgs e)
        {
            hostWindow.IracingStopped();
            dataParser = null;
        }
    }
}
