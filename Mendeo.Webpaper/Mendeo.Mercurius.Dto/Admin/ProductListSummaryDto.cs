using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Dto.Admin
{
    public class ProductListSummaryDto
    {
        public int ProductID { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public int ProductAnnounceTypeID { get; set; }

        public int SellerTypeID { get; set; }

        public DateTime ModifiedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool Activate { get; set; }

        public int? ProductLastStatusID { get; set; }
    }
}
