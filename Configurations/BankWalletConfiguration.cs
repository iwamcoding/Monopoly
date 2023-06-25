using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseMonopoly.Configurations
{
    public record BankWalletConfiguration
    {
        public int Money { get; set; }
        public string[] TitleDeeds { get; set; }
    }
}
