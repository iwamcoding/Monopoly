namespace BaseMonopoly.Assets.BoardAssets.ActionCardAssets
{
    public class ActionCardDeck : ICardDeck<ActionCard>
    {
        private int numCardsDrawn;
        private int currentIndex;
        public ActionCard[] ActionCards { get; }
        //private readonly IEnumerable<ActionCard> cards;        
        public ActionCardType ActionCardType { get; }

        public ActionCardDeck(ActionCard[] actionCards)
        {
            if (actionCards == null)
                throw new ArgumentNullException(nameof(actionCards));
            if (!actionCards.Any())
                throw new ArgumentNullException(nameof(actionCards));
            if (actionCards.DistinctBy(x => x.ActionCardType).Count() != 1)
                throw new ArgumentException("All action cards are not of the same type.");

            ActionCardType = actionCards.First().ActionCardType;
            //cards = actionCards;
            this.ActionCards = new ActionCard[actionCards.Length];
            for (int i = 0; i < this.ActionCards.Length; i++)
            {
                this.ActionCards[i] = actionCards[i];
            }
            ShuffleCards();

        }

        public ActionCard GetLastDrawnCard()
        {
            return ActionCards[currentIndex - 1];
        }

        public ActionCard? DrawCard()
        {
            bool cardNotDrawn = true;
            int cardsLeft = ActionCards.Length - numCardsDrawn;            
            ActionCard? card = null;

            do
            {
                if (currentIndex >= ActionCards.Length)
                    currentIndex = 0;
                var nextCard = ActionCards[currentIndex++];
                if (nextCard.Available)
                {
                    cardNotDrawn = false;
                    numCardsDrawn++;
                    card = nextCard;
                }                
            } while (cardNotDrawn || cardsLeft == 0);
            

            if (numCardsDrawn == ActionCards.Length)
            {
                ShuffleCards();
                numCardsDrawn = 0;
            }

            return card;
        }

        public void ShuffleCards()
        {
            var tempList = new List<ActionCard>();            

            var rnd = new Random();
            var cards = ActionCards.ToList();
            var count = ActionCards.Length;

            for (int i = 0; i < count; i++)
            {
                var index = rnd.Next(0, cards.Count);
                tempList.Add(ActionCards[index]);
            }
            for (int i = 0; i < count; i++)
            {
                ActionCards[i] = tempList[i];
            }
        }
    }
}
