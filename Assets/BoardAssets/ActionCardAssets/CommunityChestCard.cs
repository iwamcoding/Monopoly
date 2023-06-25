namespace BaseMonopoly.Assets.BoardAssets.ActionCardAssets
{
    public class CommunityChestCard : ActionCard
    {
        public override ActionCardType ActionCardType => ActionCardType.communitychest;

        public CommunityChestCard(IAction action, string message) : base(action, message)
        {
        }
    }
}
