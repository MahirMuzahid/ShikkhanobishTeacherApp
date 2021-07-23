using ShikkhanobishTeacherApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShikkhanobishTeacherApp.View_Model
{
    public class VideoCallPageViewModel: BaseViewMode, INotifyPropertyChanged
    {

        #region Methods
        #endregion


        #region Bindings
        private string toalEarned1;

        public string toalEarned { get => toalEarned1; set => SetProperty(ref toalEarned1, value); }

        private string time1;

        public string time { get => time1; set => SetProperty(ref time1, value); }
        #endregion
    }
}
