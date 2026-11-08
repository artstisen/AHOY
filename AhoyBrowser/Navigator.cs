using System.Collections.Generic;
using System.Windows.Forms;

namespace AhoyBrowser
{
    class Navigator
    {
        /*
         Rules:
         * Every entry selection is reported to the navigator
         * Deleting an entry is reported to the navigator
         * Selectin an entry outside the navigator (by search, pinned, recalled) should reset the navigator index 
         * Closing an entry will set that entry for the next entry when clicking the back button
         * Deleting an entry will set the previous entry for the next entry when clicking the back button
         */

        private static List<string> NavigationList { get; set; }
        private static int CurrentNavigationIndex { get; set; }
        private static bool HoldCurrentEntryAsLastEntry { get; set; }

        // Initialize variables
        public static void InitializeNavigator()
        {
            NavigationList = new List<string>();
            NavigationList.Clear();
            CurrentNavigationIndex = 0;
            HoldCurrentEntryAsLastEntry = false;
        }

        public static void ResetNavigator(Button backButton, Button forwardButton)
        {
            NavigationList.Clear();
            CurrentNavigationIndex = 0;
            HoldCurrentEntryAsLastEntry = false;
            ResolveNavigationButtons(backButton, forwardButton);
        }

        // Position the navigator at the beginning
        public static void RewindNavigator(Button backButton, Button forwardButton)
        {
            // When closing return to last entry
            HoldCurrentEntryInNavigationHistory();
            // And reset the navigation index
            ResetNavigationIndex();
            ResolveNavigationButtons(backButton, forwardButton);
        }

        public static void AddEntry(Entry entry)
        {
            if (entry != null && !string.IsNullOrEmpty(entry.ID))
            {
                // Release hold
                ReleaseCurrentEntryFromNavigationHistory();

                // Add ID
                NavigationList.Add(entry.ID);
                TruncateNavigationList();
                ResetNavigationIndex();
            }
        }

        public static void ResetNavigationIndex()
        {
            if (NavigationList.Count > 0)
            {
                // Reset current navigation index to be at the end of the list
                CurrentNavigationIndex = NavigationList.Count - 1;
            }
            else
            {
                CurrentNavigationIndex = 0;
            }
        }

        // Remove ID's that are next to each other (A, B, B -> A, B)
        private static void RemoveAdjacentDuplicates()
        {
            for (int i = NavigationList.Count - 1; i > 0; i--)
            {
                if (NavigationList[i] == NavigationList[i - 1])
                {
                    NavigationList.RemoveAt(i);
                }
            }
        }

        // Ensure the list never becomes bigger than 100 entries
        private static void TruncateNavigationList()
        {
            RemoveAdjacentDuplicates();

            // Remove at index 0 if more than 99
            if (NavigationList.Count > 99)
            {
                NavigationList.RemoveAt(0);
            }
        }

        public static void RemoveEntry(Entry entry)
        {
            if (NavigationList.Count > 0)
            {
                NavigationList.RemoveAll(id => id == entry.ID);
                // Removing an entry can leave the same ID twice in a row
                RemoveAdjacentDuplicates();
                // Release hold
                ReleaseCurrentEntryFromNavigationHistory();
                ResetNavigationIndex();
            }
        }

        public static bool CanNavigateBack()
        {
            // If there is only one entry in the list
            // enable selecting it
            if (NavigationList.Count == 1)
            {
                return true;
            }

            // If more than one entry in the Navigation list
            if (NavigationList.Count > 1)
            {
                // If navigation index is larger than the first index return true
                if (CurrentNavigationIndex > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool CanNavigateForward()
        {
            // If more than one entry in the Navigation list
            if (NavigationList.Count > 1)
            {
                // If navigation index is lower than the last index return true
                if (CurrentNavigationIndex < NavigationList.Count - 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // if only closed navigate to same index
        // else navigate -1
        public static void HoldCurrentEntryInNavigationHistory()
        {
            HoldCurrentEntryAsLastEntry = true;
        }

        public static void ReleaseCurrentEntryFromNavigationHistory()
        {
            HoldCurrentEntryAsLastEntry = false;
        }

        public static string NavigateBack()
        {
            if (CanNavigateBack())
            {
                // If there is only one entry in the list
                // Return that entry
                if (NavigationList.Count == 1)
                {
                    return NavigationList[0];
                }

                // Return the previous entry ID if available
                if (!HoldCurrentEntryAsLastEntry)
                {
                    CurrentNavigationIndex -= 1;
                }

                // Release hold
                ReleaseCurrentEntryFromNavigationHistory();
                return NavigationList[CurrentNavigationIndex];
            }
            else
            {
                return "";
            }
        }

        public static string NavigateForward()
        {
            if (CanNavigateForward())
            {
                // Return the preceeding entry ID if available
                CurrentNavigationIndex += 1;
                // Release hold
                ReleaseCurrentEntryFromNavigationHistory();
                return NavigationList[CurrentNavigationIndex];
            }
            else
            {
                return "";
            }
        }

        // Set Navigation button colors
        public static void ResolveNavigationButtons(Button backButton, Button forwardButton)
        {
            if (Navigator.CanNavigateBack())
            {
                backButton.BackgroundImage = ButtonBitmaps.LeftHighlight();
            }
            else
            {
                backButton.BackgroundImage = ButtonBitmaps.Left();
            }

            if (Navigator.CanNavigateForward())
            {
                forwardButton.BackgroundImage = ButtonBitmaps.RightHighlight();
            }
            else
            {
                forwardButton.BackgroundImage = ButtonBitmaps.Right();
            }
        }

    }
}
