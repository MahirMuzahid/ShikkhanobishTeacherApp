using System;
using System.Collections.Generic;
using System.Text;

namespace ShikkhanobishTeacherApp.Model
{
    public class StaticPageForPassingData
    {
        public static Teacher thisTeacher { get; set; }
        public static List<TeacherTuitionHistory> tuitionList { get; set; }
    }
}

