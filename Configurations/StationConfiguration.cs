namespace BaseMonopoly.Configurations
{
    public record StationConfiguration
    {
        public string Name { get; set; }
        public int SpaceNumber { get; set; }
        public StationConfiguration(string name, int spaceNumber)
        {
            Name = name;
            SpaceNumber = spaceNumber;
        }
        public StationConfiguration() { }
    }
}
