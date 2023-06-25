using BaseMonopoly.Assets.BoardAssets.RealStateAssets.StreetAssets;

namespace BaseMonopoly.Configurations
{
    public record StreetConfiguration
    {
        public string Name { get; set; }
        public int SpaceNumber { get; set; }
        public StreetColor Color { get; set; }

        public StreetConfiguration(string name, int spaceNumber, StreetColor color)
        {
            Name = name;
            SpaceNumber = spaceNumber;
            Color = color;
        }
        public StreetConfiguration()
        {

        }
    }
}
