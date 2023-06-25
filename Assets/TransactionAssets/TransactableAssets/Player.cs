using BaseMonopoly.Assets.BoardAssets;
using BaseMonopoly.Assets.BoardAssets.RealStateAssets.StreetAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactionAssets;
using BaseMonopoly.Assets.TransactionAssets.WalletAssets;
using BaseMonopoly.Exceptions.PlayerExceptions;

namespace BaseMonopoly.Assets.TransactionAssets.TransactableAssets
{
    public class Player : Transactable
    {        
        public int? SpaceNumber { get; protected set; }
        public int DoublesCount { get; set; }
        public int? JailDuration { get; protected set; }
        public bool IsJailed { get; protected set; }
        public bool Moved { get; protected set; }
        public PlayerToken PlayerToken { get; protected set; }
        public Bank Bank { get; set; }
        protected PlayerWallet playerWallet;
        private List<Building> houses;
        private List<Building> hotels;
        private Die[] dice;
        public ISpace Space { get; protected set; }
        public IReadOnlyCollection<Die> Dice { get => dice; }
        public IReadOnlyCollection<Building> Buildings
        {
            get
            {
                var b = new List<Building>();
                b.AddRange(houses);
                b.AddRange(hotels);
                return b;
            }
        }

        public Player(PlayerWallet wallet, Bank bank, PlayerToken playerToken, int? spaceNumber) : base(wallet)
        {
            PlayerToken = playerToken;
            SpaceNumber = spaceNumber;
            Bank = bank; //?? throw new ArgumentNullException(nameof(bank));
            playerWallet = wallet; //?? throw new ArgumentNullException(nameof(wallet));
            houses = new();
            hotels = new();
            dice = new Die[] { new Die(), new Die() };
        }

        public int GetBalance()
        {
            return wallet.Balance;
        }
        public int GetMoney()
        {
            return wallet.Money;
        }
        public int? GetDiceSum()
        {
            int? sum = 0;
            if (dice.Any(x => x.Number == null))
                sum = null;
            else
                sum = dice.Sum(x => x.Number);
            return sum;
        }

        public IUsable[] GetUsables()
        { return playerWallet.Usables.ToArray(); }


        public override int GetBuildingCount(BuildingType buildingType)
        { return buildingType == BuildingType.House ? houses.Count : hotels.Count; }

        public override Building? GetBuilding(BuildingType buildingType)
        { return buildingType == BuildingType.House ? houses.FirstOrDefault() : hotels.FirstOrDefault(); }

        private int GetAllBuildingsCount()
        { return hotels.Count + houses.Count; }

        public override void AddBuilding(Building building)
        {
            if (building.BuildingType == BuildingType.House)
                houses.Add(building);
            else
                hotels.Add(building);
        }

        public override void RemoveBuilding(Building building)
        {
            if (building.BuildingType == BuildingType.House)
                houses.Remove(building);
            else
                hotels.Remove(building);
        }


        public void LandOn(ISpace space)
        {
            if (space == null)
                throw new ArgumentNullException(nameof(space));
            if (IsJailed)
                throw new PlayerInJailException();

            SpaceNumber = space.SpaceNumber;
            Moved = true;
            Space = space;
            space.PlayerLanded(this);
        }

        public void LandOn(ISpace space, IEnumerable<IPassable> passables)
        {
            if (passables == null || passables.Count() == 0)
                throw new ArgumentNullException();

            foreach (IPassable passable in passables)
            {
                passable.PlayerPassed(this);
            }

            LandOn(space);
        }


        public int RollDice()
        {
            Array.ForEach(dice, x => x.Roll());

            if (dice.DistinctBy(x => x.Number).Count() == 1)
                DoublesCount++;
            else
                DoublesCount = 0;

            return dice.Sum(x => x.Number.Value);
        }

        public void ClearDice()
        {
            foreach (var die in dice)
                die.Clear();
        }


        public void Jail()
        {
            IsJailed = true;
            JailDuration = 0;
        }

        public void Release()
        {
            if (!IsJailed)
                throw new InvalidOperationException("Player is not jailed.");

            IsJailed = false;
        }


        public override void BankRupt(Transaction transaction)
        {
            foreach (var usable in playerWallet.Usables)
            {
                usable.Discard();
            }
            base.BankRupt(transaction);
        }

        public void EndTurn()
        {
            if (Transactions.Any(x => x.TransactionResult == TransactionResult.unavailable))
                throw new PlayerEndTurnException("Finish all transactions to end the turn.");
            if (GetAllBuildingsCount() > 0)
                throw new PlayerEndTurnException("Assign or sell all buildings to end the turn.");

            ClearDice();
            Moved = false;
            if (IsJailed)
                JailDuration++;
        }


        public void AddUsable(IUsable usable)
        {
            if (usable == null) throw new ArgumentNullException(nameof(usable));
            wallet.AddValuable(usable);
        }


    }
    public enum PlayerToken
    {
        black,
        blue,
        green,
        pink,
        purple,
        red,
        white,
        yellow
    }
}