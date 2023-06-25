using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;

namespace BaseMonopoly.Assets.BoardAssets
{
    public class RealStatePropertyGroup<T> : IRealStatePropertyGroup<T> where T : RealStateProperty
    {
        public T[] RealStateProperties { get; protected set; }

        public RealStatePropertyGroup(T[] realStateProperties) { RealStateProperties = realStateProperties; }
        public int GetNumberPropertyOwned(Player player)
        {
            int number = 0;
            var titleDeeds = player.GetTitleDeeds();
            foreach (var property in RealStateProperties)
            {
                if (titleDeeds.Any(x => x.RealStateProperty == property))
                    number++;
            }
            return number;

        }

        public bool GroupOwned(Player player)
        {
            var groupOwned = true;
            var titleDeeds = player.GetTitleDeeds();
            foreach (var property in RealStateProperties)
            {
                var owns = titleDeeds.Any(x => x.RealStateProperty == property);
                if (!owns)
                {
                    groupOwned = false;
                    break;
                }
            }
            return groupOwned;
        }
    }
}
