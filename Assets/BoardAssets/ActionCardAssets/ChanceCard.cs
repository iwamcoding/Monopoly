namespace BaseMonopoly.Assets.BoardAssets.ActionCardAssets
{
    public class ChanceCard : ActionCard
    {
        public override ActionCardType ActionCardType => ActionCardType.chance;
        public ChanceCard(IAction action, string message) : base(action, message)
        {
        }
    }
}
