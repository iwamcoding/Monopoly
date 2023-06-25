using BaseMonopoly.Assets.TransactionAssets.TitleDeedAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Commands.ValuableCommands
{
    public class MortgageCommand : Command
    {
        private TitleDeed titleDeed;

        public MortgageCommand(Game game, Player player, TitleDeed titleDeed) : base(game, player)
        {
            this.titleDeed = titleDeed ?? throw new ArgumentNullException(nameof(titleDeed));
        }

        protected override void ExecuteCore()
        {
            ValidatePlayerCommand();

            this.titleDeed.MortgageTitleDeed(this.player);
        }
    }
}
