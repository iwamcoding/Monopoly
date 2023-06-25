using BaseMonopoly.Assets.BoardAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactionAssets;
using BaseMonopoly.Exceptions.TitleDeedExceptions;

namespace BaseMonopoly.Assets.TransactionAssets.TitleDeedAssets
{
    public abstract class TitleDeed : IValuable
    {        
        public string Name => RealStateProperty.Name;
        public int Value { get; protected set; }
        public int Rent { get; protected set; }
        public int UnMortgageAmount { get => (int)(MortgageAmount + MortgageAmount * 0.1); }
        public int MortgageAmount { get => Value / 2; }
        public bool IsMortgage { get; protected set; }
        public CanBeOwnedBy CanBeOwnedBy => CanBeOwnedBy.Both;
        public IRealStateProperty RealStateProperty { get; protected set; }
        public IRealStatePropertyGroup<IRealStateProperty> RealStatePropertyGroup { get; protected set; }

        public TitleDeed(int value, int rent, IRealStateProperty realStateProperty, IRealStatePropertyGroup<IRealStateProperty> realStatePropertyGroup)
        {
            Value = value;
            Rent = rent;
            RealStateProperty = realStateProperty;
            RealStatePropertyGroup = realStatePropertyGroup;
        }


        public virtual int GetRent(Player owner)
        {
            int rent;
            if (IsMortgage)
                throw new RentCannotBeTakenException("Title deed is mortgaged.");
            if (!owner.GetTitleDeeds().Contains(this))
                throw new RentCannotBeTakenException("Owner does not own the title deed.");

            rent = Rent;
            return rent;
        }


        public void Trade(Player payee, Player payer, int amount)
        {
            TradeTitleDeed(payee, payer, amount);
        }

        protected virtual void TradeTitleDeed(Player seller, Player buyer, int amount)
        {
            new TitleDeedTransaction(amount, seller, buyer, this).StartTransaction();
        }


        public void MortgageTitleDeed(Player player)
        {
            ValidateMortgage(player);
            new Transaction(MortgageAmount, player.Bank, player, TransactionType.give, null, Mortgage).StartTransaction();
        }

        protected virtual void ValidateMortgage(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            if (IsMortgage)
                throw new InvalidOperationException("Title deed is already mortgaged.");
        }

        protected void Mortgage()
        {
            IsMortgage = true;
        }


        public void UnMortgageTitleDeed(Player player, int? amount = null)
        {
            ValidateUnMortgage(player);
            if (amount != null)
                new Transaction(amount.Value, player.Bank, player, TransactionType.give, null, UnMortgage).StartTransaction();
            else
                new Transaction(UnMortgageAmount, player.Bank, player, TransactionType.give, null, UnMortgage).StartTransaction();
        }

        protected virtual void ValidateUnMortgage(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            if (!IsMortgage)
                throw new InvalidOperationException("Title deed is not mortgaged.");
        }

        protected void UnMortgage()
        {
            IsMortgage = false;
        }
    }
}