namespace BaseMonopoly.Configurations
{
    public record ActionCardPlaceConfiguration
    {
        public int SpaceNumber { get; set; }
        public string Type { get; set; } //"c", "cc"
    }
}