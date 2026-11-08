
namespace AhoyBrowser
{
    public class Entry
    {

        public Entry()
        {
            TempHistoryState = new System.Collections.Generic.List<HistoryState>();
            MarkExport = false;
            MarkRelated = false;
            DisplayCode = 0;
        }

        // Entry variables
        public string ID { get; set; }
        public string Created { get; set; }
        public string Changed { get; set; }
        public bool Pin { get; set; }
        public int Rank { get; set; } // Rank in the pinned list
        public int PresetColor { get; set; } // Preset Colors in the pinned list
        public string CustomColor { get; set; } // Custom color
        public bool Protected { get; set; }
        public int CaretPosition { get; set; }
        public int Bookmark { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string RelatedEntries { get; set; }
        public bool RelatedView { get; set; } // true = expanded, false = collapsed
        public int RelatedStyle { get; set; } // 0 = default category style

        // Interface variables
        public bool IgnoreTextChanged { get; set; }
        public bool IsNewEntry { get; set; }
        public bool HasChanges { get; set; }
        public bool WasDeleted { get; set; }
        public bool IsDummyEntry { get; set; }
        public string SearchResult { get; set; }
        public System.Collections.Generic.List<HistoryState> TempHistoryState { get; set; }
        public bool MarkExport { get; set; }
        public bool MarkRelated { get; set; }
        public int DisplayCode { get; set; }
    }
}
