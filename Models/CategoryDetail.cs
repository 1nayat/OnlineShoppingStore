using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineShoppingStore.Models
{
    public class CategoryDetail
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Category Name Required")]
        [StringLength(100, ErrorMessage="Minimum 3 and Maximum 100 Characters are allowed")]
        public string? CategoryName { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDelete { get; set; }
    }
    public class ProductDetail
    {

        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product Name is Required")]
        [StringLength(100, ErrorMessage = "Minimum 3 and Maximum 100 Characters are allowed")]
        public string? ProductName { get; set; }
        [Required]
        [Range(1, 50)]
        public int? CategoryId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        public string? Description { get; set; }

        public string? ProductImage { get; set; }

        public bool? IsFeatured { get; set; }
        [Required]
        [Range(typeof(int), "1", "500", ErrorMessage = "Invalid Quantity")]
        public int? Quantity { get; set; }
        [Required]
        public decimal price { get; set; }
        public SelectList categories{ get; set; }
    }
}
