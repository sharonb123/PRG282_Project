using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG278_2_
{
    class Student
    {
        public int StudentNumber { get; set; }
        public string StudentName{ get; set; }
        public string StudentSurname { get; set; }
        public string ImageLocation { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Student() { }

        public Student(int studentNumber, string studentName, string studentSurname, string imageLocation, DateTime dOB, string gender, string phone, string address)
        {
            StudentNumber = studentNumber;
            StudentName = studentName;
            StudentSurname = studentSurname;
            ImageLocation = imageLocation;
            DOB = dOB;
            Gender = gender;
            Phone = phone;
            Address = address;
        }

        //To String for formatting
    }
}
