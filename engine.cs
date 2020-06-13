using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace PronunDLWPF
{
    public class fileData
    {
        /*
        public static ox oxford = new ox();
        public static ldo longman = new ldo();
        public static webl weblio = new webl();
        public static eiji eijiro = new eiji();
        */



        private List<string> line;
        public int Progress { get; set; }
        private List<List<string>> fdata = new List<List<string>>();
        public string Rfn { get; set; }
        private int dataNum { get; set; }
        public List<List<string>> Fdata
        {
            get
            {
                return this.fdata;
            }
            set
            {
                this.fdata = value;
            }
        }

        public fileData(string rfn)
        {
            Rfn = rfn;
            string line;
            String[] readLine;

            using (StreamReader sr = new StreamReader(rfn))
            {

                while (!sr.EndOfStream) //csvファイルをリストに読み込む
                {
                    // CSVファイルの一行を読み込む
                    List<string> stringList = new List<string>();
                    line = sr.ReadLine();
                    // 読み込んだ一行をカンマ毎に分けて配列に格納する
                    readLine = line.Split(',');
                    stringList.AddRange(readLine);
                    Fdata.Add(stringList);
                    readLine = null;
                }
            }

            dataNum = Fdata.Count;
        }

        public void writeData()
        {
            string line = null;
            using (StreamWriter file = new StreamWriter(this.Rfn, false, Encoding.UTF8))
            {
                foreach (var v in this.Fdata)
                {
                    foreach (var u in v)
                    {
                        line += u;
                        line += ",";
                    }
                    line = line.Substring(0, line.Length - 1);
                    file.WriteLine(line);
                    line = null;
                }
            }
        }

        public async Task<bool> treatData(string Dir)
        {
            Progress = 0;
            foreach (var values in Fdata)
            {
                line= values;
                treatMp3(Dir);
                treatSym();
                Progress++;
                await Task.Delay(1000);
            }
            return true;

        }
        public void treatMp3(string dir)
        {
            var target_word = line[2];
            target_word = target_word.Trim().Replace(" ", "+");
            line[0] = "TRKW-" + target_word;
            if (line[3] == "n")
            {
                var outpath = dir + line[0];
                foreach (var dic in dict_all.allmp3)
                {
                    if (dic.DownLoadMp3(target_word, outpath) != "0")
                    {
                        line[3] = "A";
                        return;
                    }
                }
            }
            return;
        }

        public void treatSym()
        {
            if (line[4] == "n")
            {
                var target_word = line[2];
                target_word = target_word.Trim().Replace(" ", "+");
                target_word = "search?q=" + target_word;
                var temp = dict_all.eijiro.DownLoadSymbol(target_word);
                if (temp == "0")
                {
                    return;

                }
                else
                {
                    line[4] = temp;
                }
            }
            return;

        }
    }
}
