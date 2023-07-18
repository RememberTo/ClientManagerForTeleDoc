using ClientManager.Domain.Enum;

namespace ClientManager.Domain
{
    public class Client
    {
        public int Id { get; set; }
        public string INN { get; set; }
        public string Name { get; set; }
        public ClientType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Shareholder> Shareholders { get; set; }
    }
}
