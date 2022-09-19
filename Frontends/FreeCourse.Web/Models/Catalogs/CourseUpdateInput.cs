using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FreeCourse.Web.Models.Catalogs
{
    public class CourseUpdateInput
    {
        public string Id { get; set; }
        
        [Required]
        [Display(Name = "Kurs Adı")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Kurs Açıklaması")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Kurs Fiyatı")]
        public decimal Price { get; set; }
        
        public string UserId { get; set; }


        [Display(Name = "Kurs Resmi")]
        public string Picture { get; set; }

        [Required]
        [Display(Name = "Kurs Kategori")]
        public string CategoryId { get; set; }

        public FeatureViewModel Feature { get; set; }


        [Display(Name = "Kurs Resmi")]
        public IFormFile PhotoFormFile { get; set; }
    }
}
