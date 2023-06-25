using BaseMonopoly.Assets.TransactionAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Commands.ValuableOrUsableCommands
{
    public class TradeCommand : Command
    {
        private int amount;
        private IValuable valuable;
        private Player payee;

        public TradeCommand(Game game, Player player, IValuable valuable, Player payee, int amount) : base(game, player)
        {
            this.valuable = valuable ?? throw new ArgumentNullException(nameof(valuable));
            this.payee = payee ?? throw new ArgumentNullException(nameof(payee));
            this.amount = amount;
        }

        protected override void ExecuteCore()
        {
            ValidatePlayerCommand();

            this.valuable.Trade(this.payee, this.player, this.amount);
        }
    }
}
