using GetUdemyCourse.Models;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GetUdemyCourse.Website
{
    public class Udemy
    {
        public async Task<List<Course>> CreateCourseList(List<string> udemyLinkList)
        {
            List<Course> courseList = new List<Course>();
            foreach(string udemyLink in udemyLinkList)
            {
                courseList.Add(await CreateCourse(udemyLink));
            }
            return courseList;
        }

        public async Task<Course> CreateCourse(string udemyLink)
        {
            Course course = new Course();

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(udemyLink);

            long courseId = GetCourseId(doc);
            if (courseId == 0)
            {
                return null;
            }

            course = await GetCourseInfo(courseId);

            return course;
        }

        private long GetCourseId(HtmlDocument doc)
        {
            HtmlNode body = doc.DocumentNode.SelectSingleNode("//body");
            string courseId = body?.Attributes["data-clp-course-id"]?.Value;
            bool check = long.TryParse(courseId, out long result);
            if (check)
            {
                return result;
            }
            return 0;
        }

        private async Task<Course> GetCourseInfo(long courseId)
        {
            Course course = new Course
            {
                CourseId = courseId
            };

            string url = $"https://www.udemy.com/api-2.0/courses/{courseId}/?fields[course]=title,description,headline,avg_rating_recent";
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                course.CourseName = 
            }
            return null;
        }
    }
}
