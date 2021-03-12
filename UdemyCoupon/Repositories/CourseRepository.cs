using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyCoupon.Models;

namespace UdemyCoupon.Repositories
{
    public interface ICourseRepository
    {
        Task<CourseList> CourseList(string type, int page, int pageSize);
        Task<Course> CourseDetail(long courseId);
        Task<List<string>> CourseCategory();
    }
    public class CourseRepository : RepositoryBase, ICourseRepository
    {
        public CourseRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<CourseList> CourseList(string type, int page, int pageSize)
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

            Task<int> totalCount = query.CountAsync();
            Task<List<Course>> courses = query.OrderByDescending(c => c.CreatedDate).Skip(skipRow).Take(pageSize)
                    .Select(c => new Course
                    {
                        CourseId = c.CourseId,
                        Title = c.Title,
                        Headline = c.Headline,
                        Image_200_H = c.Image_200_H,
                        RemainingTime = DateTime.Now.Subtract(c.EndTime ?? DateTime.Now),
                        Discount_percent = c.Discount_percent,
                        CreatedDate = c.CreatedDate
                    }).ToListAsync();

            CourseList response = new CourseList
            {
                TotalCount = await totalCount,
                Courses = await courses
            };
            return response;
        }

        public async Task<Course> CourseDetail(long courseId)
        {
            IQueryable<Course> query = this.RepositoryContext.Courses.AsNoTracking().Where(c => c.CourseId == courseId);
            return await query.Select(c => new Course
            {
                UdemyLink = c.UdemyLink,
                Title = c.Title,
                Headline = c.Headline,
                Description = c.Description,
                Avg_rating_recent = c.Avg_rating_recent,
                Image_200_H = c.Image_200_H,
                Category = c.Category,
                OriginalPrice = c.OriginalPrice,
                DiscountedPrice = c.DiscountedPrice,
            }).FirstOrDefaultAsync();
        }

        public async Task<List<string>> CourseCategory()
        {
            return await this.RepositoryContext.Courses.AsNoTracking().GroupBy(c => c.Category).Select(g => g.Key).ToListAsync();
        }
    }
}
