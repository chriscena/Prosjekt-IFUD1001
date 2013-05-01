using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sluttprosjekt.Model
{
    public interface ITransaction : IEntity
    {
        decimal Amount { get; set; }
        int PaidBy { get; set; }
        Member PaidByMember { get; set; }
        DateTime PaidDate { get; set; }
        string Description { get; set; }
    }

    public interface IMember : IEntity
    {
        string Name { get; set; }
        int ProjectId { get; set; }
        //Project Project { get; set; }
    }
}
