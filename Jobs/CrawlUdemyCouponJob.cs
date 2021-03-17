using Jobs.Models;
using Jobs.Website;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobs
{
    [DisallowConcurrentExecution]
    public class CrawlUdemyCouponJob : IJob
    {
        private readonly ILogger<CrawlUdemyCouponJob> _logger;
        public CrawlUdemyCouponJob(ILogger<CrawlUdemyCouponJob> logger)
        {
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Start");
            await Run();
            await Task.CompletedTask;
            _logger.LogInformation("End");
        }

        public async Task Run()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration["ConnectionStrings:AppDb"];

            Data data = new Data(connectionString);
            data.DeleteOutdatedCourse();

            var task1 = RunCourseMania(connectionString);
            var task2 = RunYoFreeSample(connectionString);
            await task1;
            await task2;
        }

        public async Task RunCourseMania(string connectionString)
        {
            List<string> udemyLinkList = new List<string>();

            CourseMania courseMania = new CourseMania();
            udemyLinkList.AddRange(await courseMania.CreateUdemyLinkList());

            Udemy udemy = new Udemy();
            List<Course> courseList = await udemy.CreateCourseList(udemyLinkList);

            Data data = new Data(connectionString);
            data.InsertCourse(courseList);
        }

        public async Task RunYoFreeSample(string connectionString)
        {
            YoFreeSample yoFreeSample = new YoFreeSample();
            List<string> udemyLinkList = yoFreeSample.CreateUdemyLinkList();

            Udemy udemy = new Udemy();
            List<Course> courseList = await udemy.CreateCourseList(udemyLinkList);

            Data data = new Data(connectionString);
            data.InsertCourse(courseList);
        }
    }
}
