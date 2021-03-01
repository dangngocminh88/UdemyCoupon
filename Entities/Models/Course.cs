using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Course")]
    public class Course
    {
        public long CourseId { get; set; }
        public string CourseName { get; set; }
        public string UdemyLink { get; set; }
        public string ImageLink { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
    }
}
