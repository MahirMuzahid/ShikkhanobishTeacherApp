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
using static Android.InputMethodServices.KeyboardView;

namespace ShikkhanobishTeacherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public event EventHandler<KeyEventArgs> KeyPressed;
        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            var current = Connectivity.NetworkAccess;
           
        }
        public async Task EndOrBackBtn()
        {
            var result = await MaterialDialog.Instance.ConfirmAsync(message: "Do you want to close app?",
                                  confirmingText: "Yes",
                                  dismissiveText: "No");
            if(result == true)
            {              
                Application.Current.Quit();
            }
            
        }
        protected override bool OnBackButtonPressed()
        {
            EndOrBackBtn();
            return true;
        }
        

    }
  
}
