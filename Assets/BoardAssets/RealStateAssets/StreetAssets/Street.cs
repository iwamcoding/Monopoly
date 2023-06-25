using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactionAssets;
using BaseMonopoly.Exceptions.StreetExceptions;

namespace BaseMonopoly.Assets.BoardAssets.RealStateAssets.StreetAssets
{
    public class Street : RealStateProperty
    {
        private bool buildingPending;
        public StreetColor Color { get; protected set; }
        public List<Building> Buildings { get; private set; }
        private readonly List<Building> pendingRemovalBuildings;
        private readonly List<Building> pendingAdditionBuilding;

        public Street(int spaceNumber, string name, StreetColor color) : base(spaceNumber, name)
        {
            Color = color;
            Buildings = new();
            pendingAdditionBuilding = new();
            pendingRemovalBuildings = new();
        }

        public BuildingType GetBuildingType()
        {
            return Buildings.First().BuildingType;
        }

        public int GetCurrentStreetLevel()
        {
            if (Buildings.Count == 0)
                return 0;
            if (GetBuildingType() == BuildingType.House)
                return Buildings.Count;
            else
                return Buildings.Count * 5;
        }


        public void AddPendingUpgradeBuilding(Building building)
        {
            building.Available = false;
            pendingAdditionBuilding.Add(building);
            buildingPending = true;
        }

        public void AddPendingDowngradeBuilding(Building building)
        {
            if (!Buildings.Contains(building))
                throw new ArgumentException("Street does not contain building.");
            pendingRemovalBuildings.Add(building);
            buildingPending = true;
        }


        private void BalanceBuildings()
        {
            if (Buildings.Count == 0)
                return;
            var type = Buildings.Last().BuildingType;
            foreach (Building building in Buildings)
            {
                if (building.BuildingType != type)
                {
                    Buildings.Remove(building);
                    building.Available = true;
                }
            }
        }
        private void ApproveBuildings()
        {
            foreach (var building in pendingRemovalBuildings.ToArray())
            {
                building.Available = true;
                Buildings.Remove(building);
                pendingRemovalBuildings.Remove(building);
            }
            foreach (var building in pendingAdditionBuilding.ToArray())
            {
                Buildings.Add(building);
                pendingAdditionBuilding.Remove(building);
            }
            BalanceBuildings();
            buildingPending = false;
        }

        private void DisapproveBuildings()
        {
            pendingRemovalBuildings.Clear();
            pendingAdditionBuilding.ForEach(x => x.Available = true);
            pendingAdditionBuilding.Clear();
            buildingPending = false;
        }


        public void UpgradeBuilding(Building building, Player player)
        {
            if (building == null) throw new ArgumentNullException(nameof(building));
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (buildingPending) throw new BuildingCannotBeAddedException("There are pending buildings.");

            var titleDeed = player.GetTitleDeeds().Where(x => x.RealStateProperty == this).FirstOrDefault() as StreetTitleDeed ?? throw new BuildingCannotBeAddedException("Player does not own title deed.");

            building.ChangeCost(titleDeed.BuildingCost);
            AddPendingUpgradeBuilding(building);

            new Transaction(titleDeed.BuildingCost, player.Bank, player, TransactionType.give, null, ApproveBuildings, DisapproveBuildings).StartTransaction();
        }

        public void DowngradeBuilding(Building building, Player player)
        {
            if (building == null) throw new ArgumentNullException(nameof(building));
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (buildingPending) throw new BuildingCannotBeAddedException("There are pending buildings.");

            var titleDeed = player.GetTitleDeeds().Where(x => x.RealStateProperty == this).FirstOrDefault() as StreetTitleDeed ?? throw new BuildingCannotBeAddedException("Player does not own title deed.");

            AddPendingDowngradeBuilding(building);

            new Transaction(titleDeed.BuildingCost / 2, player, player.Bank, TransactionType.give, null, ApproveBuildings, DisapproveBuildings).StartTransaction();
        }
    }
}
