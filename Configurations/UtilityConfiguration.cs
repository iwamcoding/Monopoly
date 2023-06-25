namespace BaseMonopoly.Configurations
{
    public record UtilityConfiguration
    {
        public string Name { get; set; }
        public int SpaceNumber { get; set; }

        public UtilityConfiguration(string name, int spaceNumber)
        {
            Name = name;
            SpaceNumber = spaceNumber;
        }

        public UtilityConfiguration()
        {
        }
    }
}
