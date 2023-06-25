using BaseMonopoly.Assets.BoardAssets.RealStateAssets.StreetAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Commands.StreetCommands
{
    public class UpgradeHotelStreetCommand : Command
    {
        private Street street;        

        public UpgradeHotelStreetCommand(Game game, Player player, Street street) : base(game, player)
        {
            this.street = street ?? throw new ArgumentNullException(nameof(street));            
        }

        protected override void ExecuteCore()
        {
            ValidatePlayerCommand();

            var titleDeed = this.player.GetTitleDeeds().Where(x => x.RealStateProperty == this.street) as StreetTitleDeed;
            var colorSet = this.game.ColorSets.Where(x => x.StreetTitleDeeds.Contains(titleDeed)).FirstOrDefault();

            var building = player.GetBuilding(BuildingType.Hotel);
            colorSet.UpgradeHotelStreet(this.street, this.player, building);
        }
    }
}
