
//改进书上例子9-10的爬虫程序。
// (1)优选爬取起始网站上的网页 
// (2)只有当爬取的是html文本时，才解析并爬取下一级URL。
// (3)相对地址转成绝对地址进行爬取。
// (4)尝试使用Winform来配置初始URL，启动爬虫，显示已经爬取的URL和错误的URL信息。
//将上次作业的爬虫程序，使用并行处理进行优化，实现更快速的网页爬取。



using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace work09
{
    class SimpleCrawler
    {
        private static object locker = new object();//作为锁
        private Queue<string> urls = new Queue<string>();
        private int count = 0;
        public int Num = 5;
        private string html;
        private string current;

        public SimpleCrawler(string url)
        {
            if (url.Length > 0)
            {
                html = "";
                urls.Enqueue(url);
            }
        }

        static void Main(string[] args)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                SimpleCrawler crawler = new SimpleCrawler("http://www.cnblogs.com/dstang2000/");//初始页面
                crawler.Crawl();
                stopwatch.Stop();
                TimeSpan timeSpan = stopwatch.Elapsed;
                Console.WriteLine(timeSpan.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("无法解析");
            }

        }

        private void Crawl()
        {
            Console.WriteLine("开始爬行了.... ");
            while (true)
            {
                if (urls.Count == 0)
                {
                    break;
                }
                current = urls.Dequeue();
                html = current;
                if (current == null || count > 50)
                {
                    break;
                }
                Console.WriteLine("爬行" + current + "页面!");
                DownLoad(current);
                Parse();//解析并加入新的链接
                Console.WriteLine("爬行结束");
            }
        }

        public void DownLoad(string url)
        {
            try
            {
                lock (locker)
                {
                    WebClient webClient = new WebClient();
                    webClient.Encoding = Encoding.UTF8;
                    html = webClient.DownloadString(url);

                    string fileName = count.ToString();
                    File.WriteAllText(fileName + ".html", html, Encoding.UTF8);

                    count++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                urls.Dequeue();
            }
        }

        private void Parse()
        {

            lock (locker)
            {
                string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+[^""'#>]+(.html|.HTML|.aspx)[^""'#>]*[""']";
                MatchCollection matches = new Regex(strRef).Matches(html);
                foreach (Match match in matches)
                {
                    strRef = match.Value.Substring(match.Value.IndexOf('=') + 1).Trim('"', '\\', '#', ' ', '>');
                    if (strRef.Length == 0)
                    {
                        continue;
                    }
                    if (strRef.StartsWith("/"))
                    {
                        strRef = "https://www.cnblogs.com" + strRef;
                    }
                    Console.WriteLine(strRef);
                    if (urls.Count <= 1000)
                        urls.Enqueue(strRef);
                }
            }
            Thread[] downloadThread;//声名下载线程

            downloadThread = new Thread[21];//为线程申请资源，确定线程总数

            for (int i = 0; i < Num; i++)
            {
                if (urls.Count != 0)
                {
                    ThreadStart startDownload = new ThreadStart(() => DownLoad(urls.Dequeue()));

                    downloadThread[i] = new Thread(startDownload);//指定线程起始设置
                    downloadThread[i].Start();//逐个开启线程
                }
            }

        }
    }
}
