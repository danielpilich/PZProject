using System.ComponentModel.DataAnnotations;

namespace AfterlifeApp.Models
{
    public class Category
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Name must be less than 100 characters.")]
        public string Name { get; set; }
    }
}
