using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public static class CitySaver
    {
        private const string FileName = @"City.txt";
        private const string DirectoryPath = @"C:\temp123\";

        public static void Save(string city)
        {

            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }


            // Создание файла
            using (FileStream fs = File.Create(DirectoryPath + FileName))
            {

                byte[] info = new UTF8Encoding(true).GetBytes($"{city}");

                fs.Write(info, 0, info.Length);
            }

        }

        public static string Read()
        {
            return File.ReadAllText(DirectoryPath + FileName);
        }
    }
}
