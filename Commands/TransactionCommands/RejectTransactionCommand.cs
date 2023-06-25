using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactionAssets;

namespace BaseMonopoly.Commands.TransactionCommands
{
    public class RejectTransactionCommand : Command
    {        
        private string id;

        public RejectTransactionCommand(Game game, Player player, string id) : base(game, player)
        {
            this.id = id;
        }

        protected override void ExecuteCore()
        {
            this.player.RejectTransaction(id);
        }
    }
}
