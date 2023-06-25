namespace BaseMonopoly.Assets.BoardAssets.ActionCardAssets.VariableRentChangerAssets
{
    public class VariableRentChangerDouble : IVariableRentChanger
    {
        public void ChangeVariableRent(IVariableRent variableRent)
        {
            variableRent.ChangeTempRent(x => x * 2);
        }
    }
}
