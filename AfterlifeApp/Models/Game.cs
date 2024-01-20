namespace AfterlifeApp.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public int BundleId { get; set; }
        public virtual Bundle? Bundle { get; set;}
    }
}
