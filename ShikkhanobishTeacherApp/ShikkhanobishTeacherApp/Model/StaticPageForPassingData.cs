using System;
using System.Collections.Generic;
using System.Text;

namespace ShikkhanobishTeacherApp.Model
{
    public class StaticPageForPassingData
    {
        public static Teacher thisTeacher { get; set; }
        public static List<TeacherTuitionHistory> tuitionList { get; set; }
        public static List<Subject> allSubList { get; set; }
        public static Teacher ThisRegTeacher { get; set; }
        public static CousrList ThisTeacherCourseList { get; set; }

        public static int GenarateNewID()
        {
            Random rnd = new Random();
            int newID = rnd.Next(10000000, 99999999);
            return newID;
        }
    }
}

