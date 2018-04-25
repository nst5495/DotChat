using Dapper.FluentMap.Mapping;
using Domain.DBClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConnection.Mappers
{
    public class Contact_ListMapper : EntityMap<Contact_List>
    {
        public Contact_ListMapper()
        {
            Map(x => x.Ownerid).ToColumn("contact_lists_useraccount_owner");
            Map(x => x.Useraccountid).ToColumn("contact_lists_useraccount_id");
        }
    }
}
