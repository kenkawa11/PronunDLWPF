using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MSAPI = Microsoft.WindowsAPICodePack;
using System.Windows;


namespace PronunDLWPF
{

    public class processViewModel : INotifyPropertyChanged
    {
        private string status;
        private string progress;
        private int barProgress;
        private string fn;
        private string dir;
        private Boolean isCancel;
        private Boolean isActiveDone;

        public processViewModel()
        {
            isCancel = false;
            isActiveDone = true;
        }



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


        public Boolean IsCancel
        {
            get
            {
                return this.isCancel;
            }
            set
            {
                this.isCancel = value;
                NotifyPropertyChanged("IsCancel");
            }
        }


        public Boolean IsActiveDone
        {
            get
            {
                return this.isActiveDone;
            }
            set
            {
                this.isActiveDone = value;
                NotifyPropertyChanged("IsActiveDone");
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
        private DelegateCommand _readprocess;
        private DelegateCommand _cancel;
        private DelegateCommand _btnFile;
        private DelegateCommand _btnDir;
        public DelegateCommand Readprocess
        {
            get
            {
                if (this._readprocess == null)
                {
                    this._readprocess = new DelegateCommand(_ =>
                    {
                        if(fn!=null)
                        {
                            this.IsActiveDone = false;
                            fntreat();
                        }
                        else
                        {
                            MessageBox.Show("Select a target file !!!");
                        }
                    });
                }
                return this._readprocess;
            }
        }


        public DelegateCommand Cancel
        {
            get
            {
                if (this._cancel == null)
                {
                    this._cancel = new DelegateCommand(_ =>
                    {
                        isCancel = true;
                    });
                }
                return this._cancel;
            }
        }

        public DelegateCommand BtnFile
        {
            get
            {
                if (this._btnFile == null)
                {
                    this._btnFile = new DelegateCommand(_ =>
                    {
                        selectFile();
                    });
                }
                return this._btnFile;
            }
        }


        public DelegateCommand BtnDir
        {
            get
            {
                if (this._btnDir == null)
                {
                    this._btnDir = new DelegateCommand(_ =>
                    {
                        selectDir();
                    });
                }
                return this._btnDir;
            }
        }

        private void selectFile()
        {
            var fd = new OpenFileDialog();

            fd.FileName = "";
            fd.DefaultExt = "*.*";
            if (fd.ShowDialog() == true)
            {
                Fn = fd.FileName;
            }
        }

        private void selectDir()
        {
            var dlg = new MSAPI::Dialogs.CommonOpenFileDialog();

            // フォルダ選択ダイアログ（falseにするとファイル選択ダイアログ）
            dlg.IsFolderPicker = true;
            // タイトル
            dlg.Title = "フォルダを選択してください";
            // 初期ディレクトリ
            dlg.InitialDirectory = @"C:\Work";


            if (dlg.ShowDialog() == MSAPI::Dialogs.CommonFileDialogResult.Ok)
            {
                Dir = dlg.FileName + @"\";
                //MessageBox.Show($"{dlg.FileName}が選択されました。");
            }
        }


        private async void fntreat()
        {
            this.Status = "Processing";
            int num_treat;
            var LoadFileData = new fileData(fn);
            var num_gross = LoadFileData.Fdata.Count;

            var t1 = LoadFileData.treatData(Dir);

            while (!t1.IsCompleted)
            {
                if (isCancel)
                {
                    break;
                }
                num_treat = LoadFileData.Progress;
                BarProgress = num_treat * 100 / num_gross;
                Progress = $"{BarProgress}%";
                await Task.Delay(100);
            }
            Status = "Writing data in file";
            LoadFileData.writeData();

            if (isCancel)
            {
                Status = "Canceled";
            }
            else
            {
                Status = "Complete";
            }
            this.isCancel = false;
            this.IsActiveDone = true;
        }
    }
    public class DelegateCommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;
        public DelegateCommand(Action<object> execute)
        : this(execute, null)
        {
        }

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return (this._canExecute != null) ? this._canExecute(parameter) : true;
        }

        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            var h = this.CanExecuteChanged;
            if (h != null) h(this, EventArgs.Empty);
        }
        public void Execute(object parameter)
        {
            if (this._execute != null)
                this._execute(parameter);
        }
    }

}