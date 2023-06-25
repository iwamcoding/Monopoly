using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Assets.TransactionAssets.WalletAssets;
using BaseMonopoly.Exceptions.TransactionExceptions;

namespace BaseMonopoly.Assets.TransactionAssets.TransactionAssets
{
    public abstract class TradeTransaction : Transaction
    {
        public IValuable Valuable { get; set; }
        public TradeTransaction(int amount, Transactable payee, Transactable payer, IValuable valuable, TransactionType transactionType = TransactionType.give,
                                string? desc = null, Action? onSuccess = null, Action? onFail = null) : base(amount, payee, payer, transactionType, desc, onSuccess, onFail)
        {
            Valuable = valuable;
        }

        protected override Wallet Authenticate(Transactable transactable)
        {
            var wallet = transactable.AuthenticateTransaction(this);

            if (!wallet.CanAddValuable(Valuable))
                throw new ValuableCannotBeAddedException();
            if (transactable == Payee && !wallet.GetValuables().Contains(Valuable))
                throw new ValuableNotFoundException();

            return wallet;
        }


        protected override void AuthorizeTransactionCore(Transactable transactable)
        {
            base.AuthorizeTransactionCore(transactable);
            if (transactable == Payee)
                this.payeeWallet.RemoveValuable(Valuable);
        }

        protected override void CancelTransactionCore()
        {
            base.CancelTransactionCore();
            if (TransactionResult == TransactionResult.failed)
                this.payeeWallet.AddValuable(Valuable);

        }
        protected override void CommitTransactionCore()
        {
            base.CommitTransactionCore();
            if (TransactionResult == TransactionResult.success)
                payerWallet.AddValuable(Valuable);
        }


        protected override void RollBackTransactionCore()
        {
            this.payeeWallet.AddValuable(Valuable);
            base.RollBackTransactionCore();
        }
    }
}
