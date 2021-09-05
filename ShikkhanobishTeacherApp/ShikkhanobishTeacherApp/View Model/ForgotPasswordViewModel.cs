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
    public class ForgotPasswordViewModel: BaseViewMode, INotifyPropertyChanged
    {
        int clickCounter;
        int OTPCode;
        int thisTeacherID;
        public ForgotPasswordViewModel()
        {
            msgText = "Enter your account phone number";
            btnText = "Send OTP";
            clickCounter = 0;
            pVsibility2 = false;
            placeHolder1 = "Phone Number";
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
        private async Task btn()
        {
            if(clickCounter == 0)
            {
                
                if (!p1HasError)
                {
                    SendSms();
                    msgText = "Enter OTP";
                    pText1 = "";
                    placeHolder1 = "OTP";
                    btnText = "Check OTP";
                    clickCounter++;
                }
                
            }
            else if(clickCounter == 1)
            {
                if(int.Parse(pText1) == OTPCode)
                {
                    pText1 = "";
                    pVsibility2 = true;
                    placeHolder1 = "Enter New Password";
                    placeHolder2 = "Confirm Password";
                    btnText = "Change Password";
                    p1HasError = false;
                    p1Error = "";
                    clickCounter++;
                }
                else
                {
                    p1HasError = true;
                    p1Error = "Otp Doesn't Match";
                }      
            }
            else if(clickCounter == 2)
            {
                if(pText1 == pText2)
                {
                    using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Updating Password"))
                    {
                        Response regRes = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/changeTeacherInf0"
                .PostUrlEncodedAsync(new
                {
                    info = pText1,
                    index = 2,
                    teacherID = thisTeacherID
                })
                .ReceiveJson<Response>();
                        p2Error = "";
                        p2HAsError = false;
                        dialog.MessageText = "Passwaord is updated successfully!";
                        await Task.Delay(2000);
                        Application.Current.MainPage.Navigation.PopModalAsync();

                    }
                    
                    
                }
                else
                {
                    p2HAsError = true;
                    p2Error = "Password Doesn't Match";

                }
               
            }
        }
        public async Task checkPnumber()
        {
            if (!IsInternetConnectionAvailable())
            {
                return;
            }
            if (pText1 != null && pText1 != "")
            {              
                if(pText1.Length == 11)
                {
                    var chkPn = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/checkRegphonenumber".PostUrlEncodedAsync(new { phonenumber = pText1 })
  .ReceiveJson<Teacher>();
                    if (chkPn.teacherID == 0)
                    {
                        btnEnabled = false;
                        p1HasError = true;
                        p1Error = "Phone Number doesn't exist";

                    }
                    else
                    {
                        btnEnabled = true;
                        thisTeacherID = chkPn.teacherID;
                        p1HasError = false;
                        p1Error = "";
                    }
                }
                else
                {
                    btnEnabled = false;
                    p1HasError = false;
                    p1Error = "";
                }
                
            }
        }
        public async Task SendSms()
        {
            if (!IsInternetConnectionAvailable())
            {
                return;
            }
            Random rnd = new Random();
            int rn = rnd.Next(1000, 9999);
            string MSG = "Your Shikkhanobish Reset Password OTP is: " + rn;
            string pn = pText1;
            OTPCode = rn;
            var res = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/SendSmsAsync".PostUrlEncodedAsync(new { msg = MSG, number = pn }).ReceiveJson<SendSms>();
        }
        private void PerformbackBtn()
        {
            Application.Current.MainPage.Navigation.PopModalAsync();
        }
        #region Bindings
        private string msgText1;

        public string msgText { get => msgText1; set => SetProperty(ref msgText1, value); }

        private string placeHolder11;

        public string placeHolder1 { get => placeHolder11; set => SetProperty(ref placeHolder11, value); }

        private string placeHolder21;

        public string placeHolder2 { get => placeHolder21; set => SetProperty(ref placeHolder21, value); }

        private string pText11;

        public string pText1 { get { return pText11; } set { pText11 = value; checkPnumber(); SetProperty(ref pText11, value); } }

        private string pText21;

        public string pText2 { get => pText21; set => SetProperty(ref pText21, value); }

        private Command btnCommand1;

        public ICommand btnCommand
        {
            get
            {
                if (btnCommand1 == null)
                {
                    btnCommand1 = new Command(async => btn());
                }

                return btnCommand1;
            }
        }

        

        private string btnText1;

        public string btnText { get => btnText1; set => SetProperty(ref btnText1, value); }

        private bool pVsibility21;

        public bool pVsibility2 { get => pVsibility21; set => SetProperty(ref pVsibility21, value); }

        private Command backBtn1;

        public ICommand backBtn
        {
            get
            {
                if (backBtn1 == null)
                {
                    backBtn1 = new Command(PerformbackBtn);
                }

                return backBtn1;
            }
        }

        private string p1Error1;

        public string p1Error { get => p1Error1; set => SetProperty(ref p1Error1, value); }

        private string p2Error1;

        public string p2Error { get => p2Error1; set => SetProperty(ref p2Error1, value); }

        private bool p1HasError1;

        public bool p1HasError { get => p1HasError1; set => SetProperty(ref p1HasError1, value); }

        private bool p2HAsError1;

        public bool p2HAsError { get => p2HAsError1; set => SetProperty(ref p2HAsError1, value); }

        private bool btnEnabled1;

        public bool btnEnabled { get => btnEnabled1; set => SetProperty(ref btnEnabled1, value); }


        #endregion
    }
}
