using BaseMonopoly.Assets.BoardAssets;
using BaseMonopoly.Assets.BoardAssets.ActionCardAssets;
using BaseMonopoly.Assets.BoardAssets.RealStateAssets.StreetAssets;
using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using BaseMonopoly.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BaseMonopoly
{
    public class Game
    {        
        public Bank Bank { get; set; }
        public Player CurrentPlayer { get; set; }
        public Player NextPlayer { get; set; }
        public Player[] AllPlayers { get; }
        public List<Player> PlayersPlaying { get; private set; }
        public Board Board { get; }
        public ColorSet[] ColorSets { get; internal set; }
        public ActionCardDeck CommunityChestDeck { get; }
        public ActionCardDeck ChanceDeck { get; }        

        public Game(Bank bank, List<Player> playersPlaying, Board board, ColorSet[] colorSets, ActionCardDeck communityDeck, ActionCardDeck chanceDeck, int timerTime = 10 * 1000)
        {
            if (bank is null)
                throw new ArgumentNullException(nameof(bank));
           if (playersPlaying is null)           
                throw new ArgumentNullException(nameof(playersPlaying));           
            if (board is null)
                throw new ArgumentNullException(nameof(board));
            if (colorSets is null)
                throw new ArgumentNullException(nameof(colorSets));
            if (communityDeck is null)            
                throw new ArgumentNullException(nameof(communityDeck));            
            if (chanceDeck is null)
                throw new ArgumentNullException(nameof(chanceDeck));

            Bank = bank;
            Board = board;
            ColorSets = colorSets;
            CommunityChestDeck = communityDeck;
            ChanceDeck = chanceDeck;
            PlayersPlaying = playersPlaying;
            AllPlayers = playersPlaying.ToArray();
        }

        public void NextTurn()
        {            
            CurrentPlayer = NextPlayer;
            if (PlayersPlaying.IndexOf(CurrentPlayer) + 1 >= PlayersPlaying.Count)
                NextPlayer = PlayersPlaying[0];
            else
                NextPlayer = PlayersPlaying[PlayersPlaying.IndexOf(CurrentPlayer) + 1];            
        }
        
        public void InitializePlayers()
        {
            PlayersPlaying.ForEach(x => x.RollDice());
            PlayersPlaying = PlayersPlaying.OrderByDescending(x => x.GetDiceSum()).ToList();            
        }

        public void StartGame()
        {
            PlayersPlaying.ForEach(x => { x.EndTurn(); x.DoublesCount = 0; });

            
            CurrentPlayer = PlayersPlaying.First();
            NextPlayer = PlayersPlaying[PlayersPlaying.IndexOf(CurrentPlayer) + 1];
        }

        public static Game CreateStandardGame(PlayerToken[] playerTokens, int[] startingMoney, int timer = 10 * 1000)
        {
            var config = GetStandardConfiguration(playerTokens, startingMoney);

            var bank = config.LoadBank();
            List<Player> players = config.LoadPlayers();
            Board board = config.LoadBoard();
            ColorSet[] colorSets = config.LoadColorSets();
            ActionCardDeck communityDeck = config.LoadCommunityDeck();
            ActionCardDeck chanceDeck = config.LoadChanceDeck();
            return new Game(bank, players, board, colorSets, communityDeck, chanceDeck, timer);
        }                   

        public static Configuration GetStandardConfiguration(PlayerToken[] playerTokens, int[] startingMoney)
        {
            if (playerTokens.Length == 0)
                throw new ArgumentException(nameof(playerTokens));
            if (startingMoney.Length != playerTokens.Length)
                throw new ArgumentException(nameof(startingMoney));

            var playerConfigs = new PlayerConfiguration[playerTokens.Length];
            var playerWalletConfigs = new PlayerWalletConfiguration[playerTokens.Length];
            for (int i = 0; i < playerTokens.Length; i++)
            {
                playerConfigs[i] = new PlayerConfiguration()
                {
                    PlayerToken = playerTokens[i],
                    SpaceNumber = 1
                };
                playerWalletConfigs[i] = new PlayerWalletConfiguration()
                {
                    Money = startingMoney[i],
                    PlayerToken = playerTokens[i],
                };
            }

            var config = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(@$"Monopoly.config"));
            config.PlayerConfigurations = playerConfigs;
            config.PlayerWalletConfigurations = playerWalletConfigs;

            return config;
        }
        public static Configuration GetEmptyStandardConfiguration(IServiceProvider serviceProvider) 
        {
            return JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(@$"Monopoly.config"));
        }
        public static PlayerConfiguration[] CreatePlayerConfiguration(PlayerToken[] playerTokens)
        {
            var playerConfigs = new PlayerConfiguration[playerTokens.Length];            
            for (int i = 0; i < playerTokens.Length; i++)
            {
                playerConfigs[i] = new PlayerConfiguration()
                {
                    PlayerToken = playerTokens[i],
                    SpaceNumber = 1
                };                
            }
            return playerConfigs;
        }
        public static PlayerWalletConfiguration[] CreatePlayerWalletConfiguration(PlayerToken[] playerTokens, int[] startingMoney)
        {
            var walletConfigs = new PlayerWalletConfiguration[playerTokens.Length];
            for (int i = 0; i < playerTokens.Length; i++)
            {
                walletConfigs[i] = new PlayerWalletConfiguration()
                {
                    PlayerToken = playerTokens[i],
                    Money = startingMoney[i]
                };
            }

            return walletConfigs;
        }
    }
}
