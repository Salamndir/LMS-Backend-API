using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Model;
using StudentApi.Services;

namespace StudentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // Require authentication for the entire controller by default
    [Authorize]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // Accessible by both Admin and Trainee (as long as they have a valid token)
        [HttpGet]
        public IActionResult GetAllCourses()
        {
            var courses = _courseService.GetAllCourses();
            return Ok(courses);
        }

        // Accessible ONLY by users with the "Admin" role
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCourse([FromBody] Course course)
        {
            var createdCourse = _courseService.AddCourse(course);
            return CreatedAtAction(nameof(GetAllCourses), new { id = createdCourse.Id }, createdCourse);
        }

        // Accessible ONLY by users with the "Admin" role
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCourse(int id)
        {
            bool isDeleted = _courseService.DeleteCourse(id);
            if (!isDeleted)
            {
                return NotFound(new { message = "Course not found" });
            }
            return Ok(new { message = "Course deleted successfully" });
        }

        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCourse(int id, [FromBody] Course updatedCourse)
        {
            var course = _courseService.UpdateCourse(id, updatedCourse);
            if (course == null) return NotFound(new { message = "Course not found" });
            return Ok(course);
        }
    }
}