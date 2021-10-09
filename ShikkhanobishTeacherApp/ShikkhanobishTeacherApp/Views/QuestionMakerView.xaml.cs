using Microsoft.AspNetCore.SignalR.Client;
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
    public partial class QuestionMakerView : ContentPage
    {
        HubConnection _connection = null;
        string url = "https://shikkhanobishRealTimeAPi.shikkhanobish.com/ShikkhanobishHub";
        public QuestionMakerView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            websrc.Source = "https://makequestion.shikkhanobish.com/" + StaticPageForPassingData.thisTeacher.teacherID;
            teacherName.Text = "Teacher Name: "+StaticPageForPassingData.thisTeacher.name;
            ConnectToRealTimeApiServer();
        }
        public async Task ConnectToRealTimeApiServer()
        {
            _connection = new HubConnectionBuilder()
                 .WithUrl(url)
                 .Build();
            try { await _connection.StartAsync(); }
            catch (Exception ex)
            {
                var ss = ex.InnerException;
            }


            _connection.Closed += async (s) =>
            {
                await _connection.StartAsync();
            };

            
            _connection.On<int, int, string>("sumbitQs", async (teacherID, qsID, errorTxt) =>
            {
                if(teacherID == StaticPageForPassingData.thisTeacher.teacherID)
                {
                    if(errorTxt == "" || errorTxt == null)
                    {
                        using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Submitting Question..."))
                        {
                            await Task.Delay(3000);
                            dialog.MessageText = "Question Sumbitted!";
                            await Task.Delay(1000);
                        }
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: errorTxt,
                                     title: "Opps!");
                    }
                }                             
            });

        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            websrc.Source = "https://makequestion.shikkhanobish.com/" + StaticPageForPassingData.thisTeacher.teacherID;
            teacherName.Text = StaticPageForPassingData.thisTeacher.name;
        }

        private void MaterialButton_Clicked(object sender, EventArgs e)
        {
            ShowMSG();
        }
        public async Task ShowMSG()
        {
            await MaterialDialog.Instance.AlertAsync(message: "Question Maker হল শিক্ষানবিশের একটি এক্সক্লুসিভ ফিচার, যা শিক্ষকদের এক্সট্রা ইনকামের সুযোগ করে দিবে! টিচাররা \"Make Question\" বাটনে ক্লিক করে এনসিটিবি বই থেক প্রশ্ন সাবমিট করতে পারবেন। প্রতিটি প্রশ্ন সাবমিট হওয়ার পর আমাদের এডমান প্রশ্নটিকে রিভিউ করবেন। প্রশ্ন এপ্রুভড হলে প্রতিটি প্রশ্নের জন্য শিক্ষকের শিক্ষানবিশ একাউন্টে নির্দিষ্ট পরিমান টাকা এড হবে, যা পরে টিচার উইথড্র করতে পারবেন! ",
                                    title: "Question Maker কি?");

        }
    }
}