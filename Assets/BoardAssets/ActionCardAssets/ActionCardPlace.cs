using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseMonopoly.Assets.BoardAssets.ActionCardAssets
{
    public class ActionCardPlace : ISpace
    {
        public string Name { get; }
        public int SpaceNumber { get; set; }
        public ActionCardDeck CardDeck { get; set; }
        public ActionCardType PlaceType { get => CardDeck.ActionCardType; }
        public ActionCardPlace(int spaceNumber, ActionCardDeck actionCardDeck)
        {
            SpaceNumber = spaceNumber;
            CardDeck = actionCardDeck ?? throw new ArgumentNullException(nameof(actionCardDeck));            
            if (actionCardDeck.ActionCardType == ActionCardType.communitychest)
                Name = "CommunityChest";
            else
                Name = "Chance";
        }
        public void PlayerLanded(Player player)
        {
            CardDeck.DrawCard()?.Draw(player);
        }
    }
}
