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

            dataParser = new DataParser();
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
            dataParser.ParseTelemetry(e);
            
            displayConnection.SendMessage(dataParser.GetDisplayString(), dataParser.GetLedAmount(), dataParser.GetDotBin());
        }

        private void OnWrapperConnected(object sender, EventArgs e)
        {
            hostWindow.IracingStarted();
        }

        private void OnWrapperDisconnected(object sender, EventArgs e)
        {
            hostWindow.IracingStopped();
        }
    }
}
