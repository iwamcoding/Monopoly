using BaseMonopoly.Assets.BoardAssets.RealStateAssets.StreetAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactionAssets;

namespace BaseMonopoly.Assets.BoardAssets.ActionCardAssets
{
    public class StreetRepairAction : IAction
    {
        public int MoneyPerHouse { get; private set; }
        public int MoneyPerHotel { get; private set; }
        public bool Available => true;

        public StreetRepairAction(int moneyPerHouse, int moneyPerHotel)
        {
            MoneyPerHouse = moneyPerHouse;
            MoneyPerHotel = moneyPerHotel;
        }

        public void Execute(Player player)
        {
            var numHouses = player.Buildings.Where(x => x.BuildingType == BuildingType.House && x.Available == false).Count();
            var numHotels = player.Buildings.Where(x => x.BuildingType == BuildingType.Hotel && x.Available == false).Count();

            var housesCost = numHouses * MoneyPerHouse;
            var hotelCost = numHotels * MoneyPerHotel;

            new Transaction(housesCost + hotelCost, player.Bank, player, TransactionType.owe, "Pay for street repairs").StartTransaction();
        }
    }
}
