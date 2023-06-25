using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactionAssets;

namespace BaseMonopoly.Assets.BoardAssets
{
    public class Tax : ISpace
    {
        public string Name { get; set; }
        public int TaxAmount { get; set; }
        public int SpaceNumber { get; set; }

        public Tax(int spaceNumber, int taxAmount, string name)
        {
            SpaceNumber = spaceNumber;
            TaxAmount = taxAmount;
            Name = name;
        }

        public void PlayerLanded(Player player)
        {
            new Transaction(TaxAmount, player.Bank, player, TransactionType.owe, $"Pay Tax {TaxAmount}").StartTransaction();
        }
    }
}
