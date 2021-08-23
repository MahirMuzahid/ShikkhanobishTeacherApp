using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShikkhanobishTeacherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TakeTestView : ContentPage
    {
        public TakeTestView()
        {
            InitializeComponent();
            popupGrid.IsVisible = false;
        }

        private void MaterialButton_Clicked(object sender, EventArgs e)
        {
            popupGrid.IsVisible = true;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            popupGrid.IsVisible = false;
        }

        private void MaterialButton_Clicked_1(object sender, EventArgs e)
        {
            popupGrid.IsVisible = false;
        }
    }
}