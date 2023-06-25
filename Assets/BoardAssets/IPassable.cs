using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.BoardAssets
{
    public interface IPassable : ISpace
    {
        public void PlayerPassed(Player player)
        {
            PlayerLanded(player);
        }
    }
}
