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
    public partial class QuestionPage : ContentPage
    {
        public QuestionPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            CanCloseAlter();
            return true;
        }
        public async Task CanCloseAlter()
        {
            var result = await MaterialDialog.Instance.ConfirmAsync(message: "Do you want to close app?",
                                  confirmingText: "Yes",
                                  dismissiveText: "No");
            if (result == true)
            {
                Application.Current.MainPage.Navigation.PopModalAsync();          
            }
        }
    }
}