using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Models
{
    public class Advertisement : BaseEntity
    {
        public Person Owner { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Views { get; set; }
        public decimal Price { get; set; }
    }
}
