using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.BoardAssets.ActionCardAssets
{
    public class PositionAction : IAction
    {
        public bool Available => true;
        public IPassable[]? Passables{ get; }
        public PositionType PositionType{ get; }
        public ISpace Space { get; protected set; }

        public PositionAction(ISpace space, IPassable[]? passables = null)
        {
            this.Passables = passables;
            Space = space;

            if (passables == null)
                PositionType = PositionType.Move;
            else
            {
                if (passables.Length == 0)
                    throw new ArgumentNullException("Passables must be provided.");
                PositionType = PositionType.Advance;
            }
        }

        public virtual void Execute(Player player)
        {
            if (PositionType == PositionType.Advance)
                Advance(player);
            else
                player.LandOn(Space);
        }

        protected virtual void Advance(Player player)
        {
            if (player.SpaceNumber > Space.SpaceNumber)
            {
                var passablesPassed = this.Passables?.Where(x => x.SpaceNumber < this.Space.SpaceNumber && x.SpaceNumber < player.SpaceNumber).ToArray();
                player.LandOn(Space, passablesPassed);
            }
            else
            {
                var passablesPassed = this.Passables?.Where(x => x.SpaceNumber < this.Space.SpaceNumber && x.SpaceNumber > player.SpaceNumber).ToArray();
                player.LandOn(Space, passablesPassed);
            }
        }
    }
    public enum PositionType
    {
        Advance,
        Move
    }
}
