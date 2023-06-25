using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactionAssets;

namespace BaseMonopoly.Commands.TransactionCommands
{
    public class BidCommand : Command
    {
        private Auction auction;
        private int amount;

        public BidCommand(Game game, Player player, Auction auction, int amount) : base(game, player)
        {
            this.auction = auction ?? throw new ArgumentNullException(nameof(auction));
            this.amount = amount;
        }

        public override void Execute()
        {
            ExecuteCore();
        }
        protected override void ExecuteCore()
        {
            this.auction.IncreaseBid(this.player, this.amount);
        }
    }
}
