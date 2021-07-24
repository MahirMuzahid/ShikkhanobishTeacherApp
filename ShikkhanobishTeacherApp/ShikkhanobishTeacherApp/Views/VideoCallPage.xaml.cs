using Flurl.Http;
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
        public VideoCallPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private async void OnEndCall(object sender, EventArgs e)
        {
            var result = await MaterialDialog.Instance.ConfirmAsync(message: "Only Student Can Cut Call",
                                    confirmingText: "Ok");
        }
        public async Task EndOrBackBtn()
        {           
            CrossVonage.Current.EndSession();
            CrossVonage.Current.MessageReceived -= OnMessageReceived;
            StaticPageForPassingData.thisTeacher = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getTeacherWithID".PostUrlEncodedAsync(new { teacherID = StaticPageForPassingData.thisTeacher.teacherID })
 .ReceiveJson<Teacher>();
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                Navigation.RemovePage(page);
            }
            Application.Current.MainPage.Navigation.PushModalAsync(new AppShell());
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
        protected override bool OnBackButtonPressed()
        {
            EndOrBackBtn();
            return true;
        }
    }
}