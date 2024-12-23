using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GameEngine
{
    public class FileName
    {
        public void Name(string nickname)
        {
            string path = @"C:\temp123\Name.txt";

            
            if (!Directory.Exists(@"C:\temp123"))
            {
                Directory.CreateDirectory(@"C:\temp123");
            }


            // Создание файла
            using (FileStream fs = File.Create(path))
            {

                byte[] info = new UTF8Encoding(true).GetBytes($"{nickname}");

                fs.Write(info, 0, info.Length);
            }

        }
    }
}



