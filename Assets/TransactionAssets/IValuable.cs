using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.TransactionAssets
{
    public interface IValuable
    {
        public string Name { get;}
        public CanBeOwnedBy CanBeOwnedBy { get; }
        public void Trade(Player payee, Player payer, int amount);
    }
    public enum CanBeOwnedBy
    {
        Bank,
        Player,
        Both
    }
}