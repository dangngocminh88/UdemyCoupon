using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace UdemyCoupon.Repositories
{
    public interface ICourseRepository
    {
        List<Course> GetCourse(int page, int pageSize);
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
    }
}
