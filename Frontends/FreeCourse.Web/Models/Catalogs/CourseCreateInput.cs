using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Catalogs
{
    public class CourseCreateInput
    {
        [Display(Name= "Kurs Adı")]
        public string Name { get; set; }

        [Display(Name ="Kurs Açıklaması")]
        public string Description { get; set; }

        [Display(Name = "Kurs Fiyatı")]
        public decimal Price { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Kurs Resmi")]
        public string Picture { get; set; }

        [Display(Name = "Kurs Kategori")]
        public string CategoryId { get; set; }

        public FeatureViewModel Feature { get; set; }

        [Display(Name= "Kurs Resmi")]
        public IFormFile PhotoFormFile { get; set; }
    }
}
