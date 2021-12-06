using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPASU.Domain
{
    public class IntellegentWork
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string GRNTI { get; set; }
        public string DOI { get; set; }
        public string Place { get; set; }
        public int Year { get; set; }
        public string Status { get; set; }

        //Свойства навигации
        public List<Author> Authors { get; set; }
        public List<KeyWord> KeyWords { get; set; }
        public List<Indexation> Indexations { get; set; }

    }
}
