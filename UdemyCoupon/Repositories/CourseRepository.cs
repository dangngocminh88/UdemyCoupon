using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UdemyCoupon.Models;

namespace UdemyCoupon.Repositories
{
    public interface ICourseRepository
    {
        CourseList CourseList(string type, int page, int pageSize);
        Course CourseDetail(long courseId);
    }
    public class CourseRepository : RepositoryBase, ICourseRepository
    {
        public CourseRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public CourseList CourseList(string type, int page, int pageSize)
        {
            int skipRow = (page - 1) * pageSize;
            IQueryable<Course> query = this.RepositoryContext.Courses.AsNoTracking();
            if (type.ToLower().Equals("100off"))
            {
                query = query.Where(c => c.Discount_percent == 100);
            }
            if (type.ToLower().Equals("discount"))
            {
                query = query.Where(c => c.Discount_percent < 100);
            }
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
                        RemainingTime = DateTime.Now.Subtract(c.EndTime ?? DateTime.Now),
                        Discount_percent = c.Discount_percent,
                        CreatedDate = c.CreatedDate
                    }).ToList()
            };
            return response;
        }

        public Course CourseDetail(long courseId)
        {
            return this.RepositoryContext.Courses.Find(courseId);
        }
    }
}
