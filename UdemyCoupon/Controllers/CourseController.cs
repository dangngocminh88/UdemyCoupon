using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyCoupon.Models;
using UdemyCoupon.Repositories;

namespace UdemyCoupon.Controllers
{
    [ApiController]
    [Route("api")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository courseRepository;
        public CourseController(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        [HttpPost]
        [Route("courselist")]
        public async Task<IActionResult> CourseList([FromBody] CourseListRequest request)
        {
            try
            {
                CourseList courseList = await courseRepository.CourseList(request);
                return Ok(courseList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("coursedetail/{courseId}")]
        public async Task<IActionResult> CourseDetail(long courseId)
        {
            try
            {
                Course course = await courseRepository.CourseDetail(courseId);
                return Ok(course);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("coursecategory")]
        public async Task<IActionResult> CourseCategory()
        {
            try
            {
                List<string> courseCategories = await courseRepository.CourseCategory();
                return Ok(courseCategories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
