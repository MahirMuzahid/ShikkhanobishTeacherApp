using System;
using System.Collections.Generic;
using System.Text;

namespace ShikkhanobishTeacherApp.Model
{
    public  class TeacherTuitionHistory
    {
        public int studentID { get; set; }
        public string tuitionID { get; set; }
        public string time { get; set; }
        public int teacherID { get; set; }
        public int cost { get; set; }
        public double ratting { get; set; }
        public string firstChoiceID { get; set; }
        public string secondChoiceID { get; set; }
        public string thirdChoiceID { get; set; }
        public string forthChoiceID { get; set; }
        public string date { get; set; }
        public string studentName { get; set; }
        public string teacherName { get; set; }
        public string Response { get; set; }
        public string firstChoiceName { get; set; }
        public string secondChoiceName { get; set; }
        public string thirdChoiceName { get; set; }
        public string forthChoiceName { get; set; }
        public double teacherEarn { get; set; }
    }
}
