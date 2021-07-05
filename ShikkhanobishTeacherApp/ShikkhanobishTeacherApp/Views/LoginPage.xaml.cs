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
            using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Checking..."))
            {

                StaticPageForPassingData.thisTeacher = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getTeacherWithID".PostUrlEncodedAsync(new { teacherID = 100001 })
      .ReceiveJson<Teacher>();
                Application.Current.MainPage.Navigation.PushModalAsync(new AppShell());
                await dialog.DismissAsync();
            }
        }
        private void MaterialButton_Clicked(object sender, EventArgs e)
        {
            GetTeacher();
        }

        public async Task ShowLoading()
        {

  
        }
    }
}