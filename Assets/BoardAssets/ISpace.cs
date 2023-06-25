using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.BoardAssets
{
    public interface ISpace
    {
        public string Name { get; }
        public int SpaceNumber { get; }
        public void PlayerLanded(Player player);
    }
}
