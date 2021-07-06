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
        #region Methods
        public RegisterViewModel ()
        {
            scSelectCount = new List<int>();
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

        private void PerformpopupCollege( string  index)
        {
            CollegePopupVisibility = true;
            if (int.Parse(index) == 1)
            {
                clgScColor = Color.FromHex("#42ED88");
                clgChColor = Color.FromHex("#10000000");
                clgArColor = Color.FromHex("#10000000");
            }
            else if (int.Parse(index) == 2)
            {
                clgScColor = Color.FromHex("#10000000");
                clgChColor = Color.FromHex("#42ED88");
                clgArColor = Color.FromHex("#10000000");
            }
            else if (int.Parse(index) == 3)
            {
                clgScColor = Color.FromHex("#10000000");
                clgChColor = Color.FromHex("#10000000");
                clgArColor = Color.FromHex("#42ED88");
            }

        }
        private void PerformpopupSchool(string index)
        {
            schholPopUpVisibility = true;
            if (int.Parse(index) == 1)
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
            else if (int.Parse(index) == 2)
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
            else if (int.Parse(index) == 3)
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
        }
        private void PerformpopoutSchool()
        {
            schholPopUpVisibility = false;
        }
        private void PerformpopoutCollege()
        {
            CollegePopupVisibility = false;
        }


        private void PerformschSaved()
        {
            schholPopUpVisibility = false;
            noSubScMsgVsi = false;

            List<string> subName = new List<string>();
           //Had to add
        }
        private void PerformclgSaved()
        {
            CollegePopupVisibility = false;
            noSubMsgVsi = false;
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
                if (int.Parse(subIndex) == 1)
                {
                    if(scPhyColor == Color.FromHex("#42ED88"))
                    {
                        scPhyColor = Color.White;
                        scSelectCount.Remove(int.Parse(subIndex));
                    }
                    else
                    {
                        scPhyColor = Color.FromHex("#42ED88");
                        scSelectCount.Add(int.Parse(subIndex));
                    }
                   
                }
                if (int.Parse(subIndex) == 2)
                {
                    if(scCheColor == Color.FromHex("#42ED88"))
                    {
                        scCheColor = Color.White;
                        scSelectCount.Remove(int.Parse(subIndex));
                    }
                    else
                    {
                        scCheColor = Color.FromHex("#42ED88");
                        scSelectCount.Add(int.Parse(subIndex));
                    }
                }
                if (int.Parse(subIndex) == 3)
                {
                    if(scBioColor == Color.FromHex("#42ED88"))
                    {
                        scBioColor = Color.White;
                        scSelectCount.Remove(int.Parse(subIndex));
                    }
                    else
                    {
                        scBioColor = Color.FromHex("#42ED88");
                        scSelectCount.Add(int.Parse(subIndex));
                    }
                }
                if (int.Parse(subIndex) == 4)
                {
                    if (scMatColor == Color.FromHex("#42ED88"))
                    {
                        scMatColor = Color.White;
                        scSelectCount.Remove(int.Parse(subIndex));
                    }
                    else
                    {
                        scMatColor = Color.FromHex("#42ED88");
                        scSelectCount.Add(int.Parse(subIndex));
                    }
                }
                if (int.Parse(subIndex) == 5)
                {
                    if (scHmColor == Color.FromHex("#42ED88"))
                    {
                        scHmColor = Color.White;
                        scSelectCount.Remove(int.Parse(subIndex));
                    }
                    else
                    {
                        scHmColor = Color.FromHex("#42ED88");
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



        #endregion

    }
}
