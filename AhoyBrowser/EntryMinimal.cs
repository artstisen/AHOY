
namespace AhoyBrowser
{
    class EntryMinimal
    {
        // Light weight object used to pupulate listboxes
        public string ID { get; set; }
        public int PinRank { get; set; }
        public int PresetColor { get; set; }
        public string CustomColor { get; set; }
        private string TitlePrefix { get; set; }
        public bool MarkExport { get; set; }
        public bool MarkRelated { get; set; }
        public int DisplayCode { get; set; }
        public bool RelatedView { get; set; } // True = expanded, false = collapsed
        public int RelatedStyle { get; set; }
        private string _title;
        public string Title
        {
            get { return TitlePrefix + _title; }
            set { _title = value; }
        }

        public EntryMinimal(bool useTitlePrefix, string titlePrefix)
        {
            if (useTitlePrefix)
            {
                TitlePrefix = titlePrefix + " ";
            }
            else
            {
                TitlePrefix = "";
            }
        }
    }
}
