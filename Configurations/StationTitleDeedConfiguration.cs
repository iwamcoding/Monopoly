namespace BaseMonopoly.Configurations
{
    public record StationTitleDeedConfiguration
    {
        public string Name { get; set; }
        public StationTitleDeedConfiguration() { }
        public StationTitleDeedConfiguration(string name)
        {
            Name = name;
        }
    }
}
