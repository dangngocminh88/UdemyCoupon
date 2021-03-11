using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Course")]
    public class Course
    {
        public long? CourseId { get; set; }
        public string UdemyLink { get; set; }
        public string Title { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public decimal? Avg_rating_recent { get; set; }
        //public List<string> Objectives_summary { get; set; }
        //public List<string> Target_audiences { get; set; }
        public string Image_200_H { get; set; }
        public string Category { get; set; }
        //public List<string> Prerequisites { get; set; }
        public DateTime? EndTime { get; set; }
        public string OriginalPrice { get; set; }
        public string DiscountedPrice { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Discount_percent { get; set; }
        public DateTime? CreatedDate { get; set; }
        public TimeSpan? RemainingTime { get; set; }
    }
}
