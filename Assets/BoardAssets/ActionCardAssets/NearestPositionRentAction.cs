using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.BoardAssets.ActionCardAssets
{
    public class NearestPositionRentAction : PositionAction
    {
        public IVariableRent[] Spaces { get; }
        public IVariableRentChanger Changer { get; }
        private IVariableRent SpaceToLandOn { get => this.Space as IVariableRent; }

        public NearestPositionRentAction(IVariableRent[] spaces, IVariableRentChanger changer) : base(spaces[0])
        {
            if (spaces == null) throw new ArgumentNullException(nameof(spaces));
            if (spaces.Length == 0) throw new ArgumentException(nameof(spaces));
            if (changer == null) throw new ArgumentNullException(nameof(changer));

            this.Spaces = spaces;
            this.Changer = changer;
        }

        public override void Execute(Player player)
        {
            var inRangeSpaces = this.Spaces.Where(x => x.SpaceNumber > player.SpaceNumber).Select(x => x.SpaceNumber).ToArray();
            if (inRangeSpaces.Length > 0)
            {
                var space = this.Spaces.Where(x => x.SpaceNumber == inRangeSpaces.Min()).First();
                this.Space = space;
            }
            else
            {
                var space = this.Spaces.Where(x => x.SpaceNumber == this.Spaces.Min(x => x.SpaceNumber)).First();
                this.Space = space;
            }


            if (player.Bank.GetTitleDeeds().Where(x => x.RealStateProperty == this.Space).Any())
            {
                base.Execute(player);
            }
            else
            {
                Changer.ChangeVariableRent(SpaceToLandOn);
                base.Execute(player);
            }
        }
    }
}
