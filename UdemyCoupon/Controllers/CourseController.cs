using Microsoft.AspNetCore.Mvc;
using System;
using UdemyCoupon.Repositories;

namespace UdemyCoupon.Controllers
{
    [ApiController]
    [Route("api")]
    public class CourseController : ControllerBase
    {
        private readonly int pageSize = 10;
        private readonly ICourseRepository courseRepository;
        public CourseController(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        [HttpGet]
        [Route("courselist/{type}/{page}")]
        public IActionResult CourseList(string type, int page)
        {
            try
            {
                return Ok(courseRepository.CourseList(type, page, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("coursedetail/{courseId}")]
        public IActionResult CourseDetail(long courseId)
        {
            try
            {
                return Ok(courseRepository.CourseDetail(courseId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
