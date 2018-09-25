using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary2
{
    class FileHandler
    {
        public static void BackupFile(string fileName)
        {
            //rename file with current timestamp

            string sourceFileName = Settings.fileDirectory + fileName;
            string backupFileName = Settings.backupDirectory + fileName;

            try
            {
                int index = backupFileName.IndexOf(".");
                var newFileName = backupFileName.Substring(0, index) + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm_ssfff") + backupFileName.Substring(index);

                File.Move(sourceFileName, newFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(sourceFileName + "... the file could not be renamed/moved:");
                Console.WriteLine(ex.Message);
            }

            //// create an empty file

            using (var fred = File.Create(sourceFileName)) { };

        }
    }
}
