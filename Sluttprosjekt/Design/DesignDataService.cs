using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sluttprosjekt.Model;

namespace Sluttprosjekt.Design
{
    public class DesignDataService : IDataService
    {
        public Task<IList<Friend>> GetFriends()
        {
            var result = new List<Friend>();

            for (var index = 0; index < 30; index++)
            {
                var friend = new Friend
                {
                    FirstName = "FirstName" + index,
                    LastName = "LastName" + index,
                    Message = "This is a custom message",
                    PictureUrl = "http://www.galasoft.ch/labs/friends/Data/LogoHead128.png"
                };

                friend.IsDirty = index % 2 == 0;
                result.Add(friend);
            }

            return Task.FromResult((IList<Friend>)result);
        }

        public Task<string> SaveFriend(Friend friend)
        {
            return Task.FromResult("0");
        }
    }
}