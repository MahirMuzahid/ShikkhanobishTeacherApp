using System;
using System.Collections.Generic;
using System.Text;

namespace ShikkhanobishTeacherApp.Model
{
    public class TeacherWithdrawHistory
    {
        public int withdrawID { get; set; }
        public string date { get; set; }
        public string trxID { get; set; }
        public int amountTaka { get; set; }
        public string medium { get; set; }
        public int teacherID { get; set; }
        public string phoneNumber { get; set; }
        public int status { get; set; }
        public string statusText { get; set; }
        public string amountColor { get; set; }
        public string response { get; set; }
    }
}
