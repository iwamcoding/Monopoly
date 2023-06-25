using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.TransactionAssets
{
    public interface IUsable : IValuable
    {
        public void Discard();
        public void Use(Player player);
    }
}