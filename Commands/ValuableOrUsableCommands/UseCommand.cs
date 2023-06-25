using BaseMonopoly.Assets.TransactionAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Commands.ValuableOrUsableCommands
{
    public class UseCommand : Command
    {
        private IUsable usable;

        public UseCommand(Game game, Player player, IUsable usable) : base(game, player)
        {
            this.usable = usable ?? throw new ArgumentNullException(nameof(usable));
        }

        protected override void ExecuteCore()
        {
            ValidatePlayerCommand();

            this.usable.Use(this.player);
        }
    }
}
