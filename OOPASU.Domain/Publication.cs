using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPASU.Domain
{
    public class Publication : IntellegentWork
    {
        public Guid Id { get; set; }
        public string Publisher { get; set; }
        public string UDK { get; set; }
        public string Editor { get; set; }
        public int PageNumber { get; set; }

    }
}
