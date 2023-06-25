namespace BaseMonopoly.Configurations
{
    public record ColorSetConfiguration
    {
        public string[] Names { get; set; }

        public ColorSetConfiguration(string[] names)
        {
            Names = names;
        }

        public ColorSetConfiguration()
        {
        }
    }
}
