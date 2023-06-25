using BaseMonopoly.Assets.BoardAssets.JailAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Commands.BoardCommands
{
    public class BreakOutCommand : Command
    {
        private readonly Jail jail;
        public BreakOutCommand(Game game, Player player, Jail jail = null) : base(game, player)
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
            this.jail.BreakOut(this.player);
        }
    }
}
