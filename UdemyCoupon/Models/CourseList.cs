using Entities.Models;
using System.Collections.Generic;

namespace UdemyCoupon.Models
{
    public class CourseList
    {
        public int TotalCount { get; set; }
        public List<Course> Courses { get; set; }
    }
}
