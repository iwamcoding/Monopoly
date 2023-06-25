using BaseMonopoly.Assets.BoardAssets;
using BaseMonopoly.Assets.BoardAssets.ActionCardAssets;
using BaseMonopoly.Assets.BoardAssets.ActionCardAssets.VariableRentChangerAssets;
using BaseMonopoly.Assets.BoardAssets.JailAssets;
using BaseMonopoly.Assets.BoardAssets.RealStateAssets.StationAssets;
using BaseMonopoly.Assets.BoardAssets.RealStateAssets.StreetAssets;
using BaseMonopoly.Assets.BoardAssets.RealStateAssets.UtilityAssets;
using BaseMonopoly.Assets.TransactionAssets.TitleDeedAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Assets.TransactionAssets.WalletAssets;

namespace BaseMonopoly.Configurations
{ 

    public class Configuration
    {
        protected List<ISpace> spaces = new List<ISpace>();
        public List<ISpace> Spaces { get { return spaces; } }
        public StreetConfiguration[] StreetConfigurations { get; set; }        
        protected Street[] streets;
        public RealStateGroupConfiguration<Street>[] StreetGroupConfigurations { get; set; }        
        protected RealStatePropertyGroup<Street>[] streetGroups;
        public StreetTitleDeedConfiguration[] StreetTitleDeedConfigurations { get; set; }        
        protected StreetTitleDeed[] streetTitleDeeds;
        public ColorSetConfiguration[] ColorSetConfigurations { get; set; }        
        protected ColorSet[] colorSets;

        public StationConfiguration[] StationConfigurations { get; set; }        
        protected Station[] stations;
        public RealStateGroupConfiguration<Station>[] StationGroupConfigurations { get; set; }        
        protected RealStatePropertyGroup<Station>[] stationGroups;
        public StationTitleDeedConfiguration[] StationTitleDeedConfigurations { get; set; }        
        protected StationTitleDeed[] stationTitleDeeds;
        public int StationValue = 200;
        public int StationRent = 25;

        public UtilityConfiguration[] UtilityConfigurations { get; set; }        
        protected Utility[] utilities;
        public RealStateGroupConfiguration<Utility>[] UtilityGroupConfigurations { get; set; }        
        protected RealStatePropertyGroup<Utility>[] utilityGroups;
        public UtilityTitleDeedConfiguration[] UtilityTitleDeedsConfigurations { get; set; }        
        protected UtilityTitleDeed[] utilityTitleDeeds;
        public int UtilityValue = 150;
        public int[] UtilityRent = new int[] { 4, 10 };

        public JailConfiguration JailConfiguration { get; set; }        
        protected Jail jail;
        public int JailFine = 50;
        public GoToJailConfiguration GoToJailConfiguration { get; set; }        
        protected GoToJailSpace goToJailSpace;

        public TaxConfiguration[] TaxConfigurations { get; set; }
        protected Tax[] taxes;

        public GoConfiguration GoConfiguration { get; set; }        
        protected Go go;

        public ParkingSpaceConfiguration ParkingSpaceConfiguration { get; set; }
        protected ParkingSpace parkingSpace;

        public ActionConfiguration[] ActionConfigurations { get; set; }        
        protected IAction[] actions;
        public CardsConfiguration[] ActionCardsConfigurations { get; set; }        
        public ActionCardPlaceConfiguration[] ActionPlacesConfiguration { get; set; }

        protected CommunityChestCard[] communityChests;
        protected ActionCardDeck communityChestDeck;
        protected ActionCardPlace[] communityPlaces;
        
        protected ChanceCard[] chanceCards;
        protected ActionCardDeck chanceDeck;
        protected ActionCardPlace[] chancePlaces;

        public BankConfiguration BankConfiguration { get; set; }        
        protected Bank bank;
        public BankWalletConfiguration BankWalletConfiguration { get; set; }
        protected BankWalletConfiguration bankWalletConfiguration;
        public PlayerConfiguration[] PlayerConfigurations { get; set; }                
        protected List<Player> players;
        public PlayerWalletConfiguration[] PlayerWalletConfigurations { get; set; }
        
        private Board board;
        protected BankWallet bankWallet;

        public Configuration(StreetConfiguration[] streetConfigurations, RealStateGroupConfiguration<Street>[] streetGroupConfigurations,
                            StreetTitleDeedConfiguration[] streetTitleDeedConfigurations, ColorSetConfiguration[] colorSetConfigurations,
                            StationConfiguration[] stationConfigurations, RealStateGroupConfiguration<Station>[] stationGroupConfigurations,
                            StationTitleDeedConfiguration[] stationTitleDeedConfigurations, UtilityConfiguration[] utilityConfigurations,
                            RealStateGroupConfiguration<Utility>[] utilityGroupConfigurations, UtilityTitleDeedConfiguration[] utilityTitleDeedsConfigurations,
                            JailConfiguration jailConfiguration, GoToJailConfiguration goToJailConfiguration, TaxConfiguration[] taxConfigurations,
                            GoConfiguration goConfiguration, BankConfiguration bankConfiguration, BankWalletConfiguration bankWalletConfiguration, ActionConfiguration[] actionConfigurations,
                            CardsConfiguration[] actionCardsConfiguration, ActionCardPlaceConfiguration[] actionPlacesConfiguration,
                            ParkingSpaceConfiguration parkingSpaceConfiguration, PlayerConfiguration[] playerConfigurations, PlayerWalletConfiguration[] playerWalletConfigurations)
        {
            StreetConfigurations = streetConfigurations;
            StreetGroupConfigurations = streetGroupConfigurations;
            StreetTitleDeedConfigurations = streetTitleDeedConfigurations;
            ColorSetConfigurations = colorSetConfigurations;
            StationConfigurations = stationConfigurations;
            StationGroupConfigurations = stationGroupConfigurations;
            StationTitleDeedConfigurations = stationTitleDeedConfigurations;
            UtilityConfigurations = utilityConfigurations;
            UtilityGroupConfigurations = utilityGroupConfigurations;
            UtilityTitleDeedsConfigurations = utilityTitleDeedsConfigurations;
            JailConfiguration = jailConfiguration;
            GoToJailConfiguration = goToJailConfiguration;
            TaxConfigurations = taxConfigurations;
            GoConfiguration = goConfiguration;
            BankConfiguration = bankConfiguration;
            BankWalletConfiguration = bankWalletConfiguration;
            ActionConfigurations = actionConfigurations;
            ActionCardsConfigurations = actionCardsConfiguration;
            ActionPlacesConfiguration = actionPlacesConfiguration;
            ParkingSpaceConfiguration = parkingSpaceConfiguration;
            PlayerConfigurations = playerConfigurations;
            PlayerWalletConfigurations = playerWalletConfigurations;
        }       

        public Street[] LoadStreets()
        {
            if (StreetConfigurations == null)
                throw new InvalidOperationException("Cannot load.");
            if (this.streets != null)
                return this.streets;            

            var streets = new Street[StreetConfigurations.Length];
            for (int i = 0; i < StreetConfigurations.Length; i++)
            {
                streets[i] = new Street(StreetConfigurations[i].SpaceNumber, StreetConfigurations[i].Name, StreetConfigurations[i].Color);
            }

            this.streets = streets;
            this.spaces.AddRange(streets);
            return streets;
        }
        public RealStatePropertyGroup<Street>[] LoadStreetGroups()
        {
            if (StreetGroupConfigurations == null)
                throw new InvalidOperationException("Cannot load.");
            if (streetGroups != null)
                return streetGroups;            

            var streets = this.LoadStreets();
            var groups = new RealStatePropertyGroup<Street>[StreetGroupConfigurations.Length];


            for (int i = 0; i < StreetGroupConfigurations.Length; i++)
            {
                var config = StreetGroupConfigurations[i];
                var names = config.Names;
                var streetsOfGroup = new Street[config.Names.Length];
                for (int j = 0; j < config.Names.Length; j++)
                {
                    streetsOfGroup[j] = streets.Where(x => x.Name == config.Names[j]).First();
                }
                groups[i] = new RealStatePropertyGroup<Street>(streetsOfGroup);
            }

            streetGroups = groups;
            return groups;
        }
        public StreetTitleDeed[] LoadStreetTitleDeeds()
        {
            if (StreetTitleDeedConfigurations == null)
                throw new InvalidOperationException("Cannot load.");

            if (streetTitleDeeds != null)
                return streetTitleDeeds;
            

            var groups = this.LoadStreetGroups();
            var titleDeeds = new StreetTitleDeed[StreetTitleDeedConfigurations.Length];

            var indexTitleDeeds = 0;
            foreach (var group in groups)
            {
                for (int i = 0; i < group.RealStateProperties.Length; i++)
                {
                    var config = StreetTitleDeedConfigurations.Where(x => x.Name == group.RealStateProperties[i].Name).First();                    
                    titleDeeds[indexTitleDeeds] = new StreetTitleDeed(config.Value, config.BaseRent, group.RealStateProperties[i], group, config.HouseRents, config.HouseRents, config.BuildingCost);
                    indexTitleDeeds++;
                }
            }

            streetTitleDeeds = titleDeeds;
            return titleDeeds;
        }
        public ColorSet[] LoadColorSets()
        {
            if (ColorSetConfigurations == null)
                throw new InvalidOperationException("Cannot load.");
            if (colorSets != null)
                return colorSets;
            
            var titleDeeds = this.LoadStreetTitleDeeds();
            var colorSetsToReturn = new ColorSet[ColorSetConfigurations.Length];

            var index = 0;
            for (int i = 0; i < ColorSetConfigurations.Length; i++)
            {
                var config = ColorSetConfigurations[i];
                var titleDeedsToAdd = new StreetTitleDeed[config.Names.Length];
                foreach (var name in config.Names)
                {
                    titleDeedsToAdd[index] = titleDeeds.Where(x => x.Name == name).First();
                    index++;
                }
                index = 0;
                colorSetsToReturn[i] = new ColorSet(titleDeedsToAdd);
            }

            colorSets = colorSetsToReturn;
            return colorSetsToReturn;
        }


        public Station[] LoadStations()
        {
            if (StationConfigurations == null)
                throw new InvalidOperationException("Cannot load.");
            if (stations != null)
                return stations;
            

            var stationsToReturn = new Station[StationConfigurations.Length];
            for (int i = 0; i < stationsToReturn.Length; i++)
            {
                var config = StationConfigurations[i];
                stationsToReturn[i] = new Station(config.SpaceNumber, config.Name);
            }

            this.stations = stationsToReturn;
            this.spaces.AddRange(stationsToReturn);
            return stationsToReturn;
        }
        public RealStatePropertyGroup<Station>[] LoadStationGroup()
        {
            if (StationGroupConfigurations == null)
                throw new InvalidOperationException("Cannot load.");
            if (stationGroups != null)
                return stationGroups;
            

            var groups = new RealStatePropertyGroup<Station>[StationGroupConfigurations.Length];
            var stations = LoadStations();
            for (int i = 0; i < groups.Length; i++)
            {
                var config = StationGroupConfigurations[i];
                var stationsToAdd = new Station[config.Names.Length];
                for (int j = 0; j < config.Names.Length; j++)
                {
                    var station = stations.Where(x => x.Name == config.Names[j]).First();
                    stationsToAdd[j] = station;
                }
                groups[i] = new RealStatePropertyGroup<Station>(stationsToAdd);
            }

            stationGroups = groups;
            return groups;
        }
        public StationTitleDeed[] LoadStationTitleDeeds()
        {
            if (StationTitleDeedConfigurations == null)
                throw new InvalidOperationException("Cannot load.");
            if (stationTitleDeeds != null)
                return stationTitleDeeds;

            var groups = LoadStationGroup();
            var titleDeeds = new StationTitleDeed[StationConfigurations.Length];

            var indexTitleDeed = 0;
            foreach (var group in groups)
            {
                foreach (var station in group.RealStateProperties)
                {
                    titleDeeds[indexTitleDeed] = new StationTitleDeed(this.StationValue, this.StationRent, station, group);
                    indexTitleDeed++;
                }
            }

            stationTitleDeeds = titleDeeds;
            return titleDeeds;
        }


        public Utility[] LoadUtilities()
        {
            if (UtilityConfigurations == null)
                throw new InvalidOperationException("Cannot load utilites.");
            if (utilities != null)
                return utilities;
            

            var utilitiesToReturn = new Utility[UtilityConfigurations.Length];
            for (int i = 0; i < utilitiesToReturn.Length; i++)
            {
                var config = UtilityConfigurations[i];
                utilitiesToReturn[i] = new Utility(config.SpaceNumber, config.Name);
            }

            this.utilities = utilitiesToReturn;
            this.spaces.AddRange(utilitiesToReturn);
            return utilitiesToReturn;
        }
        public RealStatePropertyGroup<Utility>[] LoadUtilityGroups()
        {
            if (UtilityGroupConfigurations == null)
                throw new InvalidOperationException("Cannot load utility groups.");
            if (utilityGroups != null)
                return utilityGroups;
            
            var utils = LoadUtilities();
            var groups = new RealStatePropertyGroup<Utility>[UtilityGroupConfigurations.Length];
            for (int i = 0; i < groups.Length; i++)
            {
                var config = UtilityGroupConfigurations[i];
                var utilitiesToAdd = new Utility[config.Names.Length];
                for (int j = 0; j < config.Names.Length; j++)
                {
                    var utility = utils.Where(x => x.Name == config.Names[j]).First();
                    utilitiesToAdd[j] = utility;
                }
                groups[i] = new RealStatePropertyGroup<Utility>(utilitiesToAdd);
            }

            utilityGroups = groups;
            return groups;
        }
        public UtilityTitleDeed[] LoadUtilityTitleDeeds()
        {
            if (UtilityTitleDeedsConfigurations == null)
                throw new InvalidOperationException("Cannot load utility title deeds.");
            if (utilityTitleDeeds != null)
                return utilityTitleDeeds;
            

            var groups = LoadUtilityGroups();
            var titleDeeds = new UtilityTitleDeed[UtilityTitleDeedsConfigurations.Length];

            var indexTitleDeed = 0;
            foreach (var group in groups)
            {
                foreach (var utility in group.RealStateProperties)
                {                    
                    titleDeeds[indexTitleDeed] = new UtilityTitleDeed(this.UtilityValue, this.UtilityRent, utility, group);
                    indexTitleDeed++;
                }
            }

            utilityTitleDeeds = titleDeeds;
            return titleDeeds;
        }


        public Jail LoadJail()
        {
            if (JailConfiguration == null)
                throw new InvalidOperationException("Cannot load jail.");
            if (this.jail != null)
                return this.jail;            

            var jailToReturn = new Jail(JailConfiguration.SpaceNumber, this.JailFine);

            this.jail = jailToReturn;
            this.spaces.Add(jailToReturn);
            return jailToReturn;
        }
        public GoToJailSpace LoadGoToJail()
        {
            if (GoToJailConfiguration == null)
                throw new InvalidOperationException("Cannot load go to jail.");
            if (this.goToJailSpace != null)
                return this.goToJailSpace;
            

            var jail = LoadJail();
            if (jail.Name != GoToJailConfiguration.JailName)
                throw new InvalidOperationException("Cannot load go to jail. Jail is not valid.");

            this.goToJailSpace = new GoToJailSpace(GoToJailConfiguration.SpaceNumber, jail);
            this.spaces.Add(this.goToJailSpace);
            return this.goToJailSpace;
        }
        public Tax[] LoadTaxes()
        {
            if (TaxConfigurations == null)
                throw new InvalidOperationException("Cannot load taxes.");
            if (this.taxes != null)
                return this.taxes;
            

            var taxes = new Tax[TaxConfigurations.Length];
            var index = 0;
            foreach (var config in TaxConfigurations)
            {
                taxes[index] = new Tax(config.SpaceNumber, config.TaxAmount, config.Name);
                index++;
            }

            this.taxes = taxes;
            this.spaces.AddRange(taxes);
            return taxes;
        }
        public Go LoadGo()
        {
            if (GoConfiguration == null)
                throw new InvalidOperationException("Cannot Load Go.");
            if (this.go != null)
                return this.go;            

            this.go = new Go(this.GoConfiguration.SpaceNumber, this.GoConfiguration.Salary);
            this.spaces.Add(this.go);
            return this.go;
        }
        public ParkingSpace LoadParkingSpace()
        {
            if (ParkingSpaceConfiguration == null)
                throw new InvalidOperationException("Cannot load parking space.");

            if (this.parkingSpace != null)
                return this.parkingSpace;

            var config = ParkingSpaceConfiguration;
            var space = new ParkingSpace(config.SpaceNumber);
            this.parkingSpace = space;
            this.spaces.Add(this.parkingSpace);
            return this.parkingSpace;
        }

        public BankWallet LoadBankWallet()
        {
            if (this.BankWalletConfiguration == null)
                throw new InvalidOperationException("Cannot load bank configuration");
            if (this.bankWallet != null)
                return this.bankWallet;

            var walletConfig = this.BankWalletConfiguration;
            var bankWallet = new BankWallet(walletConfig.Money);
            var titleDeeds = new List<TitleDeed>();
            titleDeeds.AddRange(LoadStreetTitleDeeds());
            titleDeeds.AddRange(LoadStationTitleDeeds());
            titleDeeds.AddRange(LoadUtilityTitleDeeds());

            foreach (var name in walletConfig.TitleDeeds)
            {
                bankWallet.AddValuable(titleDeeds.Where(x => x.Name == name).FirstOrDefault());
            }
            this.bankWallet = bankWallet;

            return bankWallet;
        }
        public Bank LoadBank()
        {
            if (this.BankConfiguration == null)
                throw new InvalidOperationException("Cannot load bank configuration");
            if (bank != null)
                return this.bank;

            var config = this.BankConfiguration;

            var bankWallet = LoadBankWallet();
            

            this.bank = new Bank(bankWallet, config.NumHouses, config.NumHotels);
            return this.bank;
        }                
        public virtual List<Player> LoadPlayers()
        {
            if (PlayerConfigurations == null)
                throw new InvalidOperationException("Cannot load players.");
            if (PlayerWalletConfigurations == null)
                throw new InvalidOperationException("Cannot load players.");
            if (this.players != null)
                return players;

            var playersToReturn = new List<Player>();
            var index = 0;
            foreach (var config in PlayerConfigurations)
            {
                var walletConfig = PlayerWalletConfigurations.Where(x => x.PlayerToken == config.PlayerToken).FirstOrDefault();
                var wallet = new PlayerWallet(walletConfig.Money);
                var titleDeeds = new List<TitleDeed>();
                titleDeeds.AddRange(LoadStreetTitleDeeds());
                titleDeeds.AddRange(LoadStationTitleDeeds());
                titleDeeds.AddRange(LoadUtilityTitleDeeds());
                if (walletConfig.TitleDeeds != null)
                {
                    foreach (var name in walletConfig.TitleDeeds)
                    {
                        wallet.AddValuable(titleDeeds.Where(x => x.Name == name).FirstOrDefault());
                    }
                }                    
                playersToReturn.Add(new Player(wallet, LoadBank(), config.PlayerToken, 1));
                index++;
            }

            this.players = playersToReturn;
            return playersToReturn;
        }

        private IAction LoadAction(ActionConfiguration actionConfiguration)
        {
            IAction action = null;
            switch (actionConfiguration.Type)
            {
                case "pos":
                    if (actionConfiguration.PassableSpaceNumbers != null)
                        action = new PositionAction(this.spaces.Where(x => x.SpaceNumber == actionConfiguration.SpaceNumber).First(), this.spaces.Where(x => x is IPassable).Select(x => x as IPassable).ToArray());
                    else
                        action = new PositionAction(this.spaces.Where(x => x.SpaceNumber == actionConfiguration.SpaceNumber).First());
                    break;
                case "nearpos":
                    var possibleSpaces = new IVariableRent[actionConfiguration.PossibleSpaceNumbers.Length];
                    for (int i = 0; i < possibleSpaces.Length; i++)
                    {
                        possibleSpaces[i] = this.spaces.Where(x => x.SpaceNumber == actionConfiguration.PossibleSpaceNumbers[i]).First() as IVariableRent;
                    }
                    if (actionConfiguration.VarRentChanger == "double")
                        action = new NearestPositionRentAction(possibleSpaces, new VariableRentChangerDouble());
                    else
                        action = new NearestPositionRentAction(possibleSpaces, new VariableRentChangerConstant(actionConfiguration.TempRentValue.Value));
                    break;
                case "backpos":
                    var spacesToLandOn = new ISpace[actionConfiguration.PossibleSpaceNumbers.Length];
                    for (int i = 0; i < spacesToLandOn.Length; i++)
                    {
                        spacesToLandOn[i] = this.spaces.Where(x => x.SpaceNumber == actionConfiguration.PossibleSpaceNumbers[i]).First();
                    }
                    action = new GoBackPositionAction(spacesToLandOn);
                    break;
                case "mon":
                    if (actionConfiguration.Transactable == "bank")
                        action = new MoneyAction(actionConfiguration.Amount.Value, new Transactable[] { LoadBank() }, actionConfiguration.MoneyActionType);
                    else
                        action = new MoneyAction(actionConfiguration.Amount.Value, LoadPlayers(), actionConfiguration.MoneyActionType);
                    break;
                case "rep":
                    action = new StreetRepairAction(actionConfiguration.MoneyPerHouse.Value, actionConfiguration.MoneyPerHotel.Value);
                    break;
                case "rel":
                    action = new GetOutOfJailAction();
                    break;
                default:
                    break;
            }
            return action;
        }

        public CommunityChestCard[] LoadCommunityCards()
        {
            if (this.ActionCardsConfigurations == null)
                throw new InvalidOperationException("Cannot load community cards.");
            if (this.communityChests != null)
                return this.communityChests;
            LoadGo();
            LoadGoToJail();
            LoadBank();
            
            Array.ForEach(ActionCardsConfigurations, x => x.CardType = x.CardType.ToLower());
            Array.ForEach(ActionCardsConfigurations, x => x.Id = x.Id.ToLower());
            Array.ForEach(ActionConfigurations, x => x.Id = x.Id.ToLower());

            foreach (var item in ActionConfigurations)
            {
                if (item.Transactable != null)
                    item.Transactable = item.Transactable.ToLower();

            }

            var communityConfigurations = ActionCardsConfigurations.Where(x => x.CardType == "cc").ToArray();
            var cards = new CommunityChestCard[communityConfigurations.Length];
            var index = 0;
            foreach (var config in communityConfigurations)
            {
                var actionConfig = ActionConfigurations.Where(x => x.Id == config.Id).First();
                var action = LoadAction(actionConfig);
                cards[index] = new CommunityChestCard(action, config.Description);
                index++;
            }

            this.communityChests = cards;
            return cards;
        }
        public ActionCardDeck LoadCommunityDeck()
        {
            if (this.communityChestDeck != null)
                return this.communityChestDeck;

            this.communityChestDeck = new ActionCardDeck(LoadCommunityCards());
            return this.communityChestDeck;
        }
        public ActionCardPlace[] LoadCommunityChestPlaces()
        {
            if (ActionPlacesConfiguration == null)
                throw new InvalidOperationException("Cannot load community chest places.");
            if (this.communityPlaces != null)
            {
                return this.communityPlaces;
            }

            var config = ActionPlacesConfiguration.Where(x => x.Type == "cc").ToArray();
            var communityPlaces = new ActionCardPlace[config.Length];
            this.communityPlaces = new ActionCardPlace[communityPlaces.Length];
            for (var i = 0; i < config.Length; i++)
            {
                communityPlaces[i] = new ActionCardPlace(config[i].SpaceNumber, LoadCommunityDeck());
                this.communityPlaces[i] = communityPlaces[i];
            }

            this.spaces.AddRange(communityPlaces);
            return communityPlaces;
        }


        public ChanceCard[] LoadChanceCard()
        {
            if (this.ActionCardsConfigurations == null)
                throw new InvalidOperationException("Cannot load chance cards.");


            if (this.chanceCards != null)
                return this.chanceCards;

            Array.ForEach(ActionCardsConfigurations, x => x.CardType = x.CardType.ToLower());
            Array.ForEach(ActionCardsConfigurations, x => x.Id = x.Id.ToLower());
            Array.ForEach(ActionConfigurations, x => x.Id = x.Id.ToLower());

            foreach (var item in ActionConfigurations)
            {
                if (item.Transactable != null)
                    item.Transactable = item.Transactable.ToLower();

            }

            LoadBoard(); 
            var chanceConfig = this.ActionCardsConfigurations.Where(x => x.CardType == "c").ToArray();
            var cards = new ChanceCard[chanceConfig.Length];
            for (int i = 0; i < chanceConfig.Length; i++)
            {
                var config = chanceConfig[i];
                var actionConfig = this.ActionConfigurations.Where(x => x.Id == config.Id).First();
                var action = LoadAction(actionConfig);
                cards[i] = new ChanceCard(action, config.Description);
            }

            this.chanceCards = cards;
            return cards;
        }
        public ActionCardDeck LoadChanceDeck()
        {
            if (this.chanceDeck != null)
                return this.chanceDeck;

            this.chanceDeck = new ActionCardDeck(LoadChanceCard());
            return this.chanceDeck;
        }
        public ActionCardPlace[] LoadChancePlaces()
        {
            if (ActionPlacesConfiguration == null)
                throw new InvalidOperationException("Cannot load community chest places.");

            if (this.chancePlaces != null)
                return this.chancePlaces;

            var config = ActionPlacesConfiguration.Where(x => x.Type == "c").ToArray();
            var chancePlaces = new ActionCardPlace[config.Length];
            this.chancePlaces = new ActionCardPlace[config.Length];
            for (int i = 0; i < config.Length; i++)
            {
                chancePlaces[i] = new ActionCardPlace(config[i].SpaceNumber, LoadCommunityDeck());
                this.chancePlaces[i] = chancePlaces[i];
            }

            this.spaces.AddRange(chancePlaces);
            return chancePlaces;
        }

        //public Dictionary<ActionCard, Image>
        public Board LoadBoard()
        {
            if (this.board != null)
                return this.board;

            LoadGo();
            LoadJail();
            LoadGoToJail();
            LoadParkingSpace();
            LoadTaxes();
            LoadCommunityChestPlaces();
            LoadChancePlaces();
            LoadStreets();
            LoadStations();
            LoadUtilities();

            var board = new Board(this.spaces, LoadGo());
            this.board = board;

            return board;
        }
    }
}
