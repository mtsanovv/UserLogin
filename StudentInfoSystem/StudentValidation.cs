using System;
using System.Collections.Generic;
using System.Linq;
using UserLogin;

namespace StudentInfoSystem
{
    internal class StudentValidation
    {
        public Student getStudentDataByUser(User user)
        {
            string userFacultyNumber = user.facultyNumber;
            if(String.Equals(userFacultyNumber, String.Empty))
                return null;
            IEnumerable<Student> foundStudents = from student in StudentData.allStudents where String.Equals(student.facultyNumber, userFacultyNumber) select student;
            return foundStudents.DefaultIfEmpty(null).First();
        }
    }
}
