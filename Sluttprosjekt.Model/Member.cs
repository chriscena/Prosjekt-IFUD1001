using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Sluttprosjekt.Model
{
    /// <summary>
    /// Member entity
    /// </summary>
    [Table(Name = "Members")]
    public class Member
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public int ProjectId { get; set; }

        private EntityRef<Project> _project;
        [Association(IsForeignKey = true, ThisKey = "ProjectId", OtherKey = "Id", Storage = "_project")]
        public Project Project
        {
            get { return _project.Entity; }
            set { _project.Entity = value; }
        }

        private EntitySet<Transaction> _transactions = new EntitySet<Transaction>();
        [Association(ThisKey = "Id", OtherKey = "PaidBy", Storage = "_transactions")]
        public ICollection<Transaction> Transactions
        {
            get { return _transactions; }
            set { _transactions.Assign(value); }
        }

    }
}