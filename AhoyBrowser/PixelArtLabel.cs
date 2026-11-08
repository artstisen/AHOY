using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AhoyBrowser
{
    /// <summary>
    /// A Label that draws its text as a character grid with shapes instead of font glyphs.
    ///
    /// Ahoy's small animations (the coffee cup by the pinned list, the sleep screen) are text art
    /// made of block, box-drawing and bullet characters. How those glyphs render depends on the font,
    /// font substitution, the Windows text control and East Asian "ambiguous width" rules, so the art
    /// could break apart. Here every cell is painted directly:
    ///   blocks  █ ▀ ▄ ▌ ▐ ░ ▒ ▓     -> filled rectangles (adjacent cells touch, no gaps)
    ///   lines   ─ ═ │ ║ ┌ ┐ └ ┘ ├ ┤ ┬ ┴ ┼ -> lines through the cell centre
    ///   bubbles · • ° ○             -> circles
    ///   anything else (letters, brackets) -> drawn centred in its cell with the label's font
    /// The grid uses the font's character size, so the art keeps the size and position it had as text.
    /// </summary>
    internal class PixelArtLabel : Label
    {
        public PixelArtLabel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            string text = Text;
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            string[] rows = text.Replace("\r\n", "\n").Split('\n');
            int columns = 0;
            foreach (string row in rows)
            {
                columns = Math.Max(columns, row.Length);
            }
            if (columns == 0)
            {
                return;
            }

            // Cell size from the font, like monospaced text
            Size tenChars = TextRenderer.MeasureText(e.Graphics, "MMMMMMMMMM", Font, Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
            float cellWidth = Math.Max(1f, tenChars.Width / 10f);
            float cellHeight = Math.Max(1f, Font.Height);
            float artWidth = columns * cellWidth;
            float artHeight = rows.Length * cellHeight;

            RectangleF area = new RectangleF(Padding.Left, Padding.Top,
                ClientSize.Width - Padding.Horizontal, ClientSize.Height - Padding.Vertical);
            float originX = Align(area.Left, area.Width, artWidth, TextAlign, horizontal: true);
            float originY = Align(area.Top, area.Height, artHeight, TextAlign, horizontal: false);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Color color = Enabled ? ForeColor : SystemColors.GrayText;
            using var brush = new SolidBrush(color);
            using var pen = new Pen(color, Math.Max(1f, cellWidth / 7f));

            for (int r = 0; r < rows.Length; r++)
            {
                string row = rows[r];
                for (int c = 0; c < row.Length; c++)
                {
                    char ch = row[c];
                    if (ch == ' ')
                    {
                        continue;
                    }
                    // Snap cell edges to whole pixels so neighbouring blocks join seamlessly
                    float x0 = MathF.Round(originX + c * cellWidth);
                    float x1 = MathF.Round(originX + (c + 1) * cellWidth);
                    float y0 = MathF.Round(originY + r * cellHeight);
                    float y1 = MathF.Round(originY + (r + 1) * cellHeight);
                    DrawCell(g, ch, RectangleF.FromLTRB(x0, y0, x1, y1), brush, pen, color);
                }
            }
        }

        private static float Align(float start, float available, float size, ContentAlignment alignment, bool horizontal)
        {
            bool near, far;
            if (horizontal)
            {
                near = alignment is ContentAlignment.TopLeft or ContentAlignment.MiddleLeft or ContentAlignment.BottomLeft;
                far = alignment is ContentAlignment.TopRight or ContentAlignment.MiddleRight or ContentAlignment.BottomRight;
            }
            else
            {
                near = alignment is ContentAlignment.TopLeft or ContentAlignment.TopCenter or ContentAlignment.TopRight;
                far = alignment is ContentAlignment.BottomLeft or ContentAlignment.BottomCenter or ContentAlignment.BottomRight;
            }
            if (near) return start;
            if (far) return start + available - size;
            return start + (available - size) / 2f;
        }

        private void DrawCell(Graphics g, char ch, RectangleF cell, SolidBrush brush, Pen pen, Color color)
        {
            float cx = cell.Left + cell.Width / 2f;
            float cy = cell.Top + cell.Height / 2f;
            float halfW = cell.Width / 2f;
            float halfH = cell.Height / 2f;

            switch (ch)
            {
                // Blocks: no anti-aliasing, so edges are crisp and neighbours meet exactly
                case '█': FillCrisp(g, brush, cell); return;
                case '▀': FillCrisp(g, brush, new RectangleF(cell.Left, cell.Top, cell.Width, MathF.Round(halfH))); return;
                case '▄': FillCrisp(g, brush, RectangleF.FromLTRB(cell.Left, cell.Top + MathF.Round(halfH), cell.Right, cell.Bottom)); return;
                case '▌': FillCrisp(g, brush, new RectangleF(cell.Left, cell.Top, MathF.Round(halfW), cell.Height)); return;
                case '▐': FillCrisp(g, brush, RectangleF.FromLTRB(cell.Left + MathF.Round(halfW), cell.Top, cell.Right, cell.Bottom)); return;
                case '░': FillShade(g, color, cell, 64); return;
                case '▒': FillShade(g, color, cell, 128); return;
                case '▓': FillShade(g, color, cell, 192); return;

                // Bubbles
                case '·': FillCircle(g, brush, cx, cy, cell.Width * 0.12f); return;
                case '•': FillCircle(g, brush, cx, cy, cell.Width * 0.22f); return;
                case '°': DrawCircle(g, pen, cx, cell.Top + cell.Height * 0.3f, cell.Width * 0.18f); return;
                case '○': DrawCircle(g, pen, cx, cy, cell.Width * 0.32f); return;
            }

            // Box drawing: which arms leave the centre (left, right, up, down), single or double
            if (BoxArms(ch, out bool left, out bool right, out bool up, out bool down, out bool doubleLine))
            {
                SmoothingMode previous = g.SmoothingMode;
                g.SmoothingMode = SmoothingMode.None;
                float gap = doubleLine ? Math.Max(2f, pen.Width * 1.5f) : 0f;
                foreach (float offset in doubleLine ? new[] { -gap, gap } : new[] { 0f })
                {
                    if (left || right)
                    {
                        g.DrawLine(pen, left ? cell.Left : cx, cy + offset, right ? cell.Right : cx, cy + offset);
                    }
                    if (up || down)
                    {
                        g.DrawLine(pen, cx + offset, up ? cell.Top : cy, cx + offset, down ? cell.Bottom : cy);
                    }
                }
                g.SmoothingMode = previous;
                return;
            }

            // Ordinary characters (letters, digits, brackets): centred in the cell
            TextRenderer.DrawText(g, ch.ToString(), Font, Rectangle.Round(cell), color,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine);
        }

        private static bool BoxArms(char ch, out bool left, out bool right, out bool up, out bool down, out bool doubleLine)
        {
            left = right = up = down = doubleLine = false;
            switch (ch)
            {
                case '─': left = right = true; return true;
                case '═': left = right = doubleLine = true; return true;
                case '│': up = down = true; return true;
                case '║': up = down = doubleLine = true; return true;
                case '┌': right = down = true; return true;
                case '┐': left = down = true; return true;
                case '└': right = up = true; return true;
                case '┘': left = up = true; return true;
                case '├': up = down = right = true; return true;
                case '┤': up = down = left = true; return true;
                case '┬': left = right = down = true; return true;
                case '┴': left = right = up = true; return true;
                case '┼': left = right = up = down = true; return true;
                default: return false;
            }
        }

        private static void FillCrisp(Graphics g, Brush brush, RectangleF rect)
        {
            SmoothingMode previous = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.None;
            g.FillRectangle(brush, rect);
            g.SmoothingMode = previous;
        }

        private static void FillShade(Graphics g, Color color, RectangleF rect, int alpha)
        {
            using var shade = new SolidBrush(Color.FromArgb(alpha, color));
            FillCrisp(g, shade, rect);
        }

        private static void FillCircle(Graphics g, Brush brush, float cx, float cy, float radius)
        {
            g.FillEllipse(brush, cx - radius, cy - radius, radius * 2, radius * 2);
        }

        private static void DrawCircle(Graphics g, Pen pen, float cx, float cy, float radius)
        {
            g.DrawEllipse(pen, cx - radius, cy - radius, radius * 2, radius * 2);
        }
    }
}
