using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApi.Model
{

    // Represents the Course table in the database
    [Table("Courses")]
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        // As requested in the assignment: a YouTube video link
        public string YouTubeVideoUrl { get; set; }
    }
}
