using Flurl.Http;
using ShikkhanobishTeacherApp.Model;
using ShikkhanobishTeacherApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShikkhanobishTeacherApp.View_Model
{
    public class LoginViewModel: BaseViewMode, INotifyPropertyChanged
    {

        private void PerformRegistercmd()
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new TeacherRegistration());
        }
        private void PerformforgotPass()
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new ForgetPasswordView());
        }

        #region Binding
        private Command registercmd;

        public ICommand Registercmd
        {
            get
            {
                if (registercmd == null)
                {
                    registercmd = new Command(PerformRegistercmd);
                }

                return registercmd;
            }
        }

        private bool isChecked1;

        public bool isChecked { get => isChecked1; set => SetProperty(ref isChecked1, value); }

        private Command forgotPass1;

        public ICommand forgotPass
        {
            get
            {
                if (forgotPass1 == null)
                {
                    forgotPass1 = new Command(PerformforgotPass);
                }

                return forgotPass1;
            }
        }

       
        #endregion




    }
}
