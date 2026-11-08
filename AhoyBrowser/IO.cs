using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using AhoyBrowser.Crypto;

namespace AhoyBrowser
{
    class IO
    {
        // Default rank of 99999 to avoid having an index inside the pinned list
        internal const int DefaultRank = 99999;
        // Default status (pinned entry color)
        internal const int DefaultStatus = 0;
        internal const bool DefaultBoolean = false;
        internal static bool UseSecondaryPool { get; set; }
        internal static bool UseDemoPool { get; set; }
        private static bool UseCustomFilePath { get; set; }
        private static string CustomFilePath { get; set; }
        private static string CustomFileName { get; set; }
        internal static string CurrentSessionId { get; set; }
        internal static string PoolSessionId { get; set; }
        internal static string PoolSessionChanged { get; set; }

        #region ISO 8601 Timestamp
        // Get culture invariant datetime string
        /// yyyy-MM-dd HH:mm:ss
        private static string _tempDateTimeIso;
        public static string GetIsoDateTimeNow()
        {
            _tempDateTimeIso = "";
            _tempDateTimeIso += DateTime.Now.Year.ToString();
            _tempDateTimeIso += "-";
            _tempDateTimeIso += DateTime.Now.Month.ToString("00");
            _tempDateTimeIso += "-";
            _tempDateTimeIso += DateTime.Now.Day.ToString("00");
            _tempDateTimeIso += " ";
            _tempDateTimeIso += DateTime.Now.Hour.ToString("00");
            _tempDateTimeIso += ":";
            _tempDateTimeIso += DateTime.Now.Minute.ToString("00");
            _tempDateTimeIso += ":";
            _tempDateTimeIso += DateTime.Now.Second.ToString("00");
            return _tempDateTimeIso;
        }

        public static string GetISODateTimeNowFlat()
        {
            _tempDateTimeIso = "";
            _tempDateTimeIso += DateTime.Now.Year.ToString();
            _tempDateTimeIso += DateTime.Now.Month.ToString("00");
            _tempDateTimeIso += DateTime.Now.Day.ToString("00");
            _tempDateTimeIso += DateTime.Now.Hour.ToString("00");
            _tempDateTimeIso += DateTime.Now.Minute.ToString("00");
            _tempDateTimeIso += DateTime.Now.Second.ToString("00");
            return _tempDateTimeIso;
        }

        /// HH:mm
        public static string GetISOTimeNow()
        {
            _tempDateTimeIso = "";
            _tempDateTimeIso += DateTime.Now.Hour.ToString("00");
            _tempDateTimeIso += ":";
            _tempDateTimeIso += DateTime.Now.Minute.ToString("00");
            return _tempDateTimeIso;
        }
        public static string FormatAsISODateMinutes(DateTime dt)
        {
            _tempDateTimeIso = "";
            _tempDateTimeIso += dt.Year.ToString();
            _tempDateTimeIso += "-";
            _tempDateTimeIso += dt.Month.ToString("00");
            _tempDateTimeIso += "-";
            _tempDateTimeIso += dt.Day.ToString("00");
            _tempDateTimeIso += " ";
            _tempDateTimeIso += dt.Hour.ToString("00");
            _tempDateTimeIso += ":";
            _tempDateTimeIso += dt.Minute.ToString("00");
            return _tempDateTimeIso;
        }

        public static string FormatAsISODate(DateTime dt)
        {
            _tempDateTimeIso = "";
            _tempDateTimeIso += dt.Year.ToString();
            _tempDateTimeIso += "-";
            _tempDateTimeIso += dt.Month.ToString("00");
            _tempDateTimeIso += "-";
            _tempDateTimeIso += dt.Day.ToString("00");
            return _tempDateTimeIso;
        }

        public static string FormatAsISOTime(DateTime dt, bool seconds)
        {
            _tempDateTimeIso = "";
            _tempDateTimeIso += dt.Hour.ToString("00");
            _tempDateTimeIso += ":";
            _tempDateTimeIso += dt.Minute.ToString("00");
            if (seconds)
            {
                _tempDateTimeIso += ":";
                _tempDateTimeIso += dt.Second.ToString("00");
            }
            return _tempDateTimeIso;
        }

        public static string EnforceSemicolons(string input)
        {
            _tempDateTimeIso = input;
            if (!string.IsNullOrEmpty(input) && input.Length > 0)
            {
                if (input.Contains("."))
                {
                    input = input.Replace(".", ":");
                    _tempDateTimeIso = input;
                }
            }
            return _tempDateTimeIso;
        }

        // Only accepts yyyy-MM-dd HH:mm:ss
        internal const string ISODateTimeRegex = @"^(\d{4})-(\d{2})-(\d{2}) (\d{2}):(\d{2}):(\d{2})$";
        private static bool IsValidISODateTimeFormat(string input)
        {
            return Regex.IsMatch(input, ISODateTimeRegex);
        }
        #endregion


        #region FILE LOCATION
        // Data path
        internal static string DataPathMessage { get; set; }
        internal const string ProgramFolderName = "Ahoy";
        internal const string SecondaryPoolFolderName = "Sub";
        internal const string DemoPoolFolderName = "Demo";
        internal const string PoolFileExtension = ".ahoy";
        internal const string PoolFileName = "Pool" + PoolFileExtension;
        internal const string InitFileName = "Init.xml";
        internal const string SettingsFileName = "Settings.xml";
        internal const string BackupFileName = "Backup" + PoolFileExtension;
        internal const string BackupFolderName = "Backup";
        // Define path composites
        internal const string PrimaryFolderPath = "\\" + ProgramFolderName;
        internal const string SecondaryFolderPath = "\\" + ProgramFolderName + "\\" + SecondaryPoolFolderName;
        internal const string DemoFolderPath = "\\" + ProgramFolderName + "\\" + DemoPoolFolderName;
        internal const string DemoSecondaryFolderPath = "\\" + ProgramFolderName + "\\" + DemoPoolFolderName + "\\" + SecondaryPoolFolderName;
        internal const string BackupFolderPath = "\\" + BackupFolderName;

        /// <summary>
        /// Root folder for Ahoy's data (normally %APPDATA%). Can be redirected, e.g. by tests,
        /// so nothing touches the user's real pool.
        /// </summary>
        internal static string DataRootOverride { get; set; }
        internal static string ApplicationDataRoot()
        {
            return string.IsNullOrEmpty(DataRootOverride) ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) : DataRootOverride;
        }

        internal static bool CheckDataPath()
        {
            try
            {
                string tempPath = GetDataPath();
                DataPathMessage = "";

                // Check the current datapath
                if (!Directory.Exists(tempPath))
                {
                    DataPathMessage = "Directory was created: " + tempPath;
                    Directory.CreateDirectory(tempPath);
                    // Create backup directory
                    CheckBackupDataPath(tempPath);
                }
                else
                {
                    DataPathMessage = "Directory exists: " + tempPath;
                }
                return true;
            }
            catch
            {
                Log.LogFilePathError();
                DataPathMessage = "Data path check failed.";
                return false;
            }
        }

        internal static void CheckBackupDataPath(string rootDirectory)
        {
            try
            {
                // Create backup directory from root directory
                if (!string.IsNullOrEmpty(rootDirectory) && !Directory.Exists(rootDirectory + BackupFolderPath))
                {
                    Directory.CreateDirectory(rootDirectory + BackupFolderPath);
                }
            }
            catch
            {
                Log.LogFilePathError();
            }
        }

        internal static bool DoesPoolExist()
        {
            if (File.Exists(GetDataPathAndFileName()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static bool ValidateCustomDirectoryPath(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    return false;
                }
                if (path.Length > 0 && Directory.Exists(path.Substring(0, path.LastIndexOf('\\'))))
                {

                    return true;

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

        internal static bool SetAndValidateCustomFilePath(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    return false;
                }

                // Set path and filename
                CustomFilePath = path.Substring(0, path.LastIndexOf('\\'));
                CustomFileName = path.Substring(path.LastIndexOf('\\'), path.Length - (path.Substring(0, path.LastIndexOf('\\')).Length));
                CustomFileName = CustomFileName.Replace("\\", "");
                // Validate path
                if (CustomFilePath.Length > 0 && Directory.Exists(CustomFilePath) && File.Exists(CustomFilePath + "\\" + CustomFileName))
                {
                    // OBS: This log entry will never appear because old Log is discarded earlier.
                    //Log.LogPoolStatus("Pool: FileName \"" + CustomFileName + "\""); 

                    // Set custom filename usage:
                    UseCustomFilePath = true;
                    // Save to init file
                    SaveInitFile(CustomFilePath + "\\" + CustomFileName);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                Log.LogErrorLoadPoolFromFilePath();
                return false;
            }
        }

        internal static void ClearCustomFilePath(bool saveInit)
        {
            UseCustomFilePath = false;
            CustomFilePath = "";
            CustomFileName = "";
            if (saveInit)
            {
                SaveInitFile("");
            }
        }

        internal static string GetCurrentPoolFileNameForDisplay(bool ommitDefault)
        {
            // If custom
            if (UsingCustomFilePath())
            {
                return GetCustomFileNameStripped();
            }
            else
            {// Else return
                if (UseSecondaryPool)
                {
                    return "Secondary Pool";
                }
                else
                {
                    if (ommitDefault)
                    {
                        return "";
                    }
                    else
                    {
                        return "Default Pool";
                    }
                }
            }
        }

        internal static string GetCurrentPoolFileName()
        {
            // If custom
            if (UsingCustomFilePath())
            {
                return GetCustomFileName();
            }
            else
            {// Else return
                return PoolFileName;
            }
        }

        internal static string GetCustomFileName()
        {
            return CustomFileName;
        }

        internal static string GetCustomFilePathAndName()
        {
            if (!string.IsNullOrEmpty(CustomFilePath) && !string.IsNullOrEmpty(CustomFileName))
            {
                return CustomFilePath + "\\" + CustomFileName;
            }
            else
            {
                return "";
            }
        }


        internal static string GetCustomFileNameStripped()
        {
            if (CustomFileName.Contains(PoolFileExtension))
            {
                return CustomFileName.Replace(PoolFileExtension, "");
            }
            else
            {
                return CustomFileName;
            }
        }

        internal static bool UsingCustomFilePath()
        {
            return UseCustomFilePath;
        }

        /// <summary>
        /// Always gets the current filepath root + any subfolders needed
        /// </summary>
        private static string tempCustomFilePath;
        internal static string GetDataPath()
        {
            try
            {
                // Demo pool and pool switch
                if (UseDemoPool && !UseSecondaryPool)
                {
                    return ApplicationDataRoot() + DemoFolderPath;
                }
                else if (UseDemoPool && UseSecondaryPool)
                {
                    return ApplicationDataRoot() + DemoSecondaryFolderPath;
                }
                else if (!UseCustomFilePath && UseSecondaryPool)
                {
                    return ApplicationDataRoot() + SecondaryFolderPath;
                }
                else
                {
                    // Custom filepath
                    if (UseCustomFilePath)
                    {
                        tempCustomFilePath = CustomFilePath;
                        return tempCustomFilePath;
                    }
                    else
                    {
                        // Primary (default directory) path
                        return ApplicationDataRoot() + PrimaryFolderPath;
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        internal static string GetDataPathOmmitCustom()
        {
            try
            {
                // Demo pool and pool switch
                if (UseDemoPool && !UseSecondaryPool)
                {
                    return ApplicationDataRoot() + DemoFolderPath;
                }
                else if (UseDemoPool && UseSecondaryPool)
                {
                    return ApplicationDataRoot() + DemoSecondaryFolderPath;
                }
                else if (!UseCustomFilePath && UseSecondaryPool)
                {
                    return ApplicationDataRoot() + SecondaryFolderPath;
                }
                else
                {
                    // Primary (default directory) path
                    return ApplicationDataRoot() + PrimaryFolderPath;
                }
            }
            catch
            {
                return "";
            }
        }

        internal static string GetDefaultDataPath()
        {
            try
            {
                return ApplicationDataRoot() + PrimaryFolderPath;
            }
            catch
            {
                return "N/A";
            }
        }

        internal static string GetInitDataPathAndFileName()
        {
            try
            {
                return GetDefaultDataPath() + "\\" + InitFileName;
            }
            catch
            {
                return "N/A";
            }
        }

        internal static string GetDataPathAndFileName()
        {
            string tempApplicationDataPath = GetDataPath();
            if (UseCustomFilePath)
            {
                return tempApplicationDataPath + "\\" + CustomFileName;
            }
            else
            {
                return tempApplicationDataPath + "\\" + PoolFileName;
            }
        }

        internal static string GetBackupDirectory()
        {
            try
            {
                // Establish directory
                string tempApplicationDataPath = GetDataPath();
                CheckBackupDataPath(tempApplicationDataPath);
                return tempApplicationDataPath + BackupFolderPath;
            }
            catch
            {
                return "";
            }
        }

        internal static string GetDataPathAndBackupFileName()
        {
            try
            {
                // Establish directory
                string tempApplicationDataPath = GetDataPath();
                CheckBackupDataPath(tempApplicationDataPath);
                if (UseCustomFilePath)
                {
                    return tempApplicationDataPath + BackupFolderPath + "\\" + GetCustomFileNameStripped() + "-" + GetCurrentBackupIdentifier() + "-" + BackupFileName;
                }
                else
                {
                    return tempApplicationDataPath + BackupFolderPath + "\\" + GetCurrentBackupIdentifier() + "-" + BackupFileName;
                }
            }
            catch
            {
                return "";
            }
        }
        internal static string GetAlternateBackupFileName(string path)
        {
            return GetAlternateBackupFileName(path, GetCurrentAlternateBackupIdentifier());
        }

        // Name of backup "A" or "B" in the remote backup folder for the current pool
        private static string GetAlternateBackupFileName(string path, string identifier)
        {
            if (!UseCustomFilePath && UseSecondaryPool)
            {
                return path + "\\" + identifier + "-Sub-" + BackupFileName;
            }
            if (UseCustomFilePath)
            {
                return path + "\\" + GetCustomFileNameStripped() + "-" + identifier + "-" + BackupFileName;
            }
            return path + "\\" + identifier + "-" + BackupFileName;
        }
        internal static string GetDataPathAndBackupFileNameWithPrimaryIdentifier()
        {
            try
            {
                // Establish directory
                string tempApplicationDataPath = GetDataPath();
                CheckBackupDataPath(tempApplicationDataPath);
                if (UseCustomFilePath)
                {
                    return tempApplicationDataPath + BackupFolderPath + "\\" + GetCustomFileNameStripped() + "-A-" + BackupFileName;
                }
                else
                {
                    return tempApplicationDataPath + BackupFolderPath + "\\" + "A" + "-" + BackupFileName;
                }

            }
            catch
            {
                return "";
            }
        }

        internal static string GetDataPathAndBackupFileNameWithSecondaryIdentifier()
        {
            try
            {
                // Establish directory
                string tempApplicationDataPath = GetDataPath();
                CheckBackupDataPath(tempApplicationDataPath);
                if (UseCustomFilePath)
                {
                    return tempApplicationDataPath + BackupFolderPath + "\\" + GetCustomFileNameStripped() + "-B-" + BackupFileName;
                }
                else
                {
                    return tempApplicationDataPath + BackupFolderPath + "\\" + "B" + "-" + BackupFileName;
                }
            }
            catch
            {
                return "";
            }
        }


        // Settings are always saved to primary data path
        internal static string GetDataPathAndSettingsFileName()
        {
            try
            {
                // Get default directory, demo or switch but not custom
                // (custom datapath just uses the default settings)
                return GetDataPathOmmitCustom() + "\\" + SettingsFileName;
            }
            catch
            {
                return "";
            }
        }

        private static string GetCurrentBackupIdentifier()
        {
            // If last backup was "A" then return "B" and vice versa
            if (MainForm.BackupIdentifier == 0)
            {
                MainForm.BackupIdentifier = 1;
                MainForm.SettingsHasChanges = true;
                return "B";
            }
            else
            {
                MainForm.BackupIdentifier = 0;
                MainForm.SettingsHasChanges = true;
                return "A";

            }
        }

        private static string GetCurrentAlternateBackupIdentifier()
        {
            // If last backup was "A" then return "B" and vice versa
            if (MainForm.AlternateBackupIdentifier == 0)
            {
                MainForm.AlternateBackupIdentifier = 1;
                MainForm.SettingsHasChanges = true;
                return "B";
            }
            else
            {
                MainForm.AlternateBackupIdentifier = 0;
                MainForm.SettingsHasChanges = true;
                return "A";

            }
        }


        private static FileInfo fileInfo { get; set; }
        private static double fileSize { get; set; }
        internal static string GetFileSize()
        {
            if (File.Exists(GetDataPathAndFileName()))
            {
                fileInfo = new FileInfo(GetDataPathAndFileName());
                if ((fileInfo.Length / 100) >= 1000)
                {
                    fileSize = fileInfo.Length / 1000000.0;
                    return fileSize.ToString("0.0") + "MB";
                }
                else
                {
                    fileSize = fileInfo.Length / 1000.0;
                    return fileSize.ToString("0.0") + "kB";
                }
            }
            else
            {
                return "";
            }
        }

        private static string GetAlternateBackupPath()
        {
            if (!String.IsNullOrEmpty(MainForm.AlternateBackupPath))
            {
                return MainForm.AlternateBackupPath;
            }
            else
            {
                return "";
            }
        }

        #endregion

        #region SAVE/LOAD POOL
        // Save
        private static string SelectedFilePath { get; set; }
        private static string SelectedBackupFilePath { get; set; }
        private static string SelectedAlternateBackupFilePath { get; set; }
        private static bool SaveAlternateBackup { get; set; }
        internal static string PoolDumpString { get; set; }
        internal static bool SaveEntries(List<Entry> Entries, List<StickySettings> StickyList, string logText, string holdEntryAID, string holdEntryBID, string memoText, int memoCaret, string displayText, PoolKey key, bool isBackup, string cypherKey, bool saveAtAlternateLocation, string alternateFilePath, bool writeProtected, string dividerA, string dividerB, string dividerC, string dividerD, bool dump)
        {
            using var invariantCulture = InvariantCulture.Use(); // locale-independent file format
            if (key == null)
            {
                return false; // Never write a pool without a password-derived key
            }

            if (!saveAtAlternateLocation && !CheckDataPath())
            {
                return false;
            }
            else
            {
                // If saved to specific location selected by the user
                // using the Save File Dialog
                if (saveAtAlternateLocation)
                {
                    // Set Pool and main Backup path
                    SelectedFilePath = alternateFilePath;
                    SaveAlternateBackup = false;
                }
                else
                {
                    // Set Pool and main Backup path
                    SelectedFilePath = GetDataPathAndFileName();
                    SaveAlternateBackup = false;

                    // Set remote backup directory
                    if (isBackup)
                    {
                        SelectedBackupFilePath = GetDataPathAndBackupFileName();
                        if (!string.IsNullOrEmpty(GetAlternateBackupPath()))
                        {
                            if (Directory.Exists(GetAlternateBackupPath()))
                            {
                                SelectedAlternateBackupFilePath = GetAlternateBackupFileName(GetAlternateBackupPath());
                                SaveAlternateBackup = true;
                                Log.LogSavePoolAlternate();
                            }
                        }
                    }
                }
            }

            StringBuilder xmlContent = new StringBuilder();

            // If any entries exist create xml file and save entries to it
            if (Entries.Count >= 0)
            {
                // Write doctype
                xmlContent.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\" ?>");
                xmlContent.AppendLine(@"<entries>");

                // Write settings
                xmlContent.AppendLine(@"    <settings>");
                xmlContent.AppendLine(@"        <protect>" + writeProtected.ToString() + "</protect>");
                xmlContent.AppendLine(@"    </settings>");

                // Write session ID
                PoolSessionChanged = IO.GetIsoDateTimeNow(); // Update session changed
                xmlContent.AppendLine(@"    <session>");
                xmlContent.AppendLine(@"        <id>" + IO.CurrentSessionId + "</id>");
                xmlContent.AppendLine(@"        <changed>" + PoolSessionChanged + "</changed>");
                xmlContent.AppendLine(@"    </session>");


                // Write log
                xmlContent.AppendLine(@"    <log>");
                xmlContent.AppendLine(@"        <text><![CDATA[" + RemoveIllegalXMLTags(logText) + "]]></text>");
                xmlContent.AppendLine(@"    </log>");

                // Write Hold button ID's
                xmlContent.AppendLine(@"    <hold>");
                xmlContent.AppendLine(@"        <ha>" + holdEntryAID + "</ha>");
                xmlContent.AppendLine(@"        <hb>" + holdEntryBID + "</hb>");
                xmlContent.AppendLine(@"    </hold>");

                // Write memo text
                xmlContent.AppendLine(@"    <memo>");
                xmlContent.AppendLine(@"        <text><![CDATA[" + RemoveIllegalXMLTags(memoText) + "]]></text>");
                xmlContent.AppendLine(@"    </memo>");

                // Write memo caret position
                xmlContent.AppendLine(@"    <mcaret>");
                xmlContent.AppendLine(@"        <caret><![CDATA[" + RemoveIllegalXMLTags(memoCaret.ToString()) + "]]></caret>");
                xmlContent.AppendLine(@"    </mcaret>");

                // Write Cypher key
                xmlContent.AppendLine(@"    <cypher>");
                xmlContent.AppendLine(@"       <text><![CDATA[" + RemoveIllegalXMLTags(cypherKey) + "]]></text>");
                xmlContent.AppendLine(@"    </cypher>");
                // Write display text
                xmlContent.AppendLine(@"    <display>");
                xmlContent.AppendLine(@"        <text><![CDATA[" + RemoveIllegalXMLTags(displayText) + "]]></text>");
                xmlContent.AppendLine(@"    </display>");

                //// Write backup path removed from version 1.4.1.7

                // Write Paragraph Dividers
                xmlContent.AppendLine(@"    <dividers>");
                xmlContent.AppendLine(@"        <dividera><![CDATA[" + RemoveIllegalXMLTags(dividerA) + "]]></dividera>");
                xmlContent.AppendLine(@"        <dividerb><![CDATA[" + RemoveIllegalXMLTags(dividerB) + "]]></dividerb>");
                xmlContent.AppendLine(@"        <dividerc><![CDATA[" + RemoveIllegalXMLTags(dividerC) + "]]></dividerc>");
                xmlContent.AppendLine(@"        <dividerd><![CDATA[" + RemoveIllegalXMLTags(dividerD) + "]]></dividerd>");
                xmlContent.AppendLine(@"    </dividers>");

                if (StickyList != null && StickyList.Count > 0)
                {
                    foreach (StickySettings s in StickyList)
                    {
                        // Write stickies to XML file
                        xmlContent.AppendLine(@"    <sticky>");
                        xmlContent.AppendLine(@"        <id>" + s.ID + "</id>");
                        xmlContent.AppendLine(@"        <w>" + s.Width + "</w>");
                        xmlContent.AppendLine(@"        <h>" + s.Height + "</h>");
                        xmlContent.AppendLine(@"        <x>" + s.Left + "</x>");
                        xmlContent.AppendLine(@"        <y>" + s.Top + "</y>");
                        xmlContent.AppendLine(@"        <ct>" + s.Theme + "</ct>");
                        xmlContent.AppendLine(@"        <po>" + s.TopMost + "</po>");
                        xmlContent.AppendLine(@"        <t><![CDATA[" + RemoveIllegalXMLTags(s.Text) + "]]></t>");
                        xmlContent.AppendLine(@"    </sticky>");
                    }
                }

                // Entries (an exception here fails the whole save; a partial pool is never written)
                foreach (Entry entry in Entries)
                {
                    // Write entries to XML file
                    xmlContent.AppendLine(@"    <entry>");
                    xmlContent.AppendLine(@"        <id>" + entry.ID + "</id>");
                    xmlContent.AppendLine(@"        <cre>" + entry.Created + "</cre>");
                    xmlContent.AppendLine(@"        <cha>" + entry.Changed + "</cha>");
                    xmlContent.AppendLine(@"        <pin>" + entry.Pin.ToString() + "</pin>");
                    xmlContent.AppendLine(@"        <rnk>" + entry.Rank.ToString() + "</rnk>");
                    xmlContent.AppendLine(@"        <sta>" + entry.PresetColor.ToString() + "</sta>");
                    xmlContent.AppendLine(@"        <cuc><![CDATA[" + entry.CustomColor.ToString() + "]]></cuc>");
                    xmlContent.AppendLine(@"        <pro>" + entry.Protected.ToString() + "</pro>");
                    xmlContent.AppendLine(@"        <pos>" + entry.CaretPosition.ToString() + "</pos>");
                    xmlContent.AppendLine(@"        <bmk>" + entry.Bookmark.ToString() + "</bmk>");
                    xmlContent.AppendLine(@"        <rel>" + entry.RelatedEntries.ToString() + "</rel>");
                    xmlContent.AppendLine(@"        <rev>" + entry.RelatedView.ToString() + "</rev>");
                    xmlContent.AppendLine(@"        <res>" + entry.RelatedStyle.ToString() + "</res>");
                    xmlContent.AppendLine(@"        <tit><![CDATA[" + RemoveIllegalXMLTags(entry.Title) + "]]></tit>");
                    xmlContent.AppendLine(@"        <con><![CDATA[" + RemoveIllegalXMLTags(entry.Content) + "]]></con>");
                    xmlContent.AppendLine(@"    </entry>");
                }
                xmlContent.AppendLine(@"</entries>");

                // Dump pool before encryption
                if (dump)
                {
                    PoolDumpString = xmlContent.ToString();
                }

                // Encrypt the content (AES-256-GCM, see Crypto/PoolFile.cs)
                byte[] encryptedPool;
                try
                {
                    encryptedPool = PoolFile.Encrypt(xmlContent.ToString(), key);
                }
                catch
                {
                    return false;
                }

                if (!WriteFileAtomically(SelectedFilePath, encryptedPool))
                {
                    return false;
                }

                if (isBackup)
                {
                    if (!WriteFileAtomically(SelectedBackupFilePath, encryptedPool))
                    {
                        Log.LogSavePoolError();
                    }
                    if (SaveAlternateBackup && !WriteFileAtomically(SelectedAlternateBackupFilePath, encryptedPool))
                    {
                        Log.LogSavePoolError();
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Write to a temporary file in the same directory, then move it over the target,
        /// so an interrupted save never leaves a half-written pool behind.
        /// </summary>
        internal static bool WriteTextFileAtomically(string path, string text)
        {
            byte[] bom = Encoding.UTF8.GetPreamble();
            byte[] body = Encoding.UTF8.GetBytes((text ?? "") + Environment.NewLine);
            byte[] data = new byte[bom.Length + body.Length];
            bom.CopyTo(data, 0);
            body.CopyTo(data, bom.Length);
            return WriteFileAtomically(path, data);
        }

        internal static bool WriteFileAtomically(string path, byte[] data)
        {
            string tempPath = path + ".tmp";
            try
            {
                using (var stream = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    stream.Write(data, 0, data.Length);
                    stream.Flush(true);
                }
                File.Move(tempPath, path, overwrite: true);
                return true;
            }
            catch
            {
                try { File.Delete(tempPath); } catch { /* ignore */ }
                return false;
            }
        }

        /// <summary>Read the current pool back from disk and check that it decrypts with the given key.</summary>
        internal static bool VerifySavedPool(PoolKey key)
        {
            try
            {
                return PoolFile.TryDecrypt(File.ReadAllBytes(GetDataPathAndFileName()), key, out _);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// The current pool's own files that are still in the Ahoy 1.x format (readable by anyone with a
        /// copy of Ahoy 1.x): the pool itself, its backups A/B and its remote backups A/B.
        /// Other pools in the same folders are never included.
        /// </summary>
        internal static List<string> FindLegacyPoolFiles()
        {
            var candidates = new List<string>
            {
                GetDataPathAndFileName(),
                GetDataPathAndBackupFileNameWithPrimaryIdentifier(),
                GetDataPathAndBackupFileNameWithSecondaryIdentifier()
            };
            string remote = GetAlternateBackupPath();
            if (!string.IsNullOrEmpty(remote))
            {
                candidates.Add(GetAlternateBackupFileName(remote, "A"));
                candidates.Add(GetAlternateBackupFileName(remote, "B"));
            }

            var result = new List<string>();
            foreach (string file in candidates)
            {
                if (string.IsNullOrEmpty(file) || result.Contains(file) || !File.Exists(file))
                {
                    continue;
                }
                try
                {
                    byte[] bytes = File.ReadAllBytes(file);
                    if (!PoolFile.IsEncryptedPool(bytes) &&
                        Legacy.TryDecryptPool(Encoding.UTF8.GetString(bytes).TrimStart('\uFEFF'), out _))
                    {
                        result.Add(file);
                    }
                }
                catch
                {
                    // Unreadable file: skip
                }
            }
            return result;
        }

        // Color theme index: <colortheme> (Ahoy 2.x) or <theme> (Ahoy 1.x, 20 themes)
        private static int ReadColorTheme(XmlNode node)
        {
            XmlNode current = node.SelectSingleNode("colortheme");
            if (current != null && int.TryParse(current.InnerText, out int index) && index >= 0 && index <= AhoyColors.ThemeCount)
            {
                return index;
            }
            XmlNode legacy = node.SelectSingleNode("theme");
            if (legacy != null && int.TryParse(legacy.InnerText, out int legacyIndex))
            {
                return AhoyColors.MapLegacyThemeIndex(legacyIndex);
            }
            return Settings.DefaultColorTheme;
        }

        // Temporary method for checking the "rnk" node
        private static int CheckNodeInt(XmlNode node, string value, int defaultValue)
        {
            XmlNode checkNode = node.SelectSingleNode(value);

            if (checkNode != null)
                return InvariantCulture.ParseInt(checkNode.InnerText);
            else
                return defaultValue;
        }

        // Temporary method for checking the "rnk" node
        private static double CheckNodeDouble(XmlNode node, string value, double defaultValue)
        {
            XmlNode checkNode = node.SelectSingleNode(value);

            if (checkNode != null)
                return InvariantCulture.ParseDouble(checkNode.InnerText);
            else
                return defaultValue;
        }

        // Temporary method for checking the "maximized" node
        private static bool CheckNodeBoolean(XmlNode node, string value, bool defaultValue)
        {
            XmlNode checkNode = node.SelectSingleNode(value);

            if (checkNode != null)
                return Convert.ToBoolean(checkNode.InnerText);
            else
                return defaultValue;
        }

        // Temporary method for checking the "bmk" node
        private static string CheckNodeString(XmlNode node, string value, string defaultValue)
        {
            XmlNode checkNode = node.SelectSingleNode(value);

            if (checkNode != null)
                return checkNode.InnerText;
            else
                return defaultValue;
        }

        // Load
        public static Load LoadEntries(List<Entry> Entries, PoolUnlocker unlockPool, bool loadBackup, bool backupA, bool openFilePath, string filePath)
        {
            using var invariantCulture = InvariantCulture.Use(); // locale-independent file format
            // Prepare load object to return
            Load loadObject = new Load();
            loadObject.Entries = new List<Entry>();
            loadObject.LogText = string.Empty;
            loadObject.DisplayText = string.Empty;
            string Filepath = "";

            // Clear custom filepath
            if (!openFilePath)
            {
                ClearCustomFilePath(false);
            }

            // Get paths
            if (loadBackup)
            {
                if (backupA)
                {
                    Filepath = GetDataPathAndBackupFileNameWithPrimaryIdentifier(); // Set backup "A" path
                }
                else
                {
                    Filepath = GetDataPathAndBackupFileNameWithSecondaryIdentifier(); // Select backup "B" path
                }
            }
            else if (openFilePath)
            {
                if (SetAndValidateCustomFilePath(filePath))
                {
                    Filepath = filePath;
                }
                else
                {
                    Log.LogErrorLoadPoolFromFilePath();
                    Filepath = GetDataPathAndFileName();
                }
            }
            else
            {
                Filepath = GetDataPathAndFileName();
            }

            // File verification
            if (CheckDataPath())
            {
                if (!File.Exists(Filepath))
                {
                    loadObject.LoadSuccess = false;
                    if (openFilePath)
                    {
                        loadObject.LoadMessage = "Unable to locate Pool using the supplied path.";
                    }
                    else
                    {
                        loadObject.LoadMessage = "Unable to locate Pool in file repository.";
                    }
                    return loadObject;
                }
            }


            // Read the file
            byte[] fileBytes;
            try
            {
                fileBytes = File.ReadAllBytes(Filepath);
            }
            catch
            {
                loadObject.LoadSuccess = false;
                loadObject.LoadMessage = "Unable to read the Pool file.";
                return loadObject;
            }

            string decryptedXmlContent;
            if (PoolFile.IsEncryptedPool(fileBytes))
            {
                // Current format: ask the caller for the password and decrypt
                decryptedXmlContent = unlockPool(fileBytes, loadObject);
                if (decryptedXmlContent == null)
                {
                    loadObject.LoadSuccess = false;
                    if (string.IsNullOrEmpty(loadObject.LoadMessage))
                    {
                        loadObject.LoadMessage = "The Pool was not unlocked.";
                    }
                    return loadObject;
                }
            }
            else
            {
                // Ahoy 1.x format (3DES with a built-in key): read it so it can be upgraded
                string legacyText = Encoding.UTF8.GetString(fileBytes).TrimStart('\uFEFF');
                if (!Legacy.TryDecryptPool(legacyText, out decryptedXmlContent))
                {
                    loadObject.LoadSuccess = false;
                    loadObject.LoadMessage = "Error decrypting the Pool (unknown or damaged file).";
                    return loadObject;
                }
                loadObject.IsLegacyFormat = true;
            }

            // Extract the XML from the string in memory
            try
            {
                TextReader textReader = new StringReader(decryptedXmlContent);

                XmlDocument document = new XmlDocument();
                document.Load(textReader);

                XmlNodeList nodes;
                nodes = document.SelectNodes("//entries/entry"); // XPath statement
                XmlNodeList settingsNodes;
                settingsNodes = document.SelectNodes("//entries/settings"); // XPath statement

                // Read settings
                foreach (XmlNode node in settingsNodes)
                {
                    if (loadObject.IsLegacyFormat)
                    {
                        // Ahoy 1.x kept the login password in the pool; used once to confirm the upgrade
                        loadObject.LegacyPassword = CheckNodeString(node, "setc", string.Empty);
                    }
                    loadObject.WriteProtected = CheckNodeBoolean(node, "protect", false); // Defaults to false (not write protected)
                }

                // Read session ID
                XmlNodeList sessionNodes;
                sessionNodes = document.SelectNodes("//entries/session"); // XPath statement
                foreach (XmlNode node in sessionNodes)
                {
                    try
                    {
                        PoolSessionId = node["id"].InnerText;
                        PoolSessionChanged = node["changed"].InnerText;
                    }
                    catch
                    {
                        PoolSessionId = "N/A";
                        PoolSessionChanged = "N/A";
                    }
                }

                // Read log
                XmlNodeList logNodes;
                logNodes = document.SelectNodes("//entries/log"); // XPath statement
                foreach (XmlNode node in logNodes)
                {
                    loadObject.LogText = RestoreIllegalXMLTags(node["text"].InnerText);
                }

                // Read hold entry ID's
                XmlNodeList holdNodes;
                holdNodes = document.SelectNodes("//entries/hold"); // XPath statement
                foreach (XmlNode node in holdNodes)
                {
                    loadObject.HoldEntryAID = node["ha"].InnerText;
                    loadObject.HoldEntryBID = node["hb"].InnerText;
                }

                // Read memo text
                XmlNodeList memoNodes;
                memoNodes = document.SelectNodes("//entries/memo"); // XPath statement
                foreach (XmlNode node in memoNodes)
                {
                    loadObject.MemoText = RestoreIllegalXMLTags(node["text"].InnerText);
                }

                // Read memo caret position
                XmlNodeList mcaretNodes;
                mcaretNodes = document.SelectNodes("//entries/mcaret"); // XPath statement
                foreach (XmlNode node in mcaretNodes)
                {
                    loadObject.MemoCaretPosition = InvariantCulture.ParseInt(node["caret"].InnerText);
                }


                // Read Cypher key
                XmlNodeList cypherKeyText;
                cypherKeyText = document.SelectNodes("//entries/cypher"); // XPath statement
                foreach (XmlNode node in cypherKeyText)
                {
                    loadObject.CypherKey = RestoreIllegalXMLTags(node["text"].InnerText);
                }
                // Read display text
                XmlNodeList displayNodes;
                displayNodes = document.SelectNodes("//entries/display"); // XPath statement
                foreach (XmlNode node in displayNodes)
                {
                    loadObject.DisplayText = RestoreIllegalXMLTags(node["text"].InnerText);
                }

                // Read remote backup path
                XmlNodeList altBackupPath;
                altBackupPath = document.SelectNodes("//entries/backup"); // XPath statement
                foreach (XmlNode node in altBackupPath)
                {
                    loadObject.AlternateBackupPath = RestoreIllegalXMLTags(node["path"].InnerText);
                }

                // Read Paragraph Dividers
                XmlNodeList dividerNodes;
                dividerNodes = document.SelectNodes("//entries/dividers"); // XPath statement
                foreach (XmlNode node in dividerNodes)
                {
                    loadObject.DividerAString = RestoreIllegalXMLTags(node["dividera"].InnerText);
                    loadObject.DividerBString = RestoreIllegalXMLTags(node["dividerb"].InnerText);
                    loadObject.DividerCString = RestoreIllegalXMLTags(node["dividerc"].InnerText);
                    loadObject.DividerDString = RestoreIllegalXMLTags(node["dividerd"].InnerText);
                }

                // Read Stickies
                XmlNodeList stickyNodes;
                stickyNodes = document.SelectNodes("//entries/sticky"); // XPath statement
                if (stickyNodes != null && stickyNodes.Count > 0)
                {
                    foreach (XmlNode node in stickyNodes)
                    {
                        StickySettings newSticky = new StickySettings();
                        newSticky.ID = node["id"].InnerText;
                        newSticky.Width = InvariantCulture.ParseInt(node["w"].InnerText);
                        newSticky.Height = InvariantCulture.ParseInt(node["h"].InnerText);
                        newSticky.Left = InvariantCulture.ParseInt(node["x"].InnerText);
                        newSticky.Top = InvariantCulture.ParseInt(node["y"].InnerText);
                        newSticky.Theme = CheckNodeInt(node, "ct", 0);
                        newSticky.TopMost = CheckNodeBoolean(node, "po", true);
                        newSticky.Text = RestoreIllegalXMLTags(node["t"].InnerText);
                        loadObject.Stickies.Add(newSticky);
                    }
                }

                // Read entries
                foreach (XmlNode node in nodes)
                {
                    Entry newEntry = new Entry();
                    newEntry.ID = node["id"].InnerText;
                    newEntry.Created = node["cre"].InnerText;
                    newEntry.Changed = node["cha"].InnerText;
                    newEntry.Pin = Convert.ToBoolean(node["pin"].InnerText);
                    newEntry.Rank = CheckNodeInt(node, "rnk", DefaultRank);
                    newEntry.PresetColor = CheckNodeInt(node, "sta", DefaultStatus);
                    newEntry.CustomColor = CheckNodeString(node, "cuc", string.Empty);
                    newEntry.Protected = Convert.ToBoolean(node["pro"].InnerText);
                    newEntry.CaretPosition = InvariantCulture.ParseInt(node["pos"].InnerText);
                    newEntry.Bookmark = CheckNodeInt(node, "bmk", DefaultStatus);
                    newEntry.RelatedEntries = CheckNodeString(node, "rel", string.Empty);
                    newEntry.RelatedView = CheckNodeBoolean(node, "rev", false);
                    newEntry.RelatedStyle = CheckNodeInt(node, "res", DefaultStatus);
                    newEntry.Title = RestoreIllegalXMLTags(node["tit"].InnerText);
                    newEntry.Content = RestoreIllegalXMLTags(node["con"].InnerText);
                    loadObject.Entries.Add(newEntry);
                }

                loadObject.LoadSuccess = true;
                return loadObject;
            }
            catch
            {
                loadObject.LoadSuccess = false;
                loadObject.LoadMessage = "Error reading decrypted XML.";
                return loadObject;
            }
        }
        #endregion

        #region COPY POOL TO CLIPBOARD
        internal static string GetPoolPath()
        {
            if (File.Exists(GetDataPathAndFileName()))
            {
                return GetDataPathAndFileName();
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region SAVE/LOAD SETTINGS

        private static string SelectedSettingsFilePath { get; set; }
        internal static bool SaveSettings(Settings settingsObject)
        {
            using var invariantCulture = InvariantCulture.Use(); // locale-independent file format
            if (!CheckDataPath())
            {
                return false;
            }
            else
            {
                // Get settings file path
                SelectedSettingsFilePath = GetDataPathAndSettingsFileName();
            }

            StringBuilder xmlContent = new StringBuilder();
            string stringData = "";

            // If any entries exist create xml file and save entries to it
            if (settingsObject != null)
            {
                // Write doctype
                xmlContent.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\" ?>");
                xmlContent.AppendLine(@"<settings>");

                // Write settings
                xmlContent.AppendLine(@"    <setting>");
                xmlContent.AppendLine(@"        <backupidnt>" + settingsObject.BackupIdentifier.ToString() + "</backupidnt>");
                xmlContent.AppendLine(@"        <fscreen>" + settingsObject.Fullscreen.ToString() + "</fscreen>");
                xmlContent.AppendLine(@"        <maximized>" + settingsObject.Maximized.ToString() + "</maximized>");
                xmlContent.AppendLine(@"        <width>" + settingsObject.Width.ToString() + "</width>");
                xmlContent.AppendLine(@"        <height>" + settingsObject.Height.ToString() + "</height>");
                xmlContent.AppendLine(@"        <posx>" + settingsObject.PositionX.ToString() + "</posx>");
                xmlContent.AppendLine(@"        <posy>" + settingsObject.PositionY.ToString() + "</posy>");
                xmlContent.AppendLine(@"        <colortheme>" + settingsObject.ColorTheme.ToString() + "</colortheme>");
                xmlContent.AppendLine(@"        <border>" + settingsObject.EnableFormBorder.ToString() + "</border>");
                xmlContent.AppendLine(@"        <btnbovr>" + settingsObject.ButtonBitmapOverride.ToString() + "</btnbovr>");
                xmlContent.AppendLine(@"        <layout>" + settingsObject.LayoutMode.ToString() + "</layout>");
                xmlContent.AppendLine(@"        <opacity>" + settingsObject.Opacity.ToString() + "</opacity>");
                xmlContent.AppendLine(@"        <texture>" + settingsObject.Texture.ToString() + "</texture>");
                xmlContent.AppendLine(@"        <anim>" + settingsObject.Animations.ToString() + "</anim>");
                xmlContent.AppendLine(@"        <qgcycle>" + settingsObject.DisplayTextCycle.ToString() + "</qgcycle>");
                xmlContent.AppendLine(@"        <wpmcnt>" + settingsObject.ShowWPMCounter.ToString() + "</wpmcnt>");
                xmlContent.AppendLine(@"        <sleep>" + settingsObject.SleepIntervalIndex.ToString() + "</sleep>");
                xmlContent.AppendLine(@"        <lsearch>" + settingsObject.LiveSearch.ToString() + "</lsearch>");
                xmlContent.AppendLine(@"        <tsearch>" + settingsObject.TitlePrioritySearch.ToString() + "</tsearch>");
                xmlContent.AppendLine(@"        <asearch>" + settingsObject.AutoSearch.ToString() + "</asearch>");
                xmlContent.AppendLine(@"        <wordwrap>" + settingsObject.WordWrap.ToString() + "</wordwrap>");
                xmlContent.AppendLine(@"        <ttab>" + settingsObject.TitleTabToContent.ToString() + "</ttab>");
                xmlContent.AppendLine(@"        <inewl>" + settingsObject.InsertNewLines.ToString() + "</inewl>");
                xmlContent.AppendLine(@"        <pwgen>" + settingsObject.SelectedPasswordStyle.ToString() + "</pwgen>");
                xmlContent.AppendLine(@"        <autos>" + settingsObject.AutoSaveEntryChanges.ToString() + "</autos>");
                xmlContent.AppendLine(@"        <splang>" + settingsObject.SpellCheckerLanguage.ToString() + "</splang>");
                xmlContent.AppendLine(@"        <font>" + settingsObject.CurrentFont.ToString() + "</font>");
                xmlContent.AppendLine(@"        <ffont>" + settingsObject.FieldsFontSize.ToString() + "</ffont>");
                xmlContent.AppendLine(@"        <lfont>" + settingsObject.ListsFontSize.ToString() + "</lfont>");
                xmlContent.AppendLine(@"        <libold>" + settingsObject.CurrentListFontWeight.ToString() + "</libold>");
                xmlContent.AppendLine(@"        <scroll>" + settingsObject.EnforceScrollbars.ToString() + "</scroll>");
                xmlContent.AppendLine(@"        <lautohide>" + settingsObject.ListsAutoHide.ToString() + "</lautohide>");
                xmlContent.AppendLine(@"        <clk>" + settingsObject.ShowClock.ToString() + "</clk>");
                xmlContent.AppendLine(@"        <pinbc>" + settingsObject.NewPinnedByClick.ToString() + "</pinbc>");
                xmlContent.AppendLine(@"        <hpinc>" + settingsObject.HidePinnedByClick.ToString() + "</hpinc>");
                xmlContent.AppendLine(@"        <browser>" + settingsObject.InternetBrowser.ToString() + "</browser>");
                xmlContent.AppendLine(@"        <durls>" + settingsObject.DetectURLs.ToString() + "</durls>");
                xmlContent.AppendLine(@"        <mbk>" + settingsObject.MoveByKeyboard + "</mbk>");
                xmlContent.AppendLine(@"        <rbk>" + settingsObject.ResizeByKeyboard + "</rbk>");
                xmlContent.AppendLine(@"        <tsf>" + settingsObject.TimestampFormat.ToString() + "</tsf>");
                xmlContent.AppendLine(@"        <ofdp><![CDATA[" + RemoveIllegalXMLTags(settingsObject.OpenFileDialogPath) + "]]></ofdp>");
                xmlContent.AppendLine(@"        <sfdp><![CDATA[" + RemoveIllegalXMLTags(settingsObject.SaveFileDialogPath) + "]]></sfdp>");
                xmlContent.AppendLine(@"        <bkpth><![CDATA[" + RemoveIllegalXMLTags(settingsObject.BackupLocation) + "]]></bkpth>");
                xmlContent.AppendLine(@"        <custt><![CDATA[" + RemoveIllegalXMLTags(settingsObject.ThemeTitle) + "]]></custt>");
                xmlContent.AppendLine(@"        <custc><![CDATA[" + RemoveIllegalXMLTags(settingsObject.CustomColors) + "]]></custc>");
                xmlContent.AppendLine(@"    </setting>");

                xmlContent.AppendLine(@"</settings>");


                // Add xml content to string
                stringData = xmlContent.ToString();


                try
                {
                    if (!WriteTextFileAtomically(SelectedSettingsFilePath, stringData))
                    {
                        throw new IOException("Unable to write " + SelectedSettingsFilePath);
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }


        // Load
        public static Settings LoadSettings()
        {
            using var invariantCulture = InvariantCulture.Use(); // locale-independent file format
            // Prepare Settings object to return
            Settings settingsObject = new Settings();

            // File verification
            if (CheckDataPath())
            {
                if (!File.Exists(GetDataPathAndSettingsFileName()))
                {
                    settingsObject.LoadSuccess = false;
                    settingsObject.LoadMessage = "Settings unavailable";
                    return settingsObject;
                }
            }

            FileStream fileStream = new FileStream(GetDataPathAndSettingsFileName(), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader reader = new StreamReader(fileStream);
            StringBuilder xmlContent = new StringBuilder();
            string stringData = "";

            // Load the file to memory
            try
            {
                while (reader.Peek() != -1)
                {
                    xmlContent.AppendLine(reader.ReadLine());
                }

                // Add loaded xml to string variable
                stringData = xmlContent.ToString();
            }
            catch
            {
                settingsObject.LoadSuccess = false;
                settingsObject.LoadMessage = "Error reading XML file.";
                return settingsObject;
            }
            finally
            {
                reader.Close();
            }

            // Extract the XML from the string in memory
            try
            {
                TextReader textReader = new StringReader(stringData);
                XmlDocument document = new XmlDocument();
                document.Load(textReader);

                XmlNodeList settingsNodes;
                settingsNodes = document.SelectNodes("//settings/setting"); // XPath statement

                // Read settings
                foreach (XmlNode node in settingsNodes)
                {
                    settingsObject.BackupIdentifier = CheckNodeInt(node, "backupidnt", DefaultStatus);
                    settingsObject.Fullscreen = Convert.ToBoolean(node["fscreen"].InnerText);
                    settingsObject.Maximized = CheckNodeBoolean(node, "maximized", DefaultBoolean);
                    settingsObject.Width = InvariantCulture.ParseInt(node["width"].InnerText);
                    settingsObject.Height = InvariantCulture.ParseInt(node["height"].InnerText);
                    settingsObject.PositionX = InvariantCulture.ParseInt(node["posx"].InnerText);
                    settingsObject.PositionY = InvariantCulture.ParseInt(node["posy"].InnerText);
                    settingsObject.ColorTheme = ReadColorTheme(node);
                    settingsObject.EnableFormBorder = CheckNodeBoolean(node, "border", Settings.DefaultEnableFormBorder);
                    settingsObject.ButtonBitmapOverride = CheckNodeInt(node, "btnbovr", Settings.DefaultButtonBitmapOverride);
                    settingsObject.LayoutMode = InvariantCulture.ParseInt(node["layout"].InnerText);
                    settingsObject.Opacity = CheckNodeDouble(node, "opacity", Settings.DefaultOpacity);
                    settingsObject.Texture = CheckNodeInt(node, "texture", Settings.DefaultTexture);
                    settingsObject.Animations = CheckNodeBoolean(node, "anim", true);
                    settingsObject.DisplayTextCycle = CheckNodeInt(node, "qgcycle", Settings.DefaultDisplayTextCycle);
                    settingsObject.ShowWPMCounter = CheckNodeBoolean(node, "wpmcnt", false);
                    settingsObject.SleepIntervalIndex = InvariantCulture.ParseInt(node["sleep"].InnerText);
                    settingsObject.LiveSearch = CheckNodeBoolean(node, "lsearch", Settings.DefaultLiveSearch);
                    settingsObject.TitlePrioritySearch = CheckNodeBoolean(node, "tsearch", Settings.DefaultTitlePrioritySearch);
                    settingsObject.AutoSearch = CheckNodeBoolean(node, "asearch", Settings.DefaultAutoSearch);
                    settingsObject.WordWrap = Convert.ToBoolean(node["wordwrap"].InnerText);
                    settingsObject.TitleTabToContent = CheckNodeBoolean(node, "ttab", Settings.DefaultTitleTabToContent);
                    settingsObject.InsertNewLines = CheckNodeBoolean(node, "inewl", Settings.DefaultInsertNewLines);
                    settingsObject.SelectedPasswordStyle = CheckNodeInt(node, "pwgen", Settings.DefaultPasswordStyle);
                    settingsObject.AutoSaveEntryChanges = CheckNodeBoolean(node, "autos", Settings.DefaultAutoSaveEntryChanges);
                    settingsObject.SpellCheckerLanguage = CheckNodeInt(node, "splang", 1);
                    settingsObject.CurrentFont = CheckNodeInt(node, "font", 0);
                    settingsObject.FieldsFontSize = InvariantCulture.ParseInt(node["ffont"].InnerText);
                    settingsObject.ListsFontSize = InvariantCulture.ParseInt(node["lfont"].InnerText);
                    settingsObject.CurrentListFontWeight = CheckNodeBoolean(node, "libold", Settings.DefaultListFontWeight);
                    settingsObject.EnforceScrollbars = CheckNodeBoolean(node, "scroll", Settings.DefaultEnforceScrollbars);
                    settingsObject.ListsAutoHide = Convert.ToBoolean(node["lautohide"].InnerText);
                    settingsObject.ShowClock = CheckNodeBoolean(node, "clk", Settings.DefaultShowClock);
                    settingsObject.NewPinnedByClick = CheckNodeBoolean(node, "pinbc", Settings.DefaultNewPinnedByClick);
                    settingsObject.HidePinnedByClick = CheckNodeBoolean(node, "hpinc", Settings.DefaultHidePinnedByClick);
                    settingsObject.InternetBrowser = CheckNodeInt(node, "browser", Settings.DefaultBrowser);
                    settingsObject.DetectURLs = CheckNodeBoolean(node, "durls", Settings.DefaultDetectURLs);
                    settingsObject.MoveByKeyboard = CheckNodeBoolean(node, "mbk", Settings.DefaultMoveByKeyboard);
                    settingsObject.ResizeByKeyboard = CheckNodeBoolean(node, "rbk", Settings.DefaultResizeByKeyboard);
                    settingsObject.TimestampFormat = CheckNodeInt(node, "tsf", Settings.DefaultTimestampFormat);
                    settingsObject.OpenFileDialogPath = RestoreIllegalXMLTags(CheckNodeString(node, "ofdp", ""));
                    settingsObject.SaveFileDialogPath = RestoreIllegalXMLTags(CheckNodeString(node, "sfdp", ""));
                    settingsObject.BackupLocation = RestoreIllegalXMLTags(CheckNodeString(node, "bkpth", ""));
                    settingsObject.ThemeTitle = RestoreIllegalXMLTags(CheckNodeString(node, "custt", Settings.DefaultThemeTitle));
                    settingsObject.CustomColors = RestoreIllegalXMLTags(CheckNodeString(node, "custc", Settings.DefaultCustomColors));
                }

                settingsObject.LoadSuccess = true;
                return settingsObject;
            }
            catch
            {
                settingsObject.LoadSuccess = false;
                settingsObject.LoadMessage = "Error reading loaded XML data.";
                return settingsObject;
            }
        }
        #endregion

        #region OPEN POOL FROM FILE DIALOG
        private static string tempOpenFilePath;
        internal static string OpenFileDialog(int fileType)
        {
            try
            {
                // Build open file dialog
                tempOpenFilePath = "";
                OpenFileDialog openFileFromPath = new OpenFileDialog();
                openFileFromPath.AddExtension = true;
                openFileFromPath.AutoUpgradeEnabled = true;
                openFileFromPath.CheckFileExists = true;
                openFileFromPath.CheckPathExists = true;
                //openPoolFromFilePath.DefaultExt = @".ahoy";
                openFileFromPath.DereferenceLinks = true;
                openFileFromPath.FilterIndex = 1;
                openFileFromPath.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileFromPath.Multiselect = false;
                openFileFromPath.ReadOnlyChecked = false;
                openFileFromPath.RestoreDirectory = false;
                openFileFromPath.ShowHelp = false;
                openFileFromPath.ShowReadOnly = false;
                openFileFromPath.SupportMultiDottedExtensions = false;
                openFileFromPath.Tag = "";
                openFileFromPath.Title = "Open";
                openFileFromPath.ValidateNames = true;

                switch (fileType)
                {
                    case 0:
                        openFileFromPath.FileName = PoolFileName;
                        openFileFromPath.DefaultExt = @".ahoy";
                        openFileFromPath.Filter = @"Ahoy pool|*.ahoy";
                        break;
                    case 1:
                        openFileFromPath.FileName = "";
                        openFileFromPath.DefaultExt = @".xml";
                        openFileFromPath.Filter = @"XML Document|*.xml";
                        break;
                    default:
                        openFileFromPath.FileName = PoolFileName;
                        openFileFromPath.DefaultExt = @".ahoy";
                        openFileFromPath.Filter = @"Ahoy pool|*.ahoy";
                        break;
                }


                // Set dialog path
                // If a path was already set from previous actions set it
                if (!string.IsNullOrEmpty(MainForm.OpenFilePath) && Directory.Exists(MainForm.OpenFilePath))
                {
                    openFileFromPath.InitialDirectory = MainForm.OpenFilePath;
                }
                else if (Directory.Exists(GetDataPath()))
                {
                    openFileFromPath.InitialDirectory = GetDataPath();
                }

                // OpenFilePath if OpenFilePath.lengh > 0 validate if path exists and set the file dialog on it...
                if (openFileFromPath.ShowDialog() == DialogResult.OK)
                {
                    // Save last path
                    tempOpenFilePath = openFileFromPath.FileName;
                    MainForm.OpenFilePath = tempOpenFilePath.Substring(0, tempOpenFilePath.LastIndexOf('\\'));
                    MainForm.SettingsHasChanges = true;
                }
                else
                {
                    tempOpenFilePath = "";
                }

                return tempOpenFilePath;
            }
            catch
            {
                tempOpenFilePath = "";
                Log.LogLoadFileError();
                return tempOpenFilePath;
            }

        }
        #endregion

        #region SAVE POOL DIALOG
        private static string tempSaveFilePathAndFileName;
        private static string tempSaveFilePathWithoutFileName;
        internal static bool SavePoolFromDialogSuccess;
        internal static string SaveFileDialog(int fileType, string txtFileName, bool returnPathWithoutFileName)
        {
            try
            {
                // Build open file dialog
                tempSaveFilePathAndFileName = "";
                tempSaveFilePathWithoutFileName = "";
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.AddExtension = true;
                saveFileDialog.AutoUpgradeEnabled = true;
                saveFileDialog.CheckFileExists = false;
                saveFileDialog.CheckPathExists = true;
                saveFileDialog.CreatePrompt = false;
                saveFileDialog.DereferenceLinks = true;
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.RestoreDirectory = false;
                saveFileDialog.ShowHelp = false;
                saveFileDialog.SupportMultiDottedExtensions = false;
                saveFileDialog.Tag = "";
                saveFileDialog.ValidateNames = true;


                if (fileType == 0)  // Save as .ahoy
                {
                    saveFileDialog.FileName = PoolFileName;
                    saveFileDialog.Title = "Save Pool";
                    saveFileDialog.DefaultExt = @".ahoy";
                    saveFileDialog.Filter = @"Ahoy pool|*.ahoy";
                }
                else if (fileType == 1) // Save as .txt
                {
                    saveFileDialog.FileName = txtFileName;
                    saveFileDialog.Title = "Save As...";
                    saveFileDialog.DefaultExt = @".txt";
                    saveFileDialog.Filter = @"Text Documents|*.txt";
                }
                else if (fileType == 2) // Save as .xml
                {
                    saveFileDialog.FileName = txtFileName;
                    saveFileDialog.Title = "Save As...";
                    saveFileDialog.DefaultExt = @".xml";
                    saveFileDialog.Filter = @"XML Documents|*.xml";
                }
                else
                {
                    saveFileDialog.FileName = txtFileName;
                    saveFileDialog.Title = "Save As...";
                    saveFileDialog.DefaultExt = @".txt";
                    saveFileDialog.Filter = @"Text Documents|*.txt";
                }

                // Set dialog path
                // If a path was already set from previous actions set it
                if (!string.IsNullOrEmpty(MainForm.SaveFilePath) && Directory.Exists(MainForm.SaveFilePath))
                {
                    saveFileDialog.InitialDirectory = MainForm.SaveFilePath;
                }
                else if (Directory.Exists(GetDataPath()))
                {
                    saveFileDialog.InitialDirectory = GetDataPath();
                }

                // OpenFilePath if OpenFilePath.lengh > 0 validate if path exists and set the file dialog on it...
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get file path with file name
                    tempSaveFilePathAndFileName = saveFileDialog.FileName;

                    // Save filepath without filename
                    tempSaveFilePathWithoutFileName = tempSaveFilePathAndFileName.Substring(0, tempSaveFilePathAndFileName.LastIndexOf('\\'));
                    MainForm.SaveFilePath = tempSaveFilePathWithoutFileName;
                    MainForm.SettingsHasChanges = true;
                    SavePoolFromDialogSuccess = true;
                }
                else
                {
                    SavePoolFromDialogSuccess = false;
                    tempSaveFilePathAndFileName = "";
                    tempSaveFilePathWithoutFileName = "";
                }

                // Return path
                if (returnPathWithoutFileName)
                {
                    return tempSaveFilePathWithoutFileName;
                }
                else
                {
                    return tempSaveFilePathAndFileName;
                }
            }
            catch
            {
                SavePoolFromDialogSuccess = false;
                tempSaveFilePathAndFileName = "";
                tempSaveFilePathWithoutFileName = "";
                Log.LogSaveFileAsError();
                return tempSaveFilePathAndFileName;
            }

        }
        #endregion

        #region SAVE AS .TXT
        private static string TextDocumentFilePath { get; set; }
        private static List<string> TextDocumentList { get; set; }
        internal static bool SaveTextDocument(string text, string path)
        {
            if (text != null && path != null && path.Length > 0)
            {
                if (TextDocumentList == null)
                {
                    TextDocumentList = new List<string>();
                }
                TextDocumentList.Clear();
                TextDocumentFilePath = path;
                StringBuilder textContent = new StringBuilder();
                string stringData = "";
                // Add content to string (tested with large documents of over 1.000.000 chars, it's fast)
                TextDocumentList.AddRange(text.Split('\n'));
                for (int i = 0; i < TextDocumentList.Count; i++)
                {
                    textContent.AppendLine(TextDocumentList[i]);
                }
                stringData = textContent.ToString();
                try
                {
                    // Main
                    if (!WriteTextFileAtomically(TextDocumentFilePath, stringData))
                    {
                        throw new IOException("Unable to write " + TextDocumentFilePath);
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region STRING CLEANING
        public static string RemoveIllegalXMLTags(string text)
        {
            string tempString = "";
            if (!string.IsNullOrEmpty(text))
            {
                // Insert ahoy replacer tag
                tempString = text.Replace("]]", "ahoyreplace1");
                tempString = tempString.Replace("<!\\[CDATA", "ahoyreplace2");
                tempString = CleanInvalidXmlChars(tempString);
            }
            return tempString;
        }

        public static string RestoreIllegalXMLTags(string text)
        {
            string tempString = "";
            if (!string.IsNullOrEmpty(text))
            {
                // Replace ahoy tags with original content
                tempString = text.Replace("ahoyreplace1", "]]");
                tempString = tempString.Replace("ahoyreplace2", "<!\\[CDATA");
            }
            return tempString;
        }

        /*
         * It tells the regex to find everything that doesn't match, instead of everything that does match.
         * The \u####-\u#### says which characters match.\u0000-\u007F is the equivilent of the first 255 characters
         * in utf-8 or unicode, which are always the ascii characters.
         * So you match every non ascii character (because of the not) and do a replace on everything that matches.
         * \u0000-\u00FF will include all the latin accent letters as well
         * Other symbols to be included:
         * Euro (u20AC)
         * Trademark (u2122)
         */

        // Keeps Latin-1 letters and the Euro/Trademark symbols
        public static string CleanInvalidXmlChars(string text)
        {
            // Keep only characters allowed by the XML spec:
            // #x9 | #xA | #xD | [#x20-#xD7FF] | [#xE000-#xFFFD] | [#x10000-#x10FFFF]
            // Characters above #xFFFF (emoji etc.) are UTF-16 surrogate pairs and are kept when well-formed.
            // (The previous regex dropped every surrogate pair, i.e. all emoji.)
            var cleaned = new StringBuilder(text.Length);
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsHighSurrogate(c) && i + 1 < text.Length && XmlConvert.IsXmlSurrogatePair(text[i + 1], c))
                {
                    cleaned.Append(c).Append(text[i + 1]);
                    i++;
                }
                else if (XmlConvert.IsXmlChar(c))
                {
                    cleaned.Append(c);
                }
            }
            return cleaned.ToString();
        }

        #endregion

        #region DummyEntry text reader
        private static string DummyText;
        public static string ReadDummyText(bool reset)
        {
            if (reset)
            {
                DummyText = string.Empty;
            }

            if (string.IsNullOrEmpty(DummyText))
            {
                StreamReader reader1, reader2, reader3;
                // Load embedded file
                reader1 = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.TextFiles.QuickGuideTop1.txt"));
                reader2 = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.TextFiles.QuickGuideTop2.txt"));
                reader3 = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.TextFiles.QuickGuideShortcutPreview.txt"));

                try
                {
                    // Read top logo + introduction
                    if (MainForm.DisplayTextCycle == 0)
                    {
                        // Read standard top
                        while (reader1.Peek() != -1)
                        {
                            DummyText += reader1.ReadLine() + Environment.NewLine;
                        }
                    }
                    else if (MainForm.DisplayTextCycle == 1)
                    {
                        // Read alternate top
                        while (reader2.Peek() != -1)
                        {

                            DummyText += reader2.ReadLine() + Environment.NewLine;
                        }
                    }

                    // Set version number by replacing the string "version"
                    DummyText = DummyText.Replace("version", Splash.AhoyVersionNumber);

                    // Only add shortcuts if homescreen 0 and 1
                    if (MainForm.DisplayTextCycle >= 0 && MainForm.DisplayTextCycle <= 1)
                    {
                        // Read shortcuts
                        DummyText += TextTransform.GetNewLines(2);
                        while (reader3.Peek() != -1)
                        {
                            DummyText += reader3.ReadLine() + Environment.NewLine;
                        }
                    }
                }
                catch
                {
                    QuickGuideText1 = "Error reading embedded file";
                    return DummyText;
                }
                finally
                {
                    // always close all readers when finished
                    reader1.Close();
                    reader2.Close();
                    reader3.Close();
                }
            }

            if (MainForm.IsPaused)
            {
                // Autosave status
                DummyText = DummyText.Replace("asave", GetPausedStatus());
                // Local backup status (Always on)
                DummyText = DummyText.Replace("lbak", GetPausedStatus());
                // Remote backup status
                DummyText = DummyText.Replace("rbak", GetPausedStatus());
            }
            else
            {
                // Autosave status
                DummyText = DummyText.Replace("asave", GetOnOffStatus(MainForm.AutoSaveEntryChanges));
                // Local backup status (Always on)
                DummyText = DummyText.Replace("lbak", "ON");
                // Remote backup status
                DummyText = DummyText.Replace("rbak", GetOnOffStatus(MainForm.AlternateBackupPathSet));
            }
            // Pool status
            DummyText = DummyText.Replace("prw", GetReadWriteStatus(MainForm.PoolIsReadOnly));
            // Pool name
            DummyText = DummyText.Replace("plname", GetCurrentPoolFileNameForDisplay(true));

            // Return dummy text
            return DummyText;
        }

        private static string GetPausedStatus()
        {
            return "N/A";
        }

        private static string GetOnOffStatus(bool status)
        {
            if (status)
            {
                return "ON";
            }
            else
            {
                return "OFF";
            }
        }

        private static string GetReadWriteStatus(bool status)
        {
            if (status)
            {
                return "READ-ONLY";
            }
            else
            {
                return "Read/Write";
            }
        }
        #endregion

        #region QuickGuide text reader
        // Variables to hold the default display text for future reads to prevent reading the file multiple times
        private static string QuickGuideText1;
        private static string QuickGuideText3;
        private static string QuickGuideText4;
        private static string QuickGuideText5;
        public static string ReadQuickGuideText()
        {
            if (string.IsNullOrEmpty(QuickGuideText1))
            {
                StreamReader reader1, reader3, reader4, reader5, reader10;
                // Load embedded dictionary file
                reader1 = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.TextFiles.QuickGuideTop3.txt"));
                reader3 = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.TextFiles.QuickGuideSectionB.txt"));
                reader4 = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.TextFiles.QuickGuideSectionC.txt"));
                reader5 = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.TextFiles.QuickGuideSectionD.txt"));
                reader10 = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.TextFiles.QuickGuideTop2.txt"));

                try
                {
                    // Read top logo + introduction
                    if (MainForm.DisplayTextCycle == 0)
                    {
                        // Read standard top
                        while (reader1.Peek() != -1)
                        {
                            QuickGuideText1 += reader1.ReadLine() + Environment.NewLine;
                        }
                    }
                    else
                    {
                        // Read alternate top
                        while (reader10.Peek() != -1)
                        {

                            QuickGuideText1 += reader10.ReadLine() + Environment.NewLine;
                        }
                    }

                    // Set version number by replacing the string "version" removed in v1501
                    // QuickGuideText1 = QuickGuideText1.Replace("version", Splash.AhoyVersionNumber);

                    // Read next section
                    while (reader3.Peek() != -1)
                    {
                        QuickGuideText3 += reader3.ReadLine() + Environment.NewLine;
                    }

                    // Get commands with description
                    QuickGuideText3 += CommandInput.GetAllCommands();

                    // Read next section
                    while (reader4.Peek() != -1)
                    {
                        QuickGuideText4 += reader4.ReadLine() + Environment.NewLine;
                    }

                    // Read next section
                    while (reader5.Peek() != -1)
                    {
                        QuickGuideText5 += reader5.ReadLine() + Environment.NewLine;
                    }

                    // Add privacy statement and license text
                    QuickGuideText5 += Environment.NewLine + Environment.NewLine + SoftwareLicenseTextReader() + Environment.NewLine;

                    return QuickGuideText1 + QuickGuideText3 + QuickGuideText4 + QuickGuideText5;
                }
                catch
                {
                    QuickGuideText1 = "Error reading documentation.";
                    return QuickGuideText1;
                }
                finally
                {
                    // always close all readers when finished
                    reader1.Close();
                    reader3.Close();
                    reader4.Close();
                    reader5.Close();
                    reader10.Close();
                }
            }
            else // Else if the text was already loaded, return the contents
            {

                return QuickGuideText1 + QuickGuideText3 + QuickGuideText4 + QuickGuideText5;
            }
        }
        #endregion

        #region Password info text reader
        public static string PasswordInfoTextReader()
        {
            StreamReader reader;
            // Load embedded dictionary file
            reader = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.TextFiles.PasswordInfo.txt"));

            try
            {
                string tmpContent = "";
                while (reader.Peek() != -1)
                {
                    tmpContent += reader.ReadLine() + Environment.NewLine;
                }

                return tmpContent;
            }
            catch
            {
                return "";
            }
            finally
            {
                // always close the reader when finished
                reader.Close();
            }
        }
        #endregion

        #region Paragraph Dividers info text reader
        public static string ParagraphDividersHelpTextReader()
        {
            StreamReader reader;
            // Load embedded dictionary file
            reader = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.TextFiles.ParagraphDividersHelp.txt"));

            try
            {
                string tmpContent = "";
                while (reader.Peek() != -1)
                {
                    tmpContent += reader.ReadLine() + Environment.NewLine;
                }

                return tmpContent;
            }
            catch
            {
                return "";
            }
            finally
            {
                // always close the reader when finished
                reader.Close();
            }
        }
        #endregion


        #region Software License text reader
        public static string SoftwareLicenseTextReader()
        {
            StreamReader reader;
            // Load embedded dictionary file
            reader = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.TextFiles.License.txt"));

            try
            {
                string tmpContent = "";

                while (reader.Peek() != -1)
                {
                    tmpContent += reader.ReadLine() + Environment.NewLine;
                }

                return tmpContent;
            }
            catch
            {
                return "";
            }
            finally
            {
                // always close the reader when finished
                reader.Close();
            }
        }
        #endregion


        #region Command reference text
        private static string tempHeadline;
        private static string tempBaseline;
        private static int tempLeftMargin;
        private static int tempLeftMarginCalculated;
        private static int tempInputLength;
        public static string GetHeadlineFormatted(string input)
        {
            tempHeadline = "";
            if (!string.IsNullOrEmpty(input) && input.Length > 0)
            {
                tempLeftMargin = 16;
                tempBaseline = new string(' ', 25); // width used to centre the heading (no underline is drawn)
                tempInputLength = input.Trim().Length;

                // If headline is shorter or equal to the baseline string
                // Remaining chars / 2, result / 2 use whole numbers
                if (tempInputLength <= tempBaseline.Length)
                {
                    // If input has same or less length as baseline
                    if (tempBaseline.Length - tempInputLength <= 0)
                    {
                        tempLeftMarginCalculated = 0;
                    }
                    else
                    {
                        tempLeftMarginCalculated = ((tempBaseline.Length - tempInputLength) / 2);
                        tempLeftMarginCalculated += tempLeftMargin;
                    }

                    tempHeadline = TextTransform.GetNewLines(2) +
                                    TextTransform.GetBlankSpace(tempLeftMarginCalculated) + input.Trim().ToUpper() + Environment.NewLine;
                    tempHeadline += TextTransform.GetNewLines(2);

                }// If headline is longer than headline string
                else if (tempInputLength > tempBaseline.Length)
                {
                    tempHeadline = "N/A";
                }
            }
            return tempHeadline;
        }

        public static string CommandReferenceReader()
        {
            StreamReader reader;
            // Load embedded dictionary file
            reader = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.TextFiles.QuickGuideSectionC.txt"));

            try
            {
                string tmpContent;
                // Set topbar
                tmpContent = CommandInput.GetAllCommands();

                while (reader.Peek() != -1)
                {
                    tmpContent += reader.ReadLine() + Environment.NewLine;
                }

                return tmpContent;
            }
            catch
            {
                return "";
            }
            finally
            {
                // always close the reader when finished
                reader.Close();
            }
        }
        #endregion

        #region Style Editor
        public static string StyleEditorTextReader()
        {
            StreamReader reader;
            // Load embedded dictionary file
            reader = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.TextFiles.StyleEditor.txt"));

            try
            {
                string tmpContent = "";

                while (reader.Peek() != -1)
                {
                    tmpContent += reader.ReadLine() + Environment.NewLine;
                }

                return tmpContent;
            }
            catch
            {
                return "";
            }
            finally
            {
                // always close the reader when finished
                reader.Close();
            }
        }
        #endregion

        #region Style Editor (List field)
        public static string StyleEditorListFieldTextReader()
        {
            StreamReader reader;
            // Load embedded dictionary file
            reader = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.TextFiles.StyleEditorList.txt"));

            try
            {
                string tmpContent = "";

                while (reader.Peek() != -1)
                {
                    tmpContent += reader.ReadLine() + Environment.NewLine;
                }

                return tmpContent;
            }
            catch
            {
                return "";
            }
            finally
            {
                // always close the reader when finished
                reader.Close();
            }
        }
        #endregion

        #region Additional About Information
        public static string AdditionalAboutInformationTextReader()
        {
            StreamReader reader;
            // Load embedded dictionary file
            reader = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.TextFiles.About.txt"));

            try
            {
                string tmpContent = "";

                while (reader.Peek() != -1)
                {
                    tmpContent += reader.ReadLine() + Environment.NewLine;
                }

                return tmpContent;
            }
            catch
            {
                return "";
            }
            finally
            {
                // always close the reader when finished
                reader.Close();
            }
        }
        #endregion

        #region ENTRY OPERATIONS
        public static Entry CloneEntry(Entry entryToBeCloned, bool includeTempProperties)
        {
            // Initialize new entry
            Entry newEntry = new Entry();
            // Entry variables
            newEntry.ID = entryToBeCloned.ID;
            newEntry.Title = entryToBeCloned.Title;
            newEntry.Content = entryToBeCloned.Content;
            newEntry.Created = entryToBeCloned.Created;
            newEntry.Changed = entryToBeCloned.Changed;
            newEntry.Bookmark = entryToBeCloned.Bookmark;
            newEntry.Pin = entryToBeCloned.Pin;
            newEntry.Rank = entryToBeCloned.Rank;
            newEntry.PresetColor = entryToBeCloned.PresetColor;
            newEntry.CustomColor = entryToBeCloned.CustomColor;
            newEntry.Protected = entryToBeCloned.Protected;
            newEntry.CaretPosition = entryToBeCloned.CaretPosition;
            newEntry.RelatedEntries = entryToBeCloned.RelatedEntries;
            newEntry.RelatedView = entryToBeCloned.RelatedView;
            newEntry.RelatedStyle = entryToBeCloned.RelatedStyle;
            // Temp properties to include
            if (includeTempProperties)
            {
                newEntry.MarkExport = entryToBeCloned.MarkExport;
            }
            else
            {
                newEntry.MarkExport = false;
            }
            // Temp variables
            newEntry.IgnoreTextChanged = false;
            newEntry.IsNewEntry = false;
            newEntry.HasChanges = false;
            newEntry.WasDeleted = false;
            newEntry.IsDummyEntry = false;
            newEntry.TempHistoryState = new List<HistoryState>();
            newEntry.MarkRelated = false;
            newEntry.DisplayCode = 0;
            if (entryToBeCloned.TempHistoryState != null && entryToBeCloned.TempHistoryState.Count > 0)
            {
                newEntry.TempHistoryState.AddRange(entryToBeCloned.TempHistoryState);
            }
            return newEntry;
        }

        public static EntryMinimal ConvertToMinimalEntry(Entry entryToBeCopied, bool usePrefix, string prefix)
        {
            EntryMinimal newEntry = new EntryMinimal(usePrefix, prefix);
            newEntry.ID = entryToBeCopied.ID;
            newEntry.Title = entryToBeCopied.Title;
            newEntry.PinRank = entryToBeCopied.Rank;
            newEntry.PresetColor = entryToBeCopied.PresetColor;
            newEntry.CustomColor = entryToBeCopied.CustomColor;
            newEntry.MarkExport = entryToBeCopied.MarkExport;
            newEntry.MarkRelated = entryToBeCopied.MarkRelated;
            newEntry.DisplayCode = entryToBeCopied.DisplayCode;
            newEntry.RelatedView = entryToBeCopied.RelatedView;
            newEntry.RelatedStyle = entryToBeCopied.RelatedStyle;
            return newEntry;
        }
        #endregion

        #region Import/Export Theme
        private static string ExportFilePath { get; set; }
        internal static bool ExportTheme(Settings settingsObject)
        {
            using var invariantCulture = InvariantCulture.Use(); // locale-independent file format
            if (settingsObject == null)
            {
                return false;
            }

            ExportFilePath = SaveFileDialog(2, settingsObject.ThemeTitle, false);
            if (ExportFilePath.Length > 0)
            {
                StringBuilder xmlContent = new StringBuilder();
                string stringData = "";

                // If any entries exist create xml file and save entries to it
                if (settingsObject != null)
                {
                    // Write doctype
                    xmlContent.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\" ?>");
                    xmlContent.AppendLine(@"<settings>");

                    // Write settings
                    xmlContent.AppendLine(@"    <setting>");
                    xmlContent.AppendLine(@"        <colortheme>" + settingsObject.ColorTheme.ToString() + "</colortheme>");
                    xmlContent.AppendLine(@"        <btnbovr>" + settingsObject.ButtonBitmapOverride.ToString() + "</btnbovr>");
                    xmlContent.AppendLine(@"        <opacity>" + settingsObject.Opacity.ToString() + "</opacity>");
                    xmlContent.AppendLine(@"        <texture>" + settingsObject.Texture.ToString() + "</texture>");
                    xmlContent.AppendLine(@"        <qgcycle>" + settingsObject.DisplayTextCycle.ToString() + "</qgcycle>");
                    xmlContent.AppendLine(@"        <custt><![CDATA[" + RemoveIllegalXMLTags(settingsObject.ThemeTitle) + "]]></custt>");
                    xmlContent.AppendLine(@"        <custc><![CDATA[" + RemoveIllegalXMLTags(settingsObject.CustomColors) + "]]></custc>");
                    xmlContent.AppendLine(@"    </setting>");
                    xmlContent.AppendLine(@"</settings>");


                    // Add xml content to string
                    stringData = xmlContent.ToString();


                    try
                    {
                        if (!WriteTextFileAtomically(ExportFilePath, stringData))
                        {
                            throw new IOException("Unable to write " + ExportFilePath);
                        }
                        return true;
                    }
                    catch
                    {
                        Log.LogExportThemeError();
                        return false;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }


        public static Settings ImportTheme(string path)
        {
            using var invariantCulture = InvariantCulture.Use(); // locale-independent file format
            // Prepare Settings object to return
            Settings settingsObject = new Settings();

            // File verification
            if (!File.Exists(path))
            {
                settingsObject.LoadSuccess = false;
                settingsObject.LoadMessage = "Settings unavailable";
                return settingsObject;
            }

            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader reader = new StreamReader(fileStream);
            StringBuilder xmlContent = new StringBuilder();
            string stringData = "";

            // Load the file to memory
            try
            {
                while (reader.Peek() != -1)
                {
                    xmlContent.AppendLine(reader.ReadLine());
                }

                // Add loaded xml to string variable
                stringData = xmlContent.ToString();
            }
            catch
            {
                settingsObject.LoadSuccess = false;
                settingsObject.LoadMessage = "Error reading XML file.";
                return settingsObject;
            }
            finally
            {
                reader.Close();
            }

            // Extract the XML from the string in memory
            try
            {
                TextReader textReader = new StringReader(stringData);
                XmlDocument document = new XmlDocument();
                document.Load(textReader);

                XmlNodeList settingsNodes;
                settingsNodes = document.SelectNodes("//settings/setting"); // XPath statement

                // Read settings
                foreach (XmlNode node in settingsNodes)
                {
                    settingsObject.ColorTheme = ReadColorTheme(node);
                    settingsObject.ButtonBitmapOverride = CheckNodeInt(node, "btnbovr", Settings.DefaultButtonBitmapOverride);
                    settingsObject.Opacity = CheckNodeDouble(node, "opacity", Settings.DefaultOpacity);
                    settingsObject.Texture = CheckNodeInt(node, "texture", Settings.DefaultTexture);
                    settingsObject.DisplayTextCycle = CheckNodeInt(node, "qgcycle", Settings.DefaultDisplayTextCycle);
                    settingsObject.ThemeTitle = RestoreIllegalXMLTags(CheckNodeString(node, "custt", Settings.DefaultThemeTitle));
                    settingsObject.CustomColors = RestoreIllegalXMLTags(CheckNodeString(node, "custc", Settings.DefaultCustomColors));
                }

                settingsObject.LoadSuccess = true;
                return settingsObject;
            }
            catch
            {
                settingsObject.LoadSuccess = false;
                settingsObject.LoadMessage = "Error reading loaded XML data.";
                return settingsObject;
            }
        }
        #endregion

        #region Import embedded theme file
        public static Settings ImportLocalTheme(int index)
        {
            using var invariantCulture = InvariantCulture.Use(); // locale-independent file format
            // Prepare Settings object to return
            Settings settingsObject = new Settings();

            StreamReader reader;
            // Load embedded file
            switch (index)
            {
                case AhoyColors.ThemeLight:
                    reader = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.ColorThemes.Light.xml"));
                    break;
                case AhoyColors.ThemeBlue:
                    reader = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.ColorThemes.Blue.xml"));
                    break;
                default:
                    reader = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.ColorThemes.Dark.xml"));
                    break;
            }
            
            StringBuilder xmlContent = new StringBuilder();
            string stringData = "";

            // Load the file to memory
            try
            {
                while (reader.Peek() != -1)
                {
                    xmlContent.AppendLine(reader.ReadLine());
                }

                // Add loaded xml to string variable
                stringData = xmlContent.ToString();
            }
            catch
            {
                settingsObject.LoadSuccess = false;
                settingsObject.LoadMessage = "Error reading XML file.";
                return settingsObject;
            }
            finally
            {
                reader.Close();
            }

            // Extract the XML from the string in memory
            try
            {
                TextReader textReader = new StringReader(stringData);
                XmlDocument document = new XmlDocument();
                document.Load(textReader);

                XmlNodeList settingsNodes;
                settingsNodes = document.SelectNodes("//settings/setting"); // XPath statement

                // Read settings
                foreach (XmlNode node in settingsNodes)
                {
                    settingsObject.ColorTheme = ReadColorTheme(node);
                    settingsObject.ButtonBitmapOverride = CheckNodeInt(node, "btnbovr", Settings.DefaultButtonBitmapOverride);
                    settingsObject.Opacity = CheckNodeDouble(node, "opacity", Settings.DefaultOpacity);
                    settingsObject.Texture = CheckNodeInt(node, "texture", Settings.DefaultTexture);
                    settingsObject.DisplayTextCycle = CheckNodeInt(node, "qgcycle", Settings.DefaultDisplayTextCycle);
                    settingsObject.ThemeTitle = RestoreIllegalXMLTags(CheckNodeString(node, "custt", Settings.DefaultThemeTitle));
                    settingsObject.CustomColors = RestoreIllegalXMLTags(CheckNodeString(node, "custc", Settings.DefaultCustomColors));
                }

                settingsObject.LoadSuccess = true;
                return settingsObject;
            }
            catch
            {
                settingsObject.LoadSuccess = false;
                settingsObject.LoadMessage = "Error reading loaded XML data.";
                return settingsObject;
            }
        }
        #endregion

        #region Import/Export entries
        // Export Collection
        internal static bool ExportCollection(List<Entry> exportCollection, string exportFilePath)
        {
            using var invariantCulture = InvariantCulture.Use(); // locale-independent file format
            StringBuilder xmlContent = new StringBuilder();

            // If any entries exist create xml file and save entries to it
            if (exportCollection.Count >= 0)
            {
                // Write doctype
                xmlContent.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\" ?>");
                xmlContent.AppendLine(@"<entries>");

                foreach (Entry entry in exportCollection)
                {
                    // Write entries to XML file
                    xmlContent.AppendLine(@"    <entry>");
                    xmlContent.AppendLine(@"        <id>AUTOMATIC</id>");
                    xmlContent.AppendLine(@"        <created>" + entry.Created + "</created>");
                    xmlContent.AppendLine(@"        <changed>" + entry.Changed + "</changed>");
                    xmlContent.AppendLine(@"        <pinned>" + entry.Pin.ToString() + "</pinned>");
                    xmlContent.AppendLine(@"        <pinnedrank>" + entry.Rank.ToString() + "</pinnedrank>");
                    xmlContent.AppendLine(@"        <presetcolor>" + entry.PresetColor.ToString() + "</presetcolor>");
                    xmlContent.AppendLine(@"        <customcolor><![CDATA[" + entry.CustomColor + "]]></customcolor>");
                    xmlContent.AppendLine(@"        <protected>" + entry.Protected.ToString() + "</protected>");
                    xmlContent.AppendLine(@"        <caretposition>" + entry.CaretPosition.ToString() + "</caretposition>");
                    xmlContent.AppendLine(@"        <bookmark>" + entry.Bookmark.ToString() + "</bookmark>");
                    xmlContent.AppendLine(@"        <title><![CDATA[" + RemoveIllegalXMLTags(entry.Title) + "]]></title>");
                    xmlContent.AppendLine(@"        <content><![CDATA[" + RemoveIllegalXMLTags(entry.Content) + "]]></content>");
                    xmlContent.AppendLine(@"    </entry>");
                    Log.LogImportExportStatus("Entry", "exported");
                }
                xmlContent.AppendLine(@"</entries>");


                return WriteTextFileAtomically(exportFilePath, xmlContent.ToString());
            }
            return false;
        }


        // Import Collection
        public static List<Entry> ImportCollection(string importFilePath)
        {
            using var invariantCulture = InvariantCulture.Use(); // locale-independent file format
            // A new list per import (callers keep the result)
            var tempImportList = new List<Entry>();

            FileStream fileStream = null;
            StreamReader reader = null;
            try
            {
                fileStream = new FileStream(importFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                reader = new StreamReader(fileStream);
            }
            catch
            {
                return tempImportList;
            }

            StringBuilder xmlContent = new StringBuilder();

            // Load the file to memory
            try
            {
                while (reader.Peek() != -1)
                {
                    xmlContent.AppendLine(reader.ReadLine());
                }
            }
            catch
            {
                return tempImportList;
            }
            finally
            {
                reader.Close();
            }

            // Extract the XML from the string in memory
            try
            {
                TextReader textReader = new StringReader(xmlContent.ToString());

                XmlDocument document = new XmlDocument();
                document.Load(textReader);

                XmlNodeList nodes;
                nodes = document.SelectNodes("//entries/entry"); // XPath statement

                // Read entries
                foreach (XmlNode node in nodes)
                {
                    Entry newEntry = new Entry();
                    newEntry.ID = Guid.NewGuid().ToString(); // Create a new GUID for imported entries...
                    newEntry.Created = EnforceSemicolons(node["created"].InnerText);
                    newEntry.Changed = EnforceSemicolons(node["changed"].InnerText);
                    newEntry.Pin = Convert.ToBoolean(node["pinned"].InnerText);
                    newEntry.Rank = CheckNodeInt(node, "pinnedrank", DefaultRank);
                    newEntry.PresetColor = CheckNodeInt(node, "presetcolor", DefaultStatus);
                    newEntry.CustomColor = CheckNodeString(node, "customcolor", string.Empty);
                    newEntry.Protected = Convert.ToBoolean(node["protected"].InnerText);
                    newEntry.CaretPosition = InvariantCulture.ParseInt(node["caretposition"].InnerText);
                    newEntry.Bookmark = CheckNodeInt(node, "bookmark", DefaultStatus);
                    newEntry.RelatedEntries = string.Empty;
                    newEntry.RelatedView = false;
                    newEntry.RelatedStyle = 0;
                    newEntry.Title = RestoreIllegalXMLTags(node["title"].InnerText);
                    Log.LogImportExportStatus("Entry", "imported");
                    newEntry.Content = RestoreIllegalXMLTags(node["content"].InnerText);
                    tempImportList.Add(newEntry);
                }

                // Validate DateTime fields in all imported entries
                if (!ValidateImport(tempImportList))
                {
                    AhoySimpleMessageBox msgDateTimeFormatError = new AhoySimpleMessageBox("Invalid DateTime format.\n(Please reformat as yyyy-mm-dd hh:mm:ss)", MessageText.ImportCaption(), false);
                    msgDateTimeFormatError.ShowDialog();
                    return null;
                }

                return tempImportList;
            }
            catch
            {
                return new List<Entry>(); // a damaged file imports nothing, not a partial list
            }
        }

        // Validate DateTime format (ISO)
        private static bool ValidateImport(List<Entry> list)
        {
            if (list != null && list.Count > 0)
            {
                foreach (Entry e in list)
                {
                    if (!IsValidISODateTimeFormat(e.Created) ||
                        !IsValidISODateTimeFormat(e.Changed))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion

        #region Init File

        internal static void InitializeInitProperties()
        {
            PoolSessionChanged = "N/A";
            PoolSessionId = string.Empty;
            CurrentSessionId = string.Empty;
        }

        internal static string RenewSessionID()
        {
            CurrentSessionId = Guid.NewGuid().ToString();
            return CurrentSessionId;
        }

        internal static bool SessionIDMatch()
        {
            // If both local and pool session IDs are present test for match
            if (!string.IsNullOrEmpty(CurrentSessionId) && !string.IsNullOrEmpty(PoolSessionId))
            {
                if (CurrentSessionId == PoolSessionId)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }// If local session ID is present but pool session ID is empty, return true (could be an older pool with no session ID)
            else if (!string.IsNullOrEmpty(CurrentSessionId) && string.IsNullOrEmpty(PoolSessionId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Call this when starting Ahoy
        internal static void InitializeInitFile()
        {
            if (!InitFileExists())
            {
                CreateInitFile("");
            }
            else
            {
                LoadInitFile();
            }
        }

        internal static void SaveInitFile(string path)
        {
            using var invariantCulture = InvariantCulture.Use(); // locale-independent file format
            CreateInitFile(path);
        }

        private static bool InitFileExists()
        {
            try
            {
                Log.LogInitCheck();
                if (File.Exists(GetInitPathAndFileName()))
                {
                    Log.LogInitValidated();
                    return true;
                }
                else
                {
                    Log.LogInitMissing();
                    return false;
                }
            }
            catch
            {
                Log.LogInitError("File check");
                return false;
            }
        }

        private static string GetInitPathAndFileName()
        {
            try
            {
                return GetDefaultDataPath() + "\\" + InitFileName;
            }
            catch
            {
                Log.LogInitError("File path");
                return "";
            }
        }

        // Load init and set fields
        private static string tempCustomInitFilePath;
        private static void LoadInitFile()
        {
            using var invariantCulture = InvariantCulture.Use(); // locale-independent file format
            try
            {
                CurrentSessionId = "";
                tempCustomInitFilePath = "";
                FileStream fileStream = new FileStream(GetInitPathAndFileName(), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader reader = new StreamReader(fileStream);
                StringBuilder xmlContent = new StringBuilder();
                string stringData = "";

                // Load the file to memory
                try
                {
                    while (reader.Peek() != -1)
                    {
                        xmlContent.AppendLine(reader.ReadLine());
                    }
                    // Add loaded xml to string variable
                    stringData = xmlContent.ToString();
                }
                catch
                {
                    Log.LogInitError("Reading file");
                    return;
                }
                finally
                {
                    reader.Close();
                }

                // Extract the XML from the string in memory
                try
                {
                    TextReader textReader = new StringReader(stringData);
                    XmlDocument document = new XmlDocument();
                    document.Load(textReader);

                    XmlNodeList settingsNodes;
                    settingsNodes = document.SelectNodes("//init/data"); // XPath statement

                    // Read settings
                    foreach (XmlNode node in settingsNodes)
                    {
                        CurrentSessionId = node["session"].InnerText;
                        tempCustomInitFilePath = node["path"].InnerText;
                    }

                    if (!string.IsNullOrEmpty(tempCustomInitFilePath) && tempCustomInitFilePath.Length > 0 && File.Exists(tempCustomInitFilePath) && tempCustomInitFilePath.Contains(PoolFileExtension))
                    {
                        Log.LogInitLoaded("Path validated");
                        CustomFilePath = tempCustomInitFilePath.Substring(0, tempCustomInitFilePath.LastIndexOf('\\'));
                        CustomFileName = tempCustomInitFilePath.Substring(tempCustomInitFilePath.LastIndexOf('\\'), tempCustomInitFilePath.Length - (tempCustomInitFilePath.Substring(0, tempCustomInitFilePath.LastIndexOf('\\')).Length));
                        CustomFileName = CustomFileName.Replace("\\", "");
                        UseCustomFilePath = true;
                    }
                    else
                    {
                        Log.LogInitLoaded("Path N/A");
                        CreateInitFile(""); // Save init file with empty path if N/A
                    }
                }
                catch
                {
                    Log.LogInitError("Loading file");
                }
            }
            catch
            {
                Log.LogInitError("File load");
            }
        }


        private static string tempInitGUID;
        private static string tempInitPath;
        private static string tempInitFileNameAndPath;
        private static void CreateInitFile(string path)
        {
            try
            {
                Log.LogInitCreate();
                if (!Directory.Exists(GetDefaultDataPath()))
                {
                    Log.LogInitDirectoryError();
                    return;
                }

                // Create session guid if the value is empty
                if (string.IsNullOrEmpty(CurrentSessionId))
                {
                    tempInitGUID = Guid.NewGuid().ToString();
                    CurrentSessionId = tempInitGUID;
                }
                else
                {
                    tempInitGUID = CurrentSessionId;
                }

                // Set init path
                if (!string.IsNullOrEmpty(path) && path.Length > 0)
                {
                    tempInitPath = path;
                    tempInitFileNameAndPath = tempInitPath;
                }
                else
                {
                    tempInitPath = "";
                    // Create filename and path
                    tempInitFileNameAndPath = tempInitPath;
                }


                StringBuilder xmlContent = new StringBuilder();
                string stringData = "";
                // If any entries exist create xml file and save entries to it

                // Write doctype
                xmlContent.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\" ?>");
                xmlContent.AppendLine(@"<!--");
                xmlContent.AppendLine(@"**********************************************************************");
                xmlContent.AppendLine(@"* CAUTION: Do not alter this file unless you have an exact knowledge *");
                xmlContent.AppendLine(@"*          of the consequences.                                      *");
                xmlContent.AppendLine(@"**********************************************************************");
                xmlContent.AppendLine(@"-->");
                xmlContent.AppendLine(@"<init>");

                // Write settings
                xmlContent.AppendLine(@"    <data>");
                xmlContent.AppendLine(@"        <session>" + tempInitGUID + "</session>");
                xmlContent.AppendLine(@"        <path><![CDATA[" + tempInitFileNameAndPath + "]]></path>");
                xmlContent.AppendLine(@"        <changed>" + GetIsoDateTimeNow() + "</changed>");
                xmlContent.AppendLine(@"    </data>");
                xmlContent.AppendLine(@"</init>");


                // Add xml content to string
                stringData = xmlContent.ToString();


                try
                {
                    if (!WriteTextFileAtomically(GetInitPathAndFileName(), stringData))
                    {
                        throw new IOException("Unable to write " + GetInitPathAndFileName());
                    }
                    Log.LogInitCreated();
                }
                catch
                {
                    Log.LogInitError("File create");
                }
            }
            catch
            {
                Log.LogInitError("Unable to create");
            }
        }
        #endregion


    }
}
