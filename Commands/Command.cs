using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactionAssets;
using BaseMonopoly.Exceptions.CommandExceptions;

namespace BaseMonopoly.Commands
{
    public abstract class Command : ICommand
    {
        protected Game game;
        protected Player player;

        public Command(Game game, Player player)
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
            this.player = player ?? throw new ArgumentNullException(nameof(player));
        }
        protected Command(Game game)
        {
            this.game = game ?? throw new ArgumentNullException(nameof (game));
            this.player = null;
        }

        public virtual void Execute()
        {
            ValidateJailedCurrentTurn();
            ValidateBid();
            ExecuteCore();
        }
        protected abstract void ExecuteCore();

        protected void ValidateBoardCommands()
        {
            if (player != game.CurrentPlayer)
                throw new CommandException("Player turn is not active");
            if (player.Moved)
                throw new CommandException("Command not valid after moving.");
        }

        protected void ValidatePlayerCommand()
        {
            if (this.player != this.game.CurrentPlayer) throw new CommandException("Player turn is not active.");
            if (!this.player.Moved) throw new CommandException("Player must move before running this command.");
            if (this.player.IsJailed && this.player.Moved) throw new CommandException("Player is in jail.");
        }

        protected void ValidateBid()
        {
            if (game.Bank.Auctions.Count > 0) throw new CommandException("An Auction is active. Command invalid.");
        }

        protected void ValidateJailedCurrentTurn()
        {
            if (player.IsJailed && player.JailDuration == 0) throw new CommandException("Player went to jail.");
        }
    }
}
