namespace BaseMonopoly.Configurations
{
    public record UtilityTitleDeedConfiguration
    {
        public string Name { get; set; }

        public UtilityTitleDeedConfiguration(string name)
        {
            Name = name;
        }

        public UtilityTitleDeedConfiguration()
        {
        }
    }
}
