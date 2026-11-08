using System;
using System.Collections.Generic;

namespace AhoyBrowser
{
    class AnsiCalendar
    {
        private static List<string> MonthList;
        private static List<string> WeekdayList;
        private static string Output { get; set; }
        private static int DayOfWeek = 0;
        public static string GetCalendar(DateTime customDate, bool singleLine)
        {
            Output = "";
            if (MonthList == null || WeekdayList == null)
            {
                MonthList = new List<string>();
                WeekdayList = new List<string>();
                MonthList.Add("Jan");
                MonthList.Add("Feb");
                MonthList.Add("Mar");
                MonthList.Add("Apr");
                MonthList.Add("May");
                MonthList.Add("Jun");
                MonthList.Add("Jul");
                MonthList.Add("Aug");
                MonthList.Add("Sep");
                MonthList.Add("Oct");
                MonthList.Add("Nov");
                MonthList.Add("Dec");

                WeekdayList.Add("Mon");
                WeekdayList.Add("Tue");
                WeekdayList.Add("Wed");
                WeekdayList.Add("Thu");
                WeekdayList.Add("Fri");
                WeekdayList.Add("Sat");
                WeekdayList.Add("Sun");
            }

            try
            {
                if (singleLine)
                {
                    // Build month names
                    for (int i = 0; i < MonthList.Count; i++)
                    {
                        Output += MonthList[i];

                        if (i < MonthList.Count - 1)
                        {
                            Output += " ";
                        }
                    }

                    // Add space
                    Output += TextTransform.GetBlankSpace(4);


                    // Build week names
                    for (int k = 0; k < WeekdayList.Count; k++)
                    {
                        Output += WeekdayList[k];

                        if (k < WeekdayList.Count - 1)
                        {
                            Output += " ";
                        }
                    }

                    // New line ------------------ INDICATORS STARTS HERE ----------------------
                    Output += "\n";


                    // Set month indicator
                    for (int j = 0; j < MonthList.Count; j++)
                    {
                        if (j == customDate.Month - 1) // The month component, expressed as a value between 1 and 12
                        {
                            //Output += TextTransform.GetRepeatedCharacter(3, "▀");
                            Output += "'" + customDate.Year.ToString().Substring(2, 2); // Take the last to chars of 2020 = 20
                        }
                        else
                        {
                            Output += TextTransform.GetBlankSpace(3);
                        }

                        // Only add blank space if not at the end of the year
                        if (j < MonthList.Count - 1)
                        {
                            Output += " ";
                        }
                    }

                    // Add space
                    Output += TextTransform.GetBlankSpace(4);

                    DayOfWeek = (int)customDate.DayOfWeek;
                    if (DayOfWeek == 0)
                    {
                        DayOfWeek = 6;
                    }
                    else
                    {
                        DayOfWeek -= 1;
                    }

                    // Set week name indicator
                    for (int l = 0; l < WeekdayList.Count; l++)
                    {
                        if (l == DayOfWeek)
                        {
                            //Output += TextTransform.GetRepeatedCharacter(3, "▀");
                            // Set date
                            Output += " " + customDate.Day.ToString("00");
                        }
                        else
                        {
                            Output += TextTransform.GetBlankSpace(3);
                        }

                        // Only add blank space if not at the end of the week
                        if (l < WeekdayList.Count - 1)
                        {
                            Output += " ";
                        }
                    }

                    
                }
                else
                {

                    // Build month names
                    for (int i = 0; i < MonthList.Count; i++)
                    {
                        Output += MonthList[i];

                        if (i < MonthList.Count - 1)
                        {
                            Output += " ";
                        }
                    }

                    Output += "\n";

                    // Set month indicator
                    for (int j = 0; j < MonthList.Count; j++)
                    {
                        if (j == customDate.Month - 1) // The month component, expressed as a value between 1 and 12
                        {
                            Output += TextTransform.GetRepeatedCharacter(3, "▀");
                        }
                        else
                        {
                            Output += TextTransform.GetBlankSpace(3);
                        }

                        // Only add blank space if not at the end of the year
                        if (j < MonthList.Count - 1)
                        {
                            Output += " ";
                        }
                    }

                    Output += "\n";

                    // Build week names
                    for (int k = 0; k < WeekdayList.Count; k++)
                    {
                        Output += WeekdayList[k];

                        if (k < WeekdayList.Count - 1)
                        {
                            Output += " ";
                        }
                    }

                    Output += "\n";

                    DayOfWeek = (int)customDate.DayOfWeek;
                    if (DayOfWeek == 0)
                    {
                        DayOfWeek = 6;
                    }
                    else
                    {
                        DayOfWeek -= 1;
                    }

                    // Set week name indicator
                    for (int l = 0; l < WeekdayList.Count; l++)
                    {
                        if (l == DayOfWeek)
                        {
                            Output += TextTransform.GetRepeatedCharacter(3, "▀");
                        }
                        else
                        {
                            Output += TextTransform.GetBlankSpace(3);
                        }

                        // Only add blank space if not at the end of the week
                        if (l < WeekdayList.Count - 1)
                        {
                            Output += " ";
                        }
                    }

                    Output += "\n";

                    // Set date
                    for (int l = 0; l < WeekdayList.Count; l++)
                    {
                        if (l == DayOfWeek)
                        {
                            Output += customDate.Day.ToString("00") + " ";
                        }
                        else
                        {
                            Output += TextTransform.GetRepeatedCharacter(3, ".");
                        }

                        // Only add blank space if not at the end of the week
                        if (l < WeekdayList.Count - 1)
                        {
                            Output += " ";
                        }
                    }
                }



                /*
                Jan Feb Mar Apr May Jun Jul Aug Sep Oct Nov Dec
                            ▀▀▀
                Mon Tue Wed Thu Fri Sat Sun
                    ▀▀▀

                 */


                /*
                2020 Jan Feb Mar Apr May Jun Jul Aug Sep Oct Nov Dec
                            ▀▀▀
                07 Mon Tue Wed Thu Fri Sat Sun
                    ▀▀▀

                 */

                return Output;
            }
            catch
            {
                return "N/A";
            }
        }

    }
}
