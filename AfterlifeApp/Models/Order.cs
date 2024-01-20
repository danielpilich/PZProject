namespace AfterlifeApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int GameId { get; set; }
        public virtual Game? Game { get; set; }
    }
}
