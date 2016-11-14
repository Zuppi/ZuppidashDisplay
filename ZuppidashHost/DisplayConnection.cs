using System;
using System.IO.Ports;


namespace ZuppidashHost
{
    public class DisplayConnection
    {
        private HostWindow hostWindow;
        private SerialPort serialPort;

        public DisplayConnection(HostWindow window)
        {
            this.hostWindow = window;
        }

        ~DisplayConnection()
        {
            ClearDisplay();
            serialPort.Close();
        }

        public bool Open(string comPort, int baudRate)
        {
            try
            {
                serialPort = new SerialPort(comPort, baudRate);
                serialPort.Open();
                
            }
            catch (Exception e)
            {
                hostWindow.SetText("Error: " + e.Message);
                return false;
            }
            finally
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    hostWindow.SetText("Connected to Arduino on: " + serialPort.PortName);
                    ClearDisplay();
                }           
            }

            return true;
        }

        public void Close()
        {
            ClearDisplay();
            if (serialPort != null)
            {
                serialPort.Close();
            }
           
        }

        public bool Connected()
        {
            if (serialPort != null)
            {
                return serialPort.IsOpen;
            }
            else
            {
                return false;
            }          
        }

        public void SendMessage(string message, int ledAmount=0, int dotBin=0)
        {
            if (Connected())
            {
                serialPort.WriteLine(message+"|"+ledAmount.ToString()+"|"+dotBin.ToString());
            }
        }

        public void ClearDisplay()
        {
            SendMessage("CLEAR");
        }
    }
}
