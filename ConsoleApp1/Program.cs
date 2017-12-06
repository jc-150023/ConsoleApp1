using Codeplex.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web;
using static ConsoleApp1.ServiceResult;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://app.rakuten.co.jp/services/api/BooksBook/Search/20170404?format=json&isbn=|&booksGenreId=001&applicationId=1088154130845539159";

            //ISBNコード入力
            var isbncode = Console.ReadLine();

            //URLにISBNコードを挿入
            url= url.Replace("|", isbncode);

            // URLエンコーディング 
            isbncode = HttpUtility.UrlEncode(string.Join(" ", url));
      
            //URLの出力
            Console.WriteLine("url:" + url);
            Console.ReadLine();


            // HTTPアクセス 
            var req = WebRequest.Create(url);
            req.Headers.Add("Accept-Language:ja");
            var res = req.GetResponse();



            // レスポンス(JSON)をオブジェクトに変換 
            Stream s = res.GetResponseStream();
            StreamReader sr = new StreamReader(s);
            string str = sr.ReadToEnd();


            var info = DynamicJson.Parse(str);
    
            /*できなかった
             
            ServiceResult info;
             using (res)
            {
                using (var resStream = res.GetResponseStream())
                {
                    var serializer = new DataContractJsonSerializer(typeof(ServiceResult));
                    info = (ServiceResult)serializer.ReadObject(resStream);
                }
            }
            
            */

            // 結果を出力 
            Console.WriteLine("count:" + info.count);
            Console.WriteLine("page:" + info.page);
            Console.WriteLine("first:" + info.first);
            Console.WriteLine("last:" + info.last);
            Console.WriteLine("hits:" + info.hits);
            Console.WriteLine("carrier:" + info.carrier);
            Console.WriteLine("pageCount;" + info.pageCount);
        
            foreach (var r in info.Items)
            Console.WriteLine("title: {0}\ntitleKana: {1}", r.Item.title, r.Item.titleKana);
            Console.ReadLine(); 
        }
    }

    [DataContract]
    public class ServiceResult
    {
        [DataMember]
        public int count { get; set; }
        [DataMember]
        public int page { get; set; }
        [DataMember]
        public string first { get; set; }
        [DataMember]
        public int last { get; set; }
        [DataMember]
        public int hits { get; set; }
        [DataMember]
        public int carrier { get; set; }
        [DataMember]
        public int pageCount { get; set; }

        [DataMember]
        public List<Item> Items { get; set; }

        [DataContract]
        public class Item
        {
            [DataMember]
            public string title { get; set; }
            [DataMember]
            public string titleKana { get; set; }
            [DataMember]
            public string subTitle { get; set;}
            [DataMember]
            public string subTitleKana { get; set; }
            [DataMember]
            public string seriesName { get; set; }
            [DataMember]
            public string seriesNameKana { get; set; }
            [DataMember]
            public string contents { get; set; }
            [DataMember]
            public string author { get; set; }
            [DataMember]
            public string authorKana { get; set; }
            [DataMember]
            public string publisherName { get; set; }
            [DataMember]
            public int size { get; set; }
            [DataMember]
            public string isbn { get; set; }
            [DataMember]
            public string itemCaption { get; set; }
            [DataMember]
            public string salesDate { get; set; }
            [DataMember]
            public int itemPrice { get; set; }
            [DataMember]
            public int listPrice { get; set; }
            [DataMember]
            public float discountRate { get; set; }
            [DataMember]
            public int discountPrice { get; set; }
            [DataMember]
            public string itemUrl { get; set; }
            [DataMember]
            public string affiliateUrl { get; set; }
            [DataMember]
            public string smallImageUrl { get; set; }
            [DataMember]
            public string mediumImageUrl { get; set; }
            [DataMember]
            public string largeImageUrl { get; set; }
            [DataMember]
            public string chirayomiUrl { get; set; }
            [DataMember]
            public int availability { get; set; }
            [DataMember]
            public int postageFlag { get; set; }
            [DataMember]
            public int limitedFlag { get; set; }
            [DataMember]
            public float reviewcount { get; set; }
            [DataMember]
            public float reviewAverage { get; set; }
            [DataMember]
            public string booksGenreId { get; set; } 

        }
    }
}