using ShikkhanobishTeacherApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;
using ShikkhanobishTeacherApp.Model;
using Plugin.LocalNotification;

namespace ShikkhanobishTeacherApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            NotificationCenter.Current.NotificationTapped += OnLocalNotificationTapped;
            XF.Material.Forms.Material.Init(this);
            MainPage = new WaitPage();
        }
        private void OnLocalNotificationTapped(NotificationEventArgs e)
        {
            // your code goes here
        }
        protected override void OnStart()
        {
            StaticPageForPassingData.isTeacherOnBackground = false;
        }

        protected override void OnSleep()
        {
            StaticPageForPassingData.isTeacherOnBackground = true;
        }

        protected override void OnResume()
        {
            StaticPageForPassingData.isTeacherOnBackground = false;
        }
    }
}
