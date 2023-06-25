using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Exceptions.TransactionExceptions;

namespace BaseMonopoly.Assets.TransactionAssets.TransactionAssets
{
    public class Auction
    {
        public static Auction? ActiveBid { get; protected set; }
        public int waitTimeMs { get; }
        private bool bidStarted;
        private bool bidFinished;
        private Timer bidTimer;
        public Transaction Transaction { get; }

        public Auction(Transaction transaction, int waitTimeMs = 30 * 1000)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));
            if (transaction.TransactionState == TransactionState.open)
                throw new ArgumentException("Transaction must be closed for auction.");
            if (transaction.Payer != null)
                throw new ArgumentException("Payer must be null.");
            if (transaction.Payee is not Bank b)
                throw new ArgumentException("Auction can only be from bank.");
            else
            {
                b.Auctions.Add(this);
            }
            Transaction = transaction;
            this.waitTimeMs = waitTimeMs;            
        }

        public void IncreaseBid(Player player, int amount)
        {
            if (bidFinished)
                throw new BidFailedException("Auction has already finished.");
            if (amount <= Transaction.Amount)
                throw new BidFailedException("Amount must be greater than previous amount.");

            Transaction.Payer = player ?? throw new ArgumentNullException();
            Transaction.Amount = amount;

            if (bidStarted)
            {
                bidTimer.Change(waitTimeMs, Timeout.Infinite);
            }
            else
            {
                bidTimer = new Timer(TimerCallBack, this, waitTimeMs, Timeout.Infinite);
                bidStarted = true;
            }
        }

        private static void TimerCallBack(object? state)
        {
            var auction = (Auction)state;
            auction.Transaction.StartTransaction();
            auction.FinishAuction();
        }

        private void FinishAuction()
        {
            bidFinished = true;
            bidTimer.Dispose();
        }
    }
}
