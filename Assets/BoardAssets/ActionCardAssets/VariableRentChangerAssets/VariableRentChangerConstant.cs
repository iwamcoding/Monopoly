namespace BaseMonopoly.Assets.BoardAssets.ActionCardAssets.VariableRentChangerAssets
{
    public class VariableRentChangerConstant : IVariableRentChanger
    {
        public int Value { get; private set; }
        public VariableRentChangerConstant(int value) { Value = value; }

        public void ChangeVariableRent(IVariableRent variableRent)
        {
            variableRent.ChangeTempRent(x => Value);
        }
    }
}
