using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.BoardAssets
{
    public interface IRealStatePropertyGroup<out T> where T : IRealStateProperty
    {
        public bool GroupOwned(Player player);
        public int GetNumberPropertyOwned(Player player);
    }
}
