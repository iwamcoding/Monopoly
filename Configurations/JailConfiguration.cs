namespace BaseMonopoly.Configurations
{
    public record JailConfiguration
    {
        public int SpaceNumber { get; set; }
        public JailConfiguration() { }
        public JailConfiguration(int spaceNumber)
        {
            SpaceNumber = spaceNumber;
        }

    }
}
