using System.Collections.Generic;

namespace AhoyBrowser
{
    class Command
    {
        public string CommandValue { get; set; }
        public string CommandDescription { get; set; }
        public Command(string value, string description)
        {
            CommandValue = value;
            CommandDescription = description;
        }
    }

    class CommandInput
    {
        // Properties
        public static bool CommandValidated { get; set; }
        public static string CommandString { get; set; }
        private static string trimmedCommand;
        private static List<Command> CommandLibrary { get; set; }
        private static List<Command> MatchingCommands { get; set; }

        // Define command type prefixes
        private const string EntryCommandPrefix = "entry.";
        private const string PoolCommandPrefix = "pool.";
        private const string GlobalCommandPrefix = "ahoy.";
        private const string OptionCommandPrefix = "options.";
        private const string ToolCommandPrefix = "tools.";
        private const string ThemeCommandPrefix = "theme.";
        private const string NotificationCommandPrefix = "notify.";
        private const string StickyCommandPrefix = "sticky.";

        // Command types
        public static CommandType SelectedCommandType { get; set; }
        public enum CommandType
        {
            GeneralCommand,
            EntryCommand,
            PoolCommand,
            GlobalCommand,
            OptionCommand,
            ToolCommand,
            ThemeCommand,
            NotificationCommand,
            StickyCommand,
            ShellCommand
        }

        #region Initialize commands (create commands with descriptions)
        public static void InitializeCommands()
        {
            if (CommandLibrary == null)
            {
                MatchingCommands = new List<Command>();
                CommandLibrary = new List<Command>();
                // General commands
                CommandLibrary.Add(new Command("help", "View Help"));
                CommandLibrary.Add(new Command("list", "List Commands"));
                CommandLibrary.Add(new Command("new", "New Entry"));
                CommandLibrary.Add(new Command("cls", "Close Entry/Clear Screen"));
                CommandLibrary.Add(new Command("az", "Sort text selection A-Z"));
                CommandLibrary.Add(new Command("za", "Sort text selection Z-A"));
                CommandLibrary.Add(new Command("sleep", "Lock interface"));
                CommandLibrary.Add(new Command("lock", "Lock interface"));
                CommandLibrary.Add(new Command("log", "Open Log"));
                CommandLibrary.Add(new Command("p", "Open PLOP Editor"));
                CommandLibrary.Add(new Command("ue", "Open Utility Editor"));
                CommandLibrary.Add(new Command("sticky", "New Sticky"));
                CommandLibrary.Add(new Command("calc", "Toggle Calculator"));
                CommandLibrary.Add(new Command("kb", "Toggle Keyboard Control"));
                CommandLibrary.Add(new Command("x", "Password Generator"));
                CommandLibrary.Add(new Command("type", "Start Typewriter"));
                CommandLibrary.Add(new Command("rand", "Random Numbers Generator"));
                CommandLibrary.Add(new Command("game", "Typermax Game"));
                CommandLibrary.Add(new Command("op", "Open Current Pool directory"));
                CommandLibrary.Add(new Command("div", "Paragraph Dividers"));
                CommandLibrary.Add(new Command("demo", "Toggle Demo Mode"));
                CommandLibrary.Add(new Command("theme", "Open Theme Selector"));
                CommandLibrary.Add(new Command("style", "Open Style Editor"));
                CommandLibrary.Add(new Command("save", "Save Pool and Settings"));
                CommandLibrary.Add(new Command("reset", "Restore Window Size/Location"));
                CommandLibrary.Add(new Command("clear", "Clear all Settings"));
                CommandLibrary.Add(new Command("info", "Show Pool Information"));
                CommandLibrary.Add(new Command("mon", "Open Log Monitor"));
                CommandLibrary.Add(new Command("debug", "Open Log Monitor"));
                CommandLibrary.Add(new Command("halt", "Suspend automated routines"));
                CommandLibrary.Add(new Command("stop", "Suspend automated routines"));
                CommandLibrary.Add(new Command("resume", "Resume automated routines"));
                CommandLibrary.Add(new Command("start", "Resume automated routines"));
                CommandLibrary.Add(new Command("clock", "Toggle Clock On/Off"));
                CommandLibrary.Add(new Command("border", "Toggle window border"));
                CommandLibrary.Add(new Command("time", "Display system ISO Date/Time"));
                CommandLibrary.Add(new Command("t", "Display system ISO Date/Time"));
                CommandLibrary.Add(new Command("flip", "Cycle Home Screen logo style"));
                CommandLibrary.Add(new Command("check", "Insert Checklist Template"));
                CommandLibrary.Add(new Command("task", "Insert Checklist Template"));
                CommandLibrary.Add(new Command("prog", "Insert Checklist Template"));
                CommandLibrary.Add(new Command("book", "Insert Bookmark Template"));
                CommandLibrary.Add(new Command("prism", "Toggle color-shifting effect"));
                CommandLibrary.Add(new Command("blocker", "Toggle Magic Screen Blocker (gimmick) On/Off"));
                //CommandLibrary.Add(new Command("update", "Check for Updates"));
                CommandLibrary.Add(new Command("default", "Load Default Pool"));
                CommandLibrary.Add(new Command("reload", "Reload current Pool"));
                CommandLibrary.Add(new Command("ent", "List all entries"));
                CommandLibrary.Add(new Command("table", "Theme color table (HEX/RGB)"));
                CommandLibrary.Add(new Command("csv", "Theme colors as CSV (HEX/RGB)"));
                CommandLibrary.Add(new Command("ts", "Display timestamp format"));
                CommandLibrary.Add(new Command("ts1", "Format timestamp YYYY-MM-DD HH:MM:SS"));
                CommandLibrary.Add(new Command("ts2", "Format timestamp YYYY-MM-DD HH:MM"));
                CommandLibrary.Add(new Command("ts3", "Format timestamp YYYY-MM-DD"));
                CommandLibrary.Add(new Command("ts4", "Format timestamp HH:MM:SS"));
                CommandLibrary.Add(new Command("ts5", "Format timestamp HH:MM"));
                CommandLibrary.Add(new Command("ts6", "Format timestamp YYYYMMDDHHMMSS"));
                // Related commands start
                CommandLibrary.Add(new Command("rel", "Count entries in category"));
                CommandLibrary.Add(new Command("relm", "Edit category"));
                CommandLibrary.Add(new Command("rela", "Add to category"));
                CommandLibrary.Add(new Command("relc", "Clear all categories"));
                CommandLibrary.Add(new Command("relr", "Remove selected from category"));
                CommandLibrary.Add(new Command("relu", "Remove all from category"));
                CommandLibrary.Add(new Command("rell", "Show related to category"));
                CommandLibrary.Add(new Command("rels", "Show editing category"));
                CommandLibrary.Add(new Command("relf", "Flip category display style"));
                CommandLibrary.Add(new Command("relq", "Create dummy category"));
                CommandLibrary.Add(new Command("relg", "Category gradient color"));
                // Related commands end
                CommandLibrary.Add(new Command("undo", "Undo latest Pool operation"));
                CommandLibrary.Add(new Command("exit", "Save and Exit"));
                CommandLibrary.Add(new Command("test", "Demo routine (service command)"));
                // General commands end

                // Entry commands
                CommandLibrary.Add(new Command(EntryCommandPrefix + "new", "New Entry"));
                CommandLibrary.Add(new Command(EntryCommandPrefix + "close", "Close Entry"));
                CommandLibrary.Add(new Command(EntryCommandPrefix + "delete", "Delete Entry"));
                CommandLibrary.Add(new Command(EntryCommandPrefix + "duplicate", "Duplicate Entry"));
                CommandLibrary.Add(new Command(EntryCommandPrefix + "saveas", "Save as .txt"));
                CommandLibrary.Add(new Command(EntryCommandPrefix + "mark", "Mark for Export"));
                CommandLibrary.Add(new Command(EntryCommandPrefix + "pin", "Toggle Entry as Pinned"));
                // Pool commands
                CommandLibrary.Add(new Command(PoolCommandPrefix + "open", "Open Pool/Backup (.ahoy)"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "new", "New Pool"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "saveas", "Save Pool Copy (.ahoy)"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "switch", "Toggle Secondary Pool"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "import", "Import entries"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "export", "Export entries (Not encrypted)"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "readonly", "Toggle Read-Only (Write Protection)"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "setbak", "Set Remote Backup directory"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "openbak", "Open Remote Backup directory"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "clearbak", "Reset Remote Backup directory"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "backup", "Initiate Pool Backup"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "recover", "Backup Recovery (Automated)"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "info", "Show Pool Information"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "show", "Open Current Pool directory"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "default", "Load Default Pool"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "reload", "Reload current Pool"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "copy", "Copy Pool to clipboard"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "undo", "Undo latest Pool operation"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "cleanup", "Delete unused entries"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "delete", "Clear Pool (Password required)"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "dump", "Dump to editor (service command)"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "modify", "ISO DateTime Auto-Correction (service command)"));
                CommandLibrary.Add(new Command(PoolCommandPrefix + "renew", "Renew Session ID (service command)"));
                // Global commands
                CommandLibrary.Add(new Command(GlobalCommandPrefix + "save", "Save All (Entry, Pool and Settings)"));
                CommandLibrary.Add(new Command(GlobalCommandPrefix + "halt", "Suspend automated routines"));
                CommandLibrary.Add(new Command(GlobalCommandPrefix + "resume", "Resume automated routines"));
                // Option commands
                CommandLibrary.Add(new Command(OptionCommandPrefix + "wpm", "Toggle Word and WPM Counter"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "anim", "Toggle Animations On/Off"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "reset", "Restore Window Size/Location"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "clear", "Clear all Settings"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "wsize", "Display Window Size/Location"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "clickpin", "Toggle new Pinned by Click"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "hidepin", "Toggle hide Pinned by Click"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "timestamp", "Display timestamp format"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "timestamp1", "Format timestamp YYYY-MM-DD HH:MM:SS"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "timestamp2", "Format timestamp YYYY-MM-DD HH:MM"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "timestamp3", "Format timestamp YYYY-MM-DD"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "timestamp4", "Format timestamp HH:MM:SS"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "timestamp5", "Format timestamp HH:MM"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "timestamp6", "Format timestamp YYYYMMDDHHMMSS"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "az", "Sort Pinned entries A-Z"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "za", "Sort Pinned entries Z-A"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "pinrank", "Reset Pinned sort order"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "pincolor", "Clear Pinned colors"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "pinreset", "Unpin all pinned entries"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "unlock", "Unlock all protected entries"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "logreset", "Clear Log"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "navreset", "Clear Navigation History"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "cache", "Toggle Undo-Cache (Memory optimization)"));
                CommandLibrary.Add(new Command(OptionCommandPrefix + "flip", "Cycle Home Screen logo style"));
                // Tool commands
                CommandLibrary.Add(new Command(ToolCommandPrefix + "div", "Paragraph Dividers"));
                CommandLibrary.Add(new Command(ToolCommandPrefix + "snippet", "Open Snippet Editor"));
                CommandLibrary.Add(new Command(ToolCommandPrefix + "plop", "Open PLOP Editor"));
                CommandLibrary.Add(new Command(ToolCommandPrefix + "ue", "Open Utility Editor"));
                CommandLibrary.Add(new Command(ToolCommandPrefix + "toue", "Open Entry in Utility Editor"));
                CommandLibrary.Add(new Command(ToolCommandPrefix + "appue", "Append Entry in Utility Editor"));
                CommandLibrary.Add(new Command(ToolCommandPrefix + "appsel", "Append text selection in Utility Editor"));
                CommandLibrary.Add(new Command(ToolCommandPrefix + "rand", "Open Random Numbers Generator"));
                CommandLibrary.Add(new Command(ToolCommandPrefix + "roman", "Open Roman Numerals Editor"));
                CommandLibrary.Add(new Command(ToolCommandPrefix + "ansi", "Open ANSI Editor"));
                CommandLibrary.Add(new Command(ToolCommandPrefix + "ascii", "Open ANSI Editor"));
                CommandLibrary.Add(new Command(ToolCommandPrefix + "pass", "Generate 20-char Strong Password"));
                CommandLibrary.Add(new Command(ToolCommandPrefix + "chars", "List character selection"));
                CommandLibrary.Add(new Command(ToolCommandPrefix + "morechars", "List all printable characters"));
                CommandLibrary.Add(new Command(ToolCommandPrefix + "typewriter", "Start Typewriter"));
                CommandLibrary.Add(new Command(ToolCommandPrefix + "typermax", "Start Typermax Game"));
                CommandLibrary.Add(new Command(ToolCommandPrefix + "typermax.reset", "Reset Typermax high score"));
                CommandLibrary.Add(new Command(ToolCommandPrefix + "mon", "Open Log Monitor"));
                // CommandLibrary.Add(new Command(ToolCommandPrefix + "dcheck", "Debug routine/Server check (service command)"));
                // CommandLibrary.Add(new Command(ToolCommandPrefix + "ucheck", "Debug routine/Server check (service command)"));
                // CommandLibrary.Add(new Command(ToolCommandPrefix + "vcheck", "Debug routine/Server check (service command)"));
                // Theme commands
                CommandLibrary.Add(new Command(ThemeCommandPrefix + "edit", "Open Style Editor"));
                CommandLibrary.Add(new Command(ThemeCommandPrefix + "select", "Open Theme Selector"));
                CommandLibrary.Add(new Command(ThemeCommandPrefix + "dev", "Developer preset layout"));
                CommandLibrary.Add(new Command(ThemeCommandPrefix + "table", "Theme color table (HEX/RGB)"));
                CommandLibrary.Add(new Command(ThemeCommandPrefix + "csv", "Theme colors as CSV (HEX/RGB)"));
                CommandLibrary.Add(new Command(ThemeCommandPrefix + "list", "All theme colors indexed (HEX/RGB)"));
                // Notification commands
                CommandLibrary.Add(new Command(NotificationCommandPrefix + "set", "Set Notification on current Entry"));
                CommandLibrary.Add(new Command(NotificationCommandPrefix + "count", "Get Notification count"));
                CommandLibrary.Add(new Command(NotificationCommandPrefix + "info", "Entry Notification details"));
                CommandLibrary.Add(new Command(NotificationCommandPrefix + "5", "Set 5 min. on Entry"));
                CommandLibrary.Add(new Command(NotificationCommandPrefix + "10", "Set 10 min. on Entry"));
                CommandLibrary.Add(new Command(NotificationCommandPrefix + "15", "Set 15 min. on Entry"));
                CommandLibrary.Add(new Command(NotificationCommandPrefix + "20", "Set 20 min. on Entry"));
                CommandLibrary.Add(new Command(NotificationCommandPrefix + "30", "Set 30 min. on Entry"));
                CommandLibrary.Add(new Command(NotificationCommandPrefix + "push", "Push +5 min."));
                CommandLibrary.Add(new Command(NotificationCommandPrefix + "push10", "Push +10 min."));
                CommandLibrary.Add(new Command(NotificationCommandPrefix + "push30", "Push +30 min."));
                CommandLibrary.Add(new Command(NotificationCommandPrefix + "remove", "Deactivate Notification"));
                CommandLibrary.Add(new Command(NotificationCommandPrefix + "clear", "Deactivate all Notifications"));
                // Sticky commands
                CommandLibrary.Add(new Command(StickyCommandPrefix + "new", "New Sticky/Show hidden"));
                CommandLibrary.Add(new Command(StickyCommandPrefix + "hide", "Hide all Stickies"));
                CommandLibrary.Add(new Command(StickyCommandPrefix + "delete", "Delete all Stickies"));
                CommandLibrary.Add(new Command(StickyCommandPrefix + "align", "Center align all Stickies"));
                CommandLibrary.Add(new Command(StickyCommandPrefix + "terminate", "Terminate all (service command)"));
                CommandLibrary.Add(new Command(StickyCommandPrefix + "get", "Retrieve all sticky text"));
            }
        }
        #endregion

        #region Check command type
        public static bool IsShellCommand(string input)
        {
            if (ShellCommands.IsShellCommand(input) || ShellCommands.IsShellEnvironmentPath(input))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsGeneralCommand(string input)
        {
            if (!input.Contains("."))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsEntryCommand(string input)
        {
            if (input.Contains(EntryCommandPrefix))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsPoolCommand(string input)
        {
            if (input.Contains(PoolCommandPrefix))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsGlobalCommand(string input)
        {
            if (input.Contains(GlobalCommandPrefix))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsOptionCommand(string input)
        {
            if (input.Contains(OptionCommandPrefix))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsToolCommand(string input)
        {
            if (input.Contains(ToolCommandPrefix))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsThemeCommand(string input)
        {
            if (input.Contains(ThemeCommandPrefix))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsNotificationCommand(string input)
        {
            if (input.Contains(NotificationCommandPrefix))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsStickyCommand(string input)
        {
            if (input.Contains(StickyCommandPrefix))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Trim command types
        private static string TrimEntryCommand(string input)
        {
            input = input.Replace(EntryCommandPrefix, "");
            return input;
        }

        private static string TrimPoolCommand(string input)
        {
            input = input.Replace(PoolCommandPrefix, "");
            return input;
        }

        private static string TrimGlobalCommand(string input)
        {
            input = input.Replace(GlobalCommandPrefix, "");
            return input;
        }

        private static string TrimOptionsCommand(string input)
        {
            input = input.Replace(OptionCommandPrefix, "");
            return input;
        }

        private static string TrimToolsCommand(string input)
        {
            input = input.Replace(ToolCommandPrefix, "");
            return input;
        }

        private static string TrimThemeCommand(string input)
        {
            input = input.Replace(ThemeCommandPrefix, "");
            return input;
        }

        private static string TrimNotificationCommand(string input)
        {
            input = input.Replace(NotificationCommandPrefix, "");
            return input;
        }

        private static string TrimStickyCommand(string input)
        {
            input = input.Replace(StickyCommandPrefix, "");
            return input;
        }

        #endregion

        #region Command string operations


        // Get a list of all commands with description
        private static string tempCommandsOutput;
        private static bool tempGeneralCommandHeader;
        private static bool tempEntryCommandHeader;
        private static bool tempPoolCommandHeader;
        private static bool tempGlobalCommandHeader;
        private static bool tempOptionsCommandHeader;
        private static bool tempToolsCommandHeader;
        private static bool tempThemeCommandHeader;
        private static bool tempNotificationCommandHeader;
        private static bool tempStickyCommandHeader;
        public static string GetAllCommands()
        {
            tempGeneralCommandHeader = true;
            tempEntryCommandHeader = true;
            tempPoolCommandHeader = true;
            tempGlobalCommandHeader = true;
            tempOptionsCommandHeader = true;
            tempToolsCommandHeader = true;
            tempThemeCommandHeader = true;
            tempNotificationCommandHeader = true;
            tempStickyCommandHeader = true;

            tempCommandsOutput = "";
            for (int i = 0; i < CommandLibrary.Count; i++)
            {
                // General command header
                if (IsGeneralCommand(CommandLibrary[i].CommandValue) && tempGeneralCommandHeader)
                {
                    tempGeneralCommandHeader = false;
                    tempCommandsOutput += IO.GetHeadlineFormatted("GENERAL COMMANDS");
                }
                // Entry command header
                if (IsEntryCommand(CommandLibrary[i].CommandValue) && tempEntryCommandHeader)
                {
                    tempEntryCommandHeader = false;
                    tempCommandsOutput += IO.GetHeadlineFormatted("ENTRY COMMANDS");
                }
                // Pool command header
                if (IsPoolCommand(CommandLibrary[i].CommandValue) && tempPoolCommandHeader)
                {
                    tempPoolCommandHeader = false;
                    tempCommandsOutput += IO.GetHeadlineFormatted("POOL COMMANDS");
                }
                // Global command header
                if (IsGlobalCommand(CommandLibrary[i].CommandValue) && tempGlobalCommandHeader)
                {
                    tempGlobalCommandHeader = false;
                    tempCommandsOutput += IO.GetHeadlineFormatted("GLOBAL COMMANDS");
                }
                // Options command header
                if (IsOptionCommand(CommandLibrary[i].CommandValue) && tempOptionsCommandHeader)
                {
                    tempOptionsCommandHeader = false;
                    tempCommandsOutput += IO.GetHeadlineFormatted("OPTION COMMANDS");
                }
                // Tools command header
                if (IsToolCommand(CommandLibrary[i].CommandValue) && tempToolsCommandHeader)
                {
                    tempToolsCommandHeader = false;
                    tempCommandsOutput += IO.GetHeadlineFormatted("TOOL COMMANDS");
                }
                // Theme command header
                if (IsThemeCommand(CommandLibrary[i].CommandValue) && tempThemeCommandHeader)
                {
                    tempThemeCommandHeader = false;
                    tempCommandsOutput += IO.GetHeadlineFormatted("THEME COMMANDS");
                }
                // Notification command header
                if (IsNotificationCommand(CommandLibrary[i].CommandValue) && tempNotificationCommandHeader)
                {
                    tempNotificationCommandHeader = false;
                    tempCommandsOutput += IO.GetHeadlineFormatted("NOTIFICATION COMMANDS");
                }
                // Sticky command header
                if (IsStickyCommand(CommandLibrary[i].CommandValue) && tempStickyCommandHeader)
                {
                    tempStickyCommandHeader = false;
                    tempCommandsOutput += IO.GetHeadlineFormatted("STICKY COMMANDS");
                }

                // Add command, dots and descriptions
                tempCommandsOutput += CommandLibrary[i].CommandValue + " " + TextTransform.CalculateSpacesFromStringToGivenLengthReturnChar(CommandLibrary[i].CommandValue, 20, ".") + " " + CommandLibrary[i].CommandDescription + System.Environment.NewLine;
            }
            return tempCommandsOutput;
        }

        public static bool CommandValid(string input)
        {
            if (ShellCommands.IsShellCommand(input) || ShellCommands.IsShellEnvironmentPath(input))
            {
                return true;
            }

            foreach (Command c in CommandLibrary)
            {
                if (c.CommandValue == input)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Command search operations
        private static bool tempMatchFound;
        public static void FindMatchingCommands(string input)
        {
            tempMatchFound = false;
            tempCommandMatchCounter = 0;
            MatchingCommands.Clear();
            input = input.ToLower().Trim();
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            for (int i = 0; i < CommandLibrary.Count; i++)
            {
                if (CommandLibrary[i].CommandValue.Contains(input))
                {
                    MatchingCommands.Add(CommandLibrary[i]);
                    tempMatchFound = true;
                }
            }

            // Sort alphabetically
            if (tempMatchFound && MatchingCommands.Count > 0)
            {
                MatchingCommands.Sort(delegate (Command x, Command y) { return x.CommandValue.CompareTo(y.CommandValue); });
            }
            else
            {
                MatchingCommands.Clear();
            }
        }

        private static int tempCommandMatchCounter;
        private static Command tempCommandMatch;
        public static Command GetNextMatchingCommand()
        {
            // Initialize command
            tempCommandMatch = new Command("noresult", "");

            // Return if zero matches
            if (MatchingCommands.Count == 0)
            {
                return tempCommandMatch;
            }

            if (tempCommandMatchCounter >= 0 && tempCommandMatchCounter < MatchingCommands.Count)
            {
                tempCommandMatch = MatchingCommands[tempCommandMatchCounter];
                tempCommandMatchCounter += 1;
                return tempCommandMatch;
            }
            else
            {
                tempCommandMatchCounter = 0;
                tempCommandMatch = MatchingCommands[tempCommandMatchCounter];
                tempCommandMatchCounter += 1;
                return tempCommandMatch;
            }
        }

        public static Command GetPreviousMatchingCommand()
        {
            // Initialize command
            tempCommandMatch = new Command("noresult", "");

            // Return if zero matches
            if (MatchingCommands.Count == 0)
            {
                return tempCommandMatch;
            }

            if (tempCommandMatchCounter > 0)
            {
                tempCommandMatchCounter -= 1;
                tempCommandMatch = MatchingCommands[tempCommandMatchCounter];
                return tempCommandMatch;
            }
            else if (tempCommandMatchCounter == 0)
            {
                tempCommandMatchCounter = MatchingCommands.Count - 1;
                tempCommandMatch = MatchingCommands[tempCommandMatchCounter];
                return tempCommandMatch;
            }
            else
            {
                return tempCommandMatch;
            }
        }
        #endregion

        // Execute command
        public static void SubmitCommandInput(string command)
        {
            // Return if null or empty
            if (string.IsNullOrEmpty(command))
            {
                return;
            }

            // Prepare properties
            CommandValidated = false;
            // Validate input with commands library
            trimmedCommand = command.Trim().ToLower();
            if (!CommandValid(trimmedCommand))
            {
                return;
            }

            // Get create a list of matching commands
            FindMatchingCommands(trimmedCommand);

            // Submit command with its proper type
            if (IsShellCommand(trimmedCommand)) // Shell command
            {
                SubmitCommand(trimmedCommand, CommandType.ShellCommand);
            }
            else if (IsGeneralCommand(trimmedCommand)) // General command
            {
                SubmitCommand(trimmedCommand, CommandType.GeneralCommand);
            }
            else if (IsEntryCommand(trimmedCommand)) // Entry command
            {
                SubmitCommand(TrimEntryCommand(trimmedCommand), CommandType.EntryCommand);
            }
            else if (IsPoolCommand(trimmedCommand)) // Pool command
            {
                SubmitCommand(TrimPoolCommand(trimmedCommand), CommandType.PoolCommand);
            }
            else if (IsGlobalCommand(trimmedCommand)) // Global command
            {
                SubmitCommand(TrimGlobalCommand(trimmedCommand), CommandType.GlobalCommand);
            }
            else if (IsOptionCommand(trimmedCommand)) // Options command
            {
                SubmitCommand(TrimOptionsCommand(trimmedCommand), CommandType.OptionCommand);
            }
            else if (IsToolCommand(trimmedCommand)) // Tools command
            {
                SubmitCommand(TrimToolsCommand(trimmedCommand), CommandType.ToolCommand);
            }
            else if (IsThemeCommand(trimmedCommand)) // Theme command
            {
                SubmitCommand(TrimThemeCommand(trimmedCommand), CommandType.ThemeCommand);
            }
            else if (IsNotificationCommand(trimmedCommand)) // Notification command
            {
                SubmitCommand(TrimNotificationCommand(trimmedCommand), CommandType.NotificationCommand);
            }
            else if (IsStickyCommand(trimmedCommand)) // Sticky command
            {
                SubmitCommand(TrimStickyCommand(trimmedCommand), CommandType.StickyCommand);
            }
        }

        private static void SubmitCommand(string command, CommandType type)
        {
            // Log operation
            Log.LogCommand(command);
            // Set command success
            CommandValidated = true;
            // Set command string
            CommandString = command;
            // Set command type
            SelectedCommandType = type;
        }

    }
}
