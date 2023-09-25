using System;
using System.IO;

namespace BackupCleaner
{
    class Program
    {
        private const string BackupFolderPath = @"C:\Program Files\Microsoft SQL Server\MSSQL15.SQL2019\MSSQL\Backup";
        private const string FilePrefix = "Precision_JB2_backup_";
        private const int DaysThreshold = 90; // 3 months

        static void Main()
        {
            try
            {
                // Check if the directory exists
                if (!Directory.Exists(BackupFolderPath))
                {
                    Console.WriteLine($"Directory {BackupFolderPath} does not exist.");
                    return;
                }

                // Get all .bak files with the specified prefix
                var backupFiles = Directory.GetFiles(BackupFolderPath, $"{FilePrefix}*.bak");

                foreach (var file in backupFiles)
                {
                    var fileInfo = new FileInfo(file);
                    var lastModified = fileInfo.LastWriteTime;

                    // Check if the file was last modified more than 3 months ago
                    if (DateTime.Now - lastModified > TimeSpan.FromDays(DaysThreshold))
                    {
                        Console.WriteLine($"Deleting {file}...");
                        File.Delete(file);
                    }
                }

                Console.WriteLine("Cleanup completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}