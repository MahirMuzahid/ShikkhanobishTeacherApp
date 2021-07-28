using Flurl.Http;
using Microsoft.AspNetCore.SignalR.Client;
using ShikkhanobishTeacherApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;
using Xamarin.Forms;
using Xamarin.Forms.Vonage;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace ShikkhanobishTeacherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoCallPage : ContentPage
    {
        double totalEarned = 0;
        private bool _isRendererSet;
        HubConnection _connection = null;
        string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/ShikkhanobishHub";
        public VideoCallPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            ConnectToRealTimeApiServer();
            timetxt.Text = "Safe Time";
            totalearnedtxt.Text = "0";
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
        private async void OnEndCall(object sender, EventArgs e)
        {
            var result = await MaterialDialog.Instance.ConfirmAsync(message: "Only Student Can Cut Call",
                                    confirmingText: "Ok");
        }
        public async Task CutCall()
        {
            if (!IsInternetConnectionAvailable())
            {
                return;
            }
            using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Student Left Video Call. Closing Video Call..."))
            {
                CrossVonage.Current.EndSession();
                StaticPageForPassingData.thisTeacher = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getTeacherWithID".PostUrlEncodedAsync(new { teacherID = StaticPageForPassingData.thisTeacher.teacherID })
     .ReceiveJson<Teacher>();
                var existingPages = Navigation.NavigationStack.ToList();
                foreach (var page in existingPages)
                {
                    Navigation.RemovePage(page);
                }
                await Task.Delay(1000);
                Application.Current.MainPage.Navigation.PushModalAsync(new AppShell());
            }
                
        }
        private void OnMessage(object sender, EventArgs e)
            => CrossVonage.Current.TrySendMessage($"Path.GetRandomFileName: {Path.GetRandomFileName()}");

        private void OnSwapCamera(object sender, EventArgs e)
            => CrossVonage.Current.CycleCamera();

        void OnShareScreen(object sender, EventArgs e)
        {
            CrossVonage.Current.PublisherVideoType = CrossVonage.Current.PublisherVideoType == VonagePublisherVideoType.Camera
                ? VonagePublisherVideoType.Screen
                : VonagePublisherVideoType.Camera;
        }

        private void OnMessageReceived(string message)
            => DisplayAlert("Random message received", message, "OK");

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "Renderer")
            {
                _isRendererSet = !_isRendererSet;
                if (!_isRendererSet)
                {
                    OnEndCall(this, EventArgs.Empty);
                }
            }
        }
        public async Task ConnectToRealTimeApiServer()
        {
            if (!IsInternetConnectionAvailable())
            {
                return;
            }
            _connection = new HubConnectionBuilder()
                 .WithUrl(url)
                 .Build();
            try { await _connection.StartAsync(); }
            catch (Exception ex)
            {
                var ss = ex.InnerException;
            }


            _connection.Closed += async (s) =>
            {
                await _connection.StartAsync();
            };

            _connection.On<int, int, bool>("CutVideoCall", async (teacherID, studentID, isCut) =>
            {
                if(teacherID == StaticPageForPassingData.thisTeacher.teacherID && studentID == StaticPageForPassingData.thisVideoCallStudentID && isCut == true)
                {
                    CutCall();
                }
            });
            _connection.On<int, int, int,double>("SendTimeAndCostInfo", async (teacherID, studentID, time, earned) =>
            {
                if (teacherID == StaticPageForPassingData.thisTeacher.teacherID && studentID == StaticPageForPassingData.thisVideoCallStudentID)
                {
                    totalEarned = totalEarned + earned;
                    timetxt.Text = "Time: " + time + "Minuite";
                    totalearnedtxt.Text = ""+ totalEarned;
                }
            });
            _connection.On<int, int, bool>("LastMinAlert", async (teacherID, studentID, isLastMin) =>
            {
                if (teacherID == StaticPageForPassingData.thisTeacher.teacherID && studentID == StaticPageForPassingData.thisVideoCallStudentID && isLastMin)
                {
                    lasMinALter();
                }
            });

        }
        public async Task lasMinALter()
        {
            var result = await MaterialDialog.Instance.ConfirmAsync(message: "Student do not have enough balance to continue. Call will cut autometically after 1 min",
                                  confirmingText: "Ok");
        }
        protected override bool OnBackButtonPressed()
        {
            CanCloseAlter();
            return true;
        }
        public async Task CanCloseAlter()
        {
            var result = await MaterialDialog.Instance.ConfirmAsync(message: "Only Student Can Cut Call",
                                     confirmingText: "Ok");
        }
    }
}