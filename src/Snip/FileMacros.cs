﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andtech.Snip
{
	internal static class FileMacros
	{

		public static void Move(string path, string destination)
		{
			var attributes = File.GetAttributes(path);
			if (attributes.HasFlag(FileAttributes.Directory))
			{
				Directory.Move(path, destination);
			}
			else
			{
				File.Move(path, destination);
			}
		}

		public static void Copy(string path, string destination)
		{
			var attributes = File.GetAttributes(path);
			if (attributes.HasFlag(FileAttributes.Directory))
			{
                DirectoryCopy(path, destination, true);
			}
			else
			{
				File.Copy(path, destination);
			}
		}

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }
    }
}
