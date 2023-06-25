namespace BaseMonopoly.Assets.BoardAssets
{
    public interface IVariableRent : IRealStateProperty
    {
        public void ChangeTempRent(Func<int, int> func);
    }
}
