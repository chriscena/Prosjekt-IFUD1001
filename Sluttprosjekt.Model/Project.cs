﻿using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sluttprosjekt.Model
{
    /// <summary>
    /// Project entity
    /// </summary>
    [Table(Name = "Projects")]
    public class Project
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public bool IsSelected { get; set; }

        private readonly EntitySet<Member> _members = new EntitySet<Member>();
        [Association(ThisKey = "Id", OtherKey = "ProjectId", Storage = "_members", DeleteRule = "CASCADE")]
        public ICollection<Member> Members
        {
            get { return _members; }
            set { _members.Assign(value); }
        }
    }
}
