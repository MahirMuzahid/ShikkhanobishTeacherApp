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

        public static int GenarateNewID()
        {
            Random rnd = new Random();
            int newID = rnd.Next(100000000, 999999999);
            return newID;
        }
    }
}

