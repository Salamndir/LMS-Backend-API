using StudentApi.Model;

namespace StudentApi.Services
{
    public interface ICourseService
    {
        // Trainee & Admin: Get all courses
        IEnumerable<Course> GetAllCourses();

        // Admin only: Add a new course
        Course AddCourse(Course course);

        // Admin only: Delete a course by its ID
        bool DeleteCourse(int id);


        Course UpdateCourse(int id, Course updatedCourse);
    }
}