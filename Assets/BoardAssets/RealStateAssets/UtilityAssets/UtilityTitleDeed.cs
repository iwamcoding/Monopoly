using BaseMonopoly.Assets.TransactionAssets.TitleDeedAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.BoardAssets.RealStateAssets.UtilityAssets
{
    public class UtilityTitleDeed : TitleDeed
    {
        private Utility utility;
        private int[] rentWithUtils;
        public UtilityTitleDeed(int value, int[] rentWithNumUtils, Utility realStateProperty, IRealStatePropertyGroup<Utility> realStatePropertyGroup) : base(value, rentWithNumUtils[0], realStateProperty, realStatePropertyGroup)
        {
            utility = realStateProperty;
            rentWithUtils = rentWithNumUtils;
        }

        public override int GetRent(Player owner)
        {
            var rent = base.GetRent(owner);
            if (RealStatePropertyGroup.GetNumberPropertyOwned(owner) > 1)
            {
                rent = rentWithUtils[RealStatePropertyGroup.GetNumberPropertyOwned(owner) - 1];
            }

            return rent;
        }
    }
}
