using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRG278_2_.DataLayer;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace PRG278_2_.BusinessLogic
{
    class Logic
    {
        public Logic() { }
        DataHandler handler = new DataHandler();
        //Create Student
        public string CreateStudent(Student student, List<string> studentModules)
        {
            string message;
            
            //check if student exisists in DB
            bool isThere = handler.SearchStudent(student.StudentNumber);
            if (isThere)
            {
                message = "Student already exists";
                return message;
            }
            else
            {
                //add student to the DB
                handler.AddStudent(student);
                //add student modules to bridge
                handler.AddStudentModules(student.StudentNumber, studentModules);

                message = $"Student successfully added!";
                return message;
            }


        }

        //create a list with each student specific modules
        public List<string> GetSpecificStudentModules(int stdNo)
        {
            //create a list to hold student modules
            List<string> studentModules = new List<string>();
            for(int i = 0; i < handler.GetSpecificStudentModules(stdNo).Rows.Count; i++)
            {
                studentModules.Add(handler.GetSpecificStudentModules(stdNo).Rows[i]["ModuleCode"].ToString());
            }
            
            return studentModules;
        }

        //create a new module
        public string CreateModule(Module module)
        {
            string message;

            //check if student exisists in DB
            bool isThere = handler.SearchModule(module);
            if (isThere)
            {
                message = "Module already exists";
                return message;
            }
            else
            {
                //add student to the DB
                handler.AddModule(module);
                
                message = $"Module successfully added!";
                return message;
            }


        }

        //search functionality
        public List<string> SearchStudent2Display(int studentNumber)
        {   //create a student object to send to main page
           List<string> foundStudent = new List<string>();

            if (handler.SearchStudent(studentNumber))
            {
                
                DataTable tblStudent = handler.getStudent(studentNumber);
                foreach(DataRow row in tblStudent.Rows)
                {
                    
                    foundStudent.Add(row["StudentNumber"].ToString());
                    foundStudent.Add(row["StudentName"].ToString());
                    foundStudent.Add(row["StudentSurname"].ToString());
                    foundStudent.Add(row["Image"].ToString());
                    foundStudent.Add(row["DOB"].ToString());
                    foundStudent.Add(row["Gender"].ToString());
                    foundStudent.Add(row["Phone"].ToString());
                    foundStudent.Add(row["StudentAddress"].ToString());
                    

                }

            }
            else
            {
                //create an empty list
                for(int i = 0; i < 8; i++)
                {
                    foundStudent.Add(null);
                }
                MessageBox.Show("Student does not exist");
            }
            return foundStudent;
        }
    }
}
