using GetUdemyCourse.Models;
using GetUdemyCourse.Website;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetUdemyCourse
{
    class Program
    {
        static async Task Main()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration["ConnectionString"];

            List<string> udemyLinkList = new List<string>();

            //CourseMania courseMania = new CourseMania();
            //udemyLinkList.AddRange(await courseMania.CreateUdemyLinkList());

            YoFreeSample yoFreeSample = new YoFreeSample();
            udemyLinkList.AddRange(yoFreeSample.CreateUdemyLinkList());

            Udemy udemy = new Udemy();
            List<Course> courseList = await udemy.CreateCourseList(udemyLinkList);

            Data data = new Data(connectionString);
            data.Execute(courseList);
        }
    }
}
