using System.ComponentModel.DataAnnotations;

namespace TherapiCareTest.Models
{
    public class Announcement
    {
        [Key]
        [Display(Name = "Id")]
        public int a_id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        [ValidateDateRangeToday]
        public DateTime a_date { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string? a_title { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string? a_description { get; set; }
      
        [Display(Name = "Announcement Image")]
        public string? a_image { get; set; }
        public bool IsHidden { get; set; }
        public bool IsViewed { get; set; }

    }
}
