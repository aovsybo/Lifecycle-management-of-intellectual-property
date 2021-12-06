using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPASU.Domain.DTO
{
    public class AuthorOutDTO
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
}
