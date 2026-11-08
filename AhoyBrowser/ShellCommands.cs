using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace AhoyBrowser
{
    class EnvironmentPath
    {
        public string Signature { get; set; }
        public Environment.SpecialFolder Path { get; set; }

        public EnvironmentPath(string signature, Environment.SpecialFolder path)
        {
            Signature = signature;
            Path = path;
        }
    }

    class ShellCommands
    {
        // Programs and Snap-ins:
        private static List<string> ShellCommandsList { get; set; }
        // Environment paths:
        private static List<EnvironmentPath> ShellEnvironemtPathsList { get; set; }

        // Initialized from MainForm upon load
        public static void InitializeShellCommandsList()
        {
            ShellCommandsList = new List<string>();
            ShellEnvironemtPathsList = new List<EnvironmentPath>();
            // Add all commands listed in the "DisplayTextShellTools.txt"
            // Just add commands here to make them work in Ahoy command line
            ShellCommandsList.Add("regedit");
            ShellCommandsList.Add("perfmon");
            ShellCommandsList.Add("eventvwr");
            ShellCommandsList.Add("services.msc");
            ShellCommandsList.Add("lusrmgr.msc");
            ShellCommandsList.Add("cmd");
            ShellCommandsList.Add("telnet");
            ShellCommandsList.Add("winver");
            ShellCommandsList.Add("powershell");
            ShellCommandsList.Add("msconfig");
            ShellCommandsList.Add("msinfo32");
            ShellCommandsList.Add("mmc");
            ShellCommandsList.Add("tasklist");
            ShellCommandsList.Add("charmap");
            ShellCommandsList.Add("control");
            ShellCommandsList.Add("dxdiag");
            ShellCommandsList.Add("mstsc");
            ShellCommandsList.Add("ncpa.cpl");
            ShellCommandsList.Add("sqlwb");
            ShellCommandsList.Add("ssmsee.exe");
            ShellCommandsList.Add("devenv");
            ShellCommandsList.Add("devenv /nosplash");
            ShellCommandsList.Add("explorer");
            ShellCommandsList.Add("msedge");
            ShellCommandsList.Add("firefox");
            ShellCommandsList.Add("chrome");
            ShellCommandsList.Add("winword");
            ShellCommandsList.Add("wmplayer");
            ShellCommandsList.Add("mspaint");
            ShellCommandsList.Add("notepad++.exe");
            ShellCommandsList.Add("notepad++");
            ShellCommandsList.Add("notepad");
            ShellCommandsList.Add("photoshop");
            ShellCommandsList.Add("bridge");
            ShellCommandsList.Add("slack");
            ShellCommandsList.Add("slack.exe");
            ShellCommandsList.Add(@"A:\");
            ShellCommandsList.Add(@"B:\");
            ShellCommandsList.Add(@"C:\");
            ShellCommandsList.Add(@"D:\");
            ShellCommandsList.Add(@"E:\");
            ShellCommandsList.Add(@"F:\");
            ShellCommandsList.Add(@"G:\");
            ShellCommandsList.Add(@"H:\");
            ShellCommandsList.Add(@"I:\");
            ShellCommandsList.Add(@"J:\");
            ShellCommandsList.Add(@"K:\");
            ShellCommandsList.Add(@"L:\");
            ShellCommandsList.Add(@"M:\");
            // Add more commands here...

            ShellEnvironemtPathsList.Add(new EnvironmentPath("documents", Environment.SpecialFolder.MyDocuments));
            ShellEnvironemtPathsList.Add(new EnvironmentPath("doc", Environment.SpecialFolder.MyDocuments));
            ShellEnvironemtPathsList.Add(new EnvironmentPath("desktop", Environment.SpecialFolder.Desktop));
            ShellEnvironemtPathsList.Add(new EnvironmentPath("fonts", Environment.SpecialFolder.Fonts));
            ShellEnvironemtPathsList.Add(new EnvironmentPath("music", Environment.SpecialFolder.MyMusic));
            ShellEnvironemtPathsList.Add(new EnvironmentPath("video", Environment.SpecialFolder.MyVideos));
            ShellEnvironemtPathsList.Add(new EnvironmentPath("pictures", Environment.SpecialFolder.MyPictures));
            ShellEnvironemtPathsList.Add(new EnvironmentPath("img", Environment.SpecialFolder.MyPictures));
            ShellEnvironemtPathsList.Add(new EnvironmentPath("admin", Environment.SpecialFolder.CommonAdminTools));
            ShellEnvironemtPathsList.Add(new EnvironmentPath("appdata", Environment.SpecialFolder.ApplicationData));
            ShellEnvironemtPathsList.Add(new EnvironmentPath("home", Environment.SpecialFolder.UserProfile));
            ShellEnvironemtPathsList.Add(new EnvironmentPath("%HOMEPATH%", Environment.SpecialFolder.UserProfile));
            ShellEnvironemtPathsList.Add(new EnvironmentPath("%PROGRAMFILES%", Environment.SpecialFolder.ProgramFiles));
            ShellEnvironemtPathsList.Add(new EnvironmentPath("%APPDATA%", Environment.SpecialFolder.ApplicationData));
            ShellEnvironemtPathsList.Add(new EnvironmentPath("%WINDIR%", Environment.SpecialFolder.Windows));
            // Add more paths here...

        }

        public static void BrowseInternet(string url, int browser)
        {
            try
            {
                // Browser selection
                if (browser == 0)
                {
                    Log.ProcessStart("Browser " + browser.ToString() + ": ("+ url + ")");
                    Process.Start(url, "");
                }
                else if (browser == 1)
                {
                    Log.ProcessStart("Browser " + browser.ToString() + ": (" + url + ")");
                    Process.Start("firefox", url);
                }
                else if (browser == 2)
                {
                    Log.ProcessStart("Browser " + browser.ToString() + ": (" + url + ")");
                    Process.Start("chrome", url);
                }
                else if (browser == 3)
                {
                    Log.ProcessStart("Browser " + browser.ToString() + ": (" + url + ")");
                    Process.Start("microsoft-edge://", url);
                }
                else if (browser == 4)
                {
                    Log.ProcessStart("Browser " + browser.ToString() + ": (" + url + ")");
                    Process.Start("chrome", "-incognito" + " " + url);
                }
            }
            catch (Exception ex)
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(ex.Message, MessageText.ShellCommandErrorCaption(), false);
                msg.ShowDialog();
            }
        }

        public static bool StartProcess(string input, string args)
        {
            // Execute shell commands
            if (IsShellCommand(input))
            {
                try
                {
                    Log.ProcessStart(input + " " + args);
                    Process.Start(input, args);
                }
                catch (Exception ex)
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(ex.Message, MessageText.ShellCommandErrorCaption(), false);
                    msg.ShowDialog();
                }
                return true;
            } // Open invironement paths
            else if (IsShellEnvironmentPath(input))
            {
                try
                {
                    Log.ProcessStart(Environment.GetFolderPath(GetPathFromSignature(input)));
                    Process.Start(Environment.GetFolderPath(GetPathFromSignature(input)));
                }
                catch (Exception ex)
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(ex.Message, MessageText.EnvironmentPathErrorCaption(), false);
                    msg.ShowDialog();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool tempCommandTestResult;
        public static bool IsShellCommand(string s)
        {
            s = s.ToLower() ;
            tempCommandTestResult = false;
            if (ShellCommandsList != null && ShellCommandsList.Count > 0)
            {
                foreach (string command in ShellCommandsList)
                {
                    if (s.Contains(command.ToLower()))
                    {
                        tempCommandTestResult = true;
                        break;
                    }
                }
            }
            return tempCommandTestResult;
        }

        public static bool IsShellEnvironmentPath(string s)
        {
            s = s.ToLower();
            tempCommandTestResult = false;
            if (ShellEnvironemtPathsList != null && ShellEnvironemtPathsList.Count > 0)
            {
                foreach (EnvironmentPath path in ShellEnvironemtPathsList)
                {
                    if (s == path.Signature.ToLower())
                    {
                        tempCommandTestResult = true;
                        break;
                    }
                }
            }
            return tempCommandTestResult;
        }

        private static Environment.SpecialFolder tempSpecialFolder;
        private static Environment.SpecialFolder GetPathFromSignature(string signature)
        {
            // Default is desktop
            tempSpecialFolder = Environment.SpecialFolder.Desktop;

            if (ShellEnvironemtPathsList != null && ShellEnvironemtPathsList.Count > 0)
            {
                foreach (EnvironmentPath path in ShellEnvironemtPathsList)
                {
                    if (signature == path.Signature.ToLower())
                    {
                        tempSpecialFolder = path.Path;
                        break;
                    }
                }
            }

            return tempSpecialFolder;
        }

        private static List<string> tempStringSectionsList;
        private static string tempStringSections;
        public static string GetStringSection(string s, int splitIndex1or2)
        {
            if (tempStringSectionsList == null)
            {
                tempStringSectionsList = new List<string>();
            }
            tempStringSections = "";
            tempStringSectionsList.Clear();

            if (s.Length > 0)
            {
                tempStringSectionsList.AddRange(s.Split(' '));

                // Get first text part
                if (splitIndex1or2 == 1)
                {
                    tempStringSections = tempStringSectionsList[0];
                }
                else if (splitIndex1or2 == 2 && tempStringSectionsList.Count > 1)
                {
                    for (int i = 1; i < tempStringSectionsList.Count; i++)
                    {
                        tempStringSections += tempStringSectionsList[i] + " ";
                    }
                    tempStringSections = tempStringSections.Trim();
                }
                else
                {
                    tempStringSections = "";
                }
            }

            return tempStringSections;
        }


    }
}
