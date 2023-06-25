using BaseMonopoly.Assets.TransactionAssets.TitleDeedAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Exceptions.TitleDeedExceptions;

namespace BaseMonopoly.Assets.BoardAssets.RealStateAssets.StreetAssets
{
    public class StreetTitleDeed : TitleDeed
    {
        public int RentWithColorSet { get => Rent * 2; }
        public int[] HouseRents { get; set; }
        public int[] HotelRents { get; set; }
        public int MaxNumHouses { get => HouseRents.Length; }
        public int MaxNumHotels { get => HouseRents.Length; }
        public int BuildingCost { get; }
        public StreetColor Color { get => street.Color; }
        private Street street;
        public StreetTitleDeed(int value, int rent, Street realStateProperty, IRealStatePropertyGroup<Street> realStatePropertyGroup, int[] houseRents, int[] hotelRents,
                               int buildingCost) : base(value, rent, realStateProperty, realStatePropertyGroup)
        {
            street = realStateProperty;
            HouseRents = houseRents;
            HotelRents = hotelRents;
            BuildingCost = buildingCost;
        }

        public override int GetRent(Player owner)
        {
            var rent = base.GetRent(owner);
            if (RealStatePropertyGroup.GroupOwned(owner))
                rent = RentWithColorSet;
            if (street.Buildings.Count > 0)
            {
                if (street.GetBuildingType() == BuildingType.House)
                    rent = HouseRents[street.Buildings.Count - 1];
                else
                    rent = HotelRents[street.Buildings.Count - 1];
            }

            return rent;
        }

        protected override void TradeTitleDeed(Player seller, Player buyer, int amount)
        {
            if (street.Buildings.Count > 0)
                throw new TitleDeedCannotBeTradeException("Buildings must be 0.");

            base.TradeTitleDeed(seller, buyer, amount);
        }

        protected override void ValidateMortgage(Player player)
        {
            base.ValidateMortgage(player);

            if (street.Buildings.Count > 0)
                throw new TitleDeedCannotBeMortgagedException("Buildings must be 0.");
        }
    }
}
