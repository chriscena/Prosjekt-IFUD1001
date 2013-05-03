using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sluttprosjekt.Model
{
    [Database]
    class SpleiselagDataContext : DataContext
    {
        public Table<Project> Projects; 
        public Table<Member> Members;
        public Table<Transaction> Transactions;

        public SpleiselagDataContext(string connection)
            : base(connection) { }
    }

}
