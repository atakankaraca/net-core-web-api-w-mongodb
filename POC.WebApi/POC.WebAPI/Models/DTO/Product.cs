using System;
using System.ComponentModel.DataAnnotations;

namespace POC.WebAPI.Models
{
    public class Product : BaseModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.EmailAddress)]
        public decimal Price { get; set; }
    }
}
