namespace BaseMonopoly.Assets.BoardAssets.RealStateAssets.StreetAssets
{
    public class Building
    {
        public bool Available { get; set; }
        public int? Cost { get; private set; }
        public BuildingType BuildingType { get; set; }
        public Building() { }
        public Building(int? cost, BuildingType buildingType)
        {
            Cost = cost;
            BuildingType = buildingType;
        }

        public void ChangeCost(int? cost)
        {
            if (Available)
                Cost = cost;
            else
                throw new InvalidOperationException("Buildig is not availabile.");
        }
    }
    public enum BuildingType
    {
        House,
        Hotel
    }
}
