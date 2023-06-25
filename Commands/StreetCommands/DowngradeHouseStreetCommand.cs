using BaseMonopoly.Assets.BoardAssets.RealStateAssets.StreetAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Commands.StreetCommands
{
    public class DowngradeHouseStreetCommand : Command
    {
        private Street street;

        public DowngradeHouseStreetCommand(Game game, Player player, Street street) : base(game, player)
        {
            this.street = street ?? throw new ArgumentNullException(nameof(street));
        }

        protected override void ExecuteCore()
        {
            ValidatePlayerCommand();

            var titleDeed = this.player.GetTitleDeeds().Where(x => x.RealStateProperty == this.street) as StreetTitleDeed;
            var colorSet = this.game.ColorSets.Where(x => x.StreetTitleDeeds.Contains(titleDeed)).FirstOrDefault();

            colorSet.DowngradeHouseStreet(this.street, this.player);
        }
    }
}
