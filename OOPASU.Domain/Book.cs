using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPASU.Domain
{
    public class Book : Publication
    {
        public string Organisation { get; set; }
        public int Level { get; set; }
        //Свойства навигации
        public List<EducationalProgram> EducationalPrograms { get; set; }
    }
}
