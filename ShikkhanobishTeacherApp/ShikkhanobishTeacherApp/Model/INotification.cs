using System;
using System.Collections.Generic;
using System.Text;

namespace ShikkhanobishTeacherApp.Model
{
    public interface INotification
    {
        void CreateNotification(string title, string msg);
    }
}
