using Flurl.Http;
using ShikkhanobishTeacherApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShikkhanobishTeacherApp.View_Model
{
    public class HomeViewModel: BaseViewMode, INotifyPropertyChanged
    {
        #region Methods
        public HomeViewModel()
        {
            GetAllInfo();
        }

        public async Task GetAllInfo()
        {
            var ThisTeacher = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getTeacherWithID".PostUrlEncodedAsync(new { teacherID = 100001 })
      .ReceiveJson<Teacher>();

            amount = ""+ThisTeacher.amount;
            if(ThisTeacher.selectionStatus == 0)
            {
                selectionSts = "Not Selected";
                selectionStsColor = Color.FromHex("#DFC628");
            }
            else
            {
                selectionSts = "Selected";
                selectionStsColor = Color.ForestGreen;
            }

            if(ThisTeacher.monetizetionStatus == 0)
            {
                monetizationSts = "Not Monetized";
                monistscolor = Color.FromHex("#DFC628");
            }
            else
            {
                monetizationSts = "Monetized";
                monistscolor = Color.ForestGreen;
            }
            if (ThisTeacher.activeStatus == 0)
            {
                teacheractivity = "Inactive";
                tracheractColor = Color.FromHex("#D4D4D4");
            }
            else
            {
                teacheractivity = "Active";
                tracheractColor = Color.Default;
            }
            teacheractivity = ""+ThisTeacher.activeStatus;
            totalMin = ThisTeacher.totalMinuite + "";
            favTeacher = ThisTeacher.favTeacherCount + "";
            report = ThisTeacher.reportCount+"";
            totalTuition = ThisTeacher.totalTuition + "";
            star5 = ThisTeacher.fiveStar +"";
            star4 = ThisTeacher.fourStar + "";
            star3 = ThisTeacher.threeStar + "";
            star2 = ThisTeacher.twoStar + "";
            star1 = ThisTeacher.oneStar + "";
            isrefreshing = false;
        }
        private void PerformwithdrawCmd()
        {
        }
        private void PerformrefreshNow()
        {
            isrefreshing = true;
            GetAllInfo();
        }
        public async Task ActiveTeacher()
        {
            var res = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/activeTeacher".PostUrlEncodedAsync(new { activeTeacher = 1 })
                   .ReceiveJson<Response>();
        }
        public async Task inActiveTeacher()
        {
            var res = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/activeTeacher".PostUrlEncodedAsync(new { activeTeacher = 0 })
                   .ReceiveJson<Response>();
        }
        #endregion

        #region Bindings
        private string amount1;

        public string amount { get => amount1; set => SetProperty(ref amount1, value); }

        private Command withdrawCmd1;

        public ICommand withdrawCmd
        {
            get
            {
                if (withdrawCmd1 == null)
                {
                    withdrawCmd1 = new Command(PerformwithdrawCmd);
                }

                return withdrawCmd1;
            }
        }

        

        private string selectionSts1;

        public string selectionSts { get => selectionSts1; set => SetProperty(ref selectionSts1, value); }

        private string monetizationSts1;

        public string monetizationSts { get => monetizationSts1; set => SetProperty(ref monetizationSts1, value); }

        private string teacheractivity1;

        public string teacheractivity { get => teacheractivity1; set => SetProperty(ref teacheractivity1, value); }

        private string totalMin1;

        public string totalMin { get => totalMin1; set => SetProperty(ref totalMin1, value); }

        private string favTeacher1;

        public string favTeacher { get => favTeacher1; set => SetProperty(ref favTeacher1, value); }

        private string report1;

        public string report { get => report1; set => SetProperty(ref report1, value); }

        private string totalTuition1;

        public string totalTuition { get => totalTuition1; set => SetProperty(ref totalTuition1, value); }

        private string star11;

        public string star1 { get => star11; set => SetProperty(ref star11, value); }

        private string star21;

        public string star2 { get => star21; set => SetProperty(ref star21, value); }

        private string star31;

        public string star3 { get => star31; set => SetProperty(ref star31, value); }

        private string star51;

        public string star5 { get => star51; set => SetProperty(ref star51, value); }

        private string star41;

        public string star4 { get => star41; set => SetProperty(ref star41, value); }

        private Command refreshNow1;

        public ICommand refreshNow
        {
            get
            {
                if (refreshNow1 == null)
                {
                    refreshNow1 = new Command(PerformrefreshNow);
                }

                return refreshNow1;
            }
        }

        private Color selectionStsColor1;

        public Color selectionStsColor { get => selectionStsColor1; set => SetProperty(ref selectionStsColor1, value); }

        private Color monistscolor1;

        public Color monistscolor { get => monistscolor1; set => SetProperty(ref monistscolor1, value); }

        private bool isrefreshing1;

        public bool isrefreshing { get => isrefreshing1; set => SetProperty(ref isrefreshing1, value); }

        private Color tracheractColor1;

        public Color tracheractColor { get => tracheractColor1; set => SetProperty(ref tracheractColor1, value); }

        private bool activeToggle1;

        public bool activeToggle { get { return activeToggle1; } set { activeToggle1 = value;
                
                if(activeToggle == true)
                {
                    teacheractivity = "Active";
                    ActiveTeacher();
                }
                else
                {
                    teacheractivity = "Inactive";
                    inActiveTeacher();
                }
                OnPropertyChanged(); } }
        #endregion


    }
}
