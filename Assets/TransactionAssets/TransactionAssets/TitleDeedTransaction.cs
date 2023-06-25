using BaseMonopoly.Assets.TransactionAssets.TitleDeedAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Exceptions.TransactionExceptions;

namespace BaseMonopoly.Assets.TransactionAssets.TransactionAssets
{
    public class TitleDeedTransaction : TradeTransaction
    {
        private TitleDeed titleDeed;
        public TitleDeedTransaction(int amount, Transactable payee, Transactable payer, TitleDeed valuable, TransactionType transactionType = TransactionType.give,
                                    string? desc = null, Action? onSuccess = null, Action? onFail = null) : base(amount, payee, payer, valuable, transactionType, desc, onSuccess, onFail)
        {
            if (valuable.IsMortgage && payee is Bank)
                throw new TransactionFailedException();
            titleDeed = valuable;
        }

        protected override void FailedTransaction()
        {
            base.FailedTransaction();
            if (Payee is Bank bank && Payer is Player)
            {
                bank.BidTitleDeed(titleDeed);
            }
        }

        protected override void SuccessTransaction()
        {
            base.SuccessTransaction();
            if (titleDeed.IsMortgage && Payer is Player p)
            {
                titleDeed.UnMortgageTitleDeed(p, (int)(titleDeed.UnMortgageAmount + titleDeed.UnMortgageAmount * 0.1));
            }
        }
    }
}
