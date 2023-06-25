using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactionAssets;
using BaseMonopoly.Exceptions.TitleDeedExceptions;
using BaseMonopoly.Exceptions.TransactionExceptions;

namespace BaseMonopoly.Assets.BoardAssets
{
    public abstract class RealStateProperty : IRealStateProperty
    {
        public string Name { get; set; }
        public int SpaceNumber { get; set; }

        public RealStateProperty(int spaceNumber, string name)
        {
            SpaceNumber = spaceNumber;
            Name = name;
        }
        public void PlayerLanded(Player player)
        {            
            var titleDeedInBank = player.Bank.GetTitleDeeds().Where(x => x.RealStateProperty == this).FirstOrDefault();
            if (titleDeedInBank != null)
                player.Bank.SellTitleDeed(titleDeedInBank, player);
        }

        public void TakeRent(Player owner, Player player)
        {
            if (owner == null)
                throw new ArgumentNullException(nameof(owner));
            if (player == null) throw new ArgumentNullException(nameof(player));

            var titlDeed = owner.GetTitleDeeds().Where(x => x.RealStateProperty == this).FirstOrDefault() ?? throw new ValuableNotFoundException("Owner does not own title deed.");
            if (player.SpaceNumber != SpaceNumber)
                throw new RentCannotBeTakenException("Player is not on the space.");

            var rent = titlDeed.GetRent(owner);
            TakeRentCoreTransaction(owner, player, rent);
        }

        protected virtual void TakeRentCoreTransaction(Player owner, Player player, int rent)
        {
            new Transaction(rent, owner, player, TransactionType.owe, $"{player.PlayerToken} pays {owner.PlayerToken} {rent} for rent.").StartTransaction();
        }
    }
}
