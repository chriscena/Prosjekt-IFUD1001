using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sluttprosjekt.Model
{
    public interface IDataService
    {
        void SaveProject(Project project);
        List<Payment> GetPayments(); 
        List<Project> GetProjects();
        void SetActiveProject(Project activeProject);
        void SaveMember(Member member);
        List<Member> GetMembers();
        Project GetActiveProject();
        void SaveTransaction(Transaction transaction);
        List<Transaction> GetTransactions();
    }
}
