using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PRG278_2_.PresentationLayer;
using PRG278_2_.BusinessLogic;
namespace PRG278_2_
{
    public partial class LoginIn : Form
    {
        public LoginIn()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginIn_Load(object sender, EventArgs e)
        {
           
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            FormLogic formLogic = new FormLogic();
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            bool login = formLogic.ValidateUser(username, password);

            if (login)
            {
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
            this.Hide();
        }
    }
}
