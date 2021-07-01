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
        Teacher ThisTeacher { get; set; }
        CousrList thisCourseList { get; set; }
        List<SubList> thisSubList { get; set; }
        List<SubList> thisCrsList { get; set; }
        #region Methods
        public HomeViewModel()
        {
            GetAllInfo();
        }

        public async Task GetAllInfo()
        {
            ThisTeacher = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getTeacherWithID".PostUrlEncodedAsync(new { teacherID = 100001 })
      .ReceiveJson<Teacher>();
            thisCourseList = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getCousrListWithID".PostUrlEncodedAsync(new { teacherID = ThisTeacher.teacherID })
      .ReceiveJson<CousrList>();

            StaticPageForPassingData.tuitionList = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getTeacherTuitionHistoryWithID".PostUrlEncodedAsync(new { teacherID = ThisTeacher.teacherID }).ReceiveJson<IEnumerable<TeacherTuitionHistory>>();

            StaticPageForPassingData.thisTeacher = ThisTeacher;

            thisSubList = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/GetSubListInfo".PostUrlEncodedAsync(new {
            sub1 = thisCourseList.sub1,
            sub2 = thisCourseList.sub2,
            sub3 = thisCourseList.sub3,
            sub4 = thisCourseList.sub4,
            sub5 = thisCourseList.sub5,
            sub6 = thisCourseList.sub6,
            sub7 = thisCourseList.sub7,
            sub8 = thisCourseList.sub8,
            sub9 = thisCourseList.sub9
            })
      .ReceiveJson<List<SubList>>();


            thisCrsList = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getCrsListInfo".PostUrlEncodedAsync(new {
            crs1 = thisCourseList.crs1,
            crs2 = thisCourseList.crs2,
            crs3 = thisCourseList.crs3,
            crs4 = thisCourseList.crs4,
            crs5 = thisCourseList.crs5,
            crs6 = thisCourseList.crs6,
            crs7 = thisCourseList.crs7,
            crs8 = thisCourseList.crs8,
            crs9 = thisCourseList.crs9,
            crs10 = thisCourseList.crs10
            })
     .ReceiveJson<List<SubList>>();
            activeswitchEnabled = true;
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
                tracheractColor = Color.Black ;
            }
            totalMin = ThisTeacher.totalMinuite + "";
            favTeacher = ThisTeacher.favTeacherCount + "";
            report = ThisTeacher.reportCount+"";
            if(ThisTeacher.reportCount == 0)
            {
                reportTextColor = Color.Green;
            }
            else
            {
                reportTextColor = Color.Red;
            }
            totalTuition = ThisTeacher.totalTuition + "";
            star5 = ThisTeacher.fiveStar +"";
            star4 = ThisTeacher.fourStar + "";
            star3 = ThisTeacher.threeStar + "";
            star2 = ThisTeacher.twoStar + "";
            star1 = ThisTeacher.oneStar + "";
            isrefreshing = false;

            sub1 = thisSubList[0].name;
            sub2 = thisSubList[1].name;
            sub3 = thisSubList[2].name;
            sub4 = thisSubList[3].name;
            sub5 = thisSubList[4].name;
            sub6 = thisSubList[5].name;
            sub7 = thisSubList[6].name;
            sub8 = thisSubList[7].name;
            sub9 = thisSubList[8].name;

            crs1 = thisCrsList[0].name;
            crs2 = thisCrsList[1].name;
            crs3 = thisCrsList[2].name;
            crs4 = thisCrsList[3].name;
            crs5 = thisCrsList[4].name;
            crs6 = thisCrsList[5].name;
            crs7 = thisCrsList[6].name;
            crs8 = thisCrsList[7].name;
            crs9 = thisCrsList[8].name;
            crs10 = thisCrsList[9].name;

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
            var res = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/activeTeacher".PostUrlEncodedAsync(new { activeStatus = 1 , teacherID = ThisTeacher.teacherID})
                   .ReceiveJson<Response>();
            activeswitchEnabled = true;
        }
        public async Task inActiveTeacher()
        {
            var res = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/activeTeacher".PostUrlEncodedAsync(new { activeStatus = 0 , teacherID = ThisTeacher.teacherID})
                   .ReceiveJson<Response>();
            activeswitchEnabled = true;
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
                    activeswitchEnabled = false;
                    teacheractivity = "Active";
                    tracheractColor = Color.Black;
                    ActiveTeacher();
                }
                else
                {
                    activeswitchEnabled = false;
                    teacheractivity = "Inactive";
                    tracheractColor = Color.FromHex("#D4D4D4");
                    inActiveTeacher();
                }
                OnPropertyChanged(); } }

        private bool activeswitchEnabled1;

        public bool activeswitchEnabled { get => activeswitchEnabled1; set => SetProperty(ref activeswitchEnabled1, value); }

        private string sub11;

        public string sub1 { get => sub11; set => SetProperty(ref sub11, value); }

        private string sub21;

        public string sub2 { get => sub21; set => SetProperty(ref sub21, value); }

        private string sub31;

        public string sub3 { get => sub31; set => SetProperty(ref sub31, value); }

        private string sub41;

        public string sub4 { get => sub41; set => SetProperty(ref sub41, value); }

        private string sub51;

        public string sub5 { get => sub51; set => SetProperty(ref sub51, value); }

        private string sub61;

        public string sub6 { get => sub61; set => SetProperty(ref sub61, value); }

        private string sub71;

        public string sub7 { get => sub71; set => SetProperty(ref sub71, value); }

        private string sub81;

        public string sub8 { get => sub81; set => SetProperty(ref sub81, value); }

        private string sub91;

        public string sub9 { get => sub91; set => SetProperty(ref sub91, value); }

        private string crs11;

        public string crs1 { get => crs11; set => SetProperty(ref crs11, value); }

        private string crs21;

        public string crs2 { get => crs21; set => SetProperty(ref crs21, value); }

        private string crs31;

        public string crs3 { get => crs31; set => SetProperty(ref crs31, value); }

        private string crs41;

        public string crs4 { get => crs41; set => SetProperty(ref crs41, value); }

        private string crs51;

        public string crs5 { get => crs51; set => SetProperty(ref crs51, value); }

        private string crs61;

        public string crs6 { get => crs61; set => SetProperty(ref crs61, value); }

        private string crs71;

        public string crs7 { get => crs71; set => SetProperty(ref crs71, value); }

        private string crs81;

        public string crs8 { get => crs81; set => SetProperty(ref crs81, value); }

        private string crs91;

        public string crs9 { get => crs91; set => SetProperty(ref crs91, value); }

        private string crs101;

        public string crs10 { get => crs101; set => SetProperty(ref crs101, value); }

        private Color reportTextColor1;

        public Color reportTextColor { get => reportTextColor1; set => SetProperty(ref reportTextColor1, value); }
        #endregion


    }
}
