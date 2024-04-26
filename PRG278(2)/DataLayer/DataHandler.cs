using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace PRG278_2_.DataLayer
{
    class DataHandler
    {
        public DataHandler() { }
        string connection = @"Data Source=MARTIN-PC\SQLEXPRESS;Initial Catalog=BelgiumCampusStudentManagement;Integrated Security=True";


        //Get All Students
        //Get All the modules
        public DataTable GetAllStudents()
        {
            DataTable dt = new DataTable();
            //StudentNumber, StudentName, StudentSurname, DOB, Gender, Phone, StudentAddress
            string query = "SELECT * FROM Students;";

            //establish connection using adapter
            SqlDataAdapter da = new SqlDataAdapter(query, connection);

            //fill the data table
            da.Fill(dt);

            return dt;
        }
        //Get All the modules
        public DataTable getAllModules()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Modules";

            //establish connection using adapter
            SqlDataAdapter da = new SqlDataAdapter(query, connection);

            //fill the data table
            da.Fill(dt);
   
            return dt;
        }
    
        //search for student
        public bool SearchStudent(int studentNumber)
        {
            DataTable dt = new DataTable();
            bool answer = true;
            string query = $"SELECT * FROM Students WHERE StudentNumber = {studentNumber};";

            //establish connection using adapter
            SqlDataAdapter da = new SqlDataAdapter(query, connection);

            //fill the data table
            da.Fill(dt);
            //check if table is emty
            if (dt.Rows.Count == 0){
                answer = false;
            }
            return answer; 

        }


        //Add student to the Database
        public void AddStudent(Student student)
        {

            string query = $"INSERT INTO Students VALUES ({student.StudentNumber}, '{student.StudentName}'," +
                    $"'{student.StudentSurname}', '{student.ImageLocation}', '{student.DOB}', '{student.Gender}', '{student.Phone}', '{student.Address}')";


            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }


        }
        //Add student modules
        public void AddStudentModules(int studentNumber, List<string> modules)
        {
            //fix student number
            //for each statement to add each module corresponding to a student
            foreach(string module in modules)
            {
                string query = $"INSERT INTO StudentModule VALUES ({studentNumber},'{module}');";
                Console.WriteLine($"{studentNumber}\t{module}");

                try
                {
                    using (SqlConnection con = new SqlConnection(connection))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }

            }

        }
    
        //get specific student modules
        public DataTable GetSpecificStudentModules(int studentNumber)
        {
            DataTable dt = new DataTable();
            string query = $"SELECT ModuleCode FROM StudentModule WHERE StudentNumber = {studentNumber};";

            //establish connection using adapter
            SqlDataAdapter da = new SqlDataAdapter(query, connection);

            //fill the data table
            da.Fill(dt);

            return dt;
        }

        //search for module
        public bool SearchModule(Module module)
        {
            DataTable dt = new DataTable();
            bool answer = true;
            string query = $"SELECT * FROM Modules WHERE ModuleCode = '{module.ModuleCode}';";

            //establish connection using adapter
            SqlDataAdapter da = new SqlDataAdapter(query, connection);

            //fill the data table
            da.Fill(dt);
            //check if table is emty
            if (dt.Rows.Count == 0)
            {
                answer = false;
            }
            return answer;

        }

        //add module
        public void AddModule(Module module)
        {

            string query = $"INSERT INTO Modules VALUES ('{module.ModuleCode}', '{module.ModuleName}'," +
                    $"'{module.Description}', '{module.Links}');";


            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }


        }


        //removing module
        

        //delete student
        private bool DeleteStudent(int studentNumber)
        {
            string query = $"DELETE FROM Students WHERE StudentNumber = {studentNumber}";

            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        { 
                            MessageBox.Show("Student and corresponding records in StudentModule deleted");
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Student not found");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting student: {ex.Message}");
                return false;
            }
        }

        public void DeleteStudentModules(int studentNumber)
        {
            string deleteStudentModuleQuery = $"DELETE FROM StudentModule WHERE StudentNumber = '{studentNumber}'";

            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(deleteStudentModuleQuery, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        
                    }
                }

                DeleteStudent(studentNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error deleting corresponding records in StudentModule");
            }
        }

        //deleting modules
        public void DeleteModule(string code)
        {
            string query = $"DELETE FROM Modules WHERE ModuleCode='{code}'";
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("module deleted");
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("module not deleted");

                throw;
            }


        }

        //-------------UPDATING----------------------------------------------
        public void UpdateModule(string code, string name, string links, string moduleDescription)
        {
            string query = $"UPDATE Modules SET ModuleName='{name}',Links='{links}',Description='{moduleDescription}' WHERE ModuleCode='{code}'";
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("module updated");
                    }
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("module not updated");

                throw;
            }
        }


        //updating student info
        public void UpdateStudent(int id, string n, string s, string p, string a, DateTime dob, string gender, string i)
        {
            string query = $"UPDATE Students SET StudentName='{n}',StudentSurname='{s}',Image = '{i}', Phone='{p}',StudentAddress='{a}',DOB='{dob}', Gender='{gender}' WHERE studentNumber={id}";


            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("student updated");
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("student not updated");

                throw;
            }

        }
    
        //get student for search result
        public DataTable getStudent(int studentNumber)
        {
        
            DataTable dt = new DataTable();
            string query = $"SELECT * FROM Students WHERE StudentNumber = {studentNumber};";

            //establish connection using adapter
            SqlDataAdapter da = new SqlDataAdapter(query, connection);

            //fill the data table
            da.Fill(dt);

            return dt;
        }
    }
}
