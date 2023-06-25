using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Configurations
{
    public record PlayerConfiguration
    {                
        public int SpaceNumber { get; set; }        
        public PlayerToken PlayerToken { get; set; }
    }
}