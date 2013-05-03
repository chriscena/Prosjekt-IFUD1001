using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Sluttprosjekt.Model
{
    public interface IEntity
    {
        int Id { get; set; }
    }

    public interface ITransaction : IEntity
    {
        int PaidBy { get; set; }
        DateTime PaidDate { get; set; }
        string Description { get; set; }
        decimal Amount { get; set; }
        Member PaidByMember { get; set; }
    }

    [Table(Name = "Transactions")]
    public class Transaction : ITransaction
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public int PaidBy { get; set; }

        [Column]
        public DateTime PaidDate { get; set; }

        [Column]
        public string Description { get; set; }
        
        [Column]
        public decimal Amount { get; set; }

        private EntityRef<Member> _member;
        [Association(IsForeignKey = true, ThisKey = "PaidBy", OtherKey = "Id", Storage = "_member")]
        public Member PaidByMember
        {
            get { return _member.Entity; }
            set { _member.Entity = value; }
        }

    }
}