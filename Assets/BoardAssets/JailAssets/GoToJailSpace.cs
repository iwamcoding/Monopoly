using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.BoardAssets.JailAssets
{
    public class GoToJailSpace : ISpace
    {
        public string Name => "Go To Jail";
        public int SpaceNumber { get; set; }
        private Jail jail;
        public GoToJailSpace(int spaceNumber, Jail jail)
        {
            SpaceNumber = spaceNumber;
            this.jail = jail ?? throw new ArgumentNullException(nameof(jail));
        }

        public void PlayerLanded(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            jail.ArrestPlayer(player);
        }
    }
}
