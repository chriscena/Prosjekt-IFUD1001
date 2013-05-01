using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sluttprosjekt.Model
{
    public interface IDataService
    {
        void SaveProject(Project project);
        List<Project> GetProjects();
        void SetActiveProject(Project activeProject);
        void SaveMember(Member member);
        List<Member> GetMembers();
        Project GetActiveProject();
        void SaveTransaction(Transaction transaction);
    }

    public class DataService : IDataService
    {
        private readonly SpleiselagDataContext _dataContext;
        public DataService()
        {
            _dataContext = new SpleiselagDataContext("isostore:/SpleiseDB.sdf");
            if (_dataContext.DatabaseExists())
                _dataContext.DeleteDatabase();
            if (!_dataContext.DatabaseExists())
                _dataContext.CreateDatabase();
        }

        
        public void SaveProject(Project project)
        {
            _dataContext.Projects.InsertOnSubmit(project);
            _dataContext.SubmitChanges();
        }

        public List<Project> GetProjects()
        {
            return _dataContext.Projects.ToList();
        }

        public void SetActiveProject(Project activeProject)
        {
            var deactivatedProjects = _dataContext.Projects.Where(p => p.IsSelected);
            foreach (var deactived in deactivatedProjects)
            {
                deactived.IsSelected = false;
            }
            var project = _dataContext.Projects.FirstOrDefault(p => p.Id == activeProject.Id);
            if (project != null)
                project.IsSelected = true;

            _dataContext.SubmitChanges();
        }

        public Project GetActiveProject()
        {
            return _dataContext.Projects.SingleOrDefault(p => p.IsSelected);
        }

        public void SaveTransaction(Transaction transaction)
        {
            _dataContext.Transactions.InsertOnSubmit(transaction);
            _dataContext.SubmitChanges();
        }

        public void SaveMember(Member member)
        {
            var activeProject = _dataContext.Projects.Single(p => p.IsSelected);
            member.ProjectId = activeProject.Id;
            _dataContext.Members.InsertOnSubmit(member);
            _dataContext.SubmitChanges();
        }

        public List<Member> GetMembers()
        {
            var activeProject = _dataContext.Projects.FirstOrDefault(p => p.IsSelected);
            if (activeProject == null) return new List<Member>();

            return _dataContext.Members.Where(m => m.ProjectId == activeProject.Id).ToList();
        }
    }
}
