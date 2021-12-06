using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPASU.Domain.DTO
{
    public class BookOutDTO
    {
        public Guid Id { get; set; }
        public string Organisation { get; set; }
        public int Level { get; set; }
    }
}
