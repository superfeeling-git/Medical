using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical.Application.Admins.Dto
{
    public class ClaimDto
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string[] roles { get; set; }
    }
}
