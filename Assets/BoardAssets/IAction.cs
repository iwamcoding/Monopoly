using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.BoardAssets
{
    public interface IAction
    {
        public bool Available { get; }
        public void Execute(Player player);
    }
}
