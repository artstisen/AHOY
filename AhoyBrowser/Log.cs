using System;
using System.Collections.Generic;

namespace AhoyBrowser
{
    class Log
    {
        // Log entries list
        private static List<string> logList { get; set; }
        private static List<string> logListPreLoad { get; set; }
        private const int LogMax = 2500; // Max log count

        // Initialize log
        internal static void InitializeLog()
        {
            if (logList == null)
            {
                logList = new List<string>();
                logListPreLoad = new List<string>();
            }
        }

        internal static bool ContainsEntries()
        {
            if (logList != null && logList.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static int GetLogEntriesCount()
        {
            if (ContainsEntries())
            {
                return logList.Count;
            }
            else
            {
                return 0;
            }
        }

        internal static string GetLogEntriesCountAsString()
        {
            if (ContainsEntries())
            {
                return logList.Count.ToString() + "/" + LogMax.ToString();
            }
            else
            {
                return "N/A";
            }
        }

        private static string tempLogContent;
        internal static string GetLogForDisplay()
        {
            if (ContainsEntries())
            {
                tempLogContent = "";
                try
                {
                    for (int i = 0; i < logList.Count; i++)
                    {
                        tempLogContent += logList[i] + Environment.NewLine;
                    }
                }
                catch
                {
                    tempLogContent += "N/A";
                }
                tempLogContent += Environment.NewLine + logList.Count.ToString() + "/" + LogMax.ToString() + " events logged." + Environment.NewLine +
                    "Please consult the documentation for more information about this log." + Environment.NewLine;
                return tempLogContent;
            }
            else
            {
                return "No data.";
            }
        }

        internal static string GetLogLines(int lines)
        {
            if (ContainsEntries())
            {
                tempLogContent = "";
                if (logList.Count > lines)
                {
                    for (int i = logList.Count - lines; i < logList.Count; i++)
                    {
                        tempLogContent += logList[i] + Environment.NewLine;
                    }
                }
                else
                {
                    for (int i = 0; i < logList.Count; i++)
                    {
                        tempLogContent += logList[i] + Environment.NewLine;
                    }
                }

                return tempLogContent;
            }
            else
            {
                return "No data.";
            }
        }


        internal static string GetLogAsSaveData()
        {
            if (ContainsEntries())
            {
                tempLogContent = "";

                foreach (string s in logList)
                {
                    tempLogContent += s + "\n";
                }
                return tempLogContent;
            }
            else
            {
                return "No data.";
            }
        }

        internal static void SetLogFromSaveData(string logContent)
        {
            if (logList != null)
            {
                logList.Clear();
                if (!string.IsNullOrEmpty(logContent))
                {
                    logList.AddRange(logContent.Split('\n'));
                }
                // Ensure to append the log for the events happened before loading the log
                CollectPreLoadLog();
            }
        }

        // Ad any entries added to the pre-log before the main log was loaded
        // from disk
        internal static void CollectPreLoadLog()
        {
            if (logListPreLoad.Count > 0)
            {
                foreach (string s in logListPreLoad)
                {
                    logList.Add(s);
                }

                logListPreLoad.Clear();
            }
        }

        internal static bool ClearLog()
        {
            if (logList != null)
            {
                logList.Clear();
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string tempSpace;
        private static string GetSpace()
        {
            tempSpace = "";

            for (int i = 0; i < 2; i++)
            {
                tempSpace += " ";
            }
            return tempSpace;
        }

        // Keep the log entry count below or equal to LogMax
        private static void RunLogMaintenance()
        {
            if (logList.Count > LogMax)
            {
                while (logList.Count > LogMax)
                {
                    logList.RemoveAt(0);
                }
            }
        }

        // Get timestamp and indicate the pool has changes
        // while the Log was updated
        private static string GetTimeStamp(bool invokeSave)
        {
            if (invokeSave)
            {
                MainForm.SetPoolHasChanges();
            }
            return IO.GetIsoDateTimeNow() + GetSpace();
        }


        // ----------------------- BELOW IS PRELOAD LOGS ----------------------

        // Preload log methods
        /// <summary>
        /// Preload
        /// </summary>
        public static void LogProgramStartup()
        {
            logListPreLoad.Add(GetTimeStamp(false) + "Startup (" + Splash.AhoyVersionNumber + ")");
        }

        /// <summary>
        /// Preload
        /// </summary>
        public static void LogLoadPool()
        {
            logListPreLoad.Add(GetTimeStamp(false) + "Pool: Load");
        }

        /// <summary>
        /// Preload
        /// </summary>
        public static void LogLegacyBackupPathUpdate() // Transfer backup URL to settings file
        {
            logListPreLoad.Add(GetTimeStamp(false) + "Pool: Legacy check...");
        }

        /// <summary>
        /// Preload
        /// </summary>
        public static void LogLoadPoolFromFilePath()
        {
            logListPreLoad.Add(GetTimeStamp(false) + "Pool: Open file dialog");
            RunLogMaintenance();
        }

        /// <summary>
        /// Preload
        /// </summary>
        public static void LogInitCheck()
        {
            logListPreLoad.Add(GetTimeStamp(false) + "Init: Check");
        }

        /// <summary>
        /// Preload
        /// </summary>
        public static void LogInitLoaded(string state)
        {
            logListPreLoad.Add(GetTimeStamp(false) + "Init: Loaded (" + state + ")");
        }

        /// <summary>
        /// Preload
        /// </summary>
        public static void LogInitValidated()
        {
            logListPreLoad.Add(GetTimeStamp(false) + "Init: Validated");
        }

        /// <summary>
        /// Preload
        /// </summary>
        public static void LogInitMissing()
        {
            logListPreLoad.Add(GetTimeStamp(false) + "Init: N/A");
        }

        /// <summary>
        /// Preload
        /// </summary>
        public static void LogInitError(string info)
        {
            logListPreLoad.Add(GetTimeStamp(false) + "Init: Error (" + info + ")");
        }

        /// <summary>
        /// Preload
        /// </summary>
        public static void LogInitDirectoryError()
        {
            logListPreLoad.Add(GetTimeStamp(false) + "Init: Directory error");
        }

        /// <summary>
        /// Preload
        /// </summary>
        public static void LogInitCreate()
        {
            logListPreLoad.Add(GetTimeStamp(false) + "Init: Update");
        }

        /// <summary>
        /// Preload
        /// </summary>
        public static void LogInitCreated()
        {
            logListPreLoad.Add(GetTimeStamp(false) + "Init: Saved");
        }

        /// <summary>
        /// Preload
        /// </summary>
        public static void LogPreloadLoadSettings()
        {
            logListPreLoad.Add(GetTimeStamp(false) + "Settings: Load");
        }

        /// <summary>
        /// Preload
        /// </summary>
        public static void LogPreloadLoadSettingsError(string message)
        {
            logList.Add(GetTimeStamp(false) + "Error: " + message);
            RunLogMaintenance();
        }

        /// <summary>
        /// Preload
        /// </summary>
        public static void LogPreloadStatus(string status)
        {
            logListPreLoad.Add(GetTimeStamp(false) + "Status: " + status);
        }

        public static void LogPreloadThemeError()
        {
            logListPreLoad.Add(GetTimeStamp(false) + "Error: Unable to process theme data");
            RunLogMaintenance();
        }



        // ----------------------- ABOVE IS PRELOAD LOGS ----------------------


        public static void LogSessionIDDeviation()
        {
            logList.Add(GetTimeStamp(false) + "Init: Session ID deviation");
            RunLogMaintenance();
        }

        public static void LogSessionIDMatch()
        {
            logList.Add(GetTimeStamp(false) + "Init: Session ID match");
            RunLogMaintenance();
        }

        public static void LogLogin()
        {
            logList.Add(GetTimeStamp(false) + "Login");
            RunLogMaintenance();
        }

        public static void LogLoginAttempt()
        {
            logList.Add(GetTimeStamp(true) + "Login: Attempt");
            RunLogMaintenance();
        }

        public static void LogCreatePassword()
        {
            logList.Add(GetTimeStamp(false) + "New Password");
            RunLogMaintenance();
        }

        public static void PasswordGenerator()
        {
            logList.Add(GetTimeStamp(false) + "Password Generator");
            RunLogMaintenance();
        }

        public static void LogShowLog()
        {
            logList.Add(GetTimeStamp(false) + "Log: Access");
            RunLogMaintenance();
        }

        public static void LogSleep()
        {
            logList.Add(GetTimeStamp(true) + "Sleep");
            RunLogMaintenance();
        }
        public static void SpellCheck()
        {
            logList.Add(GetTimeStamp(true) + "Spell Check");
            RunLogMaintenance();
        }
        public static void StickyCreated(bool perRemote)
        {
            if (perRemote)
            {
                logList.Add(GetTimeStamp(true) + "Sticky: Created (From dialog)");
            }
            else
            {
                logList.Add(GetTimeStamp(true) + "Sticky: Created (From main editor)");
            }

            RunLogMaintenance();
        }

        public static void StickyReopen()
        {
            logList.Add(GetTimeStamp(true) + "Sticky: Reopened");
            RunLogMaintenance();
        }

        public static void StickyClosed(bool save)
        {
            if (save)
            {
                logList.Add(GetTimeStamp(true) + "Sticky: Closed (Save: True)");
            }
            else
            {
                logList.Add(GetTimeStamp(true) + "Sticky: Closed (Save: False)");
            }

            RunLogMaintenance();
        }

        public static void StickyTerminated(bool save)
        {
            if (save)
            {
                logList.Add(GetTimeStamp(true) + "Sticky: Terminated (Save: True)");
            }
            else
            {
                logList.Add(GetTimeStamp(true) + "Sticky: Terminated (Save: False)");
            }

            RunLogMaintenance();
        }


        public static void StickyDeleted()
        {
            logList.Add(GetTimeStamp(true) + "Sticky: Deleted");
            RunLogMaintenance();
        }

        public static void StickyInitiatingTerminateAll()
        {
            logList.Add(GetTimeStamp(true) + "Sticky: Initiating (Terminate all)");
            RunLogMaintenance();
        }

        public static void StickyCentered()
        {
            logList.Add(GetTimeStamp(true) + "Sticky: Centered");
            RunLogMaintenance();
        }

        public static void StickyTheme()
        {
            logList.Add(GetTimeStamp(true) + "Sticky: Theme update");
            RunLogMaintenance();
        }

        public static void StickyFont()
        {
            logList.Add(GetTimeStamp(true) + "Sticky: Font changed");
            RunLogMaintenance();
        }

        public static void StickyBringToFront()
        {
            logList.Add(GetTimeStamp(true) + "Sticky: Bring to front");
            RunLogMaintenance();
        }

        public static void StickyHide()
        {
            logList.Add(GetTimeStamp(true) + "Sticky: Hide");
            RunLogMaintenance();
        }

        public static void StickyStatus(string status)
        {
            logList.Add(GetTimeStamp(true) + "Sticky: " + status);
            RunLogMaintenance();
        }

        public static void StickyError(string status)
        {
            logList.Add(GetTimeStamp(true) + "Sticky: Error (" + status + ")");
            RunLogMaintenance();
        }

        public static void StickyError(int id)
        {
            logList.Add(GetTimeStamp(true) + "Sticky: Error (" + id.ToString("000") + ")");
            RunLogMaintenance();
        }

        public static void LayoutMode(string mode)
        {
            logList.Add(GetTimeStamp(true) + "Layout Mode: " + mode);
            RunLogMaintenance();
        }

        public static void Opacity(string value)
        {
            logList.Add(GetTimeStamp(true) + "Opacity: " + value);
            RunLogMaintenance();
        }
        public static void ProcessStart(string info)
        {
            logList.Add(GetTimeStamp(true) + "Process Start: \"" + info + "\"");
            RunLogMaintenance();
        }

        public static void StyleEditor()
        {
            logList.Add(GetTimeStamp(true) + "Style editor");
            RunLogMaintenance();
        }

        public static void ThemeSelector()
        {
            logList.Add(GetTimeStamp(true) + "Theme selector");
            RunLogMaintenance();
        }

        public static void StyleChanged()
        {
            logList.Add(GetTimeStamp(true) + "Style editor: Theme submitted");
            RunLogMaintenance();
        }

        //public static void LogLoadingThemes() // Bliver ikke fanget i LoadCustomColorsIntoList
        //{
        //    logList.Add(GetTimeStamp(false) + "Loading color themes...");
        //    RunLogMaintenance();
        //}

        public static void SettingsChanged(string info)
        {
            logList.Add(GetTimeStamp(true) + "Settings: " + info);
            RunLogMaintenance();
        }

        public static void StyleUpdated()
        {
            logList.Add(GetTimeStamp(true) + "Status: Theme and texture updated");
            RunLogMaintenance();
        }

        public static void SetBookmark()
        {
            logList.Add(GetTimeStamp(true) + "Bookmark: Set");
            RunLogMaintenance();
        }

        public static void ParagraphDividerEdit()
        {
            logList.Add(GetTimeStamp(true) + "Edit: Paragraph dividers");
            RunLogMaintenance();
        }

        public static void RemoveBookmark()
        {
            logList.Add(GetTimeStamp(true) + "Bookmark: Removed");
            RunLogMaintenance();
        }

        public static void LogWakeup()
        {
            logList.Add(GetTimeStamp(false) + "Wakeup");
            RunLogMaintenance();
        }

        public static void LogSearch(string query, bool displayQuotes, bool liveSearch)
        {
            if (displayQuotes)
            {
                if (liveSearch)
                {
                    logList.Add(GetTimeStamp(false) + "Pool: Live Search \"" + query + "\"");
                }
                else
                {
                    logList.Add(GetTimeStamp(false) + "Pool: Search \"" + query + "\"");
                }
            }
            else
            {
                if (liveSearch)
                {
                    logList.Add(GetTimeStamp(false) + "Pool: Live Search " + query);
                }
                else
                {
                    logList.Add(GetTimeStamp(false) + "Pool: Search " + query);
                }
            }
            RunLogMaintenance();
        }

        public static void LogContentSearch(string query)
        {
            logList.Add(GetTimeStamp(false) + "Content search: \"" + query + "\"");
            RunLogMaintenance();
        }
        public static void LogGeneralContentSearch(string query)
        {
            logList.Add(GetTimeStamp(false) + "Editor search: \"" + query + "\"");
            RunLogMaintenance();
        }

        public static void LogSelect(string title)
        {
            logList.Add(GetTimeStamp(false) + "Entry select: \"" + title + "\"");
            RunLogMaintenance();
        }

        public static void LogNewEntry()
        {
            logList.Add(GetTimeStamp(false) + "Entry: Created");
            RunLogMaintenance();
        }

        public static void LogDuplicateEntry()
        {
            logList.Add(GetTimeStamp(false) + "Entry: Duplicated");
            RunLogMaintenance();
        }

        public static void LogDuplicateEntryError()
        {
            logList.Add(GetTimeStamp(false) + "Error: Duplication failed");
            RunLogMaintenance();
        }

        public static void LogUnpinEntry()
        {
            logList.Add(GetTimeStamp(false) + "Entry: Unpinned");
            RunLogMaintenance();
        }

        public static void GeneralEntryStatus(string status)
        {
            logList.Add(GetTimeStamp(false) + "Entry: " + status);
            RunLogMaintenance();
        }

        public static void LogSaveEntry(string title)
        {
            logList.Add(GetTimeStamp(false) + "Entry: Save \"" + title + "\"");
            RunLogMaintenance();
        }

        public static void LogEntryStateChange(string title, string message)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "(N/A)";
            }
            else
            {
                title = "(\"" + title + "\")";
            }
            logList.Add(GetTimeStamp(false) + "Entry: " + message + " " + title);
            RunLogMaintenance();
        }

        public static void LogSavePool(bool isBackup)
        {
            logList.Add(GetTimeStamp(false) + "Pool: Save (With backup: " + isBackup.ToString() + ")");
            RunLogMaintenance();
        }

        public static void LogPoolStatus(string status)
        {
            logList.Add(GetTimeStamp(false) + "Pool: " + status);
            RunLogMaintenance();
        }

        public static void LogDelete(string title)
        {
            logList.Add(GetTimeStamp(false) + "Entry: Delete \"" + title + "\"");
            RunLogMaintenance();
        }

        public static void LogCommand(string command)
        {
            logList.Add(GetTimeStamp(false) + "Command input: \"" + command + "\"");
            RunLogMaintenance();
        }

        public static void LogHomeScreenTextChanged()
        {
            logList.Add(GetTimeStamp(true) + "Home Screen changed");
            RunLogMaintenance();
        }

        public static void LogHomeScreenSelection()
        {
            logList.Add(GetTimeStamp(true) + "Home Screen selected");
            RunLogMaintenance();
        }

        //public static void LogHomeScreenStatus(string text)
        //{
        //    logList.Add(GetTimeStamp(true) + "Home Screen: " + text);
        //    RunLogMaintenance();
        //}

        public static void LogWindowStateChangeError()
        {
            logList.Add(GetTimeStamp(true) + "Error: Window state change");
            RunLogMaintenance();
        }

        public static void LogWindowStateChangeErrorSticky()
        {
            logList.Add(GetTimeStamp(true) + "Error: Window state change (Sticky)");
            RunLogMaintenance();
        }


        public static void LogLocationChangeError()
        {
            logList.Add(GetTimeStamp(true) + "Error: Cursor location change");
            RunLogMaintenance();
        }
        public static void Typermax()
        {
            logList.Add(GetTimeStamp(true) + "Game: Typermax");
            RunLogMaintenance();
        }
        public static void LogResetSettingsChanged()
        {
            logList.Add(GetTimeStamp(true) + "Settings: Reset");
            RunLogMaintenance();
        }

        public static void LogRestoreDefaultView()
        {
            logList.Add(GetTimeStamp(true) + "Settings: Default view");
            RunLogMaintenance();
        }

        public static void LogErrorLoadPoolFromFilePath()
        {
            logList.Add(GetTimeStamp(false) + "Error: Loading from specified directory");
            RunLogMaintenance();
        }

        public static void LogFilePathError()
        {
            logList.Add(GetTimeStamp(false) + "Error: Directory check/folder creation");
            RunLogMaintenance();
        }

        public static void LogSavePoolFromFilePath()
        {
            logList.Add(GetTimeStamp(false) + "Pool: Saving (Save file dialog)");
            RunLogMaintenance();
        }

        public static void LogMarkExport(string title, bool state)
        {
            if (state)
            {
                logList.Add(GetTimeStamp(false) + "Export (Mark): " + title);
            }
            else
            {
                logList.Add(GetTimeStamp(false) + "Export (Unmark): " + title);
            }
            RunLogMaintenance();
        }

        public static void LogExportCollection()
        {
            logList.Add(GetTimeStamp(false) + "Export (Save file dialog)");
            RunLogMaintenance();
        }

        public static void LogImportCollection()
        {
            logList.Add(GetTimeStamp(false) + "Import (Open file dialog)");
            RunLogMaintenance();
        }

        public static void LogImportExportStatus(string label, string status)
        {
            logList.Add(GetTimeStamp(false) + label + ": " + status);
            RunLogMaintenance();
        }

        public static void LogImportExportCollectionStatus(int status, string message)
        {
            switch (status)
            {
                case 0:
                    logList.Add(GetTimeStamp(false) + "Error: " + message);
                    break;
                case 1:
                    logList.Add(GetTimeStamp(false) + message);
                    break;
            }

            RunLogMaintenance();
        }


        public static void LogSaveEntryAsTXT()
        {
            logList.Add(GetTimeStamp(false) + "Save entry As .TXT (Save file dialog)");
            RunLogMaintenance();
        }

        public static void LogSaveEntryAsTXTError()
        {
            logList.Add(GetTimeStamp(false) + "Error saving entry as .TXT");
            RunLogMaintenance();
        }

        public static void Log1stClipboardError()
        {
            logList.Add(GetTimeStamp(false) + "Error setting clipboard (1st try)");
            RunLogMaintenance();
        }

        public static void Log2ndClipboardError()
        {
            logList.Add(GetTimeStamp(false) + "Error setting clipboard (2nd try)");
            RunLogMaintenance();
        }

        public static void LogColorPickerStatus(string message)
        {
            logList.Add(GetTimeStamp(false) + "Color Picker: " + message);
            RunLogMaintenance();
        }



        public static void LogGeneralError(string message)
        {
            logList.Add(GetTimeStamp(false) + "Error: " + message);
            RunLogMaintenance();
        }

        public static void LogEntryTemplateStatus(string message)
        {
            logList.Add(GetTimeStamp(false) + "Template: " + message);
            RunLogMaintenance();
        }

        public static void GeneralStatus(string message)
        {
            logList.Add(GetTimeStamp(false) + "Status: " + message);
            RunLogMaintenance();
        }

        public static void GeneralLogMessage(string message)
        {
            logList.Add(GetTimeStamp(false) + message);
            RunLogMaintenance();
        }

        public static void LogSaveSettings()
        {
            logList.Add(GetTimeStamp(false) + "Settings: Save");
            RunLogMaintenance();
        }

        public static void LogExportTheme()
        {
            logList.Add(GetTimeStamp(false) + "Style editor: Export");
            RunLogMaintenance();
        }

        public static void LogImportTheme()
        {
            logList.Add(GetTimeStamp(false) + "Style editor: Import");
            RunLogMaintenance();
        }

        public static void LogExportThemeError()
        {
            logList.Add(GetTimeStamp(false) + "Error: Unable to export theme");
            RunLogMaintenance();
        }

        public static void LogImportThemeError()
        {
            logList.Add(GetTimeStamp(false) + "Error: Unable to import theme");
            RunLogMaintenance();
        }

        public static void LogThemeError()
        {
            if (logList != null)
            {
                logList.Add(GetTimeStamp(false) + "Error: Unable to process theme data");
                RunLogMaintenance();
            }
        }

        public static void LogSaveSettingsError(string message)
        {
            logList.Add(GetTimeStamp(false) + "Error: " + message);
            RunLogMaintenance();
        }

        public static void LogSavePoolError()
        {
            logList.Add(GetTimeStamp(false) + "Error: Unable to save pool");
            RunLogMaintenance();
        }

        public static void LogLoadFileError()
        {
            logList.Add(GetTimeStamp(false) + "Error: Unable to load file");
            RunLogMaintenance();
        }

        public static void LogSaveFileAsError()
        {
            logList.Add(GetTimeStamp(false) + "Error: Unable to save file");
            RunLogMaintenance();
        }


        public static void LogSavePoolSuccess()
        {
            logList.Add(GetTimeStamp(false) + "Status: Pool saved");
            RunLogMaintenance();
        }

        public static void LogMaintenance(string status)
        {
            logList.Add(GetTimeStamp(false) + "Maintenance: " + status);
            RunLogMaintenance();
        }

        public static void LogBasicContextMenuError()
        {
            logList.Add(GetTimeStamp(false) + "Error: Unable to find control source...");
            RunLogMaintenance();
        }

        public static void LogSetAlternateBackup(string path)
        {
            logList.Add(GetTimeStamp(false) + "Backup: Remote set (" + path + ")");
            RunLogMaintenance();
        }

        public static void LogGeneralAlternateBackup(string input)
        {
            logList.Add(GetTimeStamp(false) + "Backup: " + input);
            RunLogMaintenance();
        }

        public static void LogClearAlternateBackup()
        {
            logList.Add(GetTimeStamp(false) + "Backup: Remote cleared");
            RunLogMaintenance();
        }

        public static void LogErrorAlternateBackup()
        {
            logList.Add(GetTimeStamp(false) + "Error: Remote backup directory invalid");
            RunLogMaintenance();
        }

        public static void LogSavingAlternateBackup()
        {
            logList.Add(GetTimeStamp(false) + "Backup: Initiating remote...");
            RunLogMaintenance();
        }

        public static void LogSavePoolAlternate()
        {
            logList.Add(GetTimeStamp(false) + "Backup: Remote directory");
            RunLogMaintenance();
        }


        // REMOVED because the bool needs to be saved implicitly for it to get registered
        // and that just makes the Ahoy too slow when shutting down.
        //public static void LogProgramExit()
        //{
        //    logList.Add(GetTimeStamp(true) + "Exit");
        //    RunLogMaintenance();
        //}

        // Not implemented
        //public static void LogCommandAttempt(string command)
        //{
        //    logList.Add(GetTimeStamp() + "Entry command attempt: \"" + command + "\"");
        //    RunLogMaintenance();
        //}

        // Not implemented
        //public static void LogError(string type)
        //{
        //    logList.Add(GetTimeStamp() + "Error: " + type);
        //    RunLogMaintenance();
        //}

    }
}
