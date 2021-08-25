using Flurl.Http;
using ShikkhanobishTeacherApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace ShikkhanobishTeacherApp.View_Model
{
    public class RegisterViewModel: BaseViewMode, INotifyPropertyChanged
    {

        List<int> scSelectCount { get; set; }
        List<int> clgSelectCount { get; set; }
        List<string> scsubName { get; set; }
        List<string> clgsubName { get; set; }

        List<string> scScsubName { get; set; }
        List<string> scChsubName { get; set; }
        List<string> scArsubName { get; set; }

        List<string> clgScsubName { get; set; }
        List<string> clgChsubName { get; set; }
        List<string> clgArsubName { get; set; }
        int OTPsec;
        bool OTPTimerContinue;
        int selectedscIndex;
        int selectedClgIndex;
        int clgSelectSubCountMax;

        List<Subject> AllsubList = new List<Subject>();
        #region Methods
        public RegisterViewModel ()
        {
            GetAllInfo();
        }
        #region Connectivity
        public bool IsInternetConnectionAvailable()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                return true;
            }
            else
            {
                ShowSnameBar();
                return false;
            }
        }
        public async Task ShowSnameBar()
        {
            var alertDialogConfiguration = new MaterialSnackbarConfiguration
            {
                BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ERROR),
                MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY).MultiplyAlpha(0.8),
                CornerRadius = 8,

                ScrimColor = Color.FromHex("#FFFFFF").MultiplyAlpha(0.32),
                ButtonAllCaps = false

            };

            await MaterialDialog.Instance.SnackbarAsync(message: "No Network Connection Avaiable",
                                        actionButtonText: "Got It",
                                        configuration: alertDialogConfiguration,
                                        msDuration: MaterialSnackbar.DurationIndefinite);
        }
        async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {

                await ShowSnameBar();
            }

        }
        #endregion
        public async Task GetAllInfo()
        {
            await GetAllSub();
            otpEnabled = false;
            otpWindow = false;
            sendotpEnabled = false;
            selectedscIndex = 0;
            selectedClgIndex = 0;
            artSubEnabled = false;
            clgSubEnabled = false;
            scSelectCount = new List<int>();
            clgSelectCount = new List<int>();

            scsubName = new List<string>();
            clgsubName = new List<string>();

            scScsubName = new List<string>();
            scChsubName = new List<string>();
            scArsubName = new List<string>();

            clgScsubName = new List<string>();
            clgChsubName = new List<string>();
            clgArsubName = new List<string>();
            for(int i = 0; i < AllsubList.Count; i++)
            {
                if(AllsubList[i].groupName == "Science" && AllsubList[i].classID == 101)
                {
                    scScsubName.Add(AllsubList[i].name);
                }
                else if (AllsubList[i].groupName == "Commerce" && AllsubList[i].classID == 101)
                {
                    scChsubName.Add(AllsubList[i].name);
                }
                else if (AllsubList[i].groupName == "Arts" && AllsubList[i].classID == 101)
                {
                    scArsubName.Add(AllsubList[i].name);
                }
                else if (AllsubList[i].groupName == "Science" && AllsubList[i].classID == 102)
                {
                    clgScsubName.Add(AllsubList[i].name);
                }
                else if (AllsubList[i].groupName == "Commerce" && AllsubList[i].classID == 102)
                {
                    clgChsubName.Add(AllsubList[i].name);
                }
                else if (AllsubList[i].groupName == "Arts" && AllsubList[i].classID == 102)
                {
                    clgArsubName.Add(AllsubList[i].name);
                }
            }

            


            CollegePopupVisibility = false;
            schholPopUpVisibility = false;
            scScColor = Color.FromHex("#10000000");
            scCmColor = Color.FromHex("#10000000");
            scarColor = Color.FromHex("#10000000");

            clgScEnabled = false;
            clgCmEnabled = false;
            clgArEnabled = false;

            clgScColor = Color.FromHex("#10000000");
            clgChColor = Color.FromHex("#10000000");
            clgArColor = Color.FromHex("#10000000");
            noSubMsgVsi = true;
            noSubScMsgVsi = true;

            ClgSavedEnabled = false;
            scEnabled = false;

            scPhyColor = Color.White;
            scCheColor = Color.White;
            scBioColor = Color.White;
            scMatColor = Color.White;
            scHmColor = Color.White;
        }
        public async Task GetAllSub()
        {
            AllsubList = StaticPageForPassingData.allSubList;
        }
        public async Task CheckEverything()
        {

            bool allOK = true;
            if (name != null && pnumber != null && password != null && conFirmPassword != null)
            {
                if (name == null || name == "")
                {
                    hasNameError = true;
                    nameErrorTxt = "Name can't be empty";
                    allOK = false;
                }
                else
                {
                    hasNameError = false;
                    nameErrorTxt = "";
                }
                if (pnumber == null || pnumber == "")
                {
                    haspnError = true;
                    pnErrorTxt = "Phone Number can't be empty";
                    allOK = false;
                }
                else
                {
                    haspnError = false;
                    pnErrorTxt = "";
                }
                if (password == null || password == "")
                {
                    hasPassError = true;
                    passErrorTxt = "Password can't be empty";
                    allOK = false;
                }
                else
                {
                    hasPassError = false;
                    passErrorTxt = "";
                }
                if (conFirmPassword == null || conFirmPassword == "")
                {
                    hasConPassError = true;
                    conPassErrorTxt = "Confirm Password can't be empty";
                    allOK = false;
                }
                else
                {
                    hasConPassError = false;
                    conPassErrorTxt = "";
                }
                if (password != null || conFirmPassword != null)
                {
                    if (password.Length < 6)
                    {
                        hasPassError = true;
                        passErrorTxt = "Minimum Password Length is 6";
                        allOK = false;
                    }
                    else
                    {
                        hasPassError = false;
                        passErrorTxt = "";
                    }
                    if (password != conFirmPassword)
                    {
                        hasConPassError = true;
                        conPassErrorTxt = "Password Doesn't Match";
                        allOK = false;
                    }
                    else
                    {
                        hasConPassError = false;
                        conPassErrorTxt = "";
                    }
                }
                if (sub1 != "" && sub1 != null && sub2 != "" && sub2 != null && sub3 != "" && sub3 != null && sub5 != "" && sub5 != null && sub6 != "" && sub6 != null && sub7 != "" && sub7 != null && sub8 != "" && sub8 != null && sub9 != "" && sub9 != null && allOK)
                {
                    allOK = true;
                }
                else
                {
                    allOK = false;
                }
                if (allOK)
                {
                    if (!IsInternetConnectionAvailable())
                    {
                        return;
                    }
                    var chkPn = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/checkRegphonenumber".PostUrlEncodedAsync(new { phonenumber = pnumber })
  .ReceiveJson<Teacher>();
                    if (chkPn.teacherID != 0)
                    {
                        haspnError = true;
                        pnErrorTxt = "Phone Number already exist";
                    }
                    else
                    {
                        hasNameError = false;
                        haspnError = false;
                        hasPassError = false;
                        hasConPassError = false;
                        sendotpEnabled = true;
                    }
                   
                }
                else
                {
                    sendotpEnabled = false;
                }


            }
            else
            {
                sendotpEnabled = false;
            }

        }
        public async Task checkPnumber()
        {
            if (!IsInternetConnectionAvailable())
            {
                return;
            }
            if (pnumber != null || pnumber != "")
            {
                var chkPn = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/checkRegphonenumber".PostUrlEncodedAsync(new { phonenumber = pnumber })
  .ReceiveJson<Teacher>();
                if(chkPn.teacherID != 0)
                {
                    haspnError = true;
                    pnErrorTxt = "Phone Number already exist";
                }
                else
                {
                    haspnError = false;
                    pnErrorTxt = "";
                }
            }
        }
        #region school popup
        private void PerformpopupSchool(string index)
        {
            GetAllSub();
            if (selectedscIndex != int.Parse(index))
            {
                scEnabled = false;
            }            
            scsubName1 = "";
            scsubName2 = "";
            scsubName3 = "";
            scsubName4 = "";
            scsubName5 = "";
            SubEnabled = false;
            SubEnabled1 = false;
            SubEnabled2 = false;
            SubEnabled3 = false;
            SubEnabled4 = false;
            selectedscIndex = int.Parse(index);
            scSelectCount.Clear();
            scPhyColor = Color.White;
            scCheColor = Color.White;
            scBioColor = Color.White;
            scHmColor = Color.White;
            scMatColor = Color.White;
            schholPopUpVisibility = true;
            if (int.Parse(index) == 1)
            {
                clgSelectSubCountMax = 6;
              
               
                scsubName = scScsubName;              
            }
            else if (int.Parse(index) == 2)
            {
                clgSelectSubCountMax = 3;
                            
               
                scsubName = scChsubName;               
            }
            else if (int.Parse(index) == 3)
            {
                clgSelectSubCountMax = 3;
                     
               
                scsubName = scArsubName;              
            }
            for (int i = 0; i < scsubName.Count; i++)
            {
                string thisname = "";
                if (i == 0)
                {
                    SubEnabled = true;
                    scsubName1 = scsubName[i];
                }
                if (i == 1)
                {
                    SubEnabled1 = true;
                    scsubName2 = scsubName[i];
                }
                if (i == 2)
                {
                    SubEnabled2 = true;
                    scsubName3 = scsubName[i];
                }
                if (i == 3)
                {
                    SubEnabled3 = true;
                    scsubName4 = scsubName[i];
                }
                if (i == 4)
                {
                    SubEnabled4 = true;
                    scsubName5 = scsubName[i];
                }
            }
        }
        private void PerformpopoutSchool()
        {
            schholPopUpVisibility = false;
        }
        private void PerformschSaved()
        {
            schholPopUpVisibility = false;
            noSubScMsgVsi = false;

            sub1 = scsubName[scSelectCount[0]];
            sub2 = scsubName[scSelectCount[1]];
            sub3 = scsubName[scSelectCount[2]];

            if(selectedscIndex == 1)
            {
                scScColor = Color.FromHex("#42ED88");
                scCmColor = Color.FromHex("#10000000");
                scarColor = Color.FromHex("#10000000");
                clgScColor = Color.FromHex("#10000000");
                clgChColor = Color.FromHex("#10000000");
                clgArColor = Color.FromHex("#10000000");
                clgScEnabled = true;
                clgCmEnabled = true;
                clgArEnabled = true;
            }
            else if(selectedscIndex == 2)
            {
                scScColor = Color.FromHex("#10000000");
                scCmColor = Color.FromHex("#42ED88");
                scarColor = Color.FromHex("#10000000");
                clgScColor = Color.FromHex("#10000000");
                clgChColor = Color.FromHex("#10000000");
                clgArColor = Color.FromHex("#10000000");
                clgScEnabled = false;
                clgCmEnabled = true;
                clgArEnabled = true;
            }
            else
            {
                scScColor = Color.FromHex("#10000000");
                scCmColor = Color.FromHex("#10000000");
                scarColor = Color.FromHex("#42ED88");
                clgScColor = Color.FromHex("#10000000");
                clgChColor = Color.FromHex("#10000000");
                clgArColor = Color.FromHex("#10000000");
                clgScEnabled = false;
                clgCmEnabled = false;
                clgArEnabled = true;
            }
            sub4 = "";
            sub5 = "";
            sub6 = "";
            sub7 = "";
            sub8 = "";
            sub9 = "";
            CheckEverything();
        }       
        private void PerformschSubSelect(string subIndex)
        {
            bool go = true;
            if(scSelectCount != null)
            {
                for (int i = 0; i < scSelectCount.Count; i++)
                {
                    if (scSelectCount[i] == int.Parse(subIndex) || scSelectCount.Count < 3)
                    {
                        go = true;
                        break;
                    }
                    else
                    {
                        go = false;
                    }
                }
            }
            
            if(go)
            {
                if (int.Parse(subIndex) == 0)
                {
                    if(scPhyColor == Color.FromHex("#D9FFBA"))
                    {
                        scPhyColor = Color.White;
                        scSelectCount.Remove(int.Parse(subIndex));
                    }
                    else
                    {
                        scPhyColor = Color.FromHex("#D9FFBA");
                        scSelectCount.Add(int.Parse(subIndex));
                    }
                   
                }
                if (int.Parse(subIndex) == 1)
                {
                    if(scCheColor == Color.FromHex("#D9FFBA"))
                    {
                        scCheColor = Color.White;
                        scSelectCount.Remove(int.Parse(subIndex));
                    }
                    else
                    {
                        scCheColor = Color.FromHex("#D9FFBA");
                        scSelectCount.Add(int.Parse(subIndex));
                    }
                }
                if (int.Parse(subIndex) == 2)
                {
                    if(scBioColor == Color.FromHex("#D9FFBA"))
                    {
                        scBioColor = Color.White;
                        scSelectCount.Remove(int.Parse(subIndex));
                    }
                    else
                    {
                        scBioColor = Color.FromHex("#D9FFBA");
                        scSelectCount.Add(int.Parse(subIndex));
                    }
                }
                if (int.Parse(subIndex) == 3)
                {
                    if (scMatColor == Color.FromHex("#D9FFBA"))
                    {
                        scMatColor = Color.White;
                        scSelectCount.Remove(int.Parse(subIndex));
                    }
                    else
                    {
                        scMatColor = Color.FromHex("#D9FFBA");
                        scSelectCount.Add(int.Parse(subIndex));
                    }
                }
                if (int.Parse(subIndex) == 4)
                {
                    if (scHmColor == Color.FromHex("#D9FFBA"))
                    {
                        scHmColor = Color.White;
                        scSelectCount.Remove(int.Parse(subIndex));
                    }
                    else
                    {
                        scHmColor = Color.FromHex("#D9FFBA");
                        scSelectCount.Add(int.Parse(subIndex));
                    }
                }               
            }

            if (scSelectCount.Count == 3)
            { 
                scEnabled = true;
            }
            else
            {
                scEnabled = false;
            }
            
        }
        #endregion


        #region College Pop
        private void PerformpopupCollege(string index)
        {
            selectedClgIndex = int.Parse(index);
            selectedClgIndex = int.Parse(index);
            CollegePopupVisibility = true;
            clgSelectCount.Clear();
            clgPhy1st = Color.White;
            clgPhy2nd = Color.White;
            clgChe1st = Color.White;
            clgChe2nd = Color.White;
            clgBio1st = Color.White;
            clgBio2nd = Color.White;
            clgHm1st = Color.White;
            clgHm2nd = Color.White;
            clgenabled1 = false;
            clgSubEnabled2 = false;
            clgSubEnabled3 = false;
            clgSubEnabled4 = false;
            clgSubEnabled5 = false;
            clgSubEnabled6 = false;
            clgSubEnabled7 = false;
            clgSubEnabled8 = false;

            clgSubName1 = "";
            clgSubName2 = "";
            clgSubName3 = "";
            clgSubName4 = "";
            clgSubName5 = "";
            clgSubName6 = "";
            clgSubName7 = "";
            clgSubName8 = "";
            if (int.Parse(index) == 1)
            {
                clgpopUpTitle = "Choose Any Six Subject From Class 11-12";
                clgSubEnabled = true;
               

                clgsubName = clgScsubName;
                clgSelectSubCountMax = 6;

            }
            else if (int.Parse(index) == 2)
            {
                clgpopUpTitle = "Choose Any three Subject From Class 11-12";
                clgSubEnabled = false;
               
                clgsubName = clgChsubName;
                clgSelectSubCountMax = 3;
            }
            else if (int.Parse(index) == 3)
            {
                clgpopUpTitle = "Choose Any three Subject From Class 11-12";
                clgSubEnabled = false;
                
                clgsubName = clgArsubName;

                clgSelectSubCountMax = 3;
            }
            for (int i = 0; i < clgsubName.Count; i++)
            {
                if (i == 0)
                {
                    clgenabled1 = true;
                    clgSubName1 = clgsubName[i];
                }
                if (i == 1)
                {
                    clgSubEnabled2 = true;
                    clgSubName2 = clgsubName[i];
                }
                if (i == 2)
                {
                    clgSubEnabled3 = true;
                    clgSubName3 = clgsubName[i];
                }
                if (i == 3)
                {
                    clgSubEnabled4 = true;
                    clgSubName4 = clgsubName[i];
                }
                if (i == 4)
                {
                    clgSubEnabled5 = true;
                    clgSubName5 = clgsubName[i];
                }
                if (i == 5)
                {
                    clgSubEnabled6 = true;
                    clgSubName6 = clgsubName[i];
                }
                if (i == 6)
                {
                    clgSubEnabled7 = true;
                    clgSubName7 = clgsubName[i];
                }
                if (i == 7)
                {
                    clgSubEnabled8 = true;
                    clgSubName8 = clgsubName[i];
                }

            }
        }

        private void PerformpopoutCollege()
        {
            CollegePopupVisibility = false;
        }
        private void PerformclgSaved()
        {
            if(selectedClgIndex == 1)
            {
                clgScColor = Color.FromHex("#42ED88");
                clgChColor = Color.FromHex("#10000000");
                clgArColor = Color.FromHex("#10000000");
            }
            else if (selectedClgIndex == 2)
            {
                clgScColor = Color.FromHex("#10000000");
                clgChColor = Color.FromHex("#42ED88");
                clgArColor = Color.FromHex("#10000000");
            }
            else if (selectedClgIndex == 3)
            {
                clgScColor = Color.FromHex("#10000000");
                clgChColor = Color.FromHex("#10000000");
                clgArColor = Color.FromHex("#42ED88");
            }
            CollegePopupVisibility = false;
            noSubMsgVsi = false;
            if(clgSelectSubCountMax == 6)
            {
                sub4 = clgsubName[clgSelectCount[0]];
                sub5 = clgsubName[clgSelectCount[1]];
                sub6 = clgsubName[clgSelectCount[2]];
                sub7 = clgsubName[clgSelectCount[3]];
                sub8 = clgsubName[clgSelectCount[4]];
                sub9 = clgsubName[clgSelectCount[5]];
            }
            else
            {
                sub4 = clgsubName[clgSelectCount[0]];
                sub5 = clgsubName[clgSelectCount[1]];
                sub6 = clgsubName[clgSelectCount[2]];
                sub7 = "n/a";
                sub8 = "n/a";
                sub9 = "n/a";
            }
            CheckEverything();
        }
        private void PerformschClgSelect(string clgIndex)
        {
            bool go = true;
            if (clgSelectCount != null)
            {
                for (int i = 0; i < clgSelectCount.Count; i++)
                {
                    if (clgSelectCount[i] == int.Parse(clgIndex) || clgSelectCount.Count < clgSelectSubCountMax)
                    {
                        go = true;
                        break;
                    }
                    else
                    {
                        go = false;
                    }
                }
            }

            if (go)
            {
                if (int.Parse(clgIndex) == 0)
                {
                    if (clgPhy1st == Color.FromHex("#D9FFBA"))
                    {
                        clgPhy1st = Color.White;
                        clgSelectCount.Remove(int.Parse(clgIndex));
                    }
                    else
                    {
                        clgPhy1st = Color.FromHex("#D9FFBA");
                        clgSelectCount.Add(int.Parse(clgIndex));
                    }

                }
                if (int.Parse(clgIndex) == 1)
                {
                    if (clgPhy2nd == Color.FromHex("#D9FFBA"))
                    {
                        clgPhy2nd = Color.White;
                        clgSelectCount.Remove(int.Parse(clgIndex));
                    }
                    else
                    {
                        clgPhy2nd = Color.FromHex("#D9FFBA");
                        clgSelectCount.Add(int.Parse(clgIndex));
                    }
                }
                if (int.Parse(clgIndex) == 2)
                {
                    if (clgChe1st == Color.FromHex("#D9FFBA"))
                    {
                        clgChe1st = Color.White;
                        clgSelectCount.Remove(int.Parse(clgIndex));
                    }
                    else
                    {
                        clgChe1st = Color.FromHex("#D9FFBA");
                        clgSelectCount.Add(int.Parse(clgIndex));
                    }
                }
                if (int.Parse(clgIndex) == 3)
                {
                    if (clgChe2nd == Color.FromHex("#D9FFBA"))
                    {
                        clgChe2nd = Color.White;
                        clgSelectCount.Remove(int.Parse(clgIndex));
                    }
                    else
                    {
                        clgChe2nd = Color.FromHex("#D9FFBA");
                        clgSelectCount.Add(int.Parse(clgIndex));
                    }
                }
                if (int.Parse(clgIndex) == 4)
                {
                    if (clgBio1st == Color.FromHex("#D9FFBA"))
                    {
                        clgBio1st = Color.White;
                        clgSelectCount.Remove(int.Parse(clgIndex));
                    }
                    else
                    {
                        clgBio1st = Color.FromHex("#D9FFBA");
                        clgSelectCount.Add(int.Parse(clgIndex));
                    }
                }
                if (int.Parse(clgIndex) == 5)
                {
                    if (clgBio2nd == Color.FromHex("#D9FFBA"))
                    {
                        clgBio2nd = Color.White;
                        clgSelectCount.Remove(int.Parse(clgIndex));
                    }
                    else
                    {
                        clgBio2nd = Color.FromHex("#D9FFBA");
                        clgSelectCount.Add(int.Parse(clgIndex));
                    }
                }
                if (int.Parse(clgIndex) == 6)
                {
                    if (clgHm1st == Color.FromHex("#D9FFBA"))
                    {
                        clgHm1st = Color.White;
                        clgSelectCount.Remove(int.Parse(clgIndex));
                    }
                    else
                    {
                        clgHm1st = Color.FromHex("#D9FFBA");
                        clgSelectCount.Add(int.Parse(clgIndex));
                    }
                }
                if (int.Parse(clgIndex) == 7)
                {
                    if (clgHm2nd == Color.FromHex("#D9FFBA"))
                    {
                        clgHm2nd = Color.White;
                        clgSelectCount.Remove(int.Parse(clgIndex));
                    }
                    else
                    {
                        clgHm2nd = Color.FromHex("#D9FFBA");
                        clgSelectCount.Add(int.Parse(clgIndex));
                    }
                }

                if ((clgSelectCount.Count == 3 && clgSelectSubCountMax == 3) || (clgSelectCount.Count == 6 && clgSelectSubCountMax == 6))
                {
                    ClgSavedEnabled = true;
                }
                else
                {
                    ClgSavedEnabled = false;
                }
            }
        }
        #endregion
       

        #region OTP
        private void Performcomandotp()
        {
            SaveThisTeacher();
            OTPsec = 60;
            sendAgainEnabled = false;
            otpEnabled = false;
            otpWindow = true;
            OTPTimerContinue = true;
            otpHasError = false;
            otpErrorTxt = "";
            showpn = pnumber;
            SendSms();

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                OTPsec--;
                sendAgainTxt = "Send Again in " + OTPsec + " Seconds";
                if (OTPsec == 0)
                {
                    sendAgainEnabled = true;
                    sendAgainTxt = "Send OTP Again";
                    OTPTimerContinue = false;
                }
                return OTPTimerContinue;
            });
        }
        public void SaveThisTeacher()
        {
            setTeacher thisTeacher = new setTeacher();
            thisTeacher.teacherID = StaticPageForPassingData.GenarateNewID();
            thisTeacher.name = name;
            thisTeacher.phonenumber = pnumber;
            thisTeacher.password = password;
            List<string> allselectedSub = new List<string>();
            allselectedSub.Add(sub1);
            allselectedSub.Add(sub2);
            allselectedSub.Add(sub3);
            allselectedSub.Add(sub4);
            allselectedSub.Add(sub5);
            allselectedSub.Add(sub6);
            allselectedSub.Add(sub7);
            allselectedSub.Add(sub8);
            allselectedSub.Add(sub9);
            int subIndexCount = 0;
            for (int i = 0; i < AllsubList.Count; i++)
            {
                if (AllsubList[i].name == allselectedSub[subIndexCount])
                {
                    if(subIndexCount == 0)
                    {
                        thisTeacher.sub1 = AllsubList[i].subjectID;
                        i = -1;
                        subIndexCount++;
                    }
                    else if (subIndexCount == 1)
                    {
                        thisTeacher.sub2 = AllsubList[i].subjectID;
                        i = -1;
                        subIndexCount++;
                    }
                    else if (subIndexCount == 2)
                    {
                        thisTeacher.sub3 = AllsubList[i].subjectID;
                        i = -1;
                        subIndexCount++;
                    }
                    else if (subIndexCount == 3)
                    {
                        thisTeacher.sub4 = AllsubList[i].subjectID;
                        i = -1;
                        subIndexCount++;
                    }
                    else if (subIndexCount == 4)
                    {
                        thisTeacher.sub5 = AllsubList[i].subjectID;
                        i = -1;
                        subIndexCount++;
                    }
                    else if (subIndexCount == 5)
                    {
                        thisTeacher.sub6 = AllsubList[i].subjectID;
                        i = -1;
                        subIndexCount++;
                    }
                    else if (subIndexCount == 6)
                    {
                        thisTeacher.sub7 = AllsubList[i].subjectID;
                        i = -1;
                        subIndexCount++;
                    }
                    else if (subIndexCount == 7)
                    {
                        thisTeacher.sub8 = AllsubList[i].subjectID;
                        i = -1;
                        subIndexCount++;
                    }
                    else if (subIndexCount == 8)
                    {
                        thisTeacher.sub9 = AllsubList[i].subjectID;
                        break;
                    }
                }
            }

            StaticPageForPassingData.ThisRegTeacher = thisTeacher;
        }
        public async Task SendSms()
        {
            if (!IsInternetConnectionAvailable())
            {
                return;
            }
            Random rnd = new Random();
            int rn = rnd.Next(1000,9999);
            string MSG = "Your Shikkhanobish Teacher Registration OTP is: "+ rn;
            string pn = "+88"+pnumber;
            OTPCode = rn;
            var res = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/SendSmsAsync".PostUrlEncodedAsync(new { msg = MSG, number = pn }).ReceiveJson<SendSms>();
        }
        private void PerformpopoutOTP()
        {
            OTPTimerContinue = false;
            OTPsec = 60;
            otpWindow = false ;
        }
        int OTPCode;
        private void PerformSendAgainOTPButton()
        {
            otpHasError = false;
            otpErrorTxt = "";
            OTPsec = 60;
            sendAgainEnabled = false;
            otpEnabled = false;
            OTPTimerContinue = true;
            SendSms();

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                OTPsec--;
                sendAgainTxt = "Send Again in " + OTPsec + " Seconds";
                if (OTPsec <= 0)
                {
                    sendAgainEnabled = true;
                    sendAgainTxt = "Send OTP Again";
                    OTPTimerContinue = false;
                }
                return OTPTimerContinue;
            });

        }
        private async Task  PerformregTeacherCmd()
        {
            if(int.Parse(otpText) != OTPCode)
            {
                otpHasError = true;
                otpErrorTxt = "OTP Does't Match";
            }
            else
            {
                StaticPageForPassingData.freomReg = true;
                otpWindow = false;
                otpHasError = false;
                otpErrorTxt = "";
                await CompleteTeachERReg();
            }
            
        }
        public async Task CompleteTeachERReg()
        {
            if (!IsInternetConnectionAvailable())
            {
                return;
            }
            using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Completing Teacher Registration..."))
            {
                var res = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/SetTeacher".PostUrlEncodedAsync(new
                {
                    teacherID = StaticPageForPassingData.ThisRegTeacher.teacherID,
                    name = StaticPageForPassingData.ThisRegTeacher.name,
                    phonenumber = StaticPageForPassingData.ThisRegTeacher.phonenumber,
                    password = StaticPageForPassingData.ThisRegTeacher.password,
                    sub1 = StaticPageForPassingData.ThisRegTeacher.sub1,
                    sub2 = StaticPageForPassingData.ThisRegTeacher.sub2,
                    sub3 = StaticPageForPassingData.ThisRegTeacher.sub3,
                    sub4 = StaticPageForPassingData.ThisRegTeacher.sub4,
                    sub5 = StaticPageForPassingData.ThisRegTeacher.sub5,
                    sub6 = StaticPageForPassingData.ThisRegTeacher.sub6,
                    sub7 = StaticPageForPassingData.ThisRegTeacher.sub7,
                    sub8 = StaticPageForPassingData.ThisRegTeacher.sub8,
                    sub9 = StaticPageForPassingData.ThisRegTeacher.sub9
                })
      .ReceiveJson<Response>();
                await StaticPageForPassingData.GetALlTeacherInfo(StaticPageForPassingData.ThisRegTeacher.password, StaticPageForPassingData.ThisRegTeacher.phonenumber);
                await SecureStorage.SetAsync("phonenumber", StaticPageForPassingData.ThisRegTeacher.phonenumber);
                await SecureStorage.SetAsync("password", StaticPageForPassingData.ThisRegTeacher.password);
                await Application.Current.MainPage.Navigation.PushModalAsync(new AppShell());
                await Task.Delay(1000);
                await dialog.DismissAsync();
            }
        }
        private void PerformgoBack()
        {
            Application.Current.MainPage.Navigation.PopModalAsync();
        }
        #endregion
        #endregion


        #region Binding
        private string name1;

        public string name { get { return name1; } set { name1 = value; CheckEverything(); SetProperty(ref name1, value); } }

        private string pnumber1;

        public string pnumber { get { return pnumber1; } set { pnumber1 = value; CheckEverything(); checkPnumber();
                    SetProperty(ref pnumber1, value); } }

        private string password1;

        public string password { get { return password1; } set { password1 = value; CheckEverything(); SetProperty(ref password1, value); } }

        private string conFirmPassword1;

        public string conFirmPassword { get { return conFirmPassword1; } set { conFirmPassword1 = value; CheckEverything(); SetProperty(ref conFirmPassword1, value); } }

        private string sub11;

        public string sub1 { get => sub11; set => SetProperty(ref sub11, value); }

        private string sub21;

        public string sub2 { get => sub21; set => SetProperty(ref sub21, value); }

        private string sub31;

        public string sub3 { get => sub31; set => SetProperty(ref sub31, value); }

        private string sub41;

        public string sub4 { get => sub41; set => SetProperty(ref sub41, value); }

        private string sub51;

        public string sub5 { get => sub51; set => SetProperty(ref sub51, value); }

        private string sub61;

        public string sub6 { get => sub61; set => SetProperty(ref sub61, value); }

        private string sub81;

        public string sub8 { get => sub81; set => SetProperty(ref sub81, value); }

        private string sub91;

        public string sub9 { get => sub91; set => SetProperty(ref sub91, value); }

        private string sub71;

        public string sub7 { get => sub71; set => SetProperty(ref sub71, value); }

        private bool schholPopUpVisibility1;

        public bool schholPopUpVisibility { get => schholPopUpVisibility1; set => SetProperty(ref schholPopUpVisibility1, value); }

        private bool collegePopupVisibility;

        public bool CollegePopupVisibility { get => collegePopupVisibility; set => SetProperty(ref collegePopupVisibility, value); }

        private Command popoutCollege1;

        public ICommand popoutCollege
        {
            get
            {
                if (popoutCollege1 == null)
                {
                    popoutCollege1 = new Command(PerformpopoutCollege);
                }

                return popoutCollege1;
            }
        }

       

        private Command popoutSchool1;

        public ICommand popoutSchool
        {
            get
            {
                if (popoutSchool1 == null)
                {
                    popoutSchool1 = new Command(PerformpopoutSchool);
                }

                return popoutSchool1;
            }
        }

       

        private Command popupSchool1;

        public ICommand popupSchool
        {
            get
            {
                if (popupSchool1 == null)
                {
                    popupSchool1 = new Command<string>(PerformpopupSchool);
                }

                return popupSchool1;
            }
        }

        

        private Command popupCollege1;

        public ICommand popupCollege
        {
            get
            {
                if (popupCollege1 == null)
                {
                    popupCollege1 = new Command<string>(PerformpopupCollege);
                }

                return popupCollege1;
            }
        }

        private Color scScColor1;

        public Color scScColor { get => scScColor1; set => SetProperty(ref scScColor1, value); }

        private Color scarColor1;

        public Color scarColor { get => scarColor1; set => SetProperty(ref scarColor1, value); }

        private Color scCmColor1;

        public Color scCmColor { get => scCmColor1; set => SetProperty(ref scCmColor1, value); }

        private bool clgScEnabled1;

        public bool clgScEnabled { get => clgScEnabled1; set => SetProperty(ref clgScEnabled1, value); }

        private bool clgCmEnabled1;

        public bool clgCmEnabled { get => clgCmEnabled1; set => SetProperty(ref clgCmEnabled1, value); }

        private bool clgArEnabled1;

        public bool clgArEnabled { get => clgArEnabled1; set => SetProperty(ref clgArEnabled1, value); }

        private Command schSaved1;

        public ICommand schSaved
        {
            get
            {
                if (schSaved1 == null)
                {
                    schSaved1 = new Command(PerformschSaved);
                }

                return schSaved1;
            }
        }

       

        private Command clgSaved1;

        public ICommand clgSaved
        {
            get
            {
                if (clgSaved1 == null)
                {
                    clgSaved1 = new Command(PerformclgSaved);
                }

                return clgSaved1;
            }
        }

        private Color clgScColor1;

        public Color clgScColor { get => clgScColor1; set => SetProperty(ref clgScColor1, value); }

        private Color clgChColor1;

        public Color clgChColor { get => clgChColor1; set => SetProperty(ref clgChColor1, value); }

        private Color clgArColor1;

        public Color clgArColor { get => clgArColor1; set => SetProperty(ref clgArColor1, value); }

        private bool noSubMsgVsi1;

        public bool noSubMsgVsi { get => noSubMsgVsi1; set => SetProperty(ref noSubMsgVsi1, value); }

        private bool noSubScMsgVsi1;

        public bool noSubScMsgVsi { get => noSubScMsgVsi1; set => SetProperty(ref noSubScMsgVsi1, value); }

        private bool scEnabled1;

        public bool scEnabled { get => scEnabled1; set => SetProperty(ref scEnabled1, value); }

       

        private Command schSubSelect1;

        public ICommand schSubSelect
        {
            get
            {
                if (schSubSelect1 == null)
                {
                    schSubSelect1 = new Command<string>(PerformschSubSelect);
                }

                return schSubSelect1;
            }
        }

        private Color scPhyColor1;

        public Color scPhyColor { get => scPhyColor1; set => SetProperty(ref scPhyColor1, value); }

        private Color scCheColor1;

        public Color scCheColor { get => scCheColor1; set => SetProperty(ref scCheColor1, value); }

        private Color scBioColor1;

        public Color scBioColor { get => scBioColor1; set => SetProperty(ref scBioColor1, value); }

        private Color scMatColor1;

        public Color scMatColor { get => scMatColor1; set => SetProperty(ref scMatColor1, value); }

        private Color scHmColor1;

        public Color scHmColor { get => scHmColor1; set => SetProperty(ref scHmColor1, value); }

        private Command schClgSelect1;

        public ICommand schClgSelect
        {
            get
            {
                if (schClgSelect1 == null)
                {
                    schClgSelect1 = new Command<string>(PerformschClgSelect);
                }

                return schClgSelect1;
            }
        }

        private Color clgPhy1st1;

        public Color clgPhy1st { get => clgPhy1st1; set => SetProperty(ref clgPhy1st1, value); }

        private Color clgPhy2nd1;

        public Color clgPhy2nd { get => clgPhy2nd1; set => SetProperty(ref clgPhy2nd1, value); }

        private Color clgChe1st1;

        public Color clgChe1st { get => clgChe1st1; set => SetProperty(ref clgChe1st1, value); }

        private Color clgChe2nd1;

        public Color clgChe2nd { get => clgChe2nd1; set => SetProperty(ref clgChe2nd1, value); }

        private Color clgBio1st1;

        public Color clgBio1st { get => clgBio1st1; set => SetProperty(ref clgBio1st1, value); }

        private Color clgBio2nd1;

        public Color clgBio2nd { get => clgBio2nd1; set => SetProperty(ref clgBio2nd1, value); }

        private Color clgHm1st1;

        public Color clgHm1st { get => clgHm1st1; set => SetProperty(ref clgHm1st1, value); }

        private Color clgHm2nd1;

        public Color clgHm2nd { get => clgHm2nd1; set => SetProperty(ref clgHm2nd1, value); }

        private string scsubName11;

        public string scsubName1 { get => scsubName11; set => SetProperty(ref scsubName11, value); }

        private string scsubName21;

        public string scsubName2 { get => scsubName21; set => SetProperty(ref scsubName21, value); }

        private string scsubName31;

        public string scsubName3 { get => scsubName31; set => SetProperty(ref scsubName31, value); }

        private string scsubName41;

        public string scsubName4 { get => scsubName41; set => SetProperty(ref scsubName41, value); }

        private string scsubName51;

        public string scsubName5 { get => scsubName51; set => SetProperty(ref scsubName51, value); }

        private bool artSubEnabled1;

        public bool artSubEnabled { get => artSubEnabled1; set => SetProperty(ref artSubEnabled1, value); }

        private string clgSubName11;

        public string clgSubName1 { get => clgSubName11; set => SetProperty(ref clgSubName11, value); }

        private string clgSubName21;

        public string clgSubName2 { get => clgSubName21; set => SetProperty(ref clgSubName21, value); }

        private string clgSubName31;

        public string clgSubName3 { get => clgSubName31; set => SetProperty(ref clgSubName31, value); }

        private string clgSubName41;

        public string clgSubName4 { get => clgSubName41; set => SetProperty(ref clgSubName41, value); }

        private string clgSubName51;

        public string clgSubName5 { get => clgSubName51; set => SetProperty(ref clgSubName51, value); }

        private string clgSubName61;

        public string clgSubName6 { get => clgSubName61; set => SetProperty(ref clgSubName61, value); }

        private string clgSubName71;

        public string clgSubName7 { get => clgSubName71; set => SetProperty(ref clgSubName71, value); }

        private string clgSubName81;

        public string clgSubName8 { get => clgSubName81; set => SetProperty(ref clgSubName81, value); }

        private bool clgSubEnabled1;

        public bool clgSubEnabled { get => clgSubEnabled1; set => SetProperty(ref clgSubEnabled1, value); }

        private string clgpopUpTitle1;

        public string clgpopUpTitle { get => clgpopUpTitle1; set => SetProperty(ref clgpopUpTitle1, value); }

        private bool sendotpEnabled1;

        public bool sendotpEnabled { get => sendotpEnabled1; set => SetProperty(ref sendotpEnabled1, value); }

        private Command comandotp1;

        public ICommand comandotp
        {
            get
            {
                if (comandotp1 == null)
                {
                    comandotp1 = new Command(Performcomandotp);
                }

                return comandotp1;
            }
        }

        private bool otpWindow1;

        public bool otpWindow { get => otpWindow1; set => SetProperty(ref otpWindow1, value); }

        private bool subEnabled;

        public bool SubEnabled { get => subEnabled; set => SetProperty(ref subEnabled, value); }

        private bool subEnabled1;

        public bool SubEnabled1 { get => subEnabled1; set => SetProperty(ref subEnabled1, value); }

        private bool subEnabled2;

        public bool SubEnabled2 { get => subEnabled2; set => SetProperty(ref subEnabled2, value); }

        private bool subEnabled3;

        public bool SubEnabled3 { get => subEnabled3; set => SetProperty(ref subEnabled3, value); }

        private bool subEnabled4;

        public bool SubEnabled4 { get => subEnabled4; set => SetProperty(ref subEnabled4, value); }

        private bool clgenabled11;

        public bool clgenabled1 { get => clgenabled11; set => SetProperty(ref clgenabled11, value); }

        private bool clgSubEnabled21;

        public bool clgSubEnabled2 { get => clgSubEnabled21; set => SetProperty(ref clgSubEnabled21, value); }

        private bool clgSubEnabled31;

        public bool clgSubEnabled3 { get => clgSubEnabled31; set => SetProperty(ref clgSubEnabled31, value); }

        private bool clgSubEnabled41;

        public bool clgSubEnabled4 { get => clgSubEnabled41; set => SetProperty(ref clgSubEnabled41, value); }

        private bool clgSubEnabled51;

        public bool clgSubEnabled5 { get => clgSubEnabled51; set => SetProperty(ref clgSubEnabled51, value); }

        private bool clgSubEnabled61;

        public bool clgSubEnabled6 { get => clgSubEnabled61; set => SetProperty(ref clgSubEnabled61, value); }

        private bool clgSubEnabled71;

        public bool clgSubEnabled7 { get => clgSubEnabled71; set => SetProperty(ref clgSubEnabled71, value); }

        private bool clgSubEnabled81;

        public bool clgSubEnabled8 { get => clgSubEnabled81; set => SetProperty(ref clgSubEnabled81, value); }

        private bool clgSavedEnabled;

        public bool ClgSavedEnabled { get => clgSavedEnabled; set => SetProperty(ref clgSavedEnabled, value); }

        private bool otpEnabled1;

        public bool otpEnabled { get => otpEnabled1; set => SetProperty(ref otpEnabled1, value); }

        private string sendAgainTxt1;

        public string sendAgainTxt { get => sendAgainTxt1; set => SetProperty(ref sendAgainTxt1, value); }

        private bool sendAgainEnabled1;

        public bool sendAgainEnabled { get => sendAgainEnabled1; set => SetProperty(ref sendAgainEnabled1, value); }

        private Command popoutOTP1;

        public ICommand popoutOTP
        {
            get
            {
                if (popoutOTP1 == null)
                {
                    popoutOTP1 = new Command(PerformpopoutOTP);
                }

                return popoutOTP1;
            }
        }

        private Command sendAgainOTPButton;

        public ICommand SendAgainOTPButton
        {
            get
            {
                if (sendAgainOTPButton == null)
                {
                    sendAgainOTPButton = new Command(PerformSendAgainOTPButton);
                }

                return sendAgainOTPButton;
            }
        }

        private bool otpHasError1;

        public bool otpHasError { get => otpHasError1; set => SetProperty(ref otpHasError1, value); }

        private string otpErrorTxt1;

        public string otpErrorTxt { get => otpErrorTxt1; set => SetProperty(ref otpErrorTxt1, value); }

        private Command regTeacherCmd1;

        public ICommand regTeacherCmd
        {
            get
            {
                if (regTeacherCmd1 == null)
                {
                    regTeacherCmd1 = new Command(async =>  PerformregTeacherCmd());
                }

                return regTeacherCmd1;
            }
        }

        private string otpText1;

        public string otpText { get { return otpText1; } set { otpText1 = value; if (otpText == null || otpText == "") { otpEnabled = false; } else { otpEnabled = true; } SetProperty(ref otpText1, value); } }

        private string passErrorTxt1;

        public string passErrorTxt { get => passErrorTxt1; set => SetProperty(ref passErrorTxt1, value); }

        private string conPassErrorTxt1;

        public string conPassErrorTxt { get => conPassErrorTxt1; set => SetProperty(ref conPassErrorTxt1, value); }

        private string pnErrorTxt1;

        public string pnErrorTxt { get => pnErrorTxt1; set => SetProperty(ref pnErrorTxt1, value); }

        private string nameErrorTxt1;

        public string nameErrorTxt { get => nameErrorTxt1; set => SetProperty(ref nameErrorTxt1, value); }

        private bool hasNameError1;

        public bool hasNameError { get => hasNameError1; set => SetProperty(ref hasNameError1, value); }

        private bool haspnError1;

        public bool haspnError { get => haspnError1; set => SetProperty(ref haspnError1, value); }

        private bool hasPassError1;

        public bool hasPassError { get => hasPassError1; set => SetProperty(ref hasPassError1, value); }

        private bool hasConPassError1;

        public bool hasConPassError { get => hasConPassError1; set => SetProperty(ref hasConPassError1, value); }

        private bool completingTeacherRegVisbility1;

        public bool completingTeacherRegVisbility { get => completingTeacherRegVisbility1; set => SetProperty(ref completingTeacherRegVisbility1, value); }

        private string showpn1;

        public string showpn { get => showpn1; set => SetProperty(ref showpn1, value); }

        private Command goBack1;

        public ICommand goBack
        {
            get
            {
                if (goBack1 == null)
                {
                    goBack1 = new Command(PerformgoBack);
                }

                return goBack1;
            }
        }

       

        #endregion

    }
}
