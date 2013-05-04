using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sluttprosjekt.Model
{
    /// <summary>
    /// Interface for all domain model operations.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Persists the given project entity to underlying store
        /// </summary>
        /// <param name="project"></param>
        void SaveProject(Project project);
        /// <summary>
        /// Gets all payments from the active project
        /// </summary>
        /// <returns></returns>
        List<Payment> GetPayments();
        /// <summary>
        /// Gets all available projects
        /// </summary>
        /// <returns></returns> 
        List<Project> GetProjects();
        /// <summary>
        /// Sets given project as the active project
        /// </summary>
        /// <param name="activeProject"></param>
        void SetActiveProject(Project activeProject);
        /// <summary>
        /// Persists the given member entity to underlying store
        /// </summary>
        void SaveMember(Member member);
        /// <summary>
        /// Gets all members of the active project
        /// </summary>
        /// <returns></returns>
        List<Member> GetMembers();
        /// <summary>
        /// Gets the current active project
        /// </summary>
        /// <returns></returns>
        Project GetActiveProject();
        /// <summary>
        /// Persists the given transaction entity to underlying store
        /// </summary>
        void SaveTransaction(Transaction transaction);
        /// <summary>
        /// Gets all transactions for the active project.
        /// </summary>
        /// <returns></returns>
        List<Transaction> GetTransactions();
        /// <summary>
        /// Gets all members and their total due amount for the active project.
        /// </summary>
        /// <returns></returns>
        List<MemberWithTotalDueAmount> GetMembersWithTotalDueAmount();
    }
}
