using BaseMonopoly.Assets.TransactionAssets.TitleDeedAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.BoardAssets.RealStateAssets.StationAssets
{
    public class StationTitleDeed : TitleDeed
    {
        private Station station;
        public StationTitleDeed(int value, int rent, Station realStateProperty, IRealStatePropertyGroup<Station> realStatePropertyGroup) : base(value, rent, realStateProperty, realStatePropertyGroup)
        {
            station = realStateProperty;
        }

        public override int GetRent(Player owner)
        {
            var rent = base.GetRent(owner);
            var propertiesOwned = RealStatePropertyGroup.GetNumberPropertyOwned(owner);
            var numberForPower = propertiesOwned - 1;

            return rent * (int)Math.Pow(2, numberForPower);
        }
    }
}
