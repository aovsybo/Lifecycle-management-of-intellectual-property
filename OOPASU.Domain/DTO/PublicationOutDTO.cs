using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPASU.Domain.DTO
{
    public class PublicationOutDTO
    {
        public Guid Id { get; set; }
        public string Publisher { get; set; }
        public string UDK { get; set; }
        public string Editor { get; set; }
        public int PageNumber { get; set; }
    }
}
