using BaseMonopoly.Exceptions.TransactionExceptions;

namespace BaseMonopoly.Assets.TransactionAssets.WalletAssets
{
    public class PlayerWallet : Wallet
    {
        public IReadOnlyList<IUsable> Usables { get => valuables.Where(x => x is IUsable).Select(y => y as IUsable).ToList(); }

        public PlayerWallet(int money) : base(money) { }


        public override bool CanAddValuable(IValuable valuable)
        {
            return valuable.CanBeOwnedBy == CanBeOwnedBy.Player || valuable.CanBeOwnedBy == CanBeOwnedBy.Both;
        }

        public override void AddValuable(IValuable valuable)
        {
            if (valuable.CanBeOwnedBy == CanBeOwnedBy.Player || valuable.CanBeOwnedBy == CanBeOwnedBy.Both)
                base.AddValuable(valuable);
            else
                throw new ValuableCannotBeAddedException();
        }
    }
}