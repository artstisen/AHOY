using System.Collections.Generic;

namespace AhoyBrowser
{
    class Load
    {
        internal bool LoadSuccess { get; set; }
        internal string LoadMessage { get; set; }

        /// <summary>The pool was not decrypted because the user cancelled the password prompt.</summary>
        internal bool UnlockCancelled { get; set; }
        /// <summary>The user pressed F5 / Ctrl+O in the password prompt to open another pool.</summary>
        internal bool OpenOtherPoolRequested { get; set; }

        /// <summary>The pool was stored in the Ahoy 1.x (3DES) format and must be upgraded.</summary>
        internal bool IsLegacyFormat { get; set; }
        /// <summary>Ahoy 1.x stored the login password inside the pool; only used to confirm identity before upgrading.</summary>
        internal string LegacyPassword { get; set; }

        internal bool WriteProtected { get; set; }
        internal string LogText { get; set; }
        internal string DisplayText { get; set; }
        internal string MemoText { get; set; }
        internal int MemoCaretPosition { get; set; }
        public string HoldEntryAID { get; set; }
        public string HoldEntryBID { get; set; }
        public string AlternateBackupPath { get; set; }
        public string DividerAString { get; set; }
        public string DividerBString { get; set; }
        public string DividerCString { get; set; }
        public string DividerDString { get; set; }
        internal string CypherKey { get; set; }

        internal List<StickySettings> Stickies { get; set; }
        internal List<Entry> Entries { get; set; }

        public Load()
        {
            LoadSuccess = false;
            LoadMessage = "";
            LegacyPassword = "";
            WriteProtected = false;
            LogText = "";
            DisplayText = "";
            MemoText = "";
            MemoCaretPosition = 0;
            Entries = new List<Entry>();
            Stickies = new List<StickySettings>();
            HoldEntryAID = "";
            HoldEntryBID = "";
            AlternateBackupPath = "";
            DividerAString = "";
            DividerBString = "";
            DividerCString = "";
            DividerDString = "";
            CypherKey = "";
        }
    }

    /// <summary>Called by IO.LoadEntries with an encrypted pool; returns the decrypted XML, or null if not unlocked.</summary>
    internal delegate string PoolUnlocker(byte[] encryptedPool, Load loadObject);
}
