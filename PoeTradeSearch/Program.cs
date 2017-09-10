using System;
using System.IO;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace PoeTradeSearch
{
    class Program
    {
        static void Main()
        {
            var request = new PoeTradeRequest {Name = "Tabula Rasa", League = "Harbinger"};

            var webRequest = (HttpWebRequest)WebRequest.Create("http://poe.trade/search");
            
            var payloadString = request.ToPayload();
            var data = Encoding.ASCII.GetBytes(payloadString);

            webRequest.Host = "poe.trade";
            webRequest.ContentLength = payloadString.Length;
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            webRequest.Referer = "http://poe.trade/";
            webRequest.Headers["Cache-Control"] = "max-age=0";
            webRequest.Headers["Origin"] = "http://poe.trade";
            webRequest.Headers["Upgrade-Insecure-Requests"] = "1";
            webRequest.Method = "POST";
            
            using (var stream = webRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)webRequest.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responseString);

            var nodes = doc.DocumentNode.SelectNodes("//tbody[contains(@id, 'item-container')]");
            foreach (var node in nodes)
            {
                Console.WriteLine($"Name: {node.Attributes["data-name"].Value}");
                Console.WriteLine($"Buyout: {node.Attributes["data-buyout"].Value}");
                Console.WriteLine($"IGN: {node.Attributes["data-ign"].Value}");
                Console.WriteLine();
            }
        }
    }
}
