using BaseMonopoly.Assets.BoardAssets.RealStateAssets.StreetAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Commands.StreetCommands
{
    public class RequestBuildingCommand : Command
    {
        private BuildingType buildingType;

        public RequestBuildingCommand(Game game, Player player, BuildingType buildingType) : base(game, player)
        {
            this.buildingType = buildingType;
        }

        protected override void ExecuteCore()
        {
            ValidatePlayerCommand();

            this.player.Bank.RequestBuilding(this.player, this.buildingType);
        }
    }
}
