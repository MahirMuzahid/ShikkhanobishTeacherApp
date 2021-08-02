using System;
using System.Collections.Generic;
using System.Text;

namespace ShikkhanobishTeacherApp.Model
{
    public class favouriteTeacher
    {
        public int teacherID { get; set; }
        public int studentID { get; set; }
        public string teacherName { get; set; }
        public string studentName { get; set; }
        public int teacherTotalTuition { get; set; }
        public double teacherRatting { get; set; }
        public bool rmvBtnVisibility { get; set; }
        public string popupfavSelectedbackground { get; set; }
        public string Response { get; set; }
    }
}
