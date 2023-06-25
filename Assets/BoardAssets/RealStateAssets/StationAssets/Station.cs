using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.BoardAssets.RealStateAssets.StationAssets
{
    public class Station : RealStateProperty, IVariableRent
    {
        private Func<int, int> tempRentFunc;
        public Station(int spaceNumber, string name) : base(spaceNumber, name) { }

        public void ChangeTempRent(Func<int, int> func) { tempRentFunc = func; }

        protected override void TakeRentCoreTransaction(Player owner, Player player, int rent)
        {
            if (tempRentFunc == null)
                base.TakeRentCoreTransaction(owner, player, rent);
            else
            {
                rent = tempRentFunc(rent);
                base.TakeRentCoreTransaction(owner, player, rent);
                tempRentFunc = null;
            }
        }
    }
}
