using BaseMonopoly.Assets.TransactionAssets.TransactableAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseMonopoly.Configurations
{
    public record PlayerWalletConfiguration
    {
        public int Money { get;set; }
        public string[] TitleDeeds { get; set; }
        public PlayerToken PlayerToken { get; set; }
    }
}
