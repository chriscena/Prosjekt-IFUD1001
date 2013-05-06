using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using GalaSoft.MvvmLight.Ioc;

namespace Sluttprosjekt.Model
{
    /// <summary>
    /// DataService implementation using <seealso cref="LinqToSqlContext"/>.
    /// </summary>
    public class DataService : IDataService
    {
        private readonly IDataContext _dbContext;
        private readonly TransactionCalculator _calculator;
        
        [PreferredConstructor]
        public DataService()
        {
            var dataContext = new SpleiselagDataContext("isostore:/SpleiseDB.sdf");
            if (!dataContext.DatabaseExists())
                dataContext.CreateDatabase();
            _dbContext = new LinqToSqlContext(dataContext);
            _calculator = new TransactionCalculator(this);
        }

        public DataService(IDataContext dbContext)
        {
            _dbContext = dbContext;
            _calculator = new TransactionCalculator(this);
        }

        
        public void SaveProject(Project project)
        {
            _dbContext.Insert(project);
            _dbContext.Commit();
        }

        public List<Payment> GetPayments()
        {
            var activeProject = GetActiveProject();
            if (activeProject == null) return new List<Payment>();
            return _calculator.GetDuePayments(activeProject.Id);
        }

        public List<Project> GetProjects()
        {
            return _dbContext.Repository<Project>().ToList();
        }

        public void SetActiveProject(Project activeProject)
        {
            var deactivatedProjects = _dbContext.Repository<Project>().Where(p => p.IsSelected);
            foreach (var deactived in deactivatedProjects)
            {
                deactived.IsSelected = false;
            }
            var project = _dbContext.Repository<Project>().FirstOrDefault(p => p.Id == activeProject.Id);
            if (project != null)
                project.IsSelected = true;

            _dbContext.Commit();
        }

        public Project GetActiveProject()
        {
            return _dbContext.Repository<Project>().SingleOrDefault(p => p.IsSelected);
        }

        public void SaveTransaction(Transaction transaction)
        {
            _dbContext.Insert(transaction);
            _dbContext.Commit();
        }

        public List<Transaction> GetTransactions()
        {
            var activeProject = GetActiveProject();

            if (activeProject == null) return new List<Transaction>();

            return _dbContext.Repository<Transaction>()
                               .Where(t => t.PaidByMember.ProjectId == activeProject.Id)
                               .OrderByDescending(t => t.PaidDate)
                               .ToList();
        }

        public void SaveMember(Member member)
        {
            var activeProject = GetActiveProject();

            member.ProjectId = activeProject.Id;
            _dbContext.Insert(member);
            _dbContext.Commit();
        }

        public List<Member> GetMembers()
        {
            var activeProject = GetActiveProject();

            if (activeProject == null) return new List<Member>();
            return _dbContext.Repository<Member>().Where(m => m.ProjectId == activeProject.Id).ToList();
        }

        public List<MemberWithTotalDueAmount> GetMembersWithTotalDueAmount()
        {
            var members = GetMembers();
            return members.Select(Map).ToList();
        }

        public Project DeleteActiveProject()
        {
            
            var activeProject = GetActiveProject();
            if (activeProject == null) return null;
            var membersToDelete = activeProject.Members.ToList();
            var transactionsToDelete = membersToDelete.SelectMany(m => m.Transactions).ToList();
            
            _dbContext.DeleteAll(transactionsToDelete);
            _dbContext.DeleteAll(membersToDelete);
            _dbContext.Delete(activeProject);
            try
            {
                _dbContext.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception("Klarte ikke å slette spleiselaget. Applikasjonen vil nå avslutte så du kan starte på nytt for å slette.", ex);
            }

            if (!_dbContext.Repository<Project>().Any()) return null;

                var projectToSelect =
                    _dbContext.Repository<Project>().OrderBy(p => p.Id).LastOrDefault(p => !p.IsSelected);
            return projectToSelect;
        }

        private MemberWithTotalDueAmount Map(Member member)
        {
            return member == null
                       ? null
                       : new MemberWithTotalDueAmount
                           {
                               Id = member.Id,
                               Name = member.Name,
                               Project = member.Project,
                               ProjectId = member.ProjectId,
                               TotalDueAmount = _calculator.GetTotalDuePaymentForMember(member.Id)
                           };
        } 
    }

    public class MemberWithTotalDueAmount : Member
    {
        public decimal TotalDueAmount { get; set; }
    }
}