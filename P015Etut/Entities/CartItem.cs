using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace P015Etut.Entities
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Adet alanı gereklidir.")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir adet girin.")]
        public int Quantity { get; set; }

        // Navigation properties for the CartEntity and ProductEntity
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
