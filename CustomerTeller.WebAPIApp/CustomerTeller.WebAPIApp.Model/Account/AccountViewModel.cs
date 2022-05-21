using System.Collections.Generic;
using System.Web.Mvc;

namespace CutomerTeller.WebAPIApp.Model
{
    public class AccountViewModel : AccountModel
    {
        public IEnumerable<SelectListItem> customerList { get; set; }

    }

}
