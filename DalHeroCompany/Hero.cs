using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalHeroCompany
{
    public class Hero
    {
        public string Name{ get; set; }

        public string Ability { get; set; } = "attacker" ?? "defender";
        public Guid Id{ get; set; }
        public DateTime TrainBeginning{ get; set; }
        public string SuitColors { get; set; }
        public int StartingPower { get; set;}
        public int CurrentPower { get; set;}
        public Guid TrainerId { get; set; }
        //optinal
        public int TrainCount { get; set;}
       
    }
}
