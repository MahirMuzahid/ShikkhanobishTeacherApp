using ShikkhanobishTeacherApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace ShikkhanobishTeacherApp.View_Model
{
    public class TuitionLogViewModel: BaseViewMode, INotifyPropertyChanged
    {
        int sec = 20;
        public TuitionLogViewModel()
        {
            List<int> a = new List<int>();
            a.Add(0);
            a.Add(0);
            a.Add(0);
            a.Add(0);
            a.Add(0);

            logList = a;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                refreshlbl = "Refreshing In : " + sec + " Seconds";
                sec--;
                if(sec== 0)
                {
                    sec = 20;
                }
                return true; 
            });
        }



        private Command qsCmd1;

        public ICommand qsCmd
        {
            get
            {
                if (qsCmd1 == null)
                {
                    qsCmd1 = new Command(async =>  PerformqsCmd());
                }

                return qsCmd1;
            }
        }

        private async Task PerformqsCmd()
        {
            await MaterialDialog.Instance.AlertAsync(message: "টিউশন লগ উইন্ডোতে আপনি লাইভ দেখতে পারবেন কোন সাবজেক্টের উপর টিউশন কল হচ্ছে! যা আপনার টিউশন পাওয়ার সম্ভবনা বাড়িয়ে দিবে কয়েকগুন।");
        }

        private string refreshCMD1;

        public string refreshCMD { get => refreshCMD1; set => SetProperty(ref refreshCMD1, value); }

        private string refreshlbl1;

        public string refreshlbl { get => refreshlbl1; set => SetProperty(ref refreshlbl1, value); }

        private List<int> logList1;

        public List<int> logList { get => logList1; set => SetProperty(ref logList1, value); }
    }
}
