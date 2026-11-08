using System.Collections.Generic;

namespace AhoyBrowser
{
    class Related
    {
        // Properties used throughout this class
        // ------------------------------------------------
        private static string MasterId = string.Empty;
        private static int tempCounter = 0;
        // ------------------------------------------------



        public static bool SetMark(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                MasterId = id;
                Log.GeneralEntryStatus("Relation marked");
                return true;
            }
            else
            {
                Log.GeneralEntryStatus("Relation mark failed");
                return false;
            }
        }

        public static string GetMark()
        {
            if (!string.IsNullOrEmpty(MasterId))
            {
                return MasterId;
            }
            return string.Empty;
        }

        public static void ClearMark()
        {
            MasterId = string.Empty;
            Log.GeneralEntryStatus("Relation unmarked");
        }

        public static bool CanMark()
        {
            if (!string.IsNullOrEmpty(MasterId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check if entry contains related entries
        /// </summary>
        /// <param name="master"></param>
        /// <returns></returns>
        public static bool HasRelated(Entry master)
        {
            if (master != null && !string.IsNullOrEmpty(master.RelatedEntries))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check if list contains entries with related entries
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        public static bool HasRelated(List<Entry> entries)
        {
            if (IsValidPool(entries))
            {
                foreach (Entry e in entries)
                {
                    if (HasRelated(e))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Check if slave is related to a master.
        /// </summary>
        /// <param name="slave"></param>
        /// <returns></returns>
        public static bool IsRelated(Entry slave, List<Entry> entries)
        {
            if (IsValidSlave(slave) && IsValidPool(entries))
            {
                foreach (Entry e in entries)
                {
                    if (ContainsMatch(e, slave))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsMasterPinned(string slaveId, List<Entry> entries)
        {
            if (IsValidPool(entries) && !string.IsNullOrEmpty(slaveId))
            {
                foreach (Entry e in entries)
                {
                    if (e.RelatedEntries.Contains(slaveId) && e.Pin)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsAllMastersPinned(Entry slave, List<Entry> entries)
        {
            tempCounter = 0;
            if (IsValidSlave(slave) && IsValidPool(entries))
            {
                foreach (Entry e in entries)
                {
                    if (e.RelatedEntries.Contains(slave.ID) && e.Pin)
                    {
                        tempCounter += 1;
                    }
                }
            }

            if (tempCounter == GetMasterRelationsCount(slave, entries))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool AddToMaster(Entry originalSlave, Entry newSlave, List<Entry> entries)
        {
            if (IsValidPool(entries) && IsValidSlave(originalSlave) && IsValidSlave(newSlave))
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    if (entries[i].RelatedEntries.Contains(originalSlave.ID))
                    {
                        AddRelated(entries[i], newSlave);
                        return true;
                    }
                }
            }
            return false;
        }

        public static void CopyColorToAllRelated(Entry master, List<Entry> entries)
        {
            if (HasRelated(master) && IsValidPool(entries))
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    if (master.RelatedEntries.Contains(entries[i].ID))
                    {
                        entries[i].PresetColor = master.PresetColor;
                        entries[i].CustomColor = master.CustomColor;
                    }
                }
            }
        }

        public static void GradientColorToAllRelated(Entry master, List<Entry> entries)
        {
            if (HasRelated(master) && IsValidPool(entries))
            {
                try
                {
                    Log.GeneralEntryStatus("Set gradient");
                    // Initialize variables
                    int tempGradientCounter = 0;
                    int tempGradientFactor = CountRelated(master);
                    System.Drawing.Color tempBackgroundColor = new System.Drawing.Color();
                    System.Drawing.Color tempForegroundColor = new System.Drawing.Color();
                    List<Entry> tempCollectedSlaves = new List<Entry>();
                    tempCollectedSlaves.Clear();

                    // Retrieve colors from master or create default
                    if (AhoyColors.HasDualHexValue(master.CustomColor)) //  Use custom
                    {
                        tempBackgroundColor = AhoyColors.ConvertFromHex(master.CustomColor.Split(';')[0], false);
                        tempForegroundColor = AhoyColors.ConvertFromHex(master.CustomColor.Split(';')[1], false);
                    }
                    else if (master.PresetColor > 0) // Use preset
                    {
                        tempBackgroundColor = Design.ReturnStatusBackgroundColor(master.PresetColor);
                        tempForegroundColor = Design.ReturnStatusForegroundColor(master.PresetColor);
                    }
                    else // Create default gradient
                    {
                        tempBackgroundColor = AhoyColors.ConvertFromHex("#002E79", false); // Set default colors
                        tempForegroundColor = AhoyColors.ConvertFromHex("#00FFFF", false);
                    }

                    // Set master to prevent other preset colors when changing theme
                    master.PresetColor = 0;
                    master.CustomColor = AhoyColors.ConvertToHex(AhoyColors.GetDimmedColor(tempBackgroundColor, 0, true)) + ";" + AhoyColors.ConvertToHex(AhoyColors.GetDimmedColor(tempForegroundColor, 0, true));

                    // Get all related and sort
                    tempCollectedSlaves.AddRange(GetRelatedAsClone(master, entries));
                    tempCollectedSlaves.Sort(delegate (Entry x, Entry y) { return x.Title.CompareTo(y.Title); });

                    // Set colors
                    for (int i = 0; i < tempCollectedSlaves.Count; i++)
                    {
                        // Remove preset color
                        tempCollectedSlaves[i].PresetColor = 0;
                        // Benyt GetDimmedColor i ahoy colors

                        // Set custom color
                        tempCollectedSlaves[i].CustomColor = AhoyColors.ConvertToHex(AhoyColors.GetDimmedColor(tempBackgroundColor, tempGradientCounter, true)) + ";" + AhoyColors.ConvertToHex(AhoyColors.GetDimmedColor(tempForegroundColor, tempGradientCounter, true));

                        // Add 10 and use Color class to flatten if above 200 or below 0
                        tempGradientCounter += (100 / tempGradientFactor);
                    }

                    // Apply settings on matching entries in the pool
                    for (int j = 0; j < tempCollectedSlaves.Count; j++)
                    {
                        for (int k = 0; k < entries.Count; k++)
                        {
                            if (tempCollectedSlaves[j].ID == entries[k].ID)
                            {
                                entries[k].PresetColor = 0; // Remove preseta
                                entries[k].CustomColor = tempCollectedSlaves[j].CustomColor;
                            }
                        }
                    }

                    // Cleanup
                    tempCollectedSlaves.Clear();
                }
                catch
                {
                    Log.LogGeneralError("Gradient failed");
                }
            }
        }

        // If slave pin all masters, and if master, just pint it
        public static void PinAllMastersRelatedToSlave(Entry slaveOrMaster, List<Entry> entries)
        {
            if (IsValidSlave(slaveOrMaster) && IsValidPool(entries))
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    if (HasRelated(entries[i]) && ContainsMatch(entries[i], slaveOrMaster))
                    {
                        entries[i].Pin = true;
                    }
                }
            }
        }


        private static List<string> tempGetRelatedStringList;
        private static List<Entry> tempGetRelatedEntryList;
        /// <summary>
        /// Get a list of cloned entries related to the supplied master
        /// </summary>
        /// <param name="master"></param>
        /// <param name="entries"></param>
        /// <returns></returns>
        public static List<Entry> GetRelatedAsClone(Entry master, List<Entry> entries)
        {
            if (HasRelated(entries))
            {
                // Ready lists
                if (tempGetRelatedStringList == null || tempGetRelatedEntryList == null)
                {
                    tempGetRelatedStringList = new List<string>();
                    tempGetRelatedEntryList = new List<Entry>();
                }
                tempGetRelatedStringList.Clear();
                tempGetRelatedEntryList.Clear();

                // IDs related to supplied master
                tempGetRelatedStringList = GetRelatedAsList(master);
                // Foreach entry in pool check related IDs and clone the entries matching the IDs
                if (tempGetRelatedStringList != null && tempGetRelatedStringList.Count > 0)
                {
                    foreach (Entry e in entries)
                    {
                        foreach (string s in tempGetRelatedStringList)
                        {
                            // If matching ID and list not already contains clone with same ID
                            if (s == e.ID && !ContainsMatch(tempGetRelatedEntryList, s))
                            {
                                tempGetRelatedEntryList.Add(IO.CloneEntry(e, true));
                            }
                        }
                    }
                }
                try
                {
                    // Sort related before returning
                    tempGetRelatedEntryList.Sort(delegate (Entry x, Entry y) { return x.Title.CompareTo(y.Title); });
                }
                catch
                {
                    // Do nothing (skip sorting)
                }
                return tempGetRelatedEntryList;
            }
            return new List<Entry>();
        }

        private static List<Entry> tempMasterCollection;
        public static List<Entry> GetMasterCollectionAsClone(List<Entry> entries)
        {
            // Ready list
            if (tempMasterCollection == null)
            {
                tempMasterCollection = new List<Entry>();
            }
            tempMasterCollection.Clear();

            if (HasRelated(entries))
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    if (HasRelated(entries[i]) && !ContainsMatch(tempMasterCollection, entries[i].ID))
                    {
                        tempMasterCollection.Add(IO.CloneEntry(entries[i], false));
                    }
                }
            }
            return tempMasterCollection;
        }


        private static List<Entry> tempMasterSlaveCollection;
        public static List<Entry> GetAllMastersRelatedToSlave(Entry slave, List<Entry> entries)
        {
            // Ready list
            if (tempMasterSlaveCollection == null)
            {
                tempMasterSlaveCollection = new List<Entry>();
            }
            tempMasterSlaveCollection.Clear();

            if (IsValidSlave(slave) && IsValidPool(entries) && IsRelated(slave, entries))
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    if (ContainsMatch(entries[i], slave) && !ContainsMatch(tempMasterSlaveCollection, slave.ID))
                    {
                        tempMasterSlaveCollection.Add(IO.CloneEntry(entries[i], false));
                    }
                }
            }
            return tempMasterSlaveCollection;
        }

        public static int CountRelated(Entry master)
        {
            return GetRelatedAsList(master).Count;
        }

        public static int GetMasterRelationsCount(Entry slave, List<Entry> entries)
        {
            tempCounter = 0;
            if (IsValidSlave(slave) && IsValidPool(entries))
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    if (!string.IsNullOrEmpty(entries[i].RelatedEntries) && entries[i].RelatedEntries.Contains(slave.ID))
                    {
                        tempCounter += 1;
                    }
                }
            }
            return tempCounter;
        }

        private static bool IsValidSlave(Entry slave)
        {
            if (slave != null && !string.IsNullOrEmpty(slave.ID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Is not null and contains > 0 entries
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        private static bool IsValidPool(List<Entry> entries)
        {
            if (entries != null && entries.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // Used to fix discrepancies
        private static List<string> tempDuplicateRemoveList;
        public static bool RemoveDuplicateIds(Entry master, List<Entry> entries)
        {
            bool tempFixed = false;
            try
            {
                // Ready list
                if (tempDuplicateRemoveList == null)
                {
                    tempDuplicateRemoveList = new List<string>();
                }
                tempDuplicateRemoveList.Clear();


                if (IsValidPool(entries))
                {
                    Log.GeneralStatus("Correcting discrepancies");
                    for (int i = 0; i < entries.Count; i++)
                    {
                        if (HasRelated(entries[i]))
                        {
                            tempDuplicateRemoveList.Clear();
                            tempDuplicateRemoveList.AddRange(GetRelatedAsList(entries[i]));
                            Log.GeneralStatus(tempDuplicateRemoveList.Count.ToString());
                            entries[i].RelatedEntries = "";
                            // Check for duplicates
                            for (int k = 0; k < tempDuplicateRemoveList.Count; k++)
                            {
                                Log.GeneralStatus("Iterating");
                                if (!ContainsMatch(entries[i].RelatedEntries, tempDuplicateRemoveList[k]) && MainForm.EntryExist(tempDuplicateRemoveList[k]))
                                {
                                    entries[i].RelatedEntries += tempDuplicateRemoveList[k] + ";";
                                    Log.GeneralStatus("Adding");
                                    tempFixed = true;
                                }
                                else
                                {
                                    Log.GeneralStatus("Discarded (duplicate/non-existent)");
                                }
                            }
                        }

                        // Transfer master back to current entry master
                        if (master != null && tempFixed)
                        {
                            if (entries[i].ID == master.ID)
                            {
                                Log.GeneralStatus("Submitting to current");
                                master.RelatedEntries = entries[i].RelatedEntries;
                                master.RelatedView = false;
                            }
                        }
                    }

                }
            }
            catch
            {
                tempFixed = false;
            }

            // Cleanup
            if (tempDuplicateRemoveList != null)
            {
                tempDuplicateRemoveList.Clear();
            }
            return tempFixed;
        }


        private static List<string> tempDeletionList;
        public static void UpdateMasterWhenDeletingRelated(Entry slaveForDeletion, List<Entry> entries)
        {
            if (IsValidSlave(slaveForDeletion) && IsValidPool(entries))
            {
                Log.GeneralEntryStatus("Related cleanup");
                for (int i = 0; i < entries.Count; i++)
                {
                    // If a master was found containing the deleted entry update the master
                    if (HasRelated(entries[i]) && ContainsMatch(entries[i], slaveForDeletion))
                    {
                        if (tempDeletionList == null)
                        {
                            tempDeletionList = new List<string>();
                        }
                        // Clear list on each iteration
                        tempDeletionList.Clear();
                        // Get all related from master
                        tempDeletionList.AddRange(GetRelatedAsList(entries[i]));
                        // Clear related from entry to add them back without the deleted ID
                        entries[i].RelatedEntries = "";
                        // Add them back to master except the delete entrys ID
                        for (int k = 0; k < tempDeletionList.Count; k++)
                        {
                            if (tempDeletionList[k] != slaveForDeletion.ID && MainForm.EntryExist(tempDeletionList[k]))
                            {
                                entries[i].RelatedEntries += tempDeletionList[k] + ";";
                            }
                            else
                            {
                                Log.GeneralEntryStatus("Relation omitted"); // Called one or more times if an entry was related to several categories
                            }
                        }
                    }
                }
                // Cleanup
                if (tempDeletionList != null)
                {
                    tempDeletionList.Clear();
                }
            }
        }

        public static bool ContainsMatch(Entry master, Entry slave)
        {
            if (HasRelated(master) && IsValidSlave(slave))
            {
                if (master.RelatedEntries.Contains(slave.ID))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool ContainsMatch(string masterId, string slaveId)
        {
            if (!string.IsNullOrEmpty(masterId) && !string.IsNullOrEmpty(slaveId))
            {
                if (masterId.Contains(slaveId))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool ContainsMatch(List<Entry> entries, string idToMatch)
        {
            if (IsValidPool(entries) && !string.IsNullOrEmpty(idToMatch))
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    if (entries[i].ID == idToMatch)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Add related (cannot be another master). Returns true if added
        /// </summary>
        /// <param name="master"></param>
        /// <param name="slave"></param>
        /// <returns></returns>
        public static bool AddRelated(Entry master, Entry slave)
        {
            if (master != null && IsValidSlave(slave))
            {
                if (!ContainsMatch(master, slave) && !HasRelated(slave) && IsValidSlave(slave))
                {
                    master.RelatedEntries += slave.ID + ";";
                    Log.GeneralEntryStatus("Relation added");
                    return true;
                }
                else
                {
                    Log.GeneralEntryStatus("Relation rejected");
                }
            }
            return false;
        }


        private static List<string> tempRemoveRelatedList;
        /// <summary>
        /// Remove related. Returns true if removed
        /// </summary>
        /// <param name="master"></param>
        /// <param name="slave"></param>
        /// <returns></returns>
        public static bool RemoveRelated(Entry master, Entry slave)
        {
            if (tempRemoveRelatedList == null)
            {
                tempRemoveRelatedList = new List<string>();
            }
            tempRemoveRelatedList.Clear();

            bool slaveRemoved = false;
            if (HasRelated(master) && IsValidSlave(slave))
            {
                if (ContainsMatch(master, slave))
                {
                    tempRemoveRelatedList = GetRelatedAsList(master);
                    if (tempRemoveRelatedList.Count > 0)
                    {
                        for (int i = 0; i < tempRemoveRelatedList.Count; i++)
                        {
                            if (tempRemoveRelatedList[i] == slave.ID)
                            {
                                // Remove related ID
                                tempRemoveRelatedList.RemoveAt(i);
                                // Notify
                                slaveRemoved = true;
                                Log.GeneralEntryStatus("Relation removed");
                                break;
                            }
                        }

                        // Add related back to master with slave removed
                        if (slaveRemoved)
                        {
                            // And add related back to master as string
                            master.RelatedEntries = GetRelatedAsString(tempRemoveRelatedList);
                            Log.GeneralEntryStatus("Relations updated");
                        }
                    }
                }
            }
            // Cleanup
            if (tempRemoveRelatedList != null)
            {
                tempRemoveRelatedList.Clear();
            }
            // Notify
            return slaveRemoved;
        }



        /// <summary>
        /// Clear related. Returns true if cleared
        /// </summary>
        /// <param name="master"></param>
        /// <returns></returns>
        public static bool ClearRelated(Entry master)
        {
            if (HasRelated(master))
            {
                master.RelatedEntries = "";
                master.RelatedView = false;
                master.RelatedStyle = 0;
                Log.GeneralEntryStatus("Entry relations cleared");
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Clear all related. Returns true if cleared
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        public static bool ClearAllRelated(Entry entry, List<Entry> entries)
        {
            // Clear selected entry
            if (HasRelated(entry))
            {
                entry.RelatedEntries = string.Empty;
                entry.RelatedView = false;
                entry.RelatedStyle = 0;
                Log.LogPoolStatus("Current relations cleared");
            }

            // Clear pool
            if (HasRelated(entries))
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    entries[i].RelatedEntries = string.Empty;
                    entries[i].RelatedView = false;
                    entries[i].RelatedStyle = 0;
                }
                Log.LogPoolStatus("All relations cleared");
                return true;
            }
            return false;
        }

        private static List<string> tempGetRelatedAsList;
        private static List<string> GetRelatedAsList(Entry master)
        {
            if (HasRelated(master))
            {
                // Ready list
                if (tempGetRelatedAsList == null)
                {
                    tempGetRelatedAsList = new List<string>();
                }
                tempGetRelatedAsList.Clear();

                try
                {
                    //  Get all IDs by split
                    tempGetRelatedAsList.AddRange(master.RelatedEntries.Split(';'));
                    // Remove the last entry as it is empty (the split makes it)
                    if (tempGetRelatedAsList.Count > 0)
                    {
                        for (int i = 0; i < tempGetRelatedAsList.Count; i++)
                        {
                            if (string.IsNullOrEmpty(tempGetRelatedAsList[i]))
                            {
                                tempGetRelatedAsList.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }
                catch
                {
                    // Do nothing
                }
                return tempGetRelatedAsList;
            }
            else
            {
                return null;
            }
        }

        private static string GetRelatedAsString(List<string> related)
        {
            if (related != null && related.Count > 0)
            {
                string tempString = "";
                for (int i = 0; i < related.Count; i++)
                {
                    if (!tempString.Contains(related[i]))
                    {
                        tempString += related[i] + ";";
                    }
                }
                return tempString;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string MasterFromSlaveID { get; set; } // Also used by main form
        public static Entry GetMasterFromSlave(Entry slave, List<Entry> entries)
        {
            MasterFromSlaveID = "";
            if (IsRelated(slave, entries))
            {
                foreach (Entry e in entries)
                {
                    if (ContainsMatch(e, slave))
                    {
                        MasterFromSlaveID = e.ID; // Save ID
                        return e;
                    }
                }
            }
            return slave;
        }



        public static bool HasRelatedMastersMatchingCurrentMark(Entry slave, List<Entry> entries)
        {
            if (CanMark() && IsRelated(slave, entries))
            {
                foreach (Entry e in entries)
                {
                    if (ContainsMatch(e, slave) && e.ID == GetMark())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static List<Entry> RelatedConvert(List<Entry> entries, int currentStyle)
        {
            if (IsValidPool(entries))
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    entries[i].MarkRelated = true;
                    entries[i].RelatedStyle = currentStyle;
                    if (entries.Count == 1 && i == 0)
                    {
                        entries[i].DisplayCode = 3; // End if only one
                    }
                    else
                    {
                        if (i == 0) // Set to 1 if the first entry (to mark beginning)
                        {
                            entries[i].DisplayCode = 1;
                        }
                        else if (i < entries.Count - 1) // Else set to zero if not the last item in the list
                        {
                            entries[i].DisplayCode = 0;
                        }
                        else // Else set to 2 (to mark end)
                        {
                            entries[i].DisplayCode = 2;
                        }
                    }
                }
                return entries;
            }
            else
            {
                return entries;
            }
        }



        // Prefix for an entry inside an expanded category (pinned list).
        // Plain ASCII only: the former styles used box-drawing, shade and bullet characters, whose
        // width and look depend on the font (and are often double-width on East Asian systems).
        //   01- Category
        //      | first / middle entries
        //      \ last entry
        // (The stored RelatedStyle is kept in the Pool for compatibility but no longer changes the look.)
        public static string GetRelatedDisplayChar(Entry entry, int offset)
        {
            string indent = TextTransform.GetBlankSpace(3 + offset);
            switch (entry.DisplayCode)
            {
                case 0: // middle
                case 1: // first
                    return indent + "|";
                case 2: // last
                case 3: // only entry
                    return indent + "\\";
                default:
                    return "?";
            }
        }
        // Category prefix: "-" when expanded, "+" when collapsed
        public static string GetMasterDisplayChar(string lineNumber, Entry entry, int offset, bool ignoreView)
        {
            bool expanded = entry.RelatedView && !ignoreView;
            return lineNumber + TextTransform.GetBlankSpace(offset) + (expanded ? "-" : "+");
        }
        public static string GetDummyMasterChars(int index)
        {
            switch (index)
            {
                case 0:
                    return @"  ______  __  __    ___   __  __  __ ";
                case 1:
                    return @" /\  __ \/\ \/\ \  / __ \/\ \/\ \/\ \";
                case 2:
                    return @"a\ \ \/\ \ \ \ \ \/\ \/\ \ \ \_\ \ \ \";
                case 3:
                    return @"b \ \ \_\ \ \ \_\ \ \ \ \ \ \____ \ \_\";
                case 4:
                    return @"c  \ \  __ \ \  __ \ \ \_\ \/____\ \/_/_";
                case 5:
                    return @"d   \ \ \ \ \ \_\/\ \ \____/ /\_____\/\_\";
                case 6:
                    return @"e    \/_/\/_/\/_/\/_/\/___/  \/_____/\/_/";
                case 7:
                    return @"f";
            }
            return "N/A";
        }


    }
}
