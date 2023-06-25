namespace BaseMonopoly.Configurations
{
    public record BankConfiguration
    {        
        public int NumHouses { get; set; }
        public int NumHotels { get; set; }

        public BankConfiguration(int numHouses, int numHotel)
        {            
            NumHouses = numHouses;
            NumHotels = numHotel;
        }

        public BankConfiguration()
        {
        }
    }
}