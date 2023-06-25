using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Assets.TransactionAssets.WalletAssets;
using BaseMonopoly.Exceptions.TransactionExceptions;

namespace BaseMonopoly.Assets.TransactionAssets.TransactionAssets
{
    public class Transaction
    {
        protected static int transactionNumber;
        protected static string GenerateID()
        {
            return $"{transactionNumber++}";

        }
        public string ID { get; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int AmountToPay { get => Amount - amountPayed.Sum(); }
        public bool PayeeAuthorized { get; protected set; }
        public bool PayerAuthorized { get; protected set; }
        public Transactable Payee { get; set; }
        public Transactable Payer { get; set; }
        public IReadOnlyList<int> AmountPayed { get => amountPayed; }
        protected List<int> amountPayed;
        protected Wallet payeeWallet;
        protected Wallet payerWallet;
        protected Action? onSuccess;
        protected Action? onFailure;
        public TransactionType TransactionType { get; protected set; }
        public TransactionStatus TransactionStatus { get; protected set; }
        public TransactionState TransactionState { get; protected set; }
        public TransactionResult TransactionResult { get; protected set; }

        public Transaction(int amount, Transactable payee, Transactable payer, TransactionType transactionType, string? description = null, Action? onSuccess = null, Action? onFail = null)
        {
            Amount = amount;
            Payee = payee;
            Payer = payer;
            TransactionType = transactionType;
            TransactionState = TransactionState.close;
            TransactionStatus = TransactionStatus.idle;
            TransactionResult = TransactionResult.unavailable;
            amountPayed = new();
            this.onSuccess = onSuccess;
            onFailure = onFail;
            ID = GenerateID();
            this.Description = description;
        }

        protected void ValidateTransactionStateOpen()
        {
            if (TransactionState != TransactionState.open)
                throw new TransactionInvalidStateException();
        }
        protected void ValidateTransactionType(List<TransactionType> requiredType)
        {
            bool found = false;
            foreach (var type in requiredType)
            {
                if (TransactionType == type)
                    found = true;
            }

            if (!found)
                throw new TransactionInvalidTypeException();
        }
        protected void ValidateTransactableValid(Transactable transactable)
        {
            if (transactable != Payer && transactable != Payee)
                throw new ArgumentException("Transactable is not part of the transaction.");
        }

        public virtual void StartTransaction()
        {
            TransactionState = TransactionState.open;
            TransactionStatus = TransactionStatus.pendingAuthentication;

            payeeWallet = Authenticate(Payee);
            payerWallet = Authenticate(Payer);

            if (Payer is Bank b)
                b.AcceptTransaction(this.ID);
            if (Payee is Bank bank)
                bank.AcceptTransaction(this.ID);

            TransactionStatus = TransactionStatus.idle;

            if (TransactionType == TransactionType.temporary)
            {
                if (!PayeeAuthorized)
                    Payee.AcceptTransaction(this.ID);
                if (!PayerAuthorized)
                    Payer.AcceptTransaction(this.ID);
            }
        }


        protected virtual Wallet Authenticate(Transactable transactable)
        {
            return transactable.AuthenticateTransaction(this);
        }


        public void AuthorizeTransaction(Transactable transactable)
        {
            ValidateAuthorizeTransaction(transactable);

            AuthorizeTransactionCore(transactable);

            if (PayeeAuthorized && PayerAuthorized)
            {
                TransactionStatus = TransactionStatus.idle;
                CommitTransaction();
            }

            if (TransactionResult == TransactionResult.success || TransactionStatus == TransactionStatus.cancelled)
                FinishTransaction();
        }

        protected virtual void ValidateAuthorizeTransaction(Transactable transactable)
        {
            if (transactable == null)
                throw new ArgumentNullException(nameof(transactable));
            ValidateTransactableValid(transactable);
            ValidateTransactionStateOpen();
        }

        protected virtual void AuthorizeTransactionCore(Transactable transactable)
        {
            TransactionStatus = TransactionStatus.pendingAuthorization;
            if (transactable == Payee)
            {
                PayeeAuthorized = true;
            }
            else
            {
                PayerAuthorized = true;
            }
        }


        public void CancelTransaction(Transactable transatable)
        {
            ValidateCancelTransaction(transatable);
            CancelTransactionCore();
        }

        protected virtual void ValidateCancelTransaction(Transactable transatable)
        {
            if (transatable == null)
                throw new ArgumentNullException(nameof(transatable));
            ValidateTransactableValid(transatable);
            ValidateTransactionStateOpen();
            ValidateTransactionType(new List<TransactionType>() { TransactionType.give });

        }

        protected virtual void CancelTransactionCore()
        {
            if (TransactionResult == TransactionResult.failed)
                RollBackTransaction();
            else
                TransactionStatus = TransactionStatus.cancelled;

            FinishTransaction();
        }


        protected void CommitTransaction()
        {
            ValidateCommitTransaction();
            CommitTransactionCore();
        }

        protected virtual void ValidateCommitTransaction()
        {
            ValidateTransactionStateOpen();

        }

        protected virtual void CommitTransactionCore()
        {
            int i;
            var paymentSuccess = payerWallet.TryPayAmount(AmountToPay, out i);
            if (paymentSuccess)
            {
                payeeWallet.RecieveAmount(Amount);
                TransactionResult = TransactionResult.success;
            }
            else
            {
                TransactionResult = TransactionResult.failed;
                PayerAuthorized = false;
                amountPayed.Add(i);
                if (TransactionType == TransactionType.temporary)
                    RollBackTransaction();
                throw new TransactionFailedException("Payer does not have enough balance", new InsufficientMoneyException(payerWallet, AmountToPay));
            }
        }


        protected void RollBackTransaction()
        {
            ValidateRollBackTransaction();
            RollBackTransactionCore();
        }

        protected virtual void ValidateRollBackTransaction()
        {
            ValidateTransactionStateOpen();
            ValidateTransactionType(new List<TransactionType> { TransactionType.temporary, TransactionType.give });
        }

        protected virtual void RollBackTransactionCore()
        {
            payerWallet.RecieveAmount(AmountPayed.Sum());
            amountPayed.Clear();

            TransactionStatus = TransactionStatus.cancelled;
        }


        protected virtual void FinishTransaction()
        {
            ValidateTransactionStateOpen();

            TransactionState = TransactionState.close;

            if (TransactionResult == TransactionResult.success)
                SuccessTransaction();
            else
                FailedTransaction();
        }

        protected virtual void SuccessTransaction()
        {
            TransactionResult = TransactionResult.success;

            if (onSuccess != null)
                onSuccess();
        }

        protected virtual void FailedTransaction()
        {
            TransactionResult = TransactionResult.failed;

            if (onFailure != null)
                onFailure();
        }

        public void PayerBankRupt()
        {
            ValidateTransactionStateOpen();

            if (TransactionType != TransactionType.owe)
                CancelTransactionCore();
            else
            {
                payeeWallet.RecieveAmount(payerWallet.Money);
                payerWallet.PayAmount(payerWallet.Money);

                foreach (var valuable in payerWallet.GetValuables())
                {
                    payeeWallet.AddValuable(valuable);
                    payerWallet.RemoveValuable(valuable);
                }
            }

            FinishTransaction();
        }
    }

    public enum TransactionType
    {
        owe,
        give,
        temporary
    }
    public enum TransactionState
    {
        open,
        close
    }
    public enum TransactionStatus
    {
        idle,
        pendingAuthentication,
        pendingAuthorization,
        cancelled
    }
    public enum TransactionResult
    {
        success,
        failed,
        unavailable
    }

}

