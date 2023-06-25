namespace BaseMonopoly.Configurations
{
    public record CardsConfiguration
    {
        public string Id { get; set; } //"cc", "c"
        public string CardType { get; set; }
        public string Description { get; set; }

        public CardsConfiguration(string id, string description)
        {
            Id = id;
            Description = description;
        }

        public CardsConfiguration()
        {
        }
    }
}
