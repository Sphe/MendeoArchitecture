using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Mendeo.Mercurius.ExcelBusiness
{
    public interface IProductModule
    {
        DataTable GetAllProductFlatten();

        void UpdateFromProductFlatten(DataTable dt, DataRow row);
    }
}
