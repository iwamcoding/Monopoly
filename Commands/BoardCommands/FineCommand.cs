using BaseMonopoly.Assets.BoardAssets.JailAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Commands.BoardCommands
{
    public class FineCommand : Command
    {
        private Jail jail;
        public FineCommand(Game game, Player player, Jail jail = null) : base(game, player)
        {
            jail ??= game.Board.Spaces.Where(x => x is Jail).FirstOrDefault() as Jail;
            if (jail == null)
                throw new ArgumentNullException(nameof(jail));
            if (!game.Board.Spaces.Contains(jail))
                throw new ArgumentException("Game does not contain jail.");

            this.jail = jail;
        }

        protected override void ExecuteCore()
        {
            ValidateBoardCommands();

            this.jail.TakeFine(this.player);
        }
    }
}
