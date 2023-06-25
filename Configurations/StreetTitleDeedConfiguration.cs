namespace BaseMonopoly.Configurations
{
    public record StreetTitleDeedConfiguration
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public int BaseRent { get; set; }
        public int[] HouseRents { get; set; }
        public int HotelRent { get; set; }
        public int BuildingCost { get; set; }

        public StreetTitleDeedConfiguration(string name, int value, int baseRent, int[] houseRents, int hotelRent, int buildingCost)
        {
            Name = name;
            Value = value;
            BaseRent = baseRent;
            HouseRents = houseRents;
            HotelRent = hotelRent;
            BuildingCost = buildingCost;
        }

        public StreetTitleDeedConfiguration()
        {

        }
    }
}
