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
        Color SelectedColor = new Color();
        Color notSelectedColor = new Color();
        
        public QuestionPage()
        {
            InitializeComponent();
            SelectedColor = Color.FromHex("#E1CFFF");
            notSelectedColor = Color.Transparent;
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

        private void Button_Clicked(object sender, EventArgs e)
        {
            opone.BackgroundColor = SelectedColor;
            optwo.BackgroundColor = notSelectedColor;
            opthree.BackgroundColor = notSelectedColor;
            opfour.BackgroundColor = notSelectedColor;

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            opone.BackgroundColor = notSelectedColor;
            optwo.BackgroundColor = SelectedColor;
            opthree.BackgroundColor = notSelectedColor;
            opfour.BackgroundColor = notSelectedColor;
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            opone.BackgroundColor = notSelectedColor;
            optwo.BackgroundColor = notSelectedColor;
            opthree.BackgroundColor = SelectedColor;
            opfour.BackgroundColor = notSelectedColor;
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            opone.BackgroundColor = notSelectedColor;
            optwo.BackgroundColor = notSelectedColor;
            opthree.BackgroundColor = notSelectedColor;
            opfour.BackgroundColor = SelectedColor;
        }
    }
}