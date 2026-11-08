using System;

namespace AhoyBrowser
{
    class TimeStamp
    {
        public static string GetISOTimestamp()
        {
            try
            {
                switch (MainForm.TimeStampFormat)
                {
                    case 0:
                        return IO.GetIsoDateTimeNow(); // Full ISO timestamp
                    case 1:
                        return IO.FormatAsISODateMinutes(DateTime.Now); // Seconds omitted
                    case 2:
                        return IO.FormatAsISODate(DateTime.Now); // Date only
                    case 3:
                        return IO.FormatAsISOTime(DateTime.Now, true); // Time only with seconds
                    case 4:
                        return IO.GetISOTimeNow(); // Time only without seconds
                    case 5:
                        return IO.GetISODateTimeNowFlat(); // Timestamp stripped of all separators
                    default:
                        return IO.GetIsoDateTimeNow(); // Full ISO timestamp
                }
            }
            catch
            {
                return "N/A";
            }
        }

        public static string GetTimeStampFormat(int index)
        {
            switch (index)
            {
                case 0:
                    return "YYYY-MM-DD HH:MM:SS";
                case 1:
                    return "YYYY-MM-DD HH:MM";
                case 2:
                    return "YYYY-MM-DD";
                case 3:
                    return "HH:MM:SS";
                case 4:
                    return "HH:MM";
                case 5:
                    return "YYYYMMDDHHMMSS";
                default:
                    return "N/A";
            }
        }
    }
}
