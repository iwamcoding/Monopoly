using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.BoardAssets
{
    public class Board
    {
        private readonly int totalLocations;
        public ISpace StartingSpace { get; }
        public IEnumerable<ISpace> Spaces { get; private set; }

        public Board(IEnumerable<ISpace> spaces, ISpace? startingSpace = null)
        {
            if (spaces == null) throw new ArgumentNullException(nameof(spaces));
            if (spaces.Count() == 0) throw new ArgumentException(nameof(spaces));
            if (startingSpace != null && !spaces.Contains(startingSpace)) throw new ArgumentException("Starting space not found in spaces.");

            this.Spaces = spaces;
            totalLocations = spaces.Count();
            if (startingSpace != null)
                this.StartingSpace = startingSpace;
            else
                this.StartingSpace = spaces.FirstOrDefault();

            InitializeSpaces();
        }

        private IEnumerable<IPassable> GetPassables()
        {
            return Spaces.Where(x => x is IPassable).Select(x => x as IPassable);
        }

        private IEnumerable<IPassable> GetPassablesPassed(Player player, int spaceNumber)
        {
            var allPassables = GetPassables();
            var passablesPassed = new List<IPassable>();            
            if (spaceNumber > player.SpaceNumber)
            {                
                var passablesBeforeLimit = allPassables.Where(x => x.SpaceNumber > player.SpaceNumber && x.SpaceNumber < totalLocations);

                if (passablesBeforeLimit.Any())
                    passablesPassed.AddRange(passablesBeforeLimit);
            }                
            else
            {                
                var passablesBeforeLimit = allPassables.Where(x => x.SpaceNumber > player.SpaceNumber && x.SpaceNumber < totalLocations);

                if (passablesBeforeLimit.Any())
                    passablesPassed.AddRange(passablesBeforeLimit);

                var passablesAfterLimit = allPassables.Where(x => x.SpaceNumber >= StartingSpace.SpaceNumber && x.SpaceNumber < spaceNumber);

                if (passablesAfterLimit.Any())
                    passablesPassed.AddRange(passablesAfterLimit);
            }                                       

            return passablesPassed;
        }

        private void InitializeSpaces()
        {
            if (StartingSpace.SpaceNumber != 1)
                throw new ArgumentException("The space number of starting space is not 1.");
            if (Spaces.Distinct().Count() != Spaces.Count())
                throw new ArgumentException("Spaces must have different space numbers.");

            Spaces = Spaces.OrderBy(x => x.SpaceNumber);
        }


        public void AdvancePlayer(Player player, ISpace space, int round = 0)
        {
            if (!Spaces.Contains(space))
                throw new ArgumentException("Location does not exist in Board.");
            if (player == null || space == null)
                throw new ArgumentNullException();

            if (round > 0)
            {
                var passables = GetPassables();
                for (int i = 0; i < round; i++)
                {
                    foreach (var p in passables)
                    {
                        p.PlayerPassed(player);
                    }
                }
            }

            var passablesPassed = GetPassablesPassed(player, space.SpaceNumber);
            if (passablesPassed.Any())
            {
                player.LandOn(space, passablesPassed);
            }
            else
                player.LandOn(space);
        }


        public void AdvancePlayer(Player player, int distance, int? source = null)
        {
            if (player == null)
                throw new ArgumentNullException();

            if (source == null)
                source = player.SpaceNumber;

            int destination = source.GetValueOrDefault() + distance;
            if (destination > totalLocations)
            {
                AdvancePlayer(player, destination - totalLocations, 0);
            }
            else
            {
                AdvancePlayer(player, Spaces.Where(x => x.SpaceNumber == (source.GetValueOrDefault() + distance)).FirstOrDefault());
            }
        }
    }
}
