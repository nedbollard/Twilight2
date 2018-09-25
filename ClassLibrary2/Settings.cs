using System;
using System.IO;


namespace ClassLibrary2
{
    public static class Settings
    {
        public static string fileDirectory { get; set; }
        public static string backupDirectory { get; set; }
        public static int RoundId { get; set; }
        public static string CourseId { get; set; }


        public static void LoadSettings()
        {
            if (File.Exists(@"c:\Twilight\Files\settings.txt"))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(@"c:\Twilight\Files\settings.txt"))
                    {
                        string line;

                        while ((line = sr.ReadLine()) != null)
                        {

                            string[] settingFields = new string[2];
                            settingFields = line.Split(',');

                            if (settingFields[0] == "files")
                            {
                                Settings.fileDirectory = settingFields[1];
                            }
                            else
                            {
                                Settings.backupDirectory = settingFields[1];
                            };

                        }
                    }
                }
                catch (Exception ex)
                {
                    // Let the user know what went wrong.

                    Console.WriteLine("Settings file could not be read:");
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Settings file needs to be installed");
            };


            Settings.RoundId = 0;
            Settings.CourseId = "Blank";
        }
    }
}
