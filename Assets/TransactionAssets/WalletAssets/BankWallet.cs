using BaseMonopoly.Exceptions.TransactionExceptions;

namespace BaseMonopoly.Assets.TransactionAssets.WalletAssets
{
    public class BankWallet : Wallet
    {
        public BankWallet(int money) : base(money)
        {
        }
        public BankWallet() : base(20480)
        {
        }

        public override bool CanAddValuable(IValuable valuable)
        {
            return valuable.CanBeOwnedBy == CanBeOwnedBy.Bank || valuable.CanBeOwnedBy == CanBeOwnedBy.Both;
        }

        public override void AddValuable(IValuable valuable)
        {
            if (valuable.CanBeOwnedBy == CanBeOwnedBy.Bank || valuable.CanBeOwnedBy == CanBeOwnedBy.Both)
                base.AddValuable(valuable);
            else
                throw new ValuableCannotBeAddedException();
        }

        public void RefillBalance()
        {
            RecieveAmount(20580);
        }
    }
}