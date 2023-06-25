using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.BoardAssets.ActionCardAssets
{
    public class GoBackPositionAction : PositionAction
    {
        public ISpace[] Spaces { get; protected set; }
        public int Distance { get; protected set; }
        public GoBackPositionAction(ISpace[] spaces, int distance = 3) : base(spaces[0])
        {
            if (spaces == null || spaces.Length == 0)
                throw new ArgumentNullException(nameof(spaces));
            this.Spaces = spaces;
            this.Distance = distance;
        }

        public override void Execute(Player player)
        {
            var spaceNumber = player.SpaceNumber - Distance;
            if (spaceNumber < 0)
            {
                this.Space = Spaces.Where(x => x.SpaceNumber == Spaces.Max(x => x.SpaceNumber)).FirstOrDefault();
            }
            else
            {
                this.Space = Spaces.Where(x => x.SpaceNumber == spaceNumber).FirstOrDefault();
            }

            base.Execute(player);
        }
    }
}
