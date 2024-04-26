using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRG278_2_.DataLayer;
namespace PRG278_2_.BusinessLogic
{
    class FormLogic
    {
        public FormLogic() { }

        public bool ValidateUser(string inputUsername, string inputPassword)
        {
            FileHandler handler = new FileHandler();

            List<string> users = new List<string>();
            users = handler.ReadFromFile();
            bool login = false;

            foreach (string user in users)
            {
                string[] credentials = user.Split(',');
                if (credentials.Length == 2)
                {
                    string username = credentials[0];
                    string password = credentials[1];

                    if (username.Trim() == inputUsername && password.Trim() == inputPassword)
                    {
                        login = true;
                    }
                }
            }
            return login;

        }
    }
}
