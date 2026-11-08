using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AhoyBrowser
{
    class Notification
    {
        public const string NotificationDateTimeFormat = "yyyy-MM-dd HH:mm";
        private static int RepeatCode = 0;

        // Try to convert value to DateTime and check if it is past
        // current date time value
        private static DateTime tempDateTime;
        private static DateTime tempDateValue;
        public static bool IsDue(string dateTimeString, bool stripPrefix)
        {
            // Return if empty string
            if (string.IsNullOrEmpty(dateTimeString))
            {
                return false;
            }

            // Used to remove the prefix from repeated notification values
            if (stripPrefix)
            {
                dateTimeString = GetRepeatData(dateTimeString, 2);
            }

            if (tempDateTime == null)
            {
                tempDateTime = new DateTime();
            }

            try
            {
                if (DateTime.TryParse(dateTimeString, out tempDateValue))
                {
                    // Convert string to DateTime
                    tempDateTime = Convert.ToDateTime(dateTimeString);

                    // Check if DateTime is past or equal to system time
                    if (tempDateTime <= DateTime.Now)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 0 = return first char
        /// 1 = return repeat code (0 = All, 1 = Weekdays, 2 = Weekends)
        /// 2 = return date/time
        /// 3 = return time (HH:mm)
        /// 4 = return weekday and HH:mm
        /// 5 = return date
        /// </summary>
        private static string tempRepeatData;
        public static string GetRepeatData(string input, int options)
        {
            tempRepeatData = "";
            if (!ValidPrepeatData(input))
            {
                return string.Empty; // Important that it is empty (or a valid date time)
            }

            if (!string.IsNullOrEmpty(input))
            {
                try
                {
                    // Return first char
                    if (options == 0)
                    {
                        tempRepeatData = input.Split(';')[0]; // Return first char
                    }
                    else if (options == 1)
                    {
                        tempRepeatData = input.Split(';')[1]; // Return repeat code
                    }
                    else if (options == 2)
                    {
                        tempRepeatData = input.Split(';')[2]; // Return date/time
                    }
                    else if (options == 3)
                    {
                        tempRepeatData = input.Split(';')[2]; // Return date/time
                    }
                    else if (options == 4)
                    {
                        tempRepeatData = input.Split(';')[2];
                        tempRepeatData = tempRepeatData.Substring(tempRepeatData.Length - 8, 5); // Return HH:mm
                    }
                    else if (options == 5)
                    {
                        // Return weekday and time
                        tempRepeatData = input.Split(';')[2];
                        tempRepeatData = Convert.ToDateTime(tempRepeatData).DayOfWeek.ToString();
                        tempRepeatData += "s at " + GetRepeatData(input, 4);
                    }
                    else if (options == 6)
                    {
                        tempRepeatData = input.Split(';')[2];
                        tempRepeatData = tempRepeatData.Substring(0, 10); // Return Date
                    }
                }
                catch
                {
                    return string.Empty;
                }
            }
            return tempRepeatData;
        }


        // Test for pattern: D;999;2020-12-31 00:00:00
        public static bool ValidPrepeatData(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (Regex.IsMatch(input, @"^(\w);(\d+);(\d{4})-(\d{2})-(\d{2}) (\d{2}):(\d{2}):(\d{2})$"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        internal static DateTime GetRepeatDataAsDateTime(string input)
        {
            if (!string.IsNullOrEmpty(input) && ValidPrepeatData(input))
            {
                try
                {
                    return Convert.ToDateTime(GetRepeatData(input, 2));
                }
                catch
                {
                    return DateTime.Now;
                }
            }
            else
            {
                return DateTime.Now;
            }
        }

        internal static bool IsWeekly(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (GetRepeatData(input, 0) == "W")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        internal static DateTime SnapDateTimeToRepeatCode(DateTime input)
        {
            try
            {
                // Do nothing if 0
                if (RepeatCode == 0)
                {
                    return input;
                }
                else if (RepeatCode == 1) // Mon-Fri: Move date to closest match if current date is not matching
                {
                    if (MatchingMonFri(input))
                    {
                        return input;
                    }
                    else
                    {
                        // push here
                        do
                        {
                            input = input.AddDays(1);
                        } while (!MatchingMonFri(input));
                        return input;
                    }
                }
                else if (RepeatCode == 2) // Sat-Sun: Move date to closest match if current date is not matching
                {
                    if (MatchingSatSun(input))
                    {
                        return input;
                    }
                    else
                    {
                        // push here
                        do
                        {
                            input = input.AddDays(1);
                        } while (!MatchingSatSun(input));
                        return input;
                    }
                }
                else if (RepeatCode == 3) // Sat-Mon: Move date to closest match if current date is not matching
                {
                    if (MatchingSatMon(input))
                    {
                        return input;
                    }
                    else
                    {
                        // push here
                        do
                        {
                            input = input.AddDays(1);
                        } while (!MatchingSatMon(input));
                        return input;
                    }
                }
                else if (RepeatCode == 4) // Sat-Tue: Move date to closest match if current date is not matching
                {
                    if (MatchingSatTue(input))
                    {
                        return input;
                    }
                    else
                    {
                        // push here
                        do
                        {
                            input = input.AddDays(1);
                        } while (!MatchingSatTue(input));
                        return input;
                    }
                }
                else if (RepeatCode == 5) // Fri-Sun: Move date to closest match if current date is not matching
                {
                    if (MatchingFriSun(input))
                    {
                        return input;
                    }
                    else
                    {
                        // push here
                        do
                        {
                            input = input.AddDays(1);
                        } while (!MatchingFriSun(input));
                        return input;
                    }
                }
                else if (RepeatCode == 6) // Fri-Mon: Move date to closest match if current date is not matching
                {
                    if (MatchingFriMon(input))
                    {
                        return input;
                    }
                    else
                    {
                        // push here
                        do
                        {
                            input = input.AddDays(1);
                        } while (!MatchingFriMon(input));
                        return input;
                    }
                }
                else if (RepeatCode == 7) // Fri-Tue: Move date to closest match if current date is not matching
                {
                    if (MatchingFriTue(input))
                    {
                        return input;
                    }
                    else
                    {
                        // push here
                        do
                        {
                            input = input.AddDays(1);
                        } while (!MatchingFriTue(input));
                        return input;
                    }
                }
                else if (RepeatCode == 8) // Tue-Fri: Move date to closest match if current date is not matching
                {
                    if (MatchingTueFri(input))
                    {
                        return input;
                    }
                    else
                    {
                        // push here
                        do
                        {
                            input = input.AddDays(1);
                        } while (!MatchingTueFri(input));
                        return input;
                    }
                }
                else if (RepeatCode == 9) // Tue-Thu: Move date to closest match if current date is not matching
                {
                    if (MatchingTueThu(input))
                    {
                        return input;
                    }
                    else
                    {
                        // push here
                        do
                        {
                            input = input.AddDays(1);
                        } while (!MatchingTueThu(input));
                        return input;
                    }
                }
                else if (RepeatCode == 10) // Wed-Fri: Move date to closest match if current date is not matching
                {
                    if (MatchingWedFri(input))
                    {
                        return input;
                    }
                    else
                    {
                        // push here
                        do
                        {
                            input = input.AddDays(1);
                        } while (!MatchingWedFri(input));
                        return input;
                    }
                }
                else if (RepeatCode == 11) // Mon-Thu: Move date to closest match if current date is not matching
                {
                    if (MatchingMonThu(input))
                    {
                        return input;
                    }
                    else
                    {
                        // push here
                        do
                        {
                            input = input.AddDays(1);
                        } while (!MatchingMonThu(input));
                        return input;
                    }
                }
                else if (RepeatCode == 12) // Mon+Wed: Move date to closest match if current date is not matching
                {
                    if (MatchingMonOrWed(input))
                    {
                        return input;
                    }
                    else
                    {
                        // push here
                        do
                        {
                            input = input.AddDays(1);
                        } while (!MatchingMonOrWed(input));
                        return input;
                    }
                }
                else if (RepeatCode == 13) // Tue+Thu: Move date to closest match if current date is not matching
                {
                    if (MatchingTueOrThu(input))
                    {
                        return input;
                    }
                    else
                    {
                        // push here
                        do
                        {
                            input = input.AddDays(1);
                        } while (!MatchingTueOrThu(input));
                        return input;
                    }
                }
                else if (RepeatCode == 14) // Wed+Fri: Move date to closest match if current date is not matching
                {
                    if (MatchingWedOrFri(input))
                    {
                        return input;
                    }
                    else
                    {
                        // push here
                        do
                        {
                            input = input.AddDays(1);
                        } while (!MatchingWedOrFri(input));
                        return input;
                    }
                }
                else if (RepeatCode == 15) // Thu+Sat: Move date to closest match if current date is not matching
                {
                    if (MatchingThuOrSat(input))
                    {
                        return input;
                    }
                    else
                    {
                        // push here
                        do
                        {
                            input = input.AddDays(1);
                        } while (!MatchingThuOrSat(input));
                        return input;
                    }
                }
                else if (RepeatCode == 16) // Fri+Sun: Move date to closest match if current date is not matching
                {
                    if (MatchingFriOrSun(input))
                    {
                        return input;
                    }
                    else
                    {
                        // push here
                        do
                        {
                            input = input.AddDays(1);
                        } while (!MatchingFriOrSun(input));
                        return input;
                    }
                }
                else if (RepeatCode == 17) // Other 1st: Move date to closest match if current date is not matching
                {
                    if (MatchingOtherFirst(input))
                    {
                        return input;
                    }
                    else
                    {
                        // push here
                        do
                        {
                            input = input.AddDays(1);
                        } while (!MatchingOtherFirst(input));
                        return input;
                    }
                }
                else if (RepeatCode == 18) // Other 2nd: Move date to closest match if current date is not matching
                {
                    if (MatchingOtherSecond(input))
                    {
                        return input;
                    }
                    else
                    {
                        // push here
                        do
                        {
                            input = input.AddDays(1);
                        } while (!MatchingOtherSecond(input));
                        return input;
                    }
                }
            }
            catch
            {
                // Do nothing
            }

            return input;
        }

        private static DateTime tempPushData;
        public static string PushDays(string input, int repeatCode)
        {
            try
            {
                // Get datetime and push
                tempPushData = GetRepeatDataAsDateTime(input);

                // Push to match repeat code
                if (repeatCode == 1) // Mon-Fri
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (!MatchingMonFri(tempPushData));
                }
                else if (repeatCode == 2) // Sat-Sun
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (!MatchingSatSun(tempPushData));
                }
                else if (repeatCode == 3) // Sat-Mon
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (!MatchingSatMon(tempPushData));
                }
                else if (repeatCode == 4) // Sat-Tue
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (!MatchingSatTue(tempPushData));
                }
                else if (repeatCode == 5) // Fri-Sun
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (!MatchingFriSun(tempPushData));
                }
                else if (repeatCode == 6) // Fri-Mon
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (!MatchingFriMon(tempPushData));
                }
                else if (repeatCode == 7) // Fri-Tue
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (!MatchingFriTue(tempPushData));
                }
                else if (repeatCode == 8) // Tue-Fri
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (!MatchingTueFri(tempPushData));
                }
                else if (repeatCode == 9) // Tue-Thu
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (!MatchingTueThu(tempPushData));
                }
                else if (repeatCode == 10) // Wed-Fri
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (!MatchingWedFri(tempPushData));
                }
                else if (repeatCode == 11) // Mon-Thu
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (!MatchingMonThu(tempPushData));
                }
                else if (repeatCode == 12) // Mon+Wed
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (!MatchingMonOrWed(tempPushData));
                }
                else if (repeatCode == 13) // Tue+Thu
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (!MatchingTueOrThu(tempPushData));
                }
                else if (repeatCode == 14) // Wed+Fri
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (!MatchingWedOrFri(tempPushData));
                }
                else if (repeatCode == 15) // Thu+Sat
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (!MatchingThuOrSat(tempPushData));
                }
                else if (repeatCode == 16) // Fri+Sun
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (!MatchingFriOrSun(tempPushData));
                }
                else if (repeatCode == 17) // Other 1st
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (!MatchingOtherFirst(tempPushData));
                }
                else if (repeatCode == 18) // Other 2nd
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (!MatchingOtherSecond(tempPushData));
                }
                else // Else push forward to current day
                {
                    do
                    { // Prevent old dates by looping the push
                        tempPushData = tempPushData.AddDays(1);
                    } while (tempPushData <= DateTime.Now);
                }

                return GetRepeatData(tempPushData, false, repeatCode);
            }
            catch
            {
                return "";
            }
        }


        #region Repeat code rules

        /*  --- DayOfWeek ---
         Monday 1 	
         Tuesday 2 	
         Wednesday 3
         Thursday 4 	
         Friday 5 	
         Saturday 6 	
         Sunday 0 	
             */


        // ----------------------- Days in a row ----------------------- 
        private static bool MatchingMonFri(DateTime input)
        {
            if ((int)input.DayOfWeek >= 1 && (int)input.DayOfWeek <= 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool MatchingSatSun(DateTime input)
        {
            if ((int)input.DayOfWeek == 6 || (int)input.DayOfWeek == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool MatchingSatMon(DateTime input)
        {
            if ((int)input.DayOfWeek == 6 || (int)input.DayOfWeek == 0 || (int)input.DayOfWeek == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool MatchingSatTue(DateTime input)
        {
            if ((int)input.DayOfWeek == 6 || (int)input.DayOfWeek == 0 || (int)input.DayOfWeek == 1 || (int)input.DayOfWeek == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool MatchingFriSun(DateTime input)
        {
            if ((int)input.DayOfWeek == 5 || (int)input.DayOfWeek == 6 || (int)input.DayOfWeek == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool MatchingFriMon(DateTime input)
        {
            if ((int)input.DayOfWeek == 5 || (int)input.DayOfWeek == 6 || (int)input.DayOfWeek == 0 || (int)input.DayOfWeek == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool MatchingFriTue(DateTime input)
        {
            if ((int)input.DayOfWeek == 5 || (int)input.DayOfWeek == 6 || (int)input.DayOfWeek == 0 || (int)input.DayOfWeek == 1 || (int)input.DayOfWeek == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool MatchingTueFri(DateTime input)
        {
            if ((int)input.DayOfWeek >= 2 && (int)input.DayOfWeek <= 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool MatchingTueThu(DateTime input)
        {
            if ((int)input.DayOfWeek >= 2 && (int)input.DayOfWeek <= 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool MatchingWedFri(DateTime input)
        {
            if ((int)input.DayOfWeek >= 3 && (int)input.DayOfWeek <= 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool MatchingMonThu(DateTime input)
        {
            if ((int)input.DayOfWeek >= 1 && (int)input.DayOfWeek <= 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // ----------------------- Arbitrary days ----------------------- 
        private static bool MatchingMonOrWed(DateTime input)
        {
            if ((int)input.DayOfWeek == 1 || (int)input.DayOfWeek == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool MatchingTueOrThu(DateTime input)
        {
            if ((int)input.DayOfWeek == 2 || (int)input.DayOfWeek == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool MatchingWedOrFri(DateTime input)
        {
            if ((int)input.DayOfWeek == 3 || (int)input.DayOfWeek == 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool MatchingThuOrSat(DateTime input)
        {
            if ((int)input.DayOfWeek == 4 || (int)input.DayOfWeek == 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool MatchingFriOrSun(DateTime input)
        {
            if ((int)input.DayOfWeek == 5 || (int)input.DayOfWeek == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool MatchingOtherFirst(DateTime input) // Mon, Wed, Fri, Sun (every other day)
        {
            if ((int)input.DayOfWeek == 1 || (int)input.DayOfWeek == 3 || (int)input.DayOfWeek == 5 || (int)input.DayOfWeek == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool MatchingOtherSecond(DateTime input) // Tue, Thu, Sat (every other day 2nd)
        {
            if ((int)input.DayOfWeek == 2 || (int)input.DayOfWeek == 4 || (int)input.DayOfWeek == 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*  --- DayOfWeek ---
         Monday 1 	
         Tuesday 2 	
         Wednesday 3
         Thursday 4 	
         Friday 5 	
         Saturday 6 	
         Sunday 0 	
             */
        private const int RepeatCodeMax = 18; // 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18
        public static string GetRepeatName()
        {
            switch (RepeatCode)
            {
                case 0:
                    return "Every day";
                case 1:
                    return "Mon-Fri";
                case 2:
                    return "Sat-Sun";
                case 3:
                    return "Sat-Mon";
                case 4:
                    return "Sat-Tue";
                case 5:
                    return "Fri-Sun"; 
                case 6:
                    return "Fri-Mon";
                case 7:
                    return "Fri-Tue";
                case 8:
                    return "Tue-Fri";
                case 9:
                    return "Tue-Thu";
                case 10:
                    return "Wed-Fri";
                case 11:
                    return "Mon-Thu";
                case 12:
                    return "Mon+Wed";
                case 13:
                    return "Tue+Thu"; 
                case 14:
                    return "Wed+Fri";
                case 15:
                    return "Thu+Sat";
                case 16:
                    return "Fri+Sun";
                case 17:
                    return "Other 1st";
                case 18:
                    return "Other 2nd";
                default:
                    return "N/A";
            }
        }

        // ■□■■■
        public static string GetRepeatGraphics()
        {
            switch (RepeatCode)
            {
                case 0:
                    return "Mon □□□□□□□ Sun";
                case 1:
                    return "Mon ■■■■■□□ Sun";
                case 2:
                    return "Mon □□□□□■■ Sun";
                case 3:
                    return "Mon ■□□□□■■ Sun";
                case 4:
                    return "Mon ■■□□□■■ Sun";
                case 5:
                    return "Mon □□□□■■■ Sun";
                case 6:
                    return "Mon ■□□□■■■ Sun";
                case 7:
                    return "Mon ■■□□■■■ Sun";
                case 8:
                    return "Mon □■■■■□□ Sun";
                case 9:
                    return "Mon □■■■□□□ Sun";
                case 10:
                    return "Mon □□■■■□□ Sun";
                case 11:
                    return "Mon ■■■■□□□ Sun";
                case 12:
                    return "Mon ■□■□□□□ Sun";
                case 13:
                    return "Mon □■□■□□□ Sun";
                case 14:
                    return "Mon □□■□■□□ Sun";
                case 15:
                    return "Mon □□□■□■□ Sun";
                case 16:
                    return "Mon □□□□■□■ Sun";
                case 17:
                    return "Mon ■□■□■□■ Sun";
                case 18:
                    return "Mon □■□■□■□ Sun";
                default:
                    return "N/A";
            }
        }

        public static string GetWeekGraphics(int weekday)
        {
            switch (weekday)
            {
                case 0:
                    return "Mon □□□□□□■ Sun";
                case 1:
                    return "Mon ■□□□□□□ Sun";
                case 2:
                    return "Mon □■□□□□□ Sun";
                case 3:
                    return "Mon □□■□□□□ Sun";
                case 4:
                    return "Mon □□□■□□□ Sun";
                case 5:
                    return "Mon □□□□■□□ Sun";
                case 6:
                    return "Mon □□□□□■□ Sun";
                default:
                    return "N/A";
            }
        }

        #endregion




        public static string PushWeeks(string input, int repeatCode)
        {
            // This method ignores the repeat code but passes it as value

            try
            {
                // Get datetime and push
                tempPushData = GetRepeatDataAsDateTime(input);
                do
                { // Prevent old dates by looping the push
                    tempPushData = tempPushData.AddDays(7);
                } while (tempPushData <= DateTime.Now);

                return GetRepeatData(tempPushData, true, repeatCode);
            }
            catch
            {
                return input;
            }
        }

        internal static string GetRepeatData(DateTime input, bool isWeekly, int repeatCode)
        {
            try
            {
                if (isWeekly)
                {
                    return "W;" + repeatCode + ";" + IO.FormatAsISODateTime(input); // Weekly
                }
                else
                {
                    return "D;" + repeatCode + ";" + IO.FormatAsISODateTime(input); // Daily
                }
            }
            catch
            {
                return "";
            }
        }


        public static void NextRepeatCode()
        {
            if (RepeatCode < RepeatCodeMax)
            {
                RepeatCode += 1;
            }
            else
            {
                RepeatCode = 0;
            }
        }

        public static void ResetRepeatCode()
        {
            RepeatCode = 0;
        }

        public static int GetRepeatCode()
        {
            return RepeatCode;
        }

        public static void SetRepeatCode(string repeatData)
        {
            if (ValidPrepeatData(repeatData))
            {
                RepeatCode = Convert.ToInt32(GetRepeatData(repeatData, 1));
            }
            else
            {
                RepeatCode = 0;
            }
        }




        public static Entry CloneForNotificationSorting(Entry entryToBeCloned)
        {
            // Initialize new entry
            Entry newEntry = new Entry();
            // Entry variables
            newEntry.ID = entryToBeCloned.ID;
            newEntry.Title = entryToBeCloned.Title;
            newEntry.Content = entryToBeCloned.Content;
            newEntry.Created = entryToBeCloned.Created;
            newEntry.Changed = entryToBeCloned.Changed;

            // If no notification timestamp is present, move the repeat timestamp into the
            // notification timestamp to allow sorting of the objects
            if (string.IsNullOrEmpty(entryToBeCloned.NotificationTimeStamp))
            {
                newEntry.NotificationTimeStamp = GetRepeatData(entryToBeCloned.NotificationRepeat, 2);
            }
            else
            {
                newEntry.NotificationTimeStamp = entryToBeCloned.NotificationTimeStamp;
            }
            newEntry.NotificationRepeat = entryToBeCloned.NotificationRepeat;
            newEntry.NotificationText = entryToBeCloned.NotificationText;
            newEntry.NotificationCaretPosition = entryToBeCloned.NotificationCaretPosition;
            newEntry.Bookmark = entryToBeCloned.Bookmark;
            newEntry.Pin = entryToBeCloned.Pin;
            newEntry.Rank = entryToBeCloned.Rank;
            newEntry.PresetColor = entryToBeCloned.PresetColor;
            newEntry.CustomColor = entryToBeCloned.CustomColor;
            newEntry.Protected = entryToBeCloned.Protected;
            newEntry.CaretPosition = entryToBeCloned.CaretPosition;
            // Temp variables
            newEntry.IgnoreTextChanged = false;
            newEntry.IsNewEntry = false;
            newEntry.HasChanges = false;
            newEntry.WasDeleted = false;
            newEntry.IsDummyEntry = false;
            newEntry.TempHistoryState = new List<HistoryState>();
            newEntry.MarkExport = false;
            if (entryToBeCloned.TempHistoryState != null && entryToBeCloned.TempHistoryState.Count > 0)
            {
                newEntry.TempHistoryState.AddRange(entryToBeCloned.TempHistoryState);
            }
            return newEntry;
        }


    }



    class IsoWeekNumber
    {
        internal static int GetISO8601WeekNumber(DateTime date)
        {
            try
            {
                // Calculates the ISO 8601 Weeknumber
                // In this scenario the first day of the week is monday, 
                // and the week rule states that:
                // [...] the first calendar week of a year is the one 
                // that includes the first Thursday of that year and 
                // [...] the last calendar week of a calendar year is 
                // the week immediately preceding the first 
                // calendar week of the next year.
                // The first week of the year may thus start in the 
                // preceding year

                const int JAN = 1;
                const int DEC = 12;
                const int LASTDAYOFDEC = 31;
                const int FIRSTDAYOFJAN = 1;
                const int THURSDAY = 4;
                bool ThursdayFlag = false;

                // Get the day number since the beginning of the year
                int DayOfYear = date.DayOfYear;

                // Get the numeric weekday of the first day of the 
                // year (using sunday as FirstDay)
                int StartWeekDayOfYear =
                     (int)(new DateTime(date.Year, JAN, FIRSTDAYOFJAN)).DayOfWeek;
                int EndWeekDayOfYear =
                     (int)(new DateTime(date.Year, DEC, LASTDAYOFDEC)).DayOfWeek;

                // Compensate for the fact that we are using monday
                // as the first day of the week
                if (StartWeekDayOfYear == 0)
                    StartWeekDayOfYear = 7;
                if (EndWeekDayOfYear == 0)
                    EndWeekDayOfYear = 7;

                // Calculate the number of days in the first and last week
                int DaysInFirstWeek = 8 - (StartWeekDayOfYear);
                //int DaysInLastWeek = 8 - (EndWeekDayOfYear);

                // If the year either starts or ends on a thursday it will have a 53rd week
                if (StartWeekDayOfYear == THURSDAY || EndWeekDayOfYear == THURSDAY)
                    ThursdayFlag = true;

                // We begin by calculating the number of FULL weeks between the start of the year and
                // our date. The number is rounded up, so the smallest possible value is 0.
                int FullWeeks = (int)Math.Ceiling((DayOfYear - (DaysInFirstWeek)) / 7.0);

                int WeekNumber = FullWeeks;

                // If the first week of the year has at least four days, then the actual week number for our date
                // can be incremented by one.
                if (DaysInFirstWeek >= THURSDAY)
                    WeekNumber = WeekNumber + 1;

                // If week number is larger than week 52 (and the year doesn't either start or end on a thursday)
                // then the correct week number is 1. 
                if (WeekNumber > 52 && !ThursdayFlag)
                    WeekNumber = 1;

                // If week number is still 0, it means that we are trying to evaluate the week number for a
                // week that belongs in the previous year (since that week has 3 days or less in our date's year).
                // We therefore make a recursive call using the last day of the previous year.
                if (WeekNumber == 0)
                    WeekNumber = GetISO8601WeekNumber(
                         new DateTime(date.Year - 1, DEC, LASTDAYOFDEC));
                return WeekNumber;
            }
            catch
            {
                return 0;
            }
        }


    }
}
