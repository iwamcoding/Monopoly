using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactionAssets;

namespace BaseMonopoly.Commands.TransactionCommands
{
    public class AcceptTransactionCommand : Command
    {
        private string id;

        public AcceptTransactionCommand(Game game, Player player, string id) : base(game, player)
        {
            this.id = id;
        }

        protected override void ExecuteCore()
        {
            this.player.AcceptTransaction(id);
        }
    }
}
