using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPASU.Domain.DTO
{
    public class RegistrationSertificateDTO
    {
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public string Number { get; set; }
        public string RegistrationDate { get; set; }
        public string RequestDate { get; set; }
        public string RightHolder { get; set; }
    }
}
