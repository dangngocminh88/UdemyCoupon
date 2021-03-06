﻿using Microsoft.AspNetCore.Mvc;
using System;
using UdemyCoupon.Repositories;

namespace UdemyCoupon.Controllers
{
    [ApiController]
    [Route("api")]
    public class CourseController : ControllerBase
    {
        private readonly int pageSize = 15;
        private readonly ICourseRepository courseRepository;
        public CourseController(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        [HttpGet]
        [Route("{page}")]
        public IActionResult GetCourse(int page)
        {
            try
            {
                return Ok(courseRepository.GetCourse(page, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("courseList/{page}")]
        public IActionResult CourseList(int page)
        {
            try
            {
                return Ok(courseRepository.CourseList(page, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
