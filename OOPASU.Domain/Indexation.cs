using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPASU.Domain
{
    public class Indexation
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }

        //Свойства навигации
        public Guid IntellegentWorkId { get; set; }
        public IntellegentWork IntellegentWork { get; set; }
    }
}
