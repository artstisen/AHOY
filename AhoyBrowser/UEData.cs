namespace AhoyBrowser
{
    class UEData
    {
        // Passed to I/O Class when saving the pool
        // A to C is saved (not D/Temp bank)
        public string UETextA { get; set; }
        public string UETextB { get; set; }
        public string UETextC { get; set; }
        public int UECaretPositionA { get; set; }
        public int UECaretPositionB { get; set; }
        public int UECaretPositionC { get; set; }
        public string PlopLinkedID { get; set; }
        public string PlopMainText { get; set; }
        public string PlopUtilityText { get; set; }
    }
}
