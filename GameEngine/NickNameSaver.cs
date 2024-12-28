using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GameEngine
{
    public static class NickNameSaver
    {
        private const string FileName = @"Name.txt";
        private const string DirectoryPath = @"C:\temp123\";

        public static void Save(string nickname)
        {
                        
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }


            // Создание файла
            using (FileStream fs = File.Create(DirectoryPath + FileName))
            {

                byte[] info = new UTF8Encoding(true).GetBytes($"{nickname}");

                fs.Write(info, 0, info.Length);
            }

        }

        public static string Read()
        {
            return File.ReadAllText(DirectoryPath + FileName);
        }
    }
}



