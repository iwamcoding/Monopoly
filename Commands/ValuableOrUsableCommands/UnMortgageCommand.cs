using BaseMonopoly.Assets.TransactionAssets.TitleDeedAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Commands.ValuableCommands
{
    public class UnMortgageCommand : Command
    {
        private TitleDeed titleDeed;

        public UnMortgageCommand(Game game, Player player, TitleDeed titleDeed) : base(game, player)
        {
            this.titleDeed = titleDeed ?? throw new ArgumentNullException(nameof(titleDeed));
        }

        protected override void ExecuteCore()
        {
            ValidatePlayerCommand();

            titleDeed.UnMortgageTitleDeed(this.player);
        }
    }
}
