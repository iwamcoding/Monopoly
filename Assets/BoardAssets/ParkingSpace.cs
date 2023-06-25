using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.BoardAssets
{
    public class ParkingSpace : ISpace
    {
        public string Name => "Free Parking Space";

        public int SpaceNumber { get; set; }

        public ParkingSpace() { }
        public ParkingSpace(int spaceNumber) { SpaceNumber = spaceNumber; }
        public void PlayerLanded(Player player)
        {

        }
    }
}
