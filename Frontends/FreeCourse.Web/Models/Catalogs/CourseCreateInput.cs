using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Catalogs
{
    public class CourseCreateInput
    {
        [Required]
        [Display(Name= "Kurs Adı")]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Kurs Açıklaması")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Kurs Fiyatı")]
        public decimal Price { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Kurs Resmi")]
        public string Picture { get; set; }

        [Required]
        [Display(Name = "Kurs Kategori")]
        public string CategoryId { get; set; }

        public FeatureViewModel Feature { get; set; }
    }
}
