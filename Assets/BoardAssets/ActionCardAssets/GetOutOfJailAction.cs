using BaseMonopoly.Assets.TransactionAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactionAssets;

namespace BaseMonopoly.Assets.BoardAssets.ActionCardAssets
{
    public class GetOutOfJailAction : IAction, IUsable
    {
        private string name = "Get Out of Jail";
        public string Name { get { return name; } set { name = value; } }
        public bool Available { get; private set; }
        public CanBeOwnedBy CanBeOwnedBy => CanBeOwnedBy.Player;
        public GetOutOfJailAction() { Available = true; }

        public void Execute(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            Available = false;
            player.AddUsable(this);
        }

        public void Discard()
        {
            Available = true;
        }

        public void Use(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            if (!player.IsJailed)
                throw new ArgumentException("Player is not in jail.");

            player.Release();
            Available = true;
        }

        public void Trade(Player payee, Player payer, int amount)
        {
            if (payer == null)
                throw new ArgumentNullException(nameof(payer));
            if (payee == null)
                throw new ArgumentNullException(nameof(payer));

            new UsableTransaction(amount, payee, payer, this).StartTransaction();
        }
    }
}
