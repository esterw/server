using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angularAppaCore.ViewModel
{
//  [JsonObject(MemberSerialization.OptOut)]
  public class TokenRequestViewModel
  {
    public string username { get; set; }
    public string password { get; set; }
 
  }
}
