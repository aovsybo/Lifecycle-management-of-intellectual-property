using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPASU.Domain
{
    public class KeyWord
    {
        public Guid Id { get; set; }
        public string Word { get; set; }

        //Свойства навигации
        public Guid IntellegentWorkId { get; set; }
        public IntellegentWork IntellegentWork { get; set; }
    }
}
