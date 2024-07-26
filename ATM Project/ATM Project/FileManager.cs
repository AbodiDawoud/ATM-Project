using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Project
{
    internal class FileManager
    {
        public static string GetLineAtIndex(string path, int lineIndex)
        {
            if (!DoesFileExist(path))
            {
                return "File not exist";
            }
            string[] lines = File.ReadAllLines(path);

            if (lineIndex < 0 || lineIndex > lines.Length - 1)
            {
                return "Line not found";
            }
            return lines[lineIndex];
        }

        public static bool UpdateLineAtIndex(string path, int lineIndex, string newLineContent)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            string[] lines = File.ReadAllLines(path);

            if (lineIndex < 0 || lineIndex > lines.Length)
            {
                return false;
            }

            lines[lineIndex] = newLineContent;
            File.WriteAllLines(path, lines);
            return true;
        }

        public static bool RemoveLineAtIndex(string path, int lineIndex)
        {
            if (DoesFileExist(path))
            {
                string[] allLines = File.ReadAllLines(path);

                if (lineIndex < 0 || lineIndex > allLines.Length)
                {
                    return false;
                }
                var newLines = new string[allLines.Length - 1];
                for (int i = 0, j = 0; i < allLines.Length; i++)
                {
                    if (i != lineIndex)
                    {
                        newLines[j++] = allLines[i];
                    }
                }
                File.WriteAllLines(path, newLines);
                return true;
            }
            return false;
        }

        public static bool AddLineAtIndex(string path, int lineIndex, string lineContent)
        {
            if (!DoesFileExist(path))
            {
                return false;
            }

            string[] lines = File.ReadAllLines(path);

            if (lineIndex < 0 || lineIndex > lines.Length)
            {
                return false;
            }

            var updatedLines = new string[lines.Length + 1];
            for (int i = 0, j = 0; i < updatedLines.Length; i++)
            {
                if (i == lineIndex)
                {
                    updatedLines[i] = lineContent;
                }
                else
                {
                    updatedLines[i] = lines[j++];
                }
            }
            File.WriteAllLines(path, updatedLines);
            return true;
        }

        public static bool RemoevSavedFile(string path)
        {
            if (DoesFileExist(path))
            {
                File.Delete(path);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DoesFileExist(string filePath)
        {
            return File.Exists(filePath);
        }

        public static int GetLineCount(string path)
        {
            if (!DoesFileExist(path))
            {
                return -1;
            }
            else
            {
                string[] lines = File.ReadAllLines(path);
                return lines.Length;
            }
        }

        public static bool AddLineToFile(string path,string lineContent)
        {
            try
            {
                File.AppendAllText(path, lineContent + Environment.NewLine);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void CreateNewFile(string pathName)
        {
            StreamWriter sw = new StreamWriter(pathName);
            sw.Close();
        }
    }
}
