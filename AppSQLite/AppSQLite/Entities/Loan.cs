using AppSQLite.Entities.Base;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSQLite.Entities
{
    public class Loan : EntityBase
    {

        [ForeignKey(typeof(Customer))]
        public int CustomerId { get; set; }

        //[ManyToOne]
        //public Customer Customer { get; set; }

        public int Valor { get; set; }

        public string Texto { get; set; }
    }
}
