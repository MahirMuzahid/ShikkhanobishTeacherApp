using Flurl.Http;
using ShikkhanobishTeacherApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace ShikkhanobishTeacherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WaitPage : ContentPage
    {
        public WaitPage()
        {
           
            InitializeComponent();
            showTxt.Text = "Connecting With Shikkhanobish Server. Please Wait...";
            nonetbtn.IsVisible = false;
            if (IsInternetConnectionAvailable())
            {
                Getinfo();
            }

            
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
            nonetbtn.IsVisible = true;
        }
        async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {

                await ShowSnameBar();
            }
            else
            {
                nonetbtn.IsVisible = false;
                Getinfo();
            }

        }
        #endregion

        
        public async Task Getinfo()
        {
            prgs.Progress = .2;
            var currentAppVersion = VersionTracking.CurrentBuild;
            currentAppVersion = "10";
            var currentRealVersion = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getAppVersion".GetJsonAsync<AppVersion>();

            if (int.Parse(currentAppVersion) < currentRealVersion.teacherAtVersion)
            {
                using (await MaterialDialog.Instance.LoadingDialogAsync(message: "New Version Is Available! Please download latest version to use Shikkhanobish Teacher App..."))
                {
                    await Task.Delay(3000);
                    await Browser.OpenAsync("https://play.google.com/store/apps/details?id=com.shikkhanobishteacher.shikkhanobishteacher");
                    showTxt.Text = "New Version Available! Please download latest version to use Shikkhanobish Teacher App...";
                    prgs.IsVisible = false;

                }
            }
            else
            {
                prgs.Progress = .6;
                var pn = await SecureStorage.GetAsync("phonenumber");
                var pass = await SecureStorage.GetAsync("password");
                StaticPageForPassingData.freomReg = false;                
                if (pn != null && pass != null)
                {
                    await StaticPageForPassingData.GetALlTeacherInfo(pass, pn);
                    prgs.Progress = .9;
                    Application.Current.MainPage.Navigation.PushModalAsync(new AppShell());
                }
                else
                {
                    await Task.Delay(1000);
                    prgs.Progress = .9;
                    Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
                }
            }
            

        }

        private void MaterialButton_Clicked(object sender, EventArgs e)
        {
            nonetbtn.IsVisible = false;
            Getinfo();
        }
    }
}