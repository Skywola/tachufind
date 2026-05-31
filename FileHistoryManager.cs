using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Tachufind
{
    internal class FileHistoryManager
    {
        private List<string> filePaths; // Holds the recent file paths

        // Constructor         Usage:  FileHistoryManager fileHistoryManager = new FileHistoryManager(10);
        public FileHistoryManager() // 32 is default, this is an optional argument
        {
            this.filePaths = [];
        }

        // Get the first file path in the list
        public string GetFirstFilePath()
        {
            return filePaths.FirstOrDefault() ?? string.Empty;
        }

        // Add a new file to the list (adds to the top, removes duplicates, prevents blank entries)
        // Usage:  fileHistoryManager.AddFile("C:\\example.txt", fileToolStripMenu, STATIC_MENU_ITEMS); 
        // Usage:  fileHistoryManager.AddFilePath(SaveFileDialog1.FileName, fileToolStripMenu, STATIC_MENU_ITEMS);
        public void AddFilePath(string filePath, ToolStripMenuItem menu, int staticMenuItems)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath)) return; // Prevent blank/empty paths

                string strExt = System.IO.Path.GetExtension(filePath);
                // These three file types are never to be written to the file paths menu
                if (strExt == ".fdta" || strExt == ".qdta" || strExt == ".FTDA" || strExt == ".QDTA") return; 

                if (filePaths != null)
                {
                    // Remove the file if it already exists to avoid duplicates
                    filePaths.Remove(filePath);

                    // Insert the new file at the top of the list
                    filePaths.Insert(0, filePath);

                    // If the list exceeds the maximum allowed, remove the oldest file
                    if (filePaths.Count > AppConstants.MAXIMUM_FILEMENU_ITEMS)
                    {
                        filePaths.RemoveAt(filePaths.Count - 1); // Remove the oldest entry
                    }
                }

                // Repopulate the dropdown menu
                LoadDropDownMenu(menu, staticMenuItems);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void RemoveFilePath(string filePath, ToolStripMenuItem menu, int staticMenuItems)
        {
            try
            {
                // Ensure filePaths is not null
                if (filePaths != null && filePaths.Contains(filePath))
                {
                    // Remove the file from the list
                    filePaths.Remove(filePath);

                    // Repopulate the dropdown menu
                    LoadDropDownMenu(menu, staticMenuItems);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Populates the File menu with recent files, adding recent files to a ToolStripMenuItem for a drop-down list
        // Usage:  fileHistoryManager.LoadDropDownMenu(fileMenu, 6);   
        public void LoadDropDownMenu(ToolStripMenuItem menu, int staticMenuItems)
        {
            try
            {
                // Remove only the dynamically added recent file items (those after the static items count)
                while (menu.DropDownItems.Count > staticMenuItems)
                {
                    menu.DropDownItems.RemoveAt(menu.DropDownItems.Count - 1);
                }

                // Add each recent file to the dropdown after the initial menu items
                if (filePaths != null)
                {
                    foreach (var file in filePaths)
                    {
                        ToolStripMenuItem fileItem = new(file);
                        menu.DropDownItems.Add(fileItem); // Add after the static items
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Load files from a saved list (e.g., from a file or settings)
        // Usage:  fileHistoryManager.LoadFilesFromTextFile("recentFiles.txt");
        public void LoadFilesFromTextFile(string filePath, bool create = false)
        {
            try
            {
                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    if (create)
                    {
                        // Create a blank file
                        using (File.Create(filePath)) // The 'using' statement ensures the file is properly closed after creation
                        {
                            // 
                        }
                    }

                    return; // Exit if the file doesn't exist
                }

                // Read all the lines from the text file (each line is assumed to be a file path)
                var savedFiles = File.ReadAllLines(filePath)
                                     .Where(f => !string.IsNullOrWhiteSpace(f)) // Filter out empty lines
                                     .Distinct() // Remove any duplicates
                                     .ToList();

                // Store the valid file paths in the manager's filePaths list
                filePaths = savedFiles;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Save the current list of files (can be saved to a file, settings, etc.)
        // Usage:  fileHistoryManager.SaveFilePathsToTextFile("recentFiles.txt");
        public void SaveFilePathsToTextFile(string filePath)
        {
            // Write each file path to the text file
            File.WriteAllLines(filePath, filePaths);
        }
    }
}