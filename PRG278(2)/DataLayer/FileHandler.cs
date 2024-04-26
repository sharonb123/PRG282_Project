using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PRG278_2_.DataLayer
{
    class FileHandler
    {
       // public FileHandler() { }

        private static readonly string currentDir = Directory.GetCurrentDirectory();
        //C:\Users\Precious\Documents\secondYear\PGR282\project\PRG278(2) Project\PRG278(2)\PRG278(2)\bin\Debug
        //C:\Users\Precious\Documents\secondYear\PGR282\project\PRG278(2) Project\PRG278(2)\PRG278(2)\Resources

        public FileHandler()
        {
            
        }

        string path = $"{currentDir}"+@"\Resources\Users.txt";

        public void WriteToFile(string username, string password)
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(username.Trim() + "," + password.Trim());
            }
        }
        public List<string> ReadFromFile()
        {
            List<string> users = new List<string>();
            users = File.ReadAllLines(path).ToList();
            return users;
        }
    }
}
