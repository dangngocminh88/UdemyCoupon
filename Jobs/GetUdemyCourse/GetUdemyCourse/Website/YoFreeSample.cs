using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GetUdemyCourse.Website
{
    public class YoFreeSample
    {
        public List<string> CreateUdemyLinkList()
        {
            const string url = "https://yofreesamples.com/courses/free-discounted-udemy-courses-list/";
            List<string> udemyLinkList = new List<string>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            HtmlNodeCollection linkList = doc?.DocumentNode?.SelectNodes("//a[@class='btn btn-md btn-success']");
            if (linkList != null)
            {
                foreach (HtmlNode link in linkList)
                {
                    udemyLinkList.Add(link.Attributes["href"].Value);
                }
            }
            return udemyLinkList;
        }
    }
}
