using System.Windows;

namespace PronunDLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public static HttpClient client = new HttpClient();
        public processViewModel vm=new processViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
            vm.Dir= @"C:\Users\naobaby\Desktop\test\";
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
