using Jobs.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Jobs.Website
{
    public class Udemy
    {
        public async Task<List<Course>> CreateCourseList(List<string> udemyLinkList)
        {
            List<Course> courseList = new List<Course>();
            foreach (string udemyLink in udemyLinkList)
            {
                Console.WriteLine(udemyLink);
                try
                {
                    Course course = await CreateCourse(udemyLink);
                    if (course != null)
                    {
                        courseList.Add(course);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return courseList;
        }

        public async Task<Course> CreateCourse(string udemyLink)
        {
            long courseId = GetCourseId(udemyLink);
            if (courseId == 0)
            {
                return null;
            }

            string couponCode = GetCouponCode(udemyLink);
            if (string.IsNullOrEmpty(couponCode))
            {
                return null;
            }

            Course course = await GetCourseInfo(courseId, couponCode);
            course.UdemyLink = udemyLink;

            return course;
        }

        private long GetCourseId(string udemyLink)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(udemyLink);

            HtmlNode body = doc.DocumentNode.SelectSingleNode("//body");
            string courseId = body?.Attributes["data-clp-course-id"]?.Value;
            bool check = long.TryParse(courseId, out long result);
            if (check)
            {
                return result;
            }
            return 0;
        }

        private string GetCouponCode(string udemyLink)
        {
            Uri myUri = new Uri(udemyLink);
            string couponCode = HttpUtility.ParseQueryString(myUri.Query).Get("couponCode");
            return couponCode;
        }

        private async Task<Course> GetCourseInfo(long courseId, string couponCode)
        {
            Course course = new Course
            {
                CourseId = courseId
            };

            string url = $"https://www.udemy.com/api-2.0/course-landing-components/{courseId}/me/?couponCode={couponCode}&components=purchase";
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                PurchaseInfo purchase = JsonSerializer.Deserialize<PurchaseInfo>(json);
                if (purchase != null)
                {
                    PurchaseData purchaseData = purchase.purchase?.data;
                    if (purchaseData != null)
                    {
                        try
                        {
                            course.EndTime = DateTime.ParseExact(purchaseData.pricing_result.campaign.end_time.Substring(0, 16), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                        }
                        catch (Exception) { }
                        course.OriginalPrice = purchaseData.pricing_result.list_price.price_string;
                        course.DiscountedPrice = purchaseData.pricing_result.price.price_string;
                        course.Originalmount = purchaseData.pricing_result.list_price.amount;
                        course.Amount = purchaseData.pricing_result.price.amount;
                        course.Discount_percent = purchaseData.pricing_result.discount_percent;

                        if (course.OriginalPrice.Equals(course.DiscountedPrice))
                        {
                            return null;
                        }

                        if (course.Discount_percent == 0)
                        {
                            course.Discount_percent = (course.Originalmount - course.Amount) / course.Originalmount * 100;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

            url = $"https://www.udemy.com/api-2.0/courses/{courseId}/?fields[course]=title,description,headline,avg_rating_recent,objectives_summary,target_audiences,image_200_H,primary_category,prerequisites";
            response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                CourseInfo courseInfo = JsonSerializer.Deserialize<CourseInfo>(json);
                if (courseInfo != null)
                {
                    course.Title = courseInfo.title;
                    course.Headline = courseInfo.headline;
                    course.Description = courseInfo.description;
                    course.Avg_rating_recent = courseInfo.avg_rating_recent;
                    course.Objectives_summary = courseInfo.objectives_summary;
                    course.Target_audiences = courseInfo.target_audiences;
                    course.Image_200_H = courseInfo.image_200_H;
                    course.Category = courseInfo.primary_category.title;
                    course.Prerequisites = courseInfo.prerequisites;
                }
                else
                {
                    return null;
                }
            }

            return course;
        }
    }
}
