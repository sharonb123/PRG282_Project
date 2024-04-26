using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using PRG278_2_.DataLayer;

namespace PRG278_2_.PresentationLayer
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }
        private static readonly string currentDir = Directory.GetCurrentDirectory();
        private void btnSignUp_Click(object sender, EventArgs e)
        {
            FileHandler handler = new FileHandler();

            string path = $"{currentDir}" + @"\Resources\Users.txt"; ;

            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (File.Exists(path))
            {
                handler.WriteToFile(username, password);
                MessageBox.Show("Successfuly signed up!");
            }

            this.Hide();
            LoginIn login = new LoginIn();
            login.Show();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }
    }
}
