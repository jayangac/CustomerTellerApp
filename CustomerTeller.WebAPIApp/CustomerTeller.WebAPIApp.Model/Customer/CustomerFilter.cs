using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutomerTeller.WebAPIApp.Model.Customer
{
    public class CustomerFilter
    {
        public int PageNo { get; set; }
        public int RecPerPage { get; set; }
        public string CustomerName { get; set; }
    }
}
