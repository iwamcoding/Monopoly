using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactionAssets;
using BaseMonopoly.Exceptions.JailExceptions;

namespace BaseMonopoly.Assets.BoardAssets.JailAssets
{
    public class Jail : ISpace
    {
        public string Name => "Jail";
        public int SpaceNumber { get; set; }
        public int Fine { get; set; }
        private readonly List<Player> arrestedPlayers;

        public Jail(int spaceNumber, int fine)
        {
            SpaceNumber = spaceNumber;
            Fine = fine;
            arrestedPlayers = new List<Player>();
        }

        public void PlayerLanded(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            if (arrestedPlayers.Contains(player))
            {
                player.Jail();                
                arrestedPlayers.Remove(player);
            }
        }

        public void ArrestPlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            arrestedPlayers.Add(player);
            player.LandOn(this);
        }

        public void TakeFine(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            if (!player.IsJailed)
                throw new ArgumentException("Player is not in jail.");

            new Transaction(Fine, player.Bank, player, TransactionType.give, $"Taking {Fine} fine", player.Release).StartTransaction();
        }

        public void BreakOut(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            if (!player.IsJailed)
                throw new ArgumentException("Player is not in jail.");

            var diceRolled = player.GetDiceSum();
            if (diceRolled.HasValue)
                throw new JailBreakOutException("Player has already attempted to break out in the current turn.");

            if (player.JailDuration == 3)
                TakeFine(player);
            else
            {
                player.RollDice();
                if (player.Dice.DistinctBy(x => x.Number).Count() == 1)
                    player.Release();
            }
        }
    }
}
