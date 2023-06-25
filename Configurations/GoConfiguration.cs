namespace BaseMonopoly.Configurations
{
    public record GoConfiguration
    {
        public int SpaceNumber { get; set; }
        public int Salary { get; set; }

        public GoConfiguration(int spaceNumber, int salary)
        {
            SpaceNumber = spaceNumber;
            Salary = salary;
        }

        public GoConfiguration()
        {
        }
    }
}
