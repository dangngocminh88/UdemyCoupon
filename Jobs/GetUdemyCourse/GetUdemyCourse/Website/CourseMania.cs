using GetUdemyCourse.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GetUdemyCourse.Website
{
    public class CourseMania
    {
        public async Task<List<string>> CreateUdemyLinkList()
        {
            List<string> udemyLinkList = new List<string>();
            string url = "https://api.coursemania.xyz/api/get_courses";
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<CourseManiaObject> courseList = JsonSerializer.Deserialize<List<CourseManiaObject>>(json);
                foreach (CourseManiaObject course in courseList)
                {
                    string udemyLink = course.url;
                    if (!string.IsNullOrEmpty(udemyLink) && udemyLink.Contains("udemy.com"))
                    {
                        udemyLinkList.Add(udemyLink);
                    }
                }
            }
            return udemyLinkList;
        }
    }
}
