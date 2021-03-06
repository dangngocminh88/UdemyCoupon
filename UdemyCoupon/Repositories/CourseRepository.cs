using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using UdemyCoupon.Models;

namespace UdemyCoupon.Repositories
{
    public interface ICourseRepository
    {
        List<Course> GetCourse(int page, int pageSize);
        CourseList CourseList(int page, int pageSize);
    }
    public class CourseRepository : RepositoryBase, ICourseRepository
    {
        public CourseRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public List<Course> GetCourse(int page, int pageSize)
        {
            int skipRow = (page - 1) * pageSize;
            return this.RepositoryContext.Courses.AsNoTracking().Skip(skipRow).Take(pageSize).ToList();
        }

        public CourseList CourseList(int page, int pageSize)
        {
            int skipRow = (page - 1) * pageSize;
            IQueryable<Course> query = this.RepositoryContext.Courses.AsNoTracking();
            CourseList response = new CourseList
            {
                TotalCount = query.Count(),
                Courses = query.OrderByDescending(c => c.CreatedDate).Skip(skipRow).Take(pageSize)
                .Select(c => new Course
                {
                    CourseId = c.CourseId,
                    Title = c.Title,
                    Headline = c.Headline,
                    Image_200_H = c.Image_200_H,
                    EndTime = c.EndTime,
                    Discount_percent = c.Discount_percent
                }).ToList()
            };
            return response;
        }
    }
}
