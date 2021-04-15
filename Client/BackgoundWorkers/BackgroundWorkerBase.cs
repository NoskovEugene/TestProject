using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BackgoundWorkers
{
    public class BackgroundWorkerBase : BindableBase
    {
        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public BackgroundWorker Worker { get; set; }


        public BackgroundWorkerBase(DoWorkEventHandler doWork, RunWorkerCompletedEventHandler? onComplete)
        {
            Worker = new BackgroundWorker();
            Worker.DoWork += doWork;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            if(onComplete!= null)
            {
                Worker.RunWorkerCompleted += onComplete;
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsBusy = false;
        }

        public void RunWorker()
        {
            Worker.RunWorkerAsync();
            IsBusy = true;
        }
    }
}
