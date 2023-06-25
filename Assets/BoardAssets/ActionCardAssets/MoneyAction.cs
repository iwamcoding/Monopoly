using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactionAssets;

namespace BaseMonopoly.Assets.BoardAssets.ActionCardAssets
{
    public class MoneyAction : IAction
    {
        public int Money { get; }
        public bool Available => true;
        public MoneyActionType MoneyActionType { get; }
        public Transactable[] Transactables { get => transactablesCollection.ToArray(); }
        public IEnumerable<Transactable> transactablesCollection;

        //public MoneyAction(int money, Player[] transactables, MoneyActionType moneyActionType)
        //{
        //    Money = money;
        //    this.Transactables = transactables;
        //    this.MoneyActionType = moneyActionType;
        //}

        //public MoneyAction(int money, Bank transactable, MoneyActionType moneyActionType)
        //{
        //    Money = money;
        //    this.Transactables = new[] { transactable };
        //    this.MoneyActionType = moneyActionType;
        //}

        public MoneyAction(int money, IEnumerable<Transactable> transactables, MoneyActionType moneyActionType)
        {
            Money = money;
            this.transactablesCollection = transactables;
            this.MoneyActionType = moneyActionType;
        }
        public void Execute(Player player)
        {
            if (MoneyActionType == MoneyActionType.loss)
            {
                foreach (var transactable in Transactables)
                {
                    new Transaction(Math.Abs(Money), transactable, player, TransactionType.owe, $"Pay {Money} due to action card").StartTransaction();
                }
            }
            else
            {
                foreach (var transactable in Transactables)
                {
                    new Transaction(Money, player, transactable, TransactionType.owe, $"Take {Money} due to action card").StartTransaction();
                }
            }
        }
    }
    public enum MoneyActionType
    {
        gain,
        loss
    }
}
