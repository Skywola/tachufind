using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tachufind
{
    public class GetColorWindowSavedSearches
    {
        GeneralFns generalFns = new GeneralFns();
        Dictionary<int, string> SearchValues = new Dictionary<int, string>();
        List<string> listSearchValues = new List<string>();
        public List<string> ListOfSearches = new List<string>();
        FileIO FIO = new FileIO();
        public bool searchExists = true;

        public List<string> GetEntryTitleByName(string entryTitle)
        {
            List<string> settings = new List<string>();
            List<string> sectionsList = new List<string>();
            List<string> ItemsList = new List<string>();
            string fileContents = "";

            try
            {
                // ListFilePath is the selected find - replace item from list
                string ListFilePath = AppSettings.Data_Folder + entryTitle + AppSettings.FindReplaceFileExtension;
                if (!File.Exists(ListFilePath)) // If not exists, just drop out
                {
                    return settings;
                }
                fileContents = FileIO.ReadFile(ListFilePath);
                if (fileContents.Length < 1)
                {
                    return settings;
                }
                ItemsList = Regex.Split(fileContents, "\r\n").ToList();
                return ItemsList;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ItemsList;
        }



    }
}
