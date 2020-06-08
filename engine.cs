using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Dynamic;
using System.Windows.Shapes;

namespace PronunEngine
{
    public class dic
    {

        public static string outbase = @"C:\Users\naobaby\Desktop\test\";
        public static HttpClient client = new HttpClient();
        public string url { get; set; }
        public string ptn { get; set; }

        public dic(string a, string b)
        {
            url = a;
            ptn = b;
        }

        public static async void get_mp3(string url_mp3, string outpath)
        {
            HttpResponseMessage res = await client.GetAsync(url_mp3);
            var outputPath = outpath;
            using (var fileStream = File.Create(outputPath))
            {
                using (var httpStream = await res.Content.ReadAsStreamAsync())
                {
                    httpStream.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
        }



        public string DownLoadMp3(string w, string outpath)
        {
            var bodyUrl = url + w;
            var url_mp3 = get_body(bodyUrl, ptn).Result;
            if (url_mp3 == "0")
            {
                return "0";
            }

            if (url == "https://ejje.weblio.jp/content/")
            {
                url_mp3 = url_mp3.Substring(0, url_mp3.Length - 1);
            }
            get_mp3(url_mp3, outpath);
            return "1";
        }

        public string DownLoadSymbol(string w)
        {
            var bodyUrl = url + w;
            var url_symbol = get_body(bodyUrl, ptn).Result;
            if (url_symbol == "0")
            {
                return "0";
            }


            var pos = url_symbol.IndexOf("】");

            return url_symbol.Substring(pos + 1, url_symbol.Length - pos - 5);
        }

        public static async Task<string> get_body(string url, string ptn)
        {
            try
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                var html = await client.GetStringAsync(url).ConfigureAwait(false);
                var reg = new Regex(ptn);
                var m = reg.Match(html);
                if (m.Success)
                {
                    return m.Groups[0].Value;
                }
                return "0";
            }
            catch (HttpRequestException e)
            {
                return "0";
            }
        }
    }

    public class ID_data
    {

        private List<string> line;
        public List<string> Line
        {
            get
            {
                return this.line;
            }
            set
            {
                this.line = value;
            }
        }
        
        
        public static dic oxford = new dic("https://www.oxfordlearnersdictionaries.com/definition/english/",
            "https://www.oxfordlearnersdictionaries.com/media/english/us_pron/.+?mp3");

        public static dic longman = new dic("https://www.ldoceonline.com/jp/dictionary/",
            "https://.+/ameProns/.+?mp3");

        public static dic weblio = new dic("https://ejje.weblio.jp/content/","https://weblio.hs.llnwd.net/.+?mp3\"");

        public static dic ejiro = new dic("https://eow.alc.co.jp/", "【発音】.+?【カナ】|【発音.+?】.+?【カナ】");



        public string treatMp3(string dir)
        {
            if(line[3]=="n")
            {
                var target_word = line[2];
                target_word = target_word.Trim().Replace(" ", "+");
                //var outpath = dir + line[0]+".mp3";
                var outpath = dir + target_word+".mp3";
                if (oxford.DownLoadMp3(target_word, outpath) != "0")
                {
                    return target_word + ".mp3";
                }
                if (longman.DownLoadMp3(target_word, outpath) != "0")
                {
                    return target_word + ".mp3";
                }
                if (weblio.DownLoadMp3(target_word, outpath) != "0")
                {
                    return target_word + ".mp3";
                }
                return "n";
            }
            return line[3];

        }

        public string treatSym()
        {
            if (line[4] == "n")
            {
                var target_word = line[2];
                target_word = target_word.Trim().Replace(" ", "+");
                target_word = "search?q=" + target_word;
                var temp = ejiro.DownLoadSymbol(target_word);
                if (temp == "0")
                {
                    return  "n";

                }
                else
                {
                    return temp;
                }
            }
            return line[4];

        }


    }


    public class fileData
    {
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
    }
}
