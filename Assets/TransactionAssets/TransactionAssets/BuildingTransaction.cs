using BaseMonopoly.Assets.BoardAssets.RealStateAssets.StreetAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Assets.TransactionAssets.WalletAssets;
using BaseMonopoly.Exceptions.TransactionExceptions;

namespace BaseMonopoly.Assets.TransactionAssets.TransactionAssets
{
    public class BuildingTransaction : Transaction
    {
        private int? Cost
        {
            get
            {
                if (Amount == 0)
                    return null;
                return Amount;
            }
        }
        public Building Building { get; set; }
        public BuildingTransaction(int? amount, Transactable payee, Transactable payer, Building building,
                                    TransactionType transactionType = TransactionType.give, string? desc = null, Action? onSuccess = null, Action? onFail = null)
                                : base(amount.GetValueOrDefault(), payee, payer, transactionType, desc, onSuccess, onFail)
        {
            Building = building;
        }

        protected override Wallet Authenticate(Transactable transactable)
        {
            if (transactable == Payee && transactable.GetBuildingCount(Building.BuildingType) == 0)
                throw new InsufficientBuildingException(nameof(Payee));

            return base.Authenticate(transactable);
        }

        protected override void AuthorizeTransactionCore(Transactable transactable)
        {
            base.AuthorizeTransactionCore(transactable);
            if (transactable == Payee)
                transactable.RemoveBuilding(Building);
        }

        protected override void CancelTransactionCore()
        {
            if (TransactionResult == TransactionResult.failed)
                Payee.AddBuilding(Building);
            base.CancelTransactionCore();
        }

        protected override void CommitTransactionCore()
        {
            base.CommitTransactionCore();
            if (TransactionResult == TransactionResult.success)
            {
                Payer.AddBuilding(Building);
                Building.ChangeCost(Cost);
            }
        }
    }
}
