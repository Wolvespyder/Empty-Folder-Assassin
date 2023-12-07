using System;
using System.Collections.Generic;
using System.IO;

namespace DeleteEmptyFolders {
    internal class Program {

        public static string PATH = "";
        public static int DELETED_FOLDER_COUNT = 0;
        public static List<string> SUBDIRECTORIES = new List<string>();
        public static List<string> DELETED_FOLDERS = new List<string>();

        public static void GetAllSubdirectories(string path) {
            if (Directory.Exists(path)) {
                foreach (string subdirectory in Directory.GetDirectories(path)) {
                    if (!SUBDIRECTORIES.Contains(subdirectory))
                        SUBDIRECTORIES.Add(subdirectory);

                    var subdirectories = Directory.GetDirectories(subdirectory);
                    if (subdirectories.Length > 0) {
                        foreach (var foundSubdirectory in subdirectories) {
                            if (!SUBDIRECTORIES.Contains(foundSubdirectory))
                                SUBDIRECTORIES.Add(foundSubdirectory);

                            GetAllSubdirectories(foundSubdirectory);
                        }
                    }
                }
            }
        }

        public static void DeleteDirectories() {

            GetAllSubdirectories(PATH);
            Console.WriteLine("\n" + SUBDIRECTORIES.Count + " total directories found.\n");


            for (var i = SUBDIRECTORIES.Count - 1; i >= 0; i--) {
                var subdirectory = SUBDIRECTORIES[i];
                var files = Directory.GetFiles(subdirectory);
                var subdirectories = Directory.GetDirectories(subdirectory);
                if (files.Length == 0 && subdirectories.Length == 0) {
                    Directory.Delete(subdirectory, true);
                    DELETED_FOLDERS.Add(subdirectory);
                    DELETED_FOLDER_COUNT++;
                }
            }
        }

        static void Main(string[] args) {

            Console.Write("Enter the full path to delete empty folders from: ");
            PATH = Console.ReadLine();
            DeleteDirectories();
            Console.WriteLine(DELETED_FOLDER_COUNT + " folders deleted:");
            foreach(var deletedFolder in DELETED_FOLDERS) {
                Console.WriteLine(deletedFolder);
            }

            Console.WriteLine("\nPress ENTER to close the application");
            Console.ReadLine();
        }
    }
}


