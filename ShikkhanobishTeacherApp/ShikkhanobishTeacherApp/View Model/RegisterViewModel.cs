using ShikkhanobishTeacherApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

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
        int selectedscIndex;
        int selectedClgIndex;
        int clgSelectSubCountMax;
        #region Methods
        public RegisterViewModel ()
        {
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

            scScsubName.Add("Physics");
            scScsubName.Add("Chemistry");
            scScsubName.Add("Biology");
            scScsubName.Add("Math");
            scScsubName.Add("Higher Math");

            scChsubName.Add("Economics");
            scChsubName.Add("Bank");
            scChsubName.Add("Marketing");
            scChsubName.Add("Math");
            scChsubName.Add("Finance");

            scArsubName.Add("Loigc");
            scArsubName.Add("Math");
            scArsubName.Add("Bangla");
            scArsubName.Add("English");


            /////////////////////////////
            clgScsubName.Add("Physics 1st Paper");
            clgScsubName.Add("Physics 2nd Paper");
            clgScsubName.Add("Chemistry 1st Paper");
            clgScsubName.Add("Chemistry 2nd Paper");
            clgScsubName.Add("Biology First Paper");
            clgScsubName.Add("Bilogy 2nd Paper");
            clgScsubName.Add("Higher Math 1st Paper");
            clgScsubName.Add("Higher Math 2nd Paper");

            clgChsubName.Add("Economics");
            clgChsubName.Add("Bank");
            clgChsubName.Add("Marketing");
            clgChsubName.Add("Math");
            clgChsubName.Add("Finance");

            clgArsubName.Add("Loigc");
            clgArsubName.Add("Math");
            clgArsubName.Add("Bangla");
            clgArsubName.Add("English");


            CollegePopupVisibility = false;
            schholPopUpVisibility =  false;
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

            clgEnabled = false;
            scEnabled = false;

            scPhyColor = Color.White;
            scCheColor = Color.White;
            scBioColor = Color.White;
            scMatColor = Color.White;
            scHmColor = Color.White;
        }

        

        #region school popup
        private void PerformpopupSchool(string index)
        {
            if(selectedscIndex != int.Parse(index))
            {
                scEnabled = false;
            }
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
                artSubEnabled = true;                
                clgScEnabled = true;
                clgCmEnabled = true;
                clgArEnabled = true;
                scsubName = scScsubName;
                scsubName1 = scsubName[0];
                scsubName2 = scsubName[1];
                scsubName3 = scsubName[2];
                scsubName4 = scsubName[3];
                scsubName5 = scsubName[4];

            }
            else if (int.Parse(index) == 2)
            {
                clgSelectSubCountMax = 3;
                artSubEnabled = true;                
                clgScEnabled = false;
                clgCmEnabled = true;
                clgArEnabled = true;
                scsubName = scChsubName;
                scsubName1 = scsubName[0];
                scsubName2 = scsubName[1];
                scsubName3 = scsubName[2];
                scsubName4 = scsubName[3];
                scsubName5 = scsubName[4];
            }
            else if (int.Parse(index) == 3)
            {
                clgSelectSubCountMax = 3;
                artSubEnabled = false;              
                clgScEnabled = false;
                clgCmEnabled = false;
                clgArEnabled = true;
                scsubName = scArsubName;
                scsubName1 = scsubName[0];
                scsubName2 = scsubName[1];
                scsubName3 = scsubName[2];
                scsubName4 = scsubName[3];
                scsubName5 = "N/A";
            }

        }
        private void PerformpopoutSchool()
        {
            schholPopUpVisibility = false;
        }
        private void PerformschSaved()
        {
            sendotpEnabled = false;
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
            }
            else if(selectedscIndex == 2)
            {
                scScColor = Color.FromHex("#10000000");
                scCmColor = Color.FromHex("#42ED88");
                scarColor = Color.FromHex("#10000000");
                clgScColor = Color.FromHex("#10000000");
                clgChColor = Color.FromHex("#10000000");
                clgArColor = Color.FromHex("#10000000");
            }
            else
            {
                scScColor = Color.FromHex("#10000000");
                scCmColor = Color.FromHex("#10000000");
                scarColor = Color.FromHex("#42ED88");
                clgScColor = Color.FromHex("#10000000");
                clgChColor = Color.FromHex("#10000000");
                clgArColor = Color.FromHex("#10000000");
            }
            sub4 = "";
            sub5 = "";
            sub6 = "";
            sub7 = "";
            sub8 = "";
            sub9 = ""; 
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
            if (selectedClgIndex != int.Parse(index))
            {
                clgEnabled = false;
            }
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
            if (int.Parse(index) == 1)
            {
                clgpopUpTitle = "Choose Any Six Subject From Class 11-12";
                clgSubEnabled = true;
                clgScColor = Color.FromHex("#42ED88");
                clgChColor = Color.FromHex("#10000000");
                clgArColor = Color.FromHex("#10000000");

                clgsubName = clgScsubName;
                clgSubName1 = clgsubName[0];
                clgSubName2 = clgsubName[1];
                clgSubName3 = clgsubName[2];
                clgSubName4 = clgsubName[3];
                clgSubName5 = clgsubName[4];
                clgSubName6 = clgsubName[5];
                clgSubName7 = clgsubName[6];
                clgSubName8 = clgsubName[7];
                clgSelectSubCountMax = 6;

            }
            else if (int.Parse(index) == 2)
            {
                clgpopUpTitle = "Choose Any three Subject From Class 11-12";
                clgSubEnabled = false;
                clgScColor = Color.FromHex("#10000000");
                clgChColor = Color.FromHex("#42ED88");
                clgArColor = Color.FromHex("#10000000");
                clgsubName = clgChsubName;
                clgSubName1 = clgsubName[0];
                clgSubName2 = clgsubName[1];
                clgSubName3 = clgsubName[2];
                clgSubName4 = clgsubName[3];
                clgSubName5 = "N/A";
                clgSubName6 = "N/A";
                clgSubName7 = "N/A";
                clgSubName8 = "N/A";
                clgSelectSubCountMax = 3;
            }
            else if (int.Parse(index) == 3)
            {
                clgpopUpTitle = "Choose Any three Subject From Class 11-12";
                clgSubEnabled = false;
                clgScColor = Color.FromHex("#10000000");
                clgChColor = Color.FromHex("#10000000");
                clgArColor = Color.FromHex("#42ED88");
                clgsubName = clgArsubName;
                clgSubName1 = clgsubName[0];
                clgSubName2 = clgsubName[1];
                clgSubName3 = clgsubName[2];
                clgSubName4 = clgsubName[3];
                clgSubName5 = "N/A";
                clgSubName6 = "N/A";
                clgSubName7 = "N/A";
                clgSubName8 = "N/A";
                clgSelectSubCountMax = 3;
            }

        }

        private void PerformpopoutCollege()
        {
            CollegePopupVisibility = false;
        }
        private void PerformclgSaved()
        {
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
                sub7 = "";
                sub8 = "";
                sub9 = "";
            }
            if (sub1 != null && sub2 != null && sub3 != null && sub4 != null && sub5 != null && sub6 != null && sub7 != null && sub8 != null && sub9 != null && clgSelectSubCountMax == 6)
            {
                sendotpEnabled = true;
            }
            else if(sub1 != null && sub2 != null && sub3 != null && sub4 != null && sub5 != null && sub6 != null && clgSelectSubCountMax == 3)
            {
                sendotpEnabled = true;
            }
            else
            {
                sendotpEnabled = true;
            }
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
            }

            if (clgSelectCount.Count == clgSelectSubCountMax)
            {
                clgEnabled = true;
            }
            else
            {
                clgEnabled = false;
            }
        }
        #endregion

        private void Performcomandotp()
        {
            otpWindow = true;
        }
        #endregion


        #region Binding
        private string name1;

        public string name { get => name1; set => SetProperty(ref name1, value); }

        private string pnumber1;

        public string pnumber { get => pnumber1; set => SetProperty(ref pnumber1, value); }

        private string password1;

        public string password { get => password1; set => SetProperty(ref password1, value); }

        private string conFirmPassword1;

        public string conFirmPassword { get => conFirmPassword1; set => SetProperty(ref conFirmPassword1, value); }

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

        private bool clgEnabled1;

        public bool clgEnabled { get => clgEnabled1; set => SetProperty(ref clgEnabled1, value); }

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





        #endregion

    }
}
