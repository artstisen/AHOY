using System.Windows.Forms;

namespace AhoyBrowser
{
    class BasicContextMenuOperations
    {
        public static void ExecuteContextMenuAction(object sender, int action)
        {
            try
            {
                ToolStripMenuItem myItem = (ToolStripMenuItem)sender;
                ContextMenuStrip cms = (ContextMenuStrip)myItem.Owner;
                //MessageBox.Show(cms.SourceControl.Name);

                if (cms.SourceControl is TextBox)
                {
                    TextBox box = (TextBox)cms.SourceControl;
                    // Do action
                    TextBoxOperations(box, action);
                }
                else if (cms.SourceControl is HistoryTextBox)
                {
                    HistoryTextBox box = (HistoryTextBox)cms.SourceControl;
                    // Do action
                    TextBoxOperations(box, action);
                }
            }
            catch
            {
                Log.LogBasicContextMenuError();
            }
        }

        // Normal TextBox
        private static void TextBoxOperations(TextBox tb, int action)
        {
            switch (action)
            {
                case 0:
                    tb.Cut();
                    break;
                case 1:
                    tb.Copy();
                    break;
                case 2:
                    tb.Paste();
                    break;
            }
        }

        // History TextBox (always unformatted (cleaned) text operations)
        private static void TextBoxOperations(HistoryTextBox tb, int action)
        {
            switch (action)
            {
                case 0:
                    tb.CutUnformatted();
                    break;
                case 1:
                    tb.CopyUnformatted();
                    break;
                case 2:
                    tb.PasteUnformatted();
                    break;
                case 3:
                    tb.SelectAll();
                    break;
            }
        }


    }
}
