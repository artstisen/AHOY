using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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
        private static List<EnvironmentPath> ShellEnvironmentPathsList { get; set; }

        // Initialized from MainForm upon load
        public static void InitializeShellCommandsList()
        {
            ShellCommandsList = new List<string>();
            ShellEnvironmentPathsList = new List<EnvironmentPath>();
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

            ShellEnvironmentPathsList.Add(new EnvironmentPath("documents", Environment.SpecialFolder.MyDocuments));
            ShellEnvironmentPathsList.Add(new EnvironmentPath("doc", Environment.SpecialFolder.MyDocuments));
            ShellEnvironmentPathsList.Add(new EnvironmentPath("desktop", Environment.SpecialFolder.Desktop));
            ShellEnvironmentPathsList.Add(new EnvironmentPath("fonts", Environment.SpecialFolder.Fonts));
            ShellEnvironmentPathsList.Add(new EnvironmentPath("music", Environment.SpecialFolder.MyMusic));
            ShellEnvironmentPathsList.Add(new EnvironmentPath("video", Environment.SpecialFolder.MyVideos));
            ShellEnvironmentPathsList.Add(new EnvironmentPath("pictures", Environment.SpecialFolder.MyPictures));
            ShellEnvironmentPathsList.Add(new EnvironmentPath("img", Environment.SpecialFolder.MyPictures));
            ShellEnvironmentPathsList.Add(new EnvironmentPath("admin", Environment.SpecialFolder.CommonAdminTools));
            ShellEnvironmentPathsList.Add(new EnvironmentPath("appdata", Environment.SpecialFolder.ApplicationData));
            ShellEnvironmentPathsList.Add(new EnvironmentPath("home", Environment.SpecialFolder.UserProfile));
            ShellEnvironmentPathsList.Add(new EnvironmentPath("%HOMEPATH%", Environment.SpecialFolder.UserProfile));
            ShellEnvironmentPathsList.Add(new EnvironmentPath("%PROGRAMFILES%", Environment.SpecialFolder.ProgramFiles));
            ShellEnvironmentPathsList.Add(new EnvironmentPath("%APPDATA%", Environment.SpecialFolder.ApplicationData));
            ShellEnvironmentPathsList.Add(new EnvironmentPath("%WINDIR%", Environment.SpecialFolder.Windows));
            // Add more paths here...

        }
        /// <summary>
        /// Open a program, document or folder through the Windows shell (.NET Core and later
        /// default to UseShellExecute = false, which cannot open folders, URLs or App Paths programs).
        /// </summary>
        internal static void ShellOpen(string fileName, string arguments = "")
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments ?? "",
                UseShellExecute = true
            })?.Dispose();
        }

        /// <summary>Open a folder in Explorer. Returns false (with a message) if it can't be opened.</summary>
        internal static bool OpenFolder(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                {
                    throw new DirectoryNotFoundException("The folder does not exist:\n" + path);
                }
                ShellOpen(path);
                return true;
            }
            catch (Exception ex)
            {
                AhoySimpleMessageBox msg = new AhoySimpleMessageBox(ex.Message, MessageText.EnvironmentPathErrorCaption(), false);
                msg.ShowDialog();
                return false;
            }
        }

        /// <summary>
        /// Only web addresses may be opened from links in notes; anything else (file://, programs,
        /// UNC paths ...) could run code when handed to the shell.
        /// </summary>
        internal static bool TryGetWebUrl(string link, out string url)
        {
            url = null;
            if (string.IsNullOrWhiteSpace(link))
            {
                return false;
            }
            string candidate = link.Trim();
            if (candidate.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
            {
                candidate = "https://" + candidate;
            }
            if (Uri.TryCreate(candidate, UriKind.Absolute, out Uri uri) &&
                (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            {
                url = uri.AbsoluteUri;
                return true;
            }
            return false;
        }

        public static void BrowseInternet(string link, int browser)
        {
            if (!TryGetWebUrl(link, out string url))
            {
                AhoySimpleMessageBox notWeb = new AhoySimpleMessageBox("Only web links (http/https) can be opened from AHOY!:\n" + link, MessageText.ShellCommandErrorCaption(), false);
                notWeb.ShowDialog();
                return;
            }

            try
            {
                Log.ProcessStart("Browser " + browser.ToString(System.Globalization.CultureInfo.InvariantCulture));
                switch (browser)
                {
                    case 1:
                        ShellOpen("firefox", "\"" + url + "\"");
                        break;
                    case 2:
                        ShellOpen("chrome", "\"" + url + "\"");
                        break;
                    case 3:
                        ShellOpen("msedge", "\"" + url + "\"");
                        break;
                    case 4:
                        ShellOpen("chrome", "--incognito \"" + url + "\"");
                        break;
                    default:
                        ShellOpen(url); // Windows default browser
                        break;
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
            // Known programs and snap-ins, or a folder to open
            if (IsShellCommand(input))
            {
                try
                {
                    Log.ProcessStart(input);
                    if (Directory.Exists(input))
                    {
                        ShellOpen(input);
                    }
                    else
                    {
                        ShellOpen(input, args);
                    }
                }
                catch (Exception ex)
                {
                    AhoySimpleMessageBox msg = new AhoySimpleMessageBox(ex.Message, MessageText.ShellCommandErrorCaption(), false);
                    msg.ShowDialog();
                }
                return true;
            } // Open environment paths
            else if (IsShellEnvironmentPath(input))
            {
                string path = Environment.GetFolderPath(GetPathFromSignature(input));
                Log.ProcessStart(path);
                OpenFolder(path);
                return true;
            }
            else
            {
                return false;
            }
        }
            /// <summary>
        /// True if the command (or its first word) is one of the known commands in ShellCommandsList,
        /// or an existing folder. Arbitrary program paths are not run.
        /// </summary>
        public static bool IsShellCommand(string s)
        {
            if (string.IsNullOrWhiteSpace(s) || ShellCommandsList == null)
            {
                return false;
            }
            string command = s.Trim();
            string firstWord = command.Split(' ')[0];
            foreach (string known in ShellCommandsList)
            {
                if (string.Equals(command, known, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(firstWord, known, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return Directory.Exists(firstWord) && Path.IsPathRooted(firstWord);
        }
        public static bool IsShellEnvironmentPath(string s)
        {
            if (string.IsNullOrWhiteSpace(s) || ShellEnvironmentPathsList == null)
            {
                return false;
            }
            foreach (EnvironmentPath path in ShellEnvironmentPathsList)
            {
                if (string.Equals(s.Trim(), path.Signature, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
        private static Environment.SpecialFolder GetPathFromSignature(string signature)
        {
            foreach (EnvironmentPath path in ShellEnvironmentPathsList)
            {
                if (string.Equals(signature.Trim(), path.Signature, StringComparison.OrdinalIgnoreCase))
                {
                    return path.Path;
                }
            }
            return Environment.SpecialFolder.Desktop; // Default
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
