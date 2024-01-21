using System.ComponentModel.DataAnnotations;

namespace AfterlifeApp.Models
{
    public class Game
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Name must be less than 100 characters.")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Name must be less than 1000 characters.")]
        public string Description { get; set; }

        [Range(0, 100, ErrorMessage = "Rating must be in the range from 0 to 100.")]
        public int Rating { get; set; }

        [Range(0, 1000, ErrorMessage = "Price must be in the range from 0 to 1000.")]
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public int BundleId { get; set; }
        public virtual Bundle? Bundle { get; set;}
    }
}
