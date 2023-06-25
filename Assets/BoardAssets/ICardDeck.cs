namespace BaseMonopoly.Assets.BoardAssets
{
    public interface ICardDeck<T> where T : ActionCardAssets.ActionCard
    {
        public T? DrawCard();
        public void ShuffleCards();
    }
}
