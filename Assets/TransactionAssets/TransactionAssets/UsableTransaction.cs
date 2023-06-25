using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.TransactionAssets.TransactionAssets
{
    public class UsableTransaction : TradeTransaction
    {
        public UsableTransaction(int amount, Player payee, Player payer, IUsable usable, string? desc = null, Action? onSuccess = null, Action? onFail = null) : base(amount, payee, payer, usable, TransactionType.give, desc, onSuccess, onFail)
        {

        }
    }
}
