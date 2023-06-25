using BaseMonopoly.Assets.BoardAssets;

namespace BaseMonopoly.Configurations
{
    public record RealStateGroupConfiguration<T> where T : RealStateProperty
    {
        public string[] Names { get; set; }

        public RealStateGroupConfiguration(string[] names)
        {
            Names = names;
        }

        public RealStateGroupConfiguration()
        {

        }
    }
}
