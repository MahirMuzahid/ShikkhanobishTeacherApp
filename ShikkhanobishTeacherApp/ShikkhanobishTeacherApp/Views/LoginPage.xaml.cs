using Flurl.Http;
using ShikkhanobishTeacherApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace ShikkhanobishTeacherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        public async Task GetTeacher()
        {
            using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Checking Info..."))
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
                
                await dialog.DismissAsync();
            }
        }
        private void MaterialButton_Clicked(object sender, EventArgs e)
        {
            GetTeacher();
        }
        private async Task PerformRegistercmd()
        {
            using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Connecting..."))
            {
                StaticPageForPassingData.allSubList = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getSubject".GetJsonAsync<List<Subject>>();
                Application.Current.MainPage.Navigation.PushModalAsync(new TeacherRegistration());
                await dialog.DismissAsync();
            }
            
        }


        private void MaterialButton_Clicked_1(object sender, EventArgs e)
        {
            PerformRegistercmd();
        }
    }
}