using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.BoardAssets.ActionCardAssets
{
    public abstract class ActionCard
    {
        public bool Available => Action.Available;
        public string Message { get; set; }
        public abstract ActionCardType ActionCardType { get; }
        public IAction Action { get; set; }

        public ActionCard(IAction action, string message)
        {
            Message = message;
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Draw(Player player)
        {
            if (Available)
                Action.Execute(player);
        }
    }
    public enum ActionCardType
    {
        chance,
        communitychest
    }
}
