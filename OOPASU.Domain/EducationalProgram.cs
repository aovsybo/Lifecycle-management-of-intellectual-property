using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPASU.Domain
{
    public class EducationalProgram
    {
        public Guid Id { get; set; }
        public string EdProgramName { get; set; }
        //Свойства навигации
        public Guid BookId { get; set; }
        public Book Books { get; set; }
    }
}
