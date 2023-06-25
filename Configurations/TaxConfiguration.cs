namespace BaseMonopoly.Configurations
{
    public record TaxConfiguration
    {
        public string Name { get; set; }
        public int SpaceNumber { get; set; }
        public int TaxAmount { get; set; }

        public TaxConfiguration(string name, int spaceNumber, int taxAmount)
        {
            Name = name;
            SpaceNumber = spaceNumber;
            TaxAmount = taxAmount;
        }

        public TaxConfiguration()
        {
        }
    }
}
