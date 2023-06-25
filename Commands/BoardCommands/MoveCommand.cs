using BaseMonopoly.Assets.BoardAssets.JailAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Exceptions.CommandExceptions;

namespace BaseMonopoly.Commands.BoardCommands
{
    public class MoveCommand : Command
    {
        public MoveCommand(Game game, Player player) : base(game, player)
        {
        }

        protected override void ExecuteCore()
        {                            
            ValidateBoardCommands();

            if (this.player.GetDiceSum() == null)
                this.player.RollDice();

            this.game.Board.AdvancePlayer(this.player, this.player.GetDiceSum().Value);

            if (this.player.DoublesCount > 0 && this.player.DoublesCount < 3)
            {
                this.game.NextPlayer = this.player;
            }
            else if (this.player.DoublesCount == 3)
            {
                (this.game.Board.Spaces.Where(x => x is Jail).First() as Jail).ArrestPlayer(this.player);
            }
        }
    }
}
