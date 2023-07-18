namespace ClientManager.Domain
{
    public class Shareholder
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string INN { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Client Client { get; set; }
    }
}
