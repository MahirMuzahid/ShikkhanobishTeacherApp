using ShikkhanobishTeacherApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShikkhanobishTeacherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WaitPage : ContentPage
    {
        public WaitPage()
        {
           
            InitializeComponent();
            Getinfo();
        }
        public async Task Getinfo()
        {

            var pn = await SecureStorage.GetAsync("phonenumber");
            var pass = await SecureStorage.GetAsync("password");
            StaticPageForPassingData.freomReg = false;
            if (pn != null && pass != null)
            {
                await StaticPageForPassingData.GetALlTeacherInfo(pass, pn);
                Application.Current.MainPage.Navigation.PushModalAsync(new AppShell());
            }
            else
            {
                await Task.Delay(1000);
                Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
            }

        }
    }
}