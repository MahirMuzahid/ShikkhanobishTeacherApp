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
       
        #endregion




    }
}
