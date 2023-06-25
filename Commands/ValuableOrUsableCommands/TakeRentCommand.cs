using BaseMonopoly.Assets.BoardAssets;
using BaseMonopoly.Assets.TransactionAssets.TitleDeedAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseMonopoly.Commands.ValuableOrUsableCommands
{
    public class TakeRentCommand : Command
    {
        private readonly Player payer;
        private readonly IRealStateProperty space;

        public TakeRentCommand(Game game, Player owner, Player payer, IRealStateProperty realState) : base(game, owner)
        {
            this.payer = payer ?? throw new ArgumentNullException(nameof(payer));
            this.space = realState ?? throw new ArgumentNullException(nameof(realState));
        }

        protected override void ExecuteCore()
        {
            this.space.TakeRent(player, payer);
        }
    }
}
