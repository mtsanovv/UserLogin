﻿using System.Collections.Generic;

namespace StudentInfoSystem
{
    internal class StudentData
    {
        public static List<Student> allStudents { get; private set; }

        public static void initializeStudentData()
        {
            Student sampleStudent = new Student();
            sampleStudent.firstName = "Marin";
            sampleStudent.middleName = "Tsvetozarov";
            sampleStudent.lastName = "Tsanov";
            sampleStudent.faculty = "FCST";
            sampleStudent.major = "CSE";
            sampleStudent.degree = Degree.BACHELOR;
            sampleStudent.status = StudentStatus.ACTIVE;
            sampleStudent.facultyNumber = "121219043";
            sampleStudent.year = 3;
            sampleStudent.studentGrouping = 1;
            sampleStudent.group = 29;

            allStudents = new List<Student>();
            allStudents.Add(sampleStudent);
        }
    }
}
