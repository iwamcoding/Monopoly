using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Exceptions.ColorSetExceptions;
using BaseMonopoly.Exceptions.TransactionExceptions;

namespace BaseMonopoly.Assets.BoardAssets.RealStateAssets.StreetAssets
{
    public class ColorSet
    {
        public StreetColor Color { get; }
        public StreetTitleDeed[] StreetTitleDeeds { get; }
        private RealStatePropertyGroup<Street> group;
        private Dictionary<Street, StreetTitleDeed> titleDeedsDic;

        public ColorSet(StreetTitleDeed[] streetTitleDeeds)
        {
            var distinctStreets = streetTitleDeeds.DistinctBy(x => x.Color).ToArray();
            if (distinctStreets.Length != 1)
                throw new ArgumentException("streets don't have the same color.");

            StreetTitleDeeds = streetTitleDeeds;

            var allStreets = streetTitleDeeds.Select(x => x.RealStateProperty as Street).ToArray();
            group = new RealStatePropertyGroup<Street>(allStreets);
            Color = streetTitleDeeds[0].Color;
            titleDeedsDic = new Dictionary<Street, StreetTitleDeed>();

            foreach (var street in allStreets)
            {
                var titleDeed = streetTitleDeeds.Where(x => x.RealStateProperty == street).First();
                titleDeedsDic.Add(street, titleDeed);
            }
        }

        private void ValidateUpgradeBuildingStreet(Street street, Player player, Building building)
        {
            var streetPartOfColorSet = group.RealStateProperties.Contains(street);
            var playerOwnsStreet = player.GetTitleDeeds().Any(x => x.RealStateProperty == street);
            var playerOwnsColorSet = group.GroupOwned(player);
            var playerOwnsBuilding = building != null;
            var streetsEven = group.RealStateProperties.All(x => x.GetCurrentStreetLevel() >= street.GetCurrentStreetLevel());
            if (!playerOwnsBuilding)
                throw new UpgradingStreetFailedException("Player does not have any buildings.");
            if (!streetPartOfColorSet)
                throw new UpgradingStreetFailedException("Street is not part of the Color Set.");
            if (!playerOwnsStreet)
                throw new UpgradingStreetFailedException("Player does not own the titledeed.", new ValuableNotFoundException());
            if (!playerOwnsColorSet)
                throw new UpgradingStreetFailedException("Player does not own colorset.");
            if (!streetsEven)
                throw new UpgradingStreetFailedException("Streets must be upgraded evenly.");
        }

        private void ValidateUpgradeHouseStreet(Street street, Player player, Building building)
        {
            ValidateUpgradeBuildingStreet(street, player, building);

            if (building.BuildingType != BuildingType.House)
                throw new UpgradingStreetFailedException("Building is not house.");
            if (street.GetCurrentStreetLevel() >= titleDeedsDic[street].MaxNumHouses)
            {
                throw new UpgradingStreetFailedException("Cannot upgrade more houses.");
            }
        }

        public void UpgradeHouseStreet(Street street, Player player, Building? building = null)
        {
            var titleDeed = player.GetTitleDeeds().Where(x => x.RealStateProperty == street).FirstOrDefault() as StreetTitleDeed;
            building ??= player.GetBuilding(BuildingType.House);

            ValidateUpgradeHouseStreet(street, player, building);
            if (building.Cost == null)
            {
                street.UpgradeBuilding(building, player);
            }
            else
            {
                building.ChangeCost(titleDeed.BuildingCost);
                street.Buildings.Add(building);
            }
        }

        private void ValidateUpgradeHotelStreet(Street street, Player player, Building building)
        {
            ValidateUpgradeBuildingStreet(street, player, building);
            if (building.BuildingType != BuildingType.Hotel)
                throw new UpgradingStreetFailedException("Building is not hotel.");
            if (street.GetCurrentStreetLevel() < titleDeedsDic[street].MaxNumHouses)
                throw new UpgradingStreetFailedException("Street needs more houses to upgrade a hotel.");
            if (street.GetCurrentStreetLevel() == titleDeedsDic[street].MaxNumHotels)
                throw new UpgradingStreetFailedException("Cannot bupgrade more hotels.");
        }

        public void UpgradeHotelStreet(Street street, Player player, Building? building = null)
        {
            var titleDeed = player.GetTitleDeeds().Where(x => x.RealStateProperty == street).FirstOrDefault() as StreetTitleDeed;
            building ??= player.GetBuilding(BuildingType.Hotel);

            ValidateUpgradeHotelStreet(street, player, building);

            if (building.Cost == null)
            {
                building.ChangeCost(titleDeed.BuildingCost);
                street.UpgradeBuilding(building, player);
            }
            else
            {
                building.ChangeCost(titleDeed.BuildingCost);
                street.Buildings.Add(building);
            }
        }


        private void ValidateDowngradeBuildingStreet(Street street, Player player)
        {
            var streetPartOfColorSet = group.RealStateProperties.Contains(street);
            var playerOwnsStreet = player.GetTitleDeeds().Any(x => x.RealStateProperty == street);
            var playerOwnsColorSet = group.GroupOwned(player);
            var streetsEven = group.RealStateProperties.All(x => x.GetCurrentStreetLevel() <= street.GetCurrentStreetLevel());
            if (!streetPartOfColorSet)
                throw new DowngradingStreetFailedException("Street is not part of the Color Set.");
            if (!playerOwnsStreet)
                throw new DowngradingStreetFailedException("Player does not own the titleDeed.", new ValuableNotFoundException());
            if (!playerOwnsColorSet)
                throw new DowngradingStreetFailedException("Player does not own color set.");
            if (!streetsEven)
                throw new DowngradingStreetFailedException("Streets must be degraded evenly.");
        }

        private void ValidateDowngradeHouseStreet(Street street, Player player)
        {
            ValidateDowngradeBuildingStreet(street, player);
            if (street.GetCurrentStreetLevel() == 0)
                throw new DowngradingStreetFailedException("Cannot downgrade more houses.");
            if (street.GetBuildingType() != BuildingType.House)
                throw new DowngradingStreetFailedException("Street does not have a house.");
        }

        public void DowngradeHouseStreet(Street street, Player player)
        {
            ValidateDowngradeHouseStreet(street, player);

            street.DowngradeBuilding(street.Buildings.Last(), player);
        }

        private void ValidateDowngradeHotelStreet(Street street, Player player)
        {
            ValidateDowngradeBuildingStreet(street, player);
            if (player.GetBuildingCount(BuildingType.House) < titleDeedsDic[street].MaxNumHouses)
                throw new DowngradingStreetFailedException("Player does not have houses to put in place of hotel.");
            if (street.GetBuildingType() != BuildingType.Hotel)
                throw new DowngradingStreetFailedException("Street does not have a hotel.");
        }

        public void DowngradeHotelStreet(Street street, Player player)
        {
            ValidateDowngradeHotelStreet(street, player);

            street.DowngradeBuilding(street.Buildings.Last(), player);

            for (int i = 0; i < titleDeedsDic[street].MaxNumHouses; i++)
            {
                street.AddPendingUpgradeBuilding(player.GetBuilding(BuildingType.House));
            }
        }
    }
    public enum StreetColor
    {
        Brown,
        Blue,
        Red,
        Pink,
        Orange,
        Yellow,
        Green,
        Purple
    }
}
