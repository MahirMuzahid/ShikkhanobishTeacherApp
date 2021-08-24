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
    public partial class QuestionPage : ContentPage
    {
        Color SelectedColor = new Color();
        Color notSelectedColor = new Color();
        int thisSelectedAns;
        List<bool> seletedAnsList = new List<bool>();
        List<int> prvQuestionIDList = new List<int>();
        List<int> qsIndex = new List<int>();
        List<TeacherTest> allQS = new List<TeacherTest>();
        int questionCount;
        
        
        public QuestionPage()
        {
            InitializeComponent();
            congrid.IsVisible = false;
            SelectedColor = Color.FromHex("#E1CFFF");
            notSelectedColor = Color.Transparent;
            nextBtn.IsEnabled = false;
            questionCount = 1;
            questionCountTxt.Text = questionCount + " OF 15";
            prgsbar.Progress = 0f;
            Getqs();
        }
        public async Task Getqs()
        {
            await GetAllQuestion();
            GetNextQuestion();
        }
        protected override bool OnBackButtonPressed()
        {
            CanCloseAlter();
            return true;
        }
        public async Task CanCloseAlter()
        {
            var result = await MaterialDialog.Instance.ConfirmAsync(message: "Do you want to quit?",
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
            MakeSelection(1);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            opone.BackgroundColor = notSelectedColor;
            optwo.BackgroundColor = SelectedColor;
            opthree.BackgroundColor = notSelectedColor;
            opfour.BackgroundColor = notSelectedColor;
            MakeSelection(2);
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            opone.BackgroundColor = notSelectedColor;
            optwo.BackgroundColor = notSelectedColor;
            opthree.BackgroundColor = SelectedColor;
            opfour.BackgroundColor = notSelectedColor;
            MakeSelection(3);
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            opone.BackgroundColor = notSelectedColor;
            optwo.BackgroundColor = notSelectedColor;
            opthree.BackgroundColor = notSelectedColor;
            opfour.BackgroundColor = SelectedColor;
            MakeSelection(4);
        }

        public void MakeSelection(int i)
        {
            thisSelectedAns = i;
            nextBtn.IsEnabled = true;
        }

        private void nextBtn_Clicked(object sender, EventArgs e)
        {
            seletedAnsList.Add(thisSelectedAns == allQS[qsIndex[questionCount - 1]].rightAns);
            if (seletedAnsList.Count == 15)
            {
                ShowMsg();
            }
            else
            {                
                nextBtn.IsEnabled = false;
                opone.BackgroundColor = notSelectedColor;
                optwo.BackgroundColor = notSelectedColor;
                opthree.BackgroundColor = notSelectedColor;
                opfour.BackgroundColor = notSelectedColor;
                thisSelectedAns = 0;
                questionCount++;
                questionCountTxt.Text = questionCount + " OF 15";
                prgsbar.Progress += .07f;
                GetNextQuestion();
            }
            

        }
        public async Task ShowMsg()
        {
            int truecnt = 0;
            for (int i = 0; i < 15; i++)
            {
                if(seletedAnsList[i])
                {
                    truecnt++;
                }
            }
            if(truecnt >= 13)////////////
            {
                var res = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/selectTeacher".PostUrlEncodedAsync(new { teacherID = StaticPageForPassingData.thisTeacher.teacherID }).ReceiveJson<Response>();
                StaticPageForPassingData.thisTeacher = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getTeacherWithID".PostUrlEncodedAsync(new { teacherID = StaticPageForPassingData.thisTeacher.teacherID }).ReceiveJson<Teacher>();
                congrid.IsVisible = true;
                
               
            }
            else
            {
                var result = await MaterialDialog.Instance.ConfirmAsync("You did not pass the test", "Try Again!", "Ok");
                if (result.HasValue)
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
                
            }
            
        }
        public async Task GetAllQuestion()
        {
            allQS = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/GetTeacherTest".GetJsonAsync<List<TeacherTest>>();
            for (int i = 0; i < 15; i++)
            {
                Random rd = new Random();
               if(qsIndex.Count == 0)
               {
                   qsIndex.Add(rd.Next(1,20));
               }
               else
               {
                    bool ismatch = true;
                    int thisindex = 0;
                    while (ismatch)
                    {
                        ismatch = false;
                        thisindex = rd.Next(1, 18);

                        for (int j = 0; j < qsIndex.Count; j++)
                        {
                            if (qsIndex[j] == thisindex)
                            {
                                ismatch = true;
                            }
                        }
                    }
                    qsIndex.Add(thisindex);

                }
            }
        }
        public void GetNextQuestion()
        {
            qs.Text = allQS[qsIndex[questionCount - 1]].question;
            optxto.Text = allQS[qsIndex[questionCount - 1]].opOne;
            optxtt.Text = allQS[qsIndex[questionCount - 1]].opTwo;
            optxtth.Text = allQS[qsIndex[questionCount - 1]].opThree;
            optxtf.Text = allQS[qsIndex[questionCount - 1]].opFour;
        }

        private void MaterialButton_Clicked(object sender, EventArgs e)
        {
            GotoDashboard();
        }

        public async Task GotoDashboard()
        {
            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Please Wait..."))
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new AppShell());
            }
           
        }
    }
}