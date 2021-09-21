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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
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
        public async Task GetTeacher()
        {
            if (!IsInternetConnectionAvailable())
            {
                return;
            }
            using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Loging In..."))
            {

                await  StaticPageForPassingData.GetALlTeacherInfo(pass.Text, pn.Text);
                if (StaticPageForPassingData.LoginOK)
                {
                    Application.Current.MainPage.Navigation.PushModalAsync(new AppShell());
                }
                else
                {
                    pn.HasError = true;
                    pn.ErrorText = "Wrong User Info";
                }
                if(checkbox.IsSelected)
                {
                    await SecureStorage.SetAsync("phonenumber", pn.Text);
                    await SecureStorage.SetAsync("password", pass.Text);
                }
                await dialog.DismissAsync();
            }
        }
        private void MaterialButton_Clicked(object sender, EventArgs e)
        {
            GetTeacher();
        }
        private async Task PerformRegistercmd()
        {
            if (!IsInternetConnectionAvailable())
            {
                return;
            }
            using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Connecting..."))
            {
                StaticPageForPassingData.allSubList = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getSubject".GetJsonAsync<List<Subject>>();
                Application.Current.MainPage.Navigation.PushModalAsync(new TeacherRegistration());
                await dialog.DismissAsync();
            }
            
        }
        public async Task EndOrBackBtn()
        {
            var result = await MaterialDialog.Instance.ConfirmAsync(message: "Do you want to close app?",
                                  confirmingText: "Yes",
                                  dismissiveText: "No");
            if (result == true)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }

        }
        protected override bool OnBackButtonPressed()
        {
            EndOrBackBtn();
            return true;
        }


        private void MaterialButton_Clicked_1(object sender, EventArgs e)
        {
            PerformRegistercmd();
        }
    }
}