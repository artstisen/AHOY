using System.Collections.Generic;

namespace AhoyBrowser
{
    class Load
    {
        internal bool LoadSuccess { get; set; }
        internal string LoadMessage { get; set; }
        internal string AuthAuthQuestion { get; set; }
        internal string AuthAnswer { get; set; }
        internal string AuthPassword { get; set; }
        internal bool WriteProtected { get; set; }
        internal string LogText { get; set; }
        internal string DisplayText { get; set; }
        internal string MemoText { get; set; }
        internal int MemoCaretPosition { get; set; }
        public string UETextA { get; set; }
        public string UETextB { get; set; }
        public string UETextC { get; set; }
        public int UECaretPositionA { get; set; }
        public int UECaretPositionB { get; set; }
        public int UECaretPositionC { get; set; }
        public string HoldEntryAID { get; set; }
        public string HoldEntryBID { get; set; }
        public string PlopLinkedID { get; set; }
        public string PlopMainText { get; set; }
        public string PlopUtilityText { get; set; }
        public string AlternateBackupPath { get; set; }
        public string DividerAString { get; set; }
        public string DividerBString { get; set; }
        public string DividerCString { get; set; }
        public string DividerDString { get; set; }
        internal string CypherKey { get; set; }
        internal int TypermaxHighScore { get; set; }

        internal List<StickySettings> Stickies { get; set; }
        internal List<Entry> Entries { get; set; }

        public Load()
        {
            LoadSuccess = false;
            LoadMessage = "";
            AuthAuthQuestion = "";
            AuthAnswer = "";
            AuthPassword = "";
            WriteProtected = false;
            LogText = "";
            DisplayText = "";
            MemoText = "";
            MemoCaretPosition = 0;
            Entries = new List<Entry>();
            Stickies = new List<StickySettings>();
            UETextA = "";
            UETextB = "";
            UETextC = "";
            UECaretPositionA = 0;
            UECaretPositionB = 0;
            UECaretPositionC = 0;
            HoldEntryAID = "";
            HoldEntryBID = "";
            PlopLinkedID = "";
            PlopMainText = "";
            PlopUtilityText = "";
            AlternateBackupPath = "";
            DividerAString = "";
            DividerBString = "";
            DividerCString = "";
            DividerDString = "";
            CypherKey = "";
            TypermaxHighScore = 0;
        }
    }
}
