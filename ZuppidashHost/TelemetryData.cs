using System;
using iRacingSdkWrapper;

namespace ZuppidashHost
{
    public class TelemetryData
    {
        private double updateTime;
        private float speed;
        private int RPM;
        private int gear;
        private bool pitLimiterOn;
        private bool inCar;
        private float trackTemp;
        private double timeLeft;
        private int lap;
        private double fuelLeft;
        
        public TelemetryData(SdkWrapper.TelemetryUpdatedEventArgs e)
        {
            this.updateTime = e.UpdateTime;
            this.speed = e.TelemetryInfo.Speed.Value;
            this.RPM = (int)e.TelemetryInfo.RPM.Value;                                            
            this.gear = e.TelemetryInfo.Gear.Value;
            this.inCar = e.TelemetryInfo.IsOnTrack.Value;
            this.trackTemp = e.TelemetryInfo.TrackTemp.Value;
            this.timeLeft = e.TelemetryInfo.SessionTimeRemain.Value;
            this.lap = e.TelemetryInfo.Lap.Value;
            this.fuelLeft = e.TelemetryInfo.FuelLevel.Value;

            if (e.TelemetryInfo.EngineWarnings.GetValue().ToString().Equals("PitSpeedLimiter"))
            {
                pitLimiterOn = true;
            }
            else
            {
                pitLimiterOn = false;
            }
        }

        public double GetUpdateTime()
        {
            return updateTime;
        }

        public int GetSpeedKMH()
        {
            double test = Math.Round(speed * 3.6);
            return (int)test;
        }

        public int GetRPM()
        {
            return RPM;
        }

        public string GetGear()
        {
            switch (gear)
            {
                case -1:
                    return "R";
                case 0:
                    return "N";
                default:
                    return gear.ToString();
            }
        }

        public bool PitLimiterOn()
        {
            return pitLimiterOn;
        }

        public int GetLap()
        {
            return lap;
        }

        public double GetFuel()
        {
            return Math.Round(fuelLeft, 2);
        }

        public bool InCar()
        {
            return inCar;
        }

        public string getTempString()
        {
            return Math.Round(trackTemp).ToString();
        }

        public string getTimeLeftString()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(timeLeft);
            return " "+timeSpan.ToString("hhmmss")+" ";
            //if (timeSpan.Hours > 0)
            //{
            //    return timeSpan.ToString("hhmm");
            //}
            //else
            //{
            //    return timeSpan.ToString("mmss");
            //}
        }

        //public string GetDefaultString()
        //{
        //    return GetRPM().ToString().PadLeft(4) + GetSpeedKMH().ToString().PadLeft(4);
        //}

        //public string GetGearString()
        //{
        //    return "  " + GetGear() + " " + GetSpeedKMH().ToString().PadLeft(4);
        //}
    }
}
