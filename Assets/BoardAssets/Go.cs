using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactionAssets;

namespace BaseMonopoly.Assets.BoardAssets
{
    public class Go : IPassable
    {
        public string Name => "Go";
        public int SpaceNumber { get; set; }
        public int Salary { get; set; }
        public Go(int spaceNumber, int salary)
        {
            SpaceNumber = spaceNumber;
            Salary = salary;
        }

        public void PlayerLanded(Player player)
        {
            new Transaction(Salary, player, player.Bank, TransactionType.owe, $"Take Salary {Salary}").StartTransaction();
        }
    }
}
