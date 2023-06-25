namespace BaseMonopoly.Configurations
{
    public record GoToJailConfiguration
    {
        public string JailName { get; set; }
        public int SpaceNumber { get; set; }

        public GoToJailConfiguration(string jailName, int spaceNumber)
        {
            JailName = jailName;
            SpaceNumber = spaceNumber;
        }

        public GoToJailConfiguration()
        {
        }
    }
}
