using BaseMonopoly.Assets.BoardAssets.ActionCardAssets;

namespace BaseMonopoly.Configurations
{
    public record ActionConfiguration
    {
        public string Id { get; set; }
        public string Type { get; set; } //"pos", "nearpos", "backpos", "mon", "rep", "rel"
        public string? Transactable { get; set; } //"bank", "players"
        public string? VarRentChanger { get; set; } //"const", "double"
        public int? TempRentValue { get; set; }
        public int[]? PossibleSpaceNumbers { get; set; }
        public int? SpaceNumber { get; set; }
        public int[]? PassableSpaceNumbers { get; set; }
        public int? Amount { get; set; }
        public int? MoneyPerHouse { get; set; }
        public int? MoneyPerHotel { get; set; }
        public MoneyActionType MoneyActionType { get; set; }

        public ActionConfiguration(string id, string type)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public ActionConfiguration()
        {
        }
    }
}
