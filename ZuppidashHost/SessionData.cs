using iRacingSdkWrapper;

namespace ZuppidashHost
{
    class SessionData
    {
        private double trackTemp;
        private string weather;
        private int SOF;

        public SessionData(SdkWrapper.SessionInfoUpdatedEventArgs e)
        {
            YamlQuery query = e.SessionInfo["DriverInfo"]["DriverCarIdx"];
            query = e.SessionInfo["WeekendInfo"]["TrackSkies"];
            weather = query.GetValue();

            //query = e.SessionInfo["WeekendInfo"]["TrackSurfaceTemp"];
            //trackTemp = double.Parse(query.GetValue().Split(' ')[0], CultureInfo.InvariantCulture);
        }

        public string getWeatherString()
        {
            switch (weather)
            {
                case "Clear":
                    return "C";
                case "Partly Cloudy":
                    return "PC";
                case "Mostly Cloudy":
                    return "nC";
                case "Overcast":
                    return "OC";
                default:
                    return "";
            }
        }

        //public string getTempString()
        //{
        //    return Math.Round(trackTemp).ToString();
        //}
    }
}
