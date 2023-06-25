using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Exceptions.CommandExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseMonopoly.Commands.BoardCommands
{
    public class EndTurnCommand : Command
    {
        public EndTurnCommand(Game game, Player player) : base(game, player)
        {
        }

        public override void Execute()
        {
            ValidateBid();
            ExecuteCore();
        }
        protected override void ExecuteCore()
        {
            if (player != game.CurrentPlayer)
                throw new CommandException("Player turn is not active");
            if (!player.Moved)
                throw new CommandException("Command not valid before moving.");

            this.player.EndTurn();
            game.CurrentPlayer = null;

        }
    }
}
