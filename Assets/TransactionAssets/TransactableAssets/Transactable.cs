using BaseMonopoly.Assets.BoardAssets.RealStateAssets.StreetAssets;
using BaseMonopoly.Assets.TransactionAssets.TitleDeedAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactionAssets;
using BaseMonopoly.Assets.TransactionAssets.WalletAssets;

namespace BaseMonopoly.Assets.TransactionAssets.TransactableAssets
{
    public abstract class Transactable
    {
        protected Wallet wallet;
        protected List<Transaction> transactions;
        public IReadOnlyList<Transaction> Transactions { get => transactions; }
        public Transactable(Wallet wallet)
        {
            this.wallet = wallet;
            transactions = new();
        }

        public TitleDeed[] GetTitleDeeds() { return wallet.TitleDeeds.ToArray(); }


        public abstract int GetBuildingCount(BuildingType buildingType);
        public abstract Building? GetBuilding(BuildingType buildingType);
        public abstract void AddBuilding(Building building);
        public abstract void RemoveBuilding(Building building);


        public void AcceptTransaction(string id)
        {
            var transaction = this.Transactions.Where(x => x.ID == id).FirstOrDefault();
            if (transaction == null)
                throw new ArgumentException("Invalid transaction.");

            transaction.AuthorizeTransaction(this);
        }

        public void RejectTransaction(string id)
        {
            var transaction = this.Transactions.Where(x => x.ID == id).FirstOrDefault();
            if (transaction == null)
                throw new ArgumentException("Invalid transaction.");

            transaction.CancelTransaction(this);
        }
        
        public Wallet AuthenticateTransaction(Transaction transaction)
        {
            this.transactions.Add(transaction);
            return wallet;
        }

        protected void ClearTransactions()
        {
            var garbage = this.transactions.Where(x => x.TransactionResult != TransactionResult.unavailable).ToList();
            foreach(var item in  garbage)
                this.transactions.Remove(item);
        }
        public virtual void BankRupt(Transaction transaction)
        {
            if (!transactions.Contains(transaction))
                throw new ArgumentException("Invalid transaction.");
            if (transaction.Payer != this)
                throw new ArgumentException("Payer is not appropriate.");

            transaction.PayerBankRupt();
        }
    }
}
