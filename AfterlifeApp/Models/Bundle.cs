using System.ComponentModel.DataAnnotations;

namespace AfterlifeApp.Models
{
    public class Bundle
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Name must be less than 100 characters.")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Name must be less than 1000 characters.")]
        public string Description { get; set; }
    }
}
