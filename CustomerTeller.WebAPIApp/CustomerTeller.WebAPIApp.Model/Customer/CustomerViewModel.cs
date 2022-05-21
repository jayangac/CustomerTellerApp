using System.Collections.Generic;
using System.Web.Mvc;

namespace CutomerTeller.WebAPIApp.Model.Customer
{
   public class CustomerViewModel : CustomerModel
    {
        /// <summary>
        /// Gets or sets the website ids.
        /// </summary> 
        public IEnumerable<SelectListItem> customerList { get; set; }
    }
}
