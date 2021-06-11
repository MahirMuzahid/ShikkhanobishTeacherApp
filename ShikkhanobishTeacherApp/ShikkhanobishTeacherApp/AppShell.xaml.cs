
using ShikkhanobishTeacherApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ShikkhanobishTeacherApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(Profile), typeof(Profile));
            Shell.SetNavBarIsVisible(this, false);
        }

    }
}
