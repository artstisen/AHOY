using System.Windows.Forms;

namespace AhoyBrowser
{
    /// <summary>
    /// Folder picker. On .NET Core and later, FolderBrowserDialog shows the modern Windows folder
    /// dialog (the old version reached into WinForms internals that no longer exist).
    /// </summary>
    public class FolderSelectDialog
    {
        public string InitialDirectory { get; set; } = "";

        public string Title { get; set; } = "Select a folder";

        /// <summary>The selected folder (empty if cancelled).</summary>
        public string FileName { get; private set; } = "";

        public bool ShowDialog()
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = Title,
                UseDescriptionForTitle = true,
                ShowNewFolderButton = true,
                InitialDirectory = InitialDirectory ?? ""
            };

            if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(dialog.SelectedPath))
            {
                FileName = dialog.SelectedPath;
                return true;
            }

            FileName = "";
            return false;
        }
    }
}
