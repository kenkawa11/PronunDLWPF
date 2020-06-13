using System.Text.RegularExpressions;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace PronunDLWPF
{
    public class baseDic
    {
        public static HttpClient client = new HttpClient();
        public string url { get; set; }
        public string ptn { get; set; }

        public virtual string DownLoadMp3(string w, string outpath)
        {
            var bodyUrl = url + w;
            var url_mp3 = get_body(bodyUrl, ptn).Result;
            if (url_mp3 == "0")
            {
                return "0";
            }
            get_mp3(url_mp3, outpath);
            return "1";
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
        public static async void get_mp3(string url_mp3, string outpath)
        {
            HttpResponseMessage res = await client.GetAsync(url_mp3);
            var outputPath = outpath + ".mp3";
            using (var fileStream = File.Create(outputPath))
            {
                using (var httpStream = await res.Content.ReadAsStreamAsync())
                {
                    httpStream.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
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
    }


    public class ox : baseDic
    {
        public ox()
        {
            url = "https://www.oxfordlearnersdictionaries.com/definition/english/";
            ptn = "https://www.oxfordlearnersdictionaries.com/media/english/us_pron/.+?mp3";
        }
    }

    public class ldo : baseDic
    {
        public ldo()
        {
            url = "https://www.ldoceonline.com/jp/dictionary/";
            ptn = "https://.+/ameProns/.+?mp3";
        }
    }

    public class webl : baseDic
    {
        public webl()
        {
            url = "https://ejje.weblio.jp/content/";
            ptn = "https://weblio.hs.llnwd.net/.+?mp3\"";
        }
        public override string DownLoadMp3(string w, string outpath)
        {
            var bodyUrl = url + w;
            var url_mp3 = get_body(bodyUrl, ptn).Result;
            if (url_mp3 == "0")
            {
                return "0";
            }
            url_mp3 = url_mp3.Substring(0, url_mp3.Length - 1);
            get_mp3(url_mp3, outpath);
            return "1";
        }
    }

    public class eiji : baseDic
    {
        public eiji()
        {
            url = "https://eow.alc.co.jp/";
            ptn = "【発音】.+?【カナ】|【発音.+?】.+?【カナ】";
        }
    }
}
