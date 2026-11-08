namespace AhoyBrowser
{
    class StickySettings
    {
        // Saved to disk
        public string ID { get; set; }
        public string Text { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Theme { get; set; }
        public bool TopMost { get; set; }
        // For runtime use
        public bool Open { get; set; }
        public bool Remove { get; set; }
        public int StatusCode { get; set; }
        public bool StatusComplete { get; set; }
    }
}
