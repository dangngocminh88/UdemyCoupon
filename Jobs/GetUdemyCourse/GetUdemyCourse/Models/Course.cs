using System;
using System.Collections.Generic;
using System.Text;

namespace GetUdemyCourse.Models
{
    public class Course
    {
        public long CourseId { get; set; }
        public string Title { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public decimal Avg_rating_recent { get; set; }
        public List<string> Objectives_summary { get; set; }
        public List<string> Target_audiences { get; set; }
        public string Image_200_H { get; set; }
        public string Category { get; set; }
        public List<string> Prerequisites { get; set; }
        public DateTime? EndTime { get; set; }
        public string OriginalPrice { get; set; }
        public string DiscountedPrice { get; set; }
        public decimal Amount { get; set; }
    }
    public class CourseInfo
    {
#pragma warning disable IDE1006 // Naming Styles
        public string title { get; set; }
        public string headline { get; set; }
        public string description { get; set; }
        public decimal avg_rating_recent { get; set; }
        public List<string> objectives_summary { get; set; }
        public List<string> target_audiences { get; set; }
        public string image_200_H { get; set; }
        public PrimaryCategory primary_category { get; set; }
        public List<string> prerequisites { get; set; }
    }
    public class PrimaryCategory
    {
        public string title { get; set; }
    }
    public class PurchaseInfo
    {
        public Purchase purchase { get; set; }
    }
    public class Purchase
    {
        public PurchaseData data { get; set; }
    }
    public class PurchaseData
    {
        public PricingResult pricing_result { get; set; }
    }
    public class PricingResult
    {
        public Campaign campaign { get; set; }
        public ListPrice list_price { get; set; }
        public Price price { get; set; }
    }
    public class Campaign
    {
        public string end_time { get; set; }
    }
    public class ListPrice
    {
        public string price_string { get; set; }
    }
    public class Price
    {
        public string price_string { get; set; }
        public decimal amount { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
