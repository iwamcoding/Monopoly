using BaseMonopoly.Assets.BoardAssets.RealStateAssets.StreetAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Commands.StreetCommands
{
    public class ReturnBuildingCommand : Command
    {
        private Building building;

        public ReturnBuildingCommand(Game game, Player player, Building building) : base(game, player)
        {
            this.building = building ?? throw new ArgumentNullException(nameof(building));
        }

        protected override void ExecuteCore()
        {
            ValidatePlayerCommand();

            this.player.Bank.ReturnBuilding(this.player, this.building);
        }
    }
}
