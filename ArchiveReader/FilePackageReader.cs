using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;


namespace ArchiveReader
{
    public class FilePackageReader
    {

        public FilePackageReader()
        {
            
        }

        #region Ищет все файлы любого расширения в папках, конктретной папки
        public void SearchTxtFiles()
        {
            //получаем переменную Windows с адресом текущего пользователя
            string PatchProfile = Environment.GetEnvironmentVariable(@"C:\");
            //ищем все вложенные папки 
            string[] S = SearchDirectory(PatchProfile);
            //создаем строку в которой соберем все пути
            string ListPatch = "найденные файлы \n"; //заголовок для строк
            foreach (string folderPatch in S)
            {
                //добавляем новую строку в список
                // ListPatch += folderPatch + "\n";
                try
                {
                    //пытаемся найти данные в папке 
                    string[] F = SearchFile(folderPatch, "*.txt");
                    foreach (string FF in F)
                    {
                        //добавляем файл в список 
                        ListPatch += FF + "\n";
                    }
                }
                catch
                {

                }
            }
            //выводим список на экран 
            Console.WriteLine(ListPatch);
        }

        string[] SearchFile(string patch, string pattern)
        {
            /*флаг SearchOption.AllDirectories означает искать во всех вложенных папках*/
            string[] ResultSearch = Directory.GetFiles(patch, pattern, SearchOption.AllDirectories);
            //возвращаем список найденных файлов соответствующих условию поиска 
            return ResultSearch;
        }

        string[] SearchDirectory(string patch)
        {
            patch = @"C:\";
            //находим все папки в по указанному пути
            string[] ResultSearch = Directory.GetDirectories(patch);
            //возвращаем список директорий
            return ResultSearch;
        }
        #endregion

        #region Копирует в другое место все файлы 1-го архива
        public void Search()
        {
            string zipPath = @"C:\Zip.zip"; // Что копирует

            //Console.WriteLine("Provide path where to extract the zip file:");
            //string extractPath = Console.ReadLine();

            string extractPath = @"C:\Games\"; // Куда копирует

            // Normalizes the path.
            extractPath = Path.GetFullPath(extractPath);

            // Ensures that the last character on the extraction path
            // is the directory separator char.
            // Without this, a malicious zip file could try to traverse outside of the expected
            // extraction path.
            if (!extractPath.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
                extractPath += Path.DirectorySeparatorChar;

            using (ZipArchive archive = System.IO.Compression.ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        // Gets the full path to ensure that relative segments are removed.
                        string destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.FullName));

                        // Ordinal match is safest, case-sensitive volumes can be mounted within volumes that
                        // are case-insensitive.
                        if (destinationPath.StartsWith(extractPath, StringComparison.Ordinal))
                            entry.ExtractToFile(destinationPath);
                    }
                }
            }         
            ///////  Читает из txt файла
            String line;
            try
            {   
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C:\\Games\\Logs.txt");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
           
        }
        #endregion

        #region Ищет ВСЕ папки на компьютере
        public void ShowAllFoldersUnder(string path, int indent)
        {
            try
            {
                if ((File.GetAttributes(path) & FileAttributes.ReparsePoint)
                    != FileAttributes.ReparsePoint)
                {
                    foreach (string folder in Directory.GetDirectories(path))
                    {
                        Console.WriteLine(
                            "{0}{1}", new string(' ', indent), Path.GetFileName(folder));
                        ShowAllFoldersUnder(folder, indent + 2);
                    }
                }
            }
            catch (UnauthorizedAccessException) { }
        }
        #endregion

        #region Читает только Zip
        public string ReadZip()
        {
            var info = "";
            var zip = new ZipInputStream(File.OpenRead(@"C:\Zip.zip"));
            var filestream = new FileStream(@"C:\Zip.zip", FileMode.Open, FileAccess.Read);
            ICSharpCode.SharpZipLib.Zip.ZipFile zipfile = new ICSharpCode.SharpZipLib.Zip.ZipFile(filestream);
            ZipEntry item;
            while ((item = zip.GetNextEntry()) != null)
            {
                Console.WriteLine(item.Name);
                using (StreamReader s = new StreamReader(zipfile.GetInputStream(item)))
                {
                    // stream with the file
                    //  Console.WriteLine(s.ReadToEnd());
                    while(s.EndOfStream)
                    {
                        info = s.ReadToEnd().ToString();
                    }
                   
                }
            }           
             return info;     
        }
        #endregion

       

        public static void ReadWinrar() 
        {
           
        }
    }

}
