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
    public partial class TeacherRegistration : ContentPage
    {
        public TeacherRegistration()
        {
            InitializeComponent();
        }

        private void MaterialButton_Clicked(object sender, EventArgs e)
        {
            CompleteTeachERReg();
        }
        public async Task CompleteTeachERReg()
        {
            using(var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Completing Teacher Registration..."))
            {
                await Task.Delay(1000);
                await dialog.DismissAsync();
            }
        }
    }
}