using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadebymeWF.Models
{
    [Table("posts", Schema = "public")] 
    public class Post
    {
        [Key]
        [Column("post_id")]
        public int PostId { get; set; }

        [Column("name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; } 

        [Column("photo_path")]
        [MaxLength(100)] 
        public string PhotoPath { get; set; }

        [Column("category_ref")]
        public int? CategoryRef { get; set; } 

        [Column("seller_ref")]
        public int SellerRef { get; set; }

        [ForeignKey("SellerRef")]
        public virtual User User { get; set; }

        [Column("pay_card")]
        [MaxLength(16)]
        public string PayCard { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("rating")]
        [Range(0.00, 999.99)] 
        public decimal? Rating { get; set; }

        [Column("price")]
        [Range(0.00, 999999.99)] 
        public decimal? Price { get; set; }

    }
}
