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
            Map(x => x.Id).ToColumn("useraccount_id",false);
            Map(x => x.FirstName).ToColumn("useraccount_firstname", false);
            Map(x => x.LastName).ToColumn("useraccount_lastname", false);
            Map(x => x.UserName).ToColumn("useraccount_username", false);
            Map(x => x.Password).ToColumn("useraccount_password", false);
            Map(x => x.UserIcon).ToColumn("useraccount_usericon", false);
            Map(x => x.StatusMessage).ToColumn("useraccount_statusmessage", false);
            Map(x => x.Status).ToColumn("useraccount_status", false);
        }
    }
}
