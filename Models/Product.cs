using System.ComponentModel.DataAnnotations;

namespace CURDUSingAPIEFCore.Models
{
    public class Product
    {
        public Int64 ProductID { get; set; }

        [Required(ErrorMessage ="Product Name Required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage ="MfgName Required")]
        public string MfgName { get; set; }

        [Required(ErrorMessage ="Price Required")]
        [Range(1000,20000,ErrorMessage ="Invalid Price Range!")]
        public decimal Price { get; set; }
    }
}
