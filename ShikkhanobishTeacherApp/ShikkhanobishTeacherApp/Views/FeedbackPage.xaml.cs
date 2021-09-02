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
    public partial class FeedbackPage : ContentPage
    {
        public FeedbackPage()
        {
            InitializeComponent();
            //Creating TapGestureRecognizers    
            var tapImage = new TapGestureRecognizer();
            //Binding events    
            tapImage.Tapped += tapImage_Tapped;
            //Associating tap events to the image buttons    
            img.GestureRecognizers.Add(tapImage);

        }
        void tapImage_Tapped(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopModalAsync();
        }

        private void MaterialButton_Clicked(object sender, EventArgs e)
        {
            SendFeedback();
        }
        public async Task SendFeedback()
        {
            using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Thank you for your feedback."))
            {
                var res = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/setFeedbackFromApp".PostUrlEncodedAsync(new { userID = StaticPageForPassingData.thisTeacher.teacherID, userType = 2, msg = msg.Text }).ReceiveJson<Response>();
                await Task.Delay(1000);
                Application.Current.MainPage.Navigation.PopModalAsync();
            }
                
        }
    }
}