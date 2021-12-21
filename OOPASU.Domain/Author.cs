using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPASU.Domain
{
    public class Author
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PatronymicName { get; set; }

        //Свойства навигации
        public Guid IntellegentWorkId { get; set; }
        public IntellegentWork IntellegentWork { get; set; }
    }
}
