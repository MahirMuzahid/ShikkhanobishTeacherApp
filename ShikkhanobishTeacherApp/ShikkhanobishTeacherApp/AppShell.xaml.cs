
using ShikkhanobishTeacherApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace ShikkhanobishTeacherApp
{
    public partial class AppShell : Xamarin.Forms.TabbedPage
    {
        public AppShell()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }


    }
}
