using BaseMonopoly.Assets.TransactionAssets.TitleDeedAssets;

namespace BaseMonopoly.Assets.TransactionAssets.WalletAssets
{
    public class Wallet
    {
        public int Balance { get; protected set; }
        protected int _money;
        public int Money
        {
            get
            { return _money; }
            set
            { if (value >= 0) _money = value; else throw new ArgumentOutOfRangeException(); }
        }
        protected List<IValuable> valuables;
        public IReadOnlyCollection<TitleDeed> TitleDeeds { get => valuables.Where(x => x is TitleDeed).Select(x => x as TitleDeed).ToList().AsReadOnly(); }

        public Wallet(int money)
        {
            Money = money;
            valuables = new();
        }

        public bool CanPay(int amount)
        {
            return Money >= amount;
        }

        public void PayAmount(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            if (CanPay(amount))
            {
                Money -= amount;
            }
            else
            {
                Balance -= amount - Money;
                Money = 0;
            }

        }

        public bool TryPayAmount(int amount, out int amountPayed)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            var successfullyPaid = false;
            amountPayed = Money;
            var prevBal = Balance;

            PayAmount(amount);
            if (Balance == prevBal)
            {
                successfullyPaid = true;
                amountPayed = amount;
            }
            return successfullyPaid;
        }

        public void RecieveAmount(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            Balance += amount;
            if (Balance > 0)
            {
                Money += Balance;
                Balance = 0;
            }
        }


        public virtual bool CanAddValuable(IValuable valuable)
        {
            return true;
        }

        public virtual void AddValuable(IValuable valuable)
        {
            valuables.Add(valuable);
        }

        public virtual void RemoveValuable(IValuable valuable)
        {
            valuables.Remove(valuable);
        }

        public IValuable[] GetValuables()
        { return valuables.ToArray(); }
    }
}