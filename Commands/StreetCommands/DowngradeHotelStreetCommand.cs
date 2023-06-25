using BaseMonopoly.Assets.BoardAssets.RealStateAssets.StreetAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Commands.StreetCommands
{
    public class DowngradeHotelStreetCommand : Command
    {
        private Street street;

        public DowngradeHotelStreetCommand(Game game, Player player, Street street) : base(game, player)
        {
            this.street = street ?? throw new ArgumentNullException(nameof(street));
        }

        protected override void ExecuteCore()
        {
            ValidatePlayerCommand();

            var titleDeed = this.player.GetTitleDeeds().Where(x => x.RealStateProperty == this.street) as StreetTitleDeed;
            var colorSet = this.game.ColorSets.Where(x => x.StreetTitleDeeds.Contains(titleDeed)).FirstOrDefault();

            colorSet.DowngradeHotelStreet(this.street, this.player);
        }
    }
}
