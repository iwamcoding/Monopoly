using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Exceptions.CommandExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseMonopoly.Commands.BoardCommands
{
    public class NextTurnCommand : Command
    {
        public NextTurnCommand(Game game) : base(game)
        {
        }

        public override void Execute()
        {
            ValidateBid();
            ExecuteCore();
        }
        protected override void ExecuteCore()
        {            
            if (game.CurrentPlayer != null)
                throw new CommandException("Player turn is active");            

            game.NextTurn();
        }
    }
}
