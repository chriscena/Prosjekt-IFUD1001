﻿using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Sluttprosjekt.Model
{
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
        public double Amount { get; set; }

        private EntityRef<Member> _member;
        [Association(IsForeignKey = true, ThisKey = "PaidBy", OtherKey = "Id")]
        public Member PaidByMember
        {
            get { return _member.Entity; }
            set { _member.Entity = value; }
        }

    }
}