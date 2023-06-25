namespace BaseMonopoly.Assets.BoardAssets
{
    public class Die
    {
        public int? Number { get; set; }
        private int minValue;
        private int maxValue;

        public Die() : this(1, 6)
        {
            Number = null;
        }

        public Die(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }


        public void Roll()
        {
            Random rnd = new Random();
            Number = rnd.Next(minValue, maxValue);
        }


        public void Clear()
        {
            Number = null;
        }
    }
}
