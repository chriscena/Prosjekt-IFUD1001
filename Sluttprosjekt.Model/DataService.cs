using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Friends.Lib;
using Friends.Lib.Helpers;
using Newtonsoft.Json;

namespace Sluttprosjekt.Model
{
    public class DataService : IDataService
    {
        private const string UrlBase = "http://www.galasoft.ch/labs/friends/handle.ashx?code={0}&{1}={2}&seed={3}";

        public async Task<IList<Friend>> GetFriends()
        {
            var client = new HttpClient();
            var json = await client.GetStringAsync(
                string.Format(
                    UrlBase, Constants.Code, Constants.QueryKeyAction, Constants.ActionGet, DateTime.Now.Ticks));

            var result = JsonConvert.DeserializeObject<ListOfFriends>(json);

            foreach (var friend in result.Data)
            {
                friend.IsDirty = false;
            }

            return result.Data;
        }

        public async Task<string> SaveFriend(Friend friend)
        {
            var client = new HttpClient();

            var uri = new Uri(string.Format(
                UrlBase,
                Constants.Code,
                Constants.QueryKeyAction,
                Constants.ActionSave,
                DateTime.Now.Ticks));

            var json = JsonConvert.SerializeObject(friend);

            try
            {
                var content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(uri, content);
                var result = await response.Content.ReadAsStringAsync();

                friend.IsDirty = false;

                return result;
            }
            catch (Exception)
            {
                return "0";
            }
        }
    }
}