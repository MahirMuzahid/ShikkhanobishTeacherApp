using Flurl.Http;
using ShikkhanobishTeacherApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;
using Xamarin.Essentials;

namespace ShikkhanobishTeacherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherRegistration : ContentPage
    {
        public TeacherRegistration()
        {
            InitializeComponent();
        }

        private void MaterialButton_Clicked(object sender, EventArgs e)
        {
            CompleteTeachERReg();
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
        public async Task CompleteTeachERReg()
        {
            if (!IsInternetConnectionAvailable())
            {
                return;
            }
            using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Completing Teacher Registration..."))
            {
                var res = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/SetTeacher".PostUrlEncodedAsync(new {
                    teacherID = StaticPageForPassingData.ThisRegTeacher.teacherID,
                    name = StaticPageForPassingData.ThisRegTeacher.name,
                    phonenumber = StaticPageForPassingData.ThisRegTeacher.phonenumber,
                    password = StaticPageForPassingData.ThisRegTeacher.password,
                    sub1 = StaticPageForPassingData.ThisRegTeacher.sub1,
                    sub2 = StaticPageForPassingData.ThisRegTeacher.sub2,
                    sub3 = StaticPageForPassingData.ThisRegTeacher.sub3,
                    sub4 = StaticPageForPassingData.ThisRegTeacher.sub4,
                    sub5 = StaticPageForPassingData.ThisRegTeacher.sub5,
                    sub6 = StaticPageForPassingData.ThisRegTeacher.sub6,
                    sub7 = StaticPageForPassingData.ThisRegTeacher.sub7,
                    sub8 = StaticPageForPassingData.ThisRegTeacher.sub8,
                    sub9 = StaticPageForPassingData.ThisRegTeacher.sub9
                })
      .ReceiveJson<Response>();
                StaticPageForPassingData.thisTeacher = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getTeacherWithID".PostUrlEncodedAsync(new { teacherID = StaticPageForPassingData.ThisRegTeacher.teacherID })
      .ReceiveJson<Teacher>();
                Application.Current.MainPage.Navigation.PushModalAsync(new AppShell());
                await dialog.DismissAsync();
            }
        }
    }
}