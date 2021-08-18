using Android.Content.Res;
using Flurl.Http;
using Microsoft.AspNetCore.SignalR.Client;
using ShikkhanobishTeacherApp.Model;
using ShikkhanobishTeacherApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Vonage;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace ShikkhanobishTeacherApp.View_Model
{
    public class HomeViewModel : BaseViewMode, INotifyPropertyChanged
    {
        Teacher ThisTeacher { get; set; }
        CousrList thisCourseList { get; set; }
        List<SubList> thisSubList { get; set; }
        List<SubList> thisCrsList { get; set; }
        HubConnection _connection = null;
        List<favouriteTeacher> allfavTeacher = new List<favouriteTeacher>();
        int requestStudentID;
        string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/ShikkhanobishHub";
        #region Methods
        public HomeViewModel()
        {
            withdrawVisibility = false;
            withdrawEnabled = false;
            favouriteStudentListVisibility = false;
            GetAllInfo();
        }
        #region Connectivity
        public bool IsInternetConnectionAvailable()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                return true;
            }
            else
            {
                ShowSnameBar();
                return false;
            }
        }
        public async Task ShowSnameBar()
        {
            var alertDialogConfiguration = new MaterialSnackbarConfiguration
            {
                BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ERROR),
                MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY).MultiplyAlpha(0.8),
                CornerRadius = 8,

                ScrimColor = Color.FromHex("#FFFFFF").MultiplyAlpha(0.32),
                ButtonAllCaps = false

            };

            await MaterialDialog.Instance.SnackbarAsync(message: "No Network Connection Avaiable",
                                        actionButtonText: "Got It",
                                        configuration: alertDialogConfiguration,
                                        msDuration: MaterialSnackbar.DurationIndefinite);
        }
        async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {

                await ShowSnameBar();
            }

        }
        #endregion
        private void PerformlogOut()
        {
            SecureStorage.RemoveAll();
            inActiveTeacher();
            var existingPages = Application.Current.MainPage.Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                Application.Current.MainPage.Navigation.RemovePage(page);
            }
            Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());

        }
        public async Task GetAllInfo()
        {
            
            ThisTeacher = StaticPageForPassingData.thisTeacher;
            thisCourseList = StaticPageForPassingData.thisTeacherCourseList;
            thisSubList = StaticPageForPassingData.thisTeacherSubListName;

            tuitionFoundVisibility = false;
            activeswitchEnabled = true;
            activeswitchEnabled = false;
            amount = "" + ThisTeacher.amount;
            if (ThisTeacher.selectionStatus == 0)
            {
                selectionSts = "Not Selected";
                selectionStsColor = Color.FromHex("#DFC628");
                takeTextBtnVisibility = true;
            }
            else
            {
                selectionSts = "Selected";
                selectionStsColor = Color.ForestGreen;
                takeTextBtnVisibility = false;
            }

            if (ThisTeacher.monetizetionStatus == 0)
            {
                monetizationSts = "Not Monetized";
                monistscolor = Color.FromHex("#DFC628");
            }
            else
            {
                monetizationSts = "Monetized";
                monistscolor = Color.ForestGreen;
            }
            await inActiveTeacher();
            activestatusImageSource = "off.png";
            teacheractivity = "Inactive";
            totalMin = ThisTeacher.totalMinuite + "";
            favTeacher = ThisTeacher.favTeacherCount + "";
            report = ThisTeacher.reportCount + "";
            if (ThisTeacher.reportCount == 0)
            {
                ImageSource src;
                
                reportTextColor = Color.Black;
            }
            else
            {
                reportTextColor = Color.Red;
            }
            totalTuition = ThisTeacher.totalTuition + "";
            star5 = ThisTeacher.fiveStar + "";
            star4 = ThisTeacher.fourStar + "";
            star3 = ThisTeacher.threeStar + "";
            star2 = ThisTeacher.twoStar + "";
            star1 = ThisTeacher.oneStar + "";
           

            sub1 = thisSubList[0].name;
            sub2 = thisSubList[1].name;
            sub3 = thisSubList[2].name;
            sub4 = thisSubList[3].name;
            sub5 = thisSubList[4].name;
            sub6 = thisSubList[5].name;
            sub7 = thisSubList[6].name;
            sub8 = thisSubList[7].name;
            sub9 = thisSubList[8].name;

            isrefreshing = false;
            await GetFavTeacherList();
            await ConnectToRealTimeApiServer();
        }
        public async Task GetFavTeacherList()
        {
            var res = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getFavTeacherByTeacherID".PostUrlEncodedAsync(new { teacherID = ThisTeacher.teacherID })
                   .ReceiveJson<List<favouriteTeacher>>();
            for(int i = 0; i < res.Count;i++)
            {
                var stname = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getStudentWithID".PostUrlEncodedAsync(new { studentID = res[i].studentID })
                  .ReceiveJson<Student>();
                res[i].studentName = stname.name;
            }
            favTeacher = res.Count()+"";
            if(res.Count == 0)
            {
                favTeacherSeeVisibility = false;
            }
            else
            {
                favTeacherSeeVisibility = true;
            }
            favteacherList =  res;
            allfavTeacher = res;


        }
        private void PerformwithdrawCmd()
        {
            withdrawEnabled = false;
            withdrawVisibility = true;
        }
        private void PerformrefreshNow()
        {
            isrefreshing = true;
            GetAllInfo();
        }
        private void PerformfavList()
        {
            GetFavTeacherList();
            favouriteStudentListVisibility = true;
        }
        private void PerformpopOutfavteacherList()
        {
            favouriteStudentListVisibility = false;
        }
        public async Task ActiveTeacher()
        {
            if (!IsInternetConnectionAvailable())
            {
                return;
            }
            string uri = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishSignalR/PassActiveStatus?&teacherID=" + ThisTeacher.teacherID + "&isActive=" +true;
            HttpClient client = new HttpClient();
            StringContent content = new StringContent("", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content).ConfigureAwait(true);

            activestatusImageSource = "on.png";
            activeswitchEnabled = false;
            var res = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/activeTeacher".PostUrlEncodedAsync(new { activeStatus = 1, teacherID = ThisTeacher.teacherID })
                   .ReceiveJson<Response>();
            teacheractivity = "Active";
            ThisTeacher.activeStatus = 1;
            tracheractColor = Color.Black;
            if(!activeToggle)
            {
                activeToggle = true;
            }
            activeswitchEnabled = true;

        }
        public async Task inActiveTeacher()
        {
            if (!IsInternetConnectionAvailable())
            {
                return;
            }
            string uri = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishSignalR/PassActiveStatus?&teacherID=" + ThisTeacher.teacherID + "&isActive=" + false;
            HttpClient client = new HttpClient();
            StringContent content = new StringContent("", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content).ConfigureAwait(true);

            activestatusImageSource = "off.png";
            activeswitchEnabled = false;
            var res = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/activeTeacher".PostUrlEncodedAsync(new { activeStatus = 0, teacherID = ThisTeacher.teacherID })
                   .ReceiveJson<Response>();
            teacheractivity = "Inactive";
            ThisTeacher.activeStatus = 0;
            tracheractColor = Color.FromHex("#D4D4D4");
            if (activeToggle)
            {
                activeToggle = false;
            }
            activeswitchEnabled = true;
        }
        private void Performclocsepopup()
        {
            withdrawVisibility = false;
        }

        public void CheckWithdraw()
        {
            int errornum = 0;

            if(withdrawAmount != null)
            {
                if (int.Parse(withdrawAmount) < 0)
                {
                    errornum = 1;
                }
                if (int.Parse(withdrawAmount) > ThisTeacher.amount && withdrawAmount != null)
                {
                    errornum = 2;
                }
                if (ThisTeacher.amount < 50 && withdrawAmount != null)
                {
                    errornum = 3;
                }
            }
            
            if(bnumber != null)
            {
                if (bnumber.Length != 11)
                {
                    errornum = 4;
                }
            }
            
            if(withdrawPassword != null)
            {
                if (withdrawPassword != ThisTeacher.password)
                {
                    errornum = 5;
                }
            }           

            if (withdrawAmount != "" || withdrawAmount != null)
            {
                if (errornum == 1)
                {
                    wamError = true;
                    wamErrorText = "Invalid Amount";
                }
                else if (errornum == 2)
                {
                    wamError = true;
                    wamErrorText = "You have less then " + withdrawAmount + " Taka";
                }
                else if (errornum == 3)
                {
                    wamError = true;
                    wamErrorText = "You have to have at least 50 taka before making any withdraw";
                }
                else
                {
                    wamError = false;
                    wamErrorText = "";
                }
            }
            if (bnumber != "" || bnumber != null)
            {
                if (errornum == 4)
                {
                    wbnError = true;
                    wbnErrorText = "Invalid Number";
                }
                else
                {
                    wbnError = false;
                    wbnErrorText = "";
                }
            }
            if (withdrawPassword != "" || withdrawPassword != null)
            {
                if (errornum == 5)
                {
                    wpassError = true;
                    wpassErrorText = "Passowed doesn't match";
                }
                else
                {
                    wpassError = false;
                    wpassErrorText = "";
                }
            }
            if ((withdrawAmount != "" || withdrawAmount != null) && (bnumber != "" || bnumber != null) && (withdrawPassword != "" || withdrawPassword != null) && errornum == 0)
            {
                withdrawEnabled = true;
            }
            else
            {
                withdrawEnabled = false;
            }

        }

        private void PerformwithdrawNowComd()
        {
            SetWithdrawRequest();
        }

        public async Task SetWithdrawRequest()
        {
            if (!IsInternetConnectionAvailable())
            {
                return;
            }
            var res = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/setTeacherWithdrawHistory".PostUrlEncodedAsync(new
            {
                
                withdrawID = StaticPageForPassingData.GenarateNewID(),
                date = DateTime.Now.ToString("MM/dd/yyyy h:mm tt"),
                trxID = "N/A",
                amountTaka = withdrawAmount,
                medium = 1,
                status = 0,
                phoneNumber = bnumber,
                teacherID = ThisTeacher.teacherID
            })
                  .ReceiveJson<Response>();
                   withdrawVisibility = false;
            await MaterialDialog.Instance.AlertAsync(message: "An withdraw request has been successfully sent to our server. Minimum time to approve your request: 1 hour",
                                           title: "Successful");
        }

        public async Task ConnectToRealTimeApiServer()
        {
            _connection = new HubConnectionBuilder()
                 .WithUrl(url)
                 .Build();
            try { await _connection.StartAsync(); }
            catch (Exception ex) {
                var ss = ex.InnerException;
            }
           

            _connection.Closed += async (s) =>
            {
                await _connection.StartAsync();
            };

            _connection.On< int, string, string, string, string, int, string,int >("CallSelectedTeacher", async (teacherID, des, cls, sub, chapter, cost, name, studentID) =>
            {
                if(teacherID == ThisTeacher.teacherID && ThisTeacher.activeStatus == 1)
                {
                    await inActiveTeacher();
                    for (int i = 0; i < allfavTeacher.Count; i++)
                    {
                        if(allfavTeacher[i].studentID == studentID)
                        {
                            tuitionFoundColor = Color.FromHex("#EFE5FF");
                            break;
                        }
                        if (i == allfavTeacher.Count - 1)
                        {
                            tuitionFoundColor = Color.White ;
                        }
                    }
                    tuitionFoundVisibility = true;
                    foundInfoVisibility = true;
                    connectingstudentVisibility = false;
                    HRChapter = chapter;
                    HRClass = cls;
                    HRCost = cost+" Taka/Min";
                    HRSubject = sub;
                    HRStudentName = name;
                    HRDescription = des;
                    requestStudentID = studentID;

                }
            });
            
        }
        private async Task PerformDeclineHRCmd()
        {
            if (!IsInternetConnectionAvailable())
            {
                return;
            }
            string uri = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishSignalR/SelectedTeacherResponse?&teacherID=" + ThisTeacher.teacherID + "&studentID=" + requestStudentID + "&response=" + false + "&apikey="+ 0 + "&sessionID=" + 0 + "&token=" + 0;
            HttpClient client = new HttpClient();
            StringContent content = new StringContent("", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content).ConfigureAwait(true);
            await ActiveTeacher();
            tuitionFoundVisibility = false;
        }
        private async Task PerformAcceptHRCmd()
        {
            if (!IsInternetConnectionAvailable())
            {
                return;
            }
            VideoApiInfo info = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/GetVideoCallInfo".GetJsonAsync<VideoApiInfo>();
            CrossVonage.Current.ApiKey = info.apiKey + "";
            CrossVonage.Current.SessionId = info.SessionID;
            CrossVonage.Current.UserToken = info.token;

            if (!CrossVonage.Current.TryStartSession())
            {
                return;
            }
            string uri = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishSignalR/SelectedTeacherResponse?&teacherID=" + ThisTeacher.teacherID + "&studentID=" + requestStudentID + "&response=" + true+ "&apikey=" + info.apiKey + "&sessionID=" + info.SessionID + "&token=" + info.token; 
            HttpClient client = new HttpClient();
            StringContent content = new StringContent("", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content).ConfigureAwait(true);
            StaticPageForPassingData.thisVideoCallStudentID = requestStudentID;
            Application.Current.MainPage.Navigation.PushModalAsync(new VideoCallPage());


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
                    ActiveTeacher();

                }
                else
                {
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

        private bool withdrawVisibility1;

        public bool withdrawVisibility { get => withdrawVisibility1; set => SetProperty(ref withdrawVisibility1, value); }

        private string withdrawAmount1;

        public string withdrawAmount { get { return withdrawAmount1; } set { withdrawAmount1 = value ; CheckWithdraw(); SetProperty(ref withdrawAmount1, value); } }

        private string bnumber1;

        public string bnumber { get { return bnumber1; } set { bnumber1 = value ; CheckWithdraw(); SetProperty(ref bnumber1, value); } }

        private string withdrawPassword1;

        public string withdrawPassword { get { return withdrawPassword1; } set { withdrawPassword1 = value; CheckWithdraw(); SetProperty(ref withdrawPassword1, value); } }

        private Command clocsepopup1;

        public ICommand clocsepopup
        {
            get
            {
                if (clocsepopup1 == null)
                {
                    clocsepopup1 = new Command(Performclocsepopup);
                }

                return clocsepopup1;
            }
        }

        private Command withdrawNowComd1;

        public ICommand withdrawNowComd
        {
            get
            {
                if (withdrawNowComd1 == null)
                {
                    withdrawNowComd1 = new Command(PerformwithdrawNowComd);
                }

                return withdrawNowComd1;
            }
        }

        private bool wamError1;

        public bool wamError { get => wamError1; set => SetProperty(ref wamError1, value); }

        private string wamErrorText1;

        public string wamErrorText { get => wamErrorText1; set => SetProperty(ref wamErrorText1, value); }

        private bool wbnError1;

        public bool wbnError { get => wbnError1; set => SetProperty(ref wbnError1, value); }

        private string wbnErrorText1;

        public string wbnErrorText { get => wbnErrorText1; set => SetProperty(ref wbnErrorText1, value); }

        private bool wpass1;

        public bool wpass { get => wpass1; set => SetProperty(ref wpass1, value); }

        private bool wpassError1;

        public bool wpassError { get => wpassError1; set => SetProperty(ref wpassError1, value); }

        private string wpassErrorText1;

        public string wpassErrorText { get => wpassErrorText1; set => SetProperty(ref wpassErrorText1, value); }

        private bool withdrawEnabled1;

        public bool withdrawEnabled { get => withdrawEnabled1; set => SetProperty(ref withdrawEnabled1, value); }

        private bool tuitionFoundVisibility1;

        public bool tuitionFoundVisibility { get => tuitionFoundVisibility1; set => SetProperty(ref tuitionFoundVisibility1, value); }

        private string timeremaHireRequest1;

        public string timeremaHireRequest { get => timeremaHireRequest1; set => SetProperty(ref timeremaHireRequest1, value); }

        private string hRStudentName;

        public string HRStudentName { get => hRStudentName; set => SetProperty(ref hRStudentName, value); }

        private string hRClass;

        public string HRClass { get => hRClass; set => SetProperty(ref hRClass, value); }

        private string hRSubject;

        public string HRSubject { get => hRSubject; set => SetProperty(ref hRSubject, value); }

        private string hRChapter;

        public string HRChapter { get => hRChapter; set => SetProperty(ref hRChapter, value); }

        private string hRCost;

        public string HRCost { get => hRCost; set => SetProperty(ref hRCost, value); }

        private Command declineHRCmd;

        public ICommand DeclineHRCmd
        {
            get
            {
                if (declineHRCmd == null)
                {
                    declineHRCmd = new Command(async =>PerformDeclineHRCmd());
                }

                return declineHRCmd;
            }
        }

       

        private string hRTimer;

        public string HRTimer { get => hRTimer; set => SetProperty(ref hRTimer, value); }

        private string hRDescription;

        public string HRDescription { get => hRDescription; set => SetProperty(ref hRDescription, value); }

        private Command acceptHRCmd;

        public ICommand AcceptHRCmd
        {
            get
            {
                if (acceptHRCmd == null)
                {
                    acceptHRCmd = new Command(async => PerformAcceptHRCmd());
                }

                return acceptHRCmd;
            }
        }

        private bool connectingstudentVisibility1;

        public bool connectingstudentVisibility { get => connectingstudentVisibility1; set => SetProperty(ref connectingstudentVisibility1, value); }

        private bool foundInfoVisibility1;

        public bool foundInfoVisibility { get => foundInfoVisibility1; set => SetProperty(ref foundInfoVisibility1, value); }

        private bool takeTextBtnVisibility1;

        public bool takeTextBtnVisibility { get => takeTextBtnVisibility1; set => SetProperty(ref takeTextBtnVisibility1, value); }

        private Command logOut1;

        public ICommand logOut
        {
            get
            {
                if (logOut1 == null)
                {
                    logOut1 = new Command(PerformlogOut);
                }

                return logOut1;
            }
        }

        private bool favouriteStudentListVisibility1;

        public bool favouriteStudentListVisibility { get => favouriteStudentListVisibility1; set => SetProperty(ref favouriteStudentListVisibility1, value); }

        private Command favList1;

        public ICommand favList
        {
            get
            {
                if (favList1 == null)
                {
                    favList1 = new Command(PerformfavList);
                }

                return favList1;
            }
        }

        private List<favouriteTeacher> favteacherList1;

        public List<favouriteTeacher> favteacherList { get => favteacherList1; set => SetProperty(ref favteacherList1, value); }

        private string activestatusImageSource1;

        public string activestatusImageSource { get => activestatusImageSource1; set => SetProperty(ref activestatusImageSource1, value); }

        private Command popOutfavteacherList1;

        public ICommand popOutfavteacherList
        {
            get
            {
                if (popOutfavteacherList1 == null)
                {
                    popOutfavteacherList1 = new Command(PerformpopOutfavteacherList);
                }

                return popOutfavteacherList1;
            }
        }

        private Color tuitionFoundColor1;

        public Color tuitionFoundColor { get => tuitionFoundColor1; set => SetProperty(ref tuitionFoundColor1, value); }

        private bool favTeacherSeeVisibility1;

        public bool favTeacherSeeVisibility { get => favTeacherSeeVisibility1; set => SetProperty(ref favTeacherSeeVisibility1, value); }






        #endregion


    }
}
