using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactionAssets;

namespace BaseMonopoly.Assets.BoardAssets.RealStateAssets.UtilityAssets
{
    public class Utility : RealStateProperty, IVariableRent
    {
        private Func<int, int> tempRentFunc;
        public Utility(int spaceNumber, string name) : base(spaceNumber, name)
        {
        }

        public void ChangeTempRent(Func<int, int> func)
        {
            tempRentFunc = func;
        }

        protected override void TakeRentCoreTransaction(Player owner, Player player, int rent)
        {
            if (tempRentFunc != null)
            {
                rent = tempRentFunc(rent);
                tempRentFunc = null;
            }
            new Transaction(rent * player.GetDiceSum().Value, owner, player, TransactionType.owe, $"{player.PlayerToken} pays {owner.PlayerToken} {rent} for rent.").StartTransaction();
        }
    }
}
