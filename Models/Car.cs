

namespace CarShop.Models
{
    public class Car
    {
        public int Id { get; set; }
        public int? ClientId { get; set; } = null;
        public Client? Client { get; set; }
        public string Name { get; set; } = null!;


        
    }
}
