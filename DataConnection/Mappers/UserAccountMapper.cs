using Dapper.FluentMap.Mapping;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConnection.Mappers
{
    public class UserAccountMapper : EntityMap<UserAccount>
    {
        public UserAccountMapper()
        {
            Map(x => x.Id).ToColumn("useraccount_id");
            Map(x => x.FirstName).ToColumn("useraccount_firstname");
            Map(x => x.LastName).ToColumn("useraccount_lastname");
            Map(x => x.UserName).ToColumn("useraccount_username");
            Map(x => x.Password).ToColumn("useraccount_passowrd");
            Map(x => x.UserIcon).ToColumn("useraccount_usericon");
            Map(x => x.StatusMessage).ToColumn("useraccount_statusmessage");
            Map(x => x.Status).ToColumn("useraccount_status");
        }
    }
}
