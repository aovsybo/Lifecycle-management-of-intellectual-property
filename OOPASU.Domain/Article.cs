using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPASU.Domain
{
    public class Article : Publication
    {
        public Guid Id { get; set; }
        public string CollectionName { get; set; }
        public int CollectionNumber { get; set; }
        public int CollectionPart { get; set; }
        public int FirstPage { get; set; }
        public int LastPage { get; set; }

    }
}
