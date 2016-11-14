using System.Linq;
using iRacingSdkWrapper;
using System.Globalization;
using System.Xml.Linq;

namespace ZuppidashHost
{
    public class DataParser
    {
        private TelemetryData previousTelemetry;
        private TelemetryData currentTelemetry;
        private SessionData sessionData;

        private XDocument shiftPoints;

        private readonly string shiftPointFile = "shiftpoints.xml";
        private int displayMode;
        private double modeTimer;
        private int startRPM;
        private int endRPM;
        private int RPMInterval;
        private string carTag;
        private int driverID;
        private double prevLapFuel;

        private int dotBin;
        private bool disableLeds;

        public DataParser()
        {
            previousTelemetry = null;
            currentTelemetry = null;
            sessionData = null;
            displayMode = 0;
            modeTimer = 0;
            startRPM = 0;
            endRPM = 0;
            RPMInterval = 0;
            dotBin = 0;
            carTag = "";
            disableLeds = false;
            prevLapFuel = 0;
            LoadXML();
        }

        private void LoadXML()
        {
            try
            {
                shiftPoints = XDocument.Load(shiftPointFile);
            }
            catch
            {
                new XDocument(new XElement("shiftpoints", "")).Save(shiftPointFile);
                shiftPoints = XDocument.Load(shiftPointFile);
            }
        }

        public void SetRPMRange(int firstLed, int lastLed)
        {
            this.startRPM = firstLed;
            this.endRPM = lastLed;
            int RPMRange = endRPM - startRPM;
            RPMInterval = RPMRange / 9; 
        }

        public void SetDriverData(int driverID, string carTag)
        {
            this.driverID = driverID;
            this.carTag = carTag;
        }

        public void ParseTelemetry(SdkWrapper.TelemetryUpdatedEventArgs e)
        {
            currentTelemetry = new TelemetryData(e);
        }

        public void ParseSession(SdkWrapper.SessionInfoUpdatedEventArgs e)
        {
            YamlQuery query = e.SessionInfo["DriverInfo"]["DriverCarIdx"];
            driverID = int.Parse(query.GetValue(), NumberStyles.Any, CultureInfo.InvariantCulture);
            query = e.SessionInfo["DriverInfo"]["Drivers"]["CarIdx", driverID]["CarPath"];
            carTag = query.GetValue();

            sessionData = new SessionData(e);

            if (!GetLedPoints())
            {
                query = e.SessionInfo["DriverInfo"]["DriverCarSLFirstRPM"];
                startRPM = int.Parse(query.GetValue(), NumberStyles.Any, CultureInfo.InvariantCulture);
                query = e.SessionInfo["DriverInfo"]["DriverCarSLBlinkRPM"];
                endRPM = int.Parse(query.GetValue(), NumberStyles.Any, CultureInfo.InvariantCulture);
                SetLedPoints();
            }

            RPMInterval = ( endRPM - startRPM) / 9;
        }

        public string GetDisplayString()
        {
            string displayString = GenerateDisplayString();
            previousTelemetry = currentTelemetry;
            return displayString;
        }

        private bool GetLedPoints()
        {
            foreach (XElement element in shiftPoints.Descendants("car"))
            {
                if (element.Attribute("name").Value.Equals(carTag))
                {
                    startRPM = int.Parse(element.Descendants("startRPM").FirstOrDefault().Value);
                    endRPM = int.Parse(element.Descendants("endRPM").FirstOrDefault().Value);
                    return true;
                }
            }
            return false;
        }

        private void SetLedPoints()
        {
            XElement carElement = new XElement("car", new XAttribute("name", carTag));
            carElement.Add(new XElement("startRPM", startRPM.ToString()));
            carElement.Add(new XElement("endRPM", endRPM.ToString()));
            shiftPoints.Descendants("shiftpoints").FirstOrDefault().Add(carElement);
            shiftPoints.Save(shiftPointFile);
        }


        private string GenerateDisplayString()
        {
            dotBin = 0;
            if (sessionData == null)
            {
                return "        ";
            }

            if (!currentTelemetry.InCar())
            {
                disableLeds = true;
                dotBin = 40;
                return currentTelemetry.getTimeLeftString();
            }

            if (previousTelemetry == null)
            {
                return "        ";
            }

            if (displayMode == 1)
            {
                if (currentTelemetry.GetUpdateTime() - modeTimer > 3)
                {
                    displayMode = 0;
                    modeTimer = 0;
                    return GetDefaultString();
                }
                else
                {
                    return (previousTelemetry.GetFuel() - currentTelemetry.GetFuel()).ToString() + "L";
                }
            }
            else if (displayMode == 2)
            {
                if (currentTelemetry.GetUpdateTime() - modeTimer > 1)
                {
                    displayMode = 0;
                    modeTimer = 0;
                    return GetDefaultString();
                }
                else
                {
                    return GetGearString();
                }
            }

            if (!previousTelemetry.InCar() && currentTelemetry.InCar())
            {
                disableLeds = false;
            }

            //if (!previousTelemetry.GetLap().Equals(currentTelemetry.GetLap())){
            //    displayMode = 1;
            //    modeTimer = currentTelemetry.GetUpdateTime();
            //    return (previousTelemetry.GetFuel()-currentTelemetry.GetFuel()).ToString()+"L";
            //}
            if (!previousTelemetry.GetGear().Equals(currentTelemetry.GetGear()))
            {
                displayMode = 2;
                modeTimer = currentTelemetry.GetUpdateTime();
                return GetGearString();
            }

            else
            {
                return GetDefaultString();
            }
        }

        private string GetDefaultString()
        {
            return currentTelemetry.GetRPM().ToString().PadLeft(4) + currentTelemetry.GetSpeedKMH().ToString().PadLeft(4);
        }

        private string GetGearString()
        {
            return "  " + currentTelemetry.GetGear() + " " + currentTelemetry.GetSpeedKMH().ToString().PadLeft(4);
        }

        public int GetLedAmount()
        {
            if (currentTelemetry == null || disableLeds)
            {
                return 0;
            }

            int RPM = currentTelemetry.GetRPM();

            int ledState = 0;       
            
            //if (currentTelemetry.PitLimiterOn() != previousTelemetry.PitLimiterOn())
            //{
            //    ledState = 99;
            //} 
            if ( RPM >= endRPM){
                ledState = 9;
            }
            else if (RPM < startRPM)
            {
                ledState = 0;
            }
            else
            {
                ledState = (RPM - startRPM) / RPMInterval;
            }

            return ledState;
        }

        public int GetDotBin()
        {
            return dotBin;
        }


    }
}
