using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PRG278_2_.DataLayer;
using PRG278_2_.BusinessLogic;
using System.IO;

namespace PRG278_2_.PresentationLayer
{

    public partial class MainForm : Form
    {
        private static readonly string currentDir = Directory.GetCurrentDirectory();
        //define handler to be used throughout
        DataHandler handler = new DataHandler();
        Logic logicHander = new Logic();
        string StudentImageLocation = @"Resources\anon.png";//default value $"{currentDir}"+@"\Resources\Users.txt";

        string gender = null;//to be set by radio button

        bool inStudentTab = true;
        int counterForTab = 0;
        public MainForm()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
            
                string module = txtModuleCode.Text;
                if (txtModuleCode.Text != null || txtModuleCode.Text != "")
                { 
                    handler.DeleteModule(module);
                }
            clearStudentForm();
            
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Load All Students Information under data grid view
            dataGridViewManagement.DataSource = handler.GetAllStudents();

            //load all modules under the selection for the module code
            foreach (DataRow dr in handler.getAllModules().Rows)
            {
                ListViewItem item = new ListViewItem(dr["ModuleCode"].ToString());
                item.SubItems.Add(dr["ModuleName"].ToString());

                //add to LSV
                listViewModuleSelection.Items.Add(item);
                Current_Student_Modules.Items.Clear();

            }
        }

        private void btnInsertStudent_Click(object sender, EventArgs e)
        {

            //check selected gender
            radiobutton();
           // if(StudentImageLocation is null || StudentImageLocation == " ")
           // {
                //Student object to hold each student information
                Student studentInfo = new Student(int.Parse(txtStudentNumber.Text), txtName.Text, txtSurname.Text, StudentImageLocation, dateTimeDOB.Value, gender, txtPhone.Text, txtAddress.Text);

                //create a list of type string to hold all the modules the student takes
                List<string> studentModules = new List<string>();

                
                //store student modules in a list
                foreach (ListViewItem s in listViewModuleSelection.CheckedItems)
                {
                    studentModules.Add(s.Text);

                }
                //send student to logic layer
                 MessageBox.Show(logicHander.CreateStudent(studentInfo, studentModules));
                
                //clear form
                clearStudentForm();
            //}
            //else
            //{
            //    MessageBox.Show("Please select an image");
            //    OpenFileDialog open = new OpenFileDialog();
            //    open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.PNG|*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.PNG;";
            //    pictureBoxStudent.Image = Image.FromFile($"{currentDir}"+@"\Resources\UplodImage_icon.png");
            //}

            

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.PNG|*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.PNG;";
            if(open.ShowDialog() == DialogResult.OK)
            {
                StudentImageLocation = open.FileName;
                pictureBoxStudent.Image = new Bitmap(open.FileName);
            }

            //save images in image folder for the rest of the team to access
            File.Copy(StudentImageLocation, Path.Combine($"{currentDir}", Path.GetFileName(StudentImageLocation)), true);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Current_Student_Modules.Items.Clear();
            //create a list that will hod the student modules
            List<string> studentModules = new List<string>();

            //IF counter is even we are dealing with srudents
            if (inStudentTab && (e.RowIndex >= 0) && (counterForTab%2)==0)
            {
                DataGridViewRow row = this.dataGridViewManagement.Rows[e.RowIndex];
                txtStudentNumber.Text = row.Cells["StudentNumber"].Value.ToString();
                txtName.Text = row.Cells["StudentName"].Value.ToString();
                txtSurname.Text = row.Cells["StudentSurname"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value.ToString();
                txtAddress.Text = row.Cells["StudentAddress"].Value.ToString();
                dateTimeDOB.Value = (DateTime)row.Cells["DOB"].Value;
               string imageL = row.Cells["Image"].Value.ToString();

                //string for gender and radio button selection
                string genderFromGrid = row.Cells["Gender"].Value.ToString();
                if ( genderFromGrid == "Female" || genderFromGrid == "female" || genderFromGrid == "FEMALE")
                {
                    radioBtnFemale.Checked = true;
                }
                else
                {
                    radioBtnMale.Checked = true;
                }

                studentModules = logicHander.GetSpecificStudentModules(int.Parse(txtStudentNumber.Text));

                foreach (string s in studentModules)
                {
                    Current_Student_Modules.Items.Add(s);
                }

                //show the student image
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.PNG|*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.PNG;";
                pictureBoxStudent.Image = Image.FromFile($"{currentDir}"+@"\"+$"{imageL}");
            }

            //else we are under the module code
            else
            {
                DataGridViewRow row = this.dataGridViewManagement.Rows[e.RowIndex];
                txtModuleCode.Text = row.Cells["ModuleCode"].Value.ToString();
                txtModuleName.Text = row.Cells["ModuleName"].Value.ToString();
                richTextBoxDescription.Text = row.Cells["Description"].Value.ToString();
                txtModuleLink.Text = row.Cells["Links"].Value.ToString();
            }
        }

        private void tabModule_MouseClick(object sender, MouseEventArgs e)
        {
            counterForTab++;
            if ((counterForTab % 2) == 0)
            {
                //Load All Students Information under data grid view
                dataGridViewManagement.DataSource = handler.GetAllStudents();
            }
            else
            {
                //Load All Students Information under data grid view
                dataGridViewManagement.DataSource = handler.getAllModules();
            }


        }

        private void button11_Click(object sender, EventArgs e)
        {
            counterForTab = 1;
            inStudentTab = false;
            //Load All Students Information under data grid view
              dataGridViewManagement.DataSource = handler.getAllModules();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            counterForTab = 0;
            //Load All Students Information under data grid view
            dataGridViewManagement.DataSource = handler.GetAllStudents();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (inStudentTab && (counterForTab % 2) == 0)
            {
                int studentNumber = int.Parse(txtStudentNumber.Text);
                if (txtStudentNumber.Text != null || txtStudentNumber.Text != "")
                {

                    // Call the DeleteStudent method from your DataHandler class
                    Console.WriteLine("std to delete: " + studentNumber);
                    handler.DeleteStudentModules(studentNumber);

                    // Refresh the DataGridView to reflect the changes
                    //RefreshDataGridView();
                }
                else
                {
                    MessageBox.Show("Please select a student to delete.");
                }
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            //module object to hold each student information
            Module module = new Module(txtModuleCode.Text, txtModuleName.Text, richTextBoxDescription.Text, txtModuleLink.Text);

            
            //send module to logic layer
            MessageBox.Show(logicHander.CreateModule(module));
            //clear form
            clearModuleForm();
        }

        public void clearModuleForm()
        {
            txtModuleCode.Text = null;
            txtModuleName.Text = null;
            richTextBoxDescription.Text = null;
            txtModuleLink.Text = null;
        }
        public void clearStudentForm()
        {
            txtStudentNumber.Text = null;
            txtName.Text = null;
            txtSurname.Text = null;
            StudentImageLocation = null;
            dateTimeDOB.Value = DateTime.Now;
            txtPhone.Text = null;
            txtAddress.Text = null;
            foreach (ListViewItem s in listViewModuleSelection.CheckedItems)
            {
                s.Checked = false;

            }
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.PNG|*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.PNG;";
            pictureBoxStudent.Image = Image.FromFile($"{currentDir}"+ @"\Resources\UplodImage_icon.png");
        }

        private void btnUpdateModule_Click(object sender, EventArgs e)
        {
            if(txtModuleCode.Text != null || txtModuleCode.Text == "")
            {
                handler.UpdateModule(txtModuleCode.Text, txtModuleName.Text, txtModuleLink.Text, richTextBoxDescription.Text);
                MessageBox.Show("Module information successfully updated");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(txtStudentNumber.Text != null || txtStudentNumber.Text == "")
            {
                radiobutton();
                handler.UpdateStudent(int.Parse(txtStudentNumber.Text), txtName.Text, txtSurname.Text, txtPhone.Text, txtAddress.Text, dateTimeDOB.Value, gender, StudentImageLocation);

            }
        }

        //check radio button then set gender
        public void radiobutton()
        {
            if (radioBtnMale.Checked)
            {
                gender = "Male";
            }
            else
            {
                if (radioBtnFemale.Checked) { gender = "Female"; }
                else { MessageBox.Show("Select a gender"); }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<string> student = new List<string>();
            int studentNumber = int.Parse(txtStudentNumberFromSearch.Text);
            student = logicHander.SearchStudent2Display(studentNumber);
            txtStudentNumber.Text = student[0];
            txtName.Text = student[1];
            txtSurname.Text = student[2];
            //show the student image
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.PNG|*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.PNG;";
            pictureBoxStudent.Image = Image.FromFile($"{currentDir}"+@"\"+$"{student[3]}");
            //Console.WriteLine("=========================="+student[3]);
            dateTimeDOB.Value = Convert.ToDateTime(student[4]);

            if (student[5] == "Female" || student[5] == "female" || student[5] == "FEMALE")
            {
                radioBtnFemale.Checked = true;
            }
            else
            {
                radioBtnMale.Checked = true;
            }
            txtPhone.Text = student[6];
            txtAddress.Text = student[7];

        }
    }
}
