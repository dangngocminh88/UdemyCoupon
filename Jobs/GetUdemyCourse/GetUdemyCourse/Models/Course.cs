using System;
using System.Collections.Generic;
using System.Text;

namespace GetUdemyCourse.Models
{
    public class Course
    {
        public long CourseId { get; set; }
        public string CourseName { get; set; }
        public string UdemyLink { get; set; }
        public string ImageLink { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
    }
    public class CourseInfo
    {
        public string title { get; set; }
        public string headline { get; set; }
        public string description { get; set; }
    }
}
