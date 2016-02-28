using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Dto
{
    public class PeopleOnlineDto
    {
        public int PeopleOnlineID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Ip { get; set; }
        public string Browser { get; set; }
        public System.DateTime LastActivity { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
