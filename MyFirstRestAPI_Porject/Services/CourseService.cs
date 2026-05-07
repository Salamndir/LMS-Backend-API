using StudentApi.Data;
using StudentApi.Model;

namespace StudentApi.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;

        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetAllCourses()
        {
            // Fetch all courses from the database
            return _context.Courses.ToList();
        }

        public Course AddCourse(Course course)
        {
            // Add the new course to the context and save changes
            _context.Courses.Add(course);
            _context.SaveChanges();
            return course;
        }

        public bool DeleteCourse(int id)
        {
            // Find the course by ID
            var course = _context.Courses.Find(id);
            if (course == null) return false; // Course not found

            // Remove it and save changes
            _context.Courses.Remove(course);
            _context.SaveChanges();
            return true;
        }



       
        public Course UpdateCourse(int id, Course updatedCourse)
        {
            var existingCourse = _context.Courses.Find(id);
            if (existingCourse == null) return null;

            existingCourse.Title = updatedCourse.Title;
            existingCourse.Description = updatedCourse.Description;
            existingCourse.YouTubeVideoUrl = updatedCourse.YouTubeVideoUrl;

            _context.SaveChanges();
            return existingCourse;
        }



    }
}