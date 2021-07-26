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

using Xamarin.Forms;
using Xamarin.Forms.Vonage;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace ShikkhanobishTeacherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoCallPage : ContentPage
    {
        private bool _isRendererSet;
        HubConnection _connection = null;
        int safeTimePassed = 15;
        string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/ShikkhanobishHub";
        public VideoCallPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                safeTimePassed--;
                if(safeTimePassed == 0)
                {
                    
                    return false;
                }
                else
                {
                    timetxt.Text = "Safe Time";
                    return true;
                }
            });
        }
        private async void OnEndCall(object sender, EventArgs e)
        {
            var result = await MaterialDialog.Instance.ConfirmAsync(message: "Only Student Can Cut Call",
                                    confirmingText: "Ok");
        }
        public async Task CutCall()
        {
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
            _connection.On<int, int, bool,int>("SendTimeAndCostInfo", async (teacherID, studentID, time, earned) =>
            {
                if (teacherID == StaticPageForPassingData.thisTeacher.teacherID && studentID == StaticPageForPassingData.thisVideoCallStudentID)
                {
                    timetxt.Text = "Time: " + time + "Minuite";
                    totalearnedtxt.Text = "Toal Earn: " + earned + " (processing cost excluded)";
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
            var result = await MaterialDialog.Instance.ConfirmAsync(message: "Student do not have enough balance to continue. Call will cut autometicly after 1 min",
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