using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.BoardAssets
{
    public interface IRealStateProperty : ISpace
    {
        public void TakeRent(Player owner, Player player);
    }
}
