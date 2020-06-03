using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Runtime.CompilerServices;
//using PronunDLengine;
using System.Diagnostics;
using System.Security.Policy;
using PronunEngine;

namespace PronunDLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string fn;
        //public static HttpClient client = new HttpClient();
        Boolean isCancel;
        public processViewModel vm=new processViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fd = new OpenFileDialog();

            fd.FileName = "";
            fd.DefaultExt = "*.*";
            if (fd.ShowDialog() == true)
            {
                vm.Fn = fd.FileName;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Engine.fntreat(fn);
            doneBtn.IsEnabled = false;
            fntreat(vm.Fn);

        }

        public class processViewModel : INotifyPropertyChanged
        {
            private string status;
            private string progress;
            private int barProgress;
            private string fn;

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



        public async void fntreat(string rfn)
        {
            //label1.Content = "Processing";
            vm.Status = "Processing";
            var LoadFileData = new fileData(rfn);
            var num_treat = 0;
            var num_gross = LoadFileData.Fdata.Count;
            var linedata = new ID_data();
            foreach (var values in LoadFileData.Fdata)
            {
                linedata.Line = values;
                values[3] = linedata.treatMp3();

                values[4] = linedata.treatSym();

                num_treat = num_treat + 1;
                vm.BarProgress = num_treat*100 / num_gross ;
                vm.Progress = $"{num_treat} in {num_gross}";


                if (isCancel)
                {
                    break;
                }

                if (num_treat < num_gross)
                {
                    await Task.Delay(1000);
                }
            }

            vm.Status = "Writing data in file";
            LoadFileData.writeData();
            vm.Status= "Complete";
            doneBtn.IsEnabled = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            isCancel = true;
        }

        private void Window_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy; // マウスカーソルをコピーにする。
            e.Handled = e.Data.GetDataPresent(DataFormats.FileDrop);
        }



        private void Window_Drop(object sender, DragEventArgs e)
        {
            vm.Fn = string.Empty; // テキストボックスを空にする。
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop); // ドロップされたものがFileDrop形式の場合は、各ファイルのパス文字列を文字列配列に格納する。
            if (files != null)
            {
                vm.Fn = files[0];// パス文字列からファイル名を抜き出して、テキストボックスにファイル名を書き込む。
            }
        }
    }




}
