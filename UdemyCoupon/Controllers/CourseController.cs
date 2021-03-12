using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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
        public async Task<IActionResult> CourseList(string type, int page)
        {
            try
            {
                return Ok(await courseRepository.CourseListAsync(type, page, pageSize));
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
                return Ok(courseRepository.CourseDetailAsync(courseId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("coursecategory")]
        public IActionResult CourseCategory()
        {
            try
            {
                return Ok(courseRepository.CourseCategory());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
