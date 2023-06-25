using BaseMonopoly.Assets.TransactionAssets.WalletAssets;

namespace BaseMonopoly.Exceptions.TransactionExceptions
{
    internal class InsufficientMoneyException
    {
        public Wallet PayerWallet { get; }
        public int AmountToPay { get; }

        public InsufficientMoneyException(Wallet payerWallet, int amountToPay)
        {
            PayerWallet = payerWallet;
            AmountToPay = amountToPay;
        }
    }
}