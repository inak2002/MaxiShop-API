using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Common
{
    public class ApplicationUser:IdentityUser
    {
        public string firstname {  get; set; }
        public string lastname { get; set; }
    }
}
