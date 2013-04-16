using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sluttprosjekt.Model
{
    public class ListOfFriends
    {
        [JsonProperty("data")]
        public List<Friend> Data
        {
            get;
            set;
        }
    }
}