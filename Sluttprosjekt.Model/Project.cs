using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sluttprosjekt.Model
{
    public interface IProject : IEntity
    {
        string Name { get; set; }
        bool IsSelected { get; set; }
        ICollection<Member> Members { get; set; }
    }

    [Table(Name = "Projects")]
    public class Project : IProject
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public bool IsSelected { get; set; }

        private EntitySet<Member> _members = new EntitySet<Member>();
        [Association(ThisKey = "Id", OtherKey = "ProjectId", Storage = "_members")]
        public ICollection<Member> Members
        {
            get { return _members; }
            set { _members.Assign(value); }
        }
    }
}
