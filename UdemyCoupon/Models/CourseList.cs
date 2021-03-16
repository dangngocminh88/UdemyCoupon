using Entities.Models;
using System.Collections.Generic;

namespace UdemyCoupon.Models
{
    public class CourseList
    {
        public int TotalCount { get; set; }
        public List<Course> Courses { get; set; }
    }
    public class CourseListRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Type { get; set; }   
        public string Category { get; set; }
    }
}
