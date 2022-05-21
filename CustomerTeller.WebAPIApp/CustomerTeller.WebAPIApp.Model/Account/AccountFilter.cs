using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutomerTeller.WebAPIApp.Model.Account
{
    public class AccountFilter
    {
        public int PageNo { get; set; }
        public int RecPerPage { get; set; }
        public string CustomerName { get; set; }
        public string AccountName { get; set; }
    }
}
