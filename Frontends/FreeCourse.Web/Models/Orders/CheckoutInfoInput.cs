using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Orders
{
    public class CheckoutInfoInput
    {
        [Display(Name = "İl")]
        public string Province { get; set; }

        [Display(Name = "İlçe")]
        public string District { get; set; }

        [Display(Name = "Cadde")]
        public string Street { get; set; }

        [Display(Name = "Posta Kodu")]
        public string ZipCode { get; set; }

        [Display(Name = "Adres")]
        public string Line { get; set; }

        [Display(Name = "Kart isim soy isim")]
        public string CardName { get; set; }

        [Display(Name = "Kart numarası")]
        public string CardNumber { get; set; }

        [Display(Name = "Son Kullanma Tarihi (ay/yıl)")]
        public string Expiration { get; set; }

        [Display(Name = "CVV/CVC2 Numarası")]
        public string CVV { get; set; }
    }
}
