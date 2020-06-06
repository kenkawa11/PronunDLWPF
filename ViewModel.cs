using System;
using System.ComponentModel;

namespace PronunEngine
{
    public class processViewModel : INotifyPropertyChanged
    {
        private string status;
        private string progress;
        private int barProgress;
        private string fn;
        private string dir;
        public string Fn
        {
            get
            {
                return this.fn;
            }
            set
            {
                this.fn = value;
                NotifyPropertyChanged("Fn");
            }
        }

        public string Dir
        {
            get
            {
                return this.dir;
            }
            set
            {
                this.dir = value;
                NotifyPropertyChanged("Dir");
            }
        }


        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
                NotifyPropertyChanged("Status");
            }
        }

        public string Progress
        {
            get
            {
                return this.progress;
            }
            set
            {
                this.progress = value;
                NotifyPropertyChanged("Progress");

            }
        }

        public int BarProgress
        {
            get
            {
                return this.barProgress;
            }
            set
            {
                this.barProgress = value;
                NotifyPropertyChanged("BarProgress");

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}