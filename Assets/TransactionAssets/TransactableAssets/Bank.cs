using BaseMonopoly.Assets.BoardAssets.RealStateAssets.StreetAssets;
using BaseMonopoly.Assets.TransactionAssets.TitleDeedAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactionAssets;
using BaseMonopoly.Assets.TransactionAssets.WalletAssets;
using BaseMonopoly.Exceptions.TransactionExceptions;

namespace BaseMonopoly.Assets.TransactionAssets.TransactableAssets
{
    public class Bank : Transactable
    {
        public int Houses { get; set; }
        public int Hotels { get; set; }
        public List<Auction> Auctions { get; set; }
        public Bank(BankWallet wallet, int houses, int hotels) : base(wallet)
        {
            Houses = houses;
            Hotels = hotels;
            Auctions = new List<Auction>();
        }

        private int GetAvailableBuildings(BuildingType buildingType)
        {
            var available = 0;
            var unAcceptedBuildingTransactions = GetUnAcceptedBuildingTransactions(buildingType);

            if (buildingType == BuildingType.House)
                available = Houses + unAcceptedBuildingTransactions.Length;
            else
                available = Hotels + unAcceptedBuildingTransactions.Length;

            return available;
        }

        private Transaction[] GetUnAcceptedBuildingTransactions(BuildingType buildingType)
        {
            return Transactions.Where(x => x is BuildingTransaction bt && bt.TransactionState == TransactionState.open).
                                Where(x => (x as BuildingTransaction).Building.BuildingType == buildingType).ToArray();
        }

        public override int GetBuildingCount(BuildingType buildingType)
        { return buildingType == BuildingType.House ? Houses : Hotels; }

        public override Building? GetBuilding(BuildingType buildingType)
        {
            if (buildingType == BuildingType.House)
            {
                if (Houses > 0)
                    return new Building() { BuildingType = BuildingType.House };
            }
            else
            {
                if (Hotels > 0)
                    return new Building() { BuildingType = BuildingType.Hotel };
            }

            return null;
        }

        public override void AddBuilding(Building building)
        {
            if (building.BuildingType == BuildingType.House)
                Houses++;
            else
                Hotels++;
        }

        public override void RemoveBuilding(Building building)
        {
            if (building.BuildingType == BuildingType.House)
                Houses--;
            else
                Hotels--;
        }

        public void RequestBuilding(Player player, BuildingType buildingType)
        {
            int availableBuildings = GetAvailableBuildings(buildingType);
            var unAcceptedTransactions = GetUnAcceptedBuildingTransactions(buildingType);

            if (availableBuildings == 0)
                throw new InsufficientBuildingException();

            if (unAcceptedTransactions.Length < availableBuildings)
                new BuildingTransaction(null, this, player, new Building(null, buildingType)).StartTransaction();
            else
            {
                foreach (var transaction in unAcceptedTransactions)
                    transaction.CancelTransaction(this);

                for (var i = 0; i < GetBuildingCount(buildingType); i++)
                {
                    var transaction = new BuildingTransaction(0, this, null, new Building(null, buildingType));
                    new Auction(transaction);
                }

            }
        }

        public void ReturnBuilding(Player player, Building building)
        {
            if (building.Cost == null)
                new BuildingTransaction(null, player, this, building).StartTransaction();
            else
                new BuildingTransaction(building.Cost / 2, player, this, building).StartTransaction();
        }

        public void SellTitleDeed(TitleDeed titleDeed, Player player)
        {
            if (titleDeed == null)
                throw new ArgumentNullException(nameof(titleDeed));
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            new TitleDeedTransaction(titleDeed.Value, this, player, titleDeed).StartTransaction();
        }
        public void BidTitleDeed(TitleDeed titleDeed)
        {
            if (titleDeed == null)
                throw new ArgumentNullException(nameof(titleDeed));

            var titleDeedTransaction = new TitleDeedTransaction(0, this, null, titleDeed, TransactionType.owe);
            new Auction(titleDeedTransaction);
        }
    }
}