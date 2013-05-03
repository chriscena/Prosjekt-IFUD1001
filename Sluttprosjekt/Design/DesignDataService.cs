using System;
using System.Collections.Generic;
using System.Linq;
using Sluttprosjekt.Model;

namespace Sluttprosjekt.Design
{
    public class DesignDataService : IDataService
    {
        public void SaveProject(Project project)
        {
            throw new NotImplementedException();
        }

        public List<Payment> GetPayments()
        {
            return new List<Payment>
                {
                    new Payment
                        {
                            Payer = GetMembers().Single(m => m.Id == 1),
                            Payee = GetMembers().Single(m => m.Id ==2),
                            Amount = 100.25m
                        },
                    new Payment
                        {
                            Payer = GetMembers().Single(m => m.Id == 3),
                            Payee = GetMembers().Single(m => m.Id ==2),
                            Amount = 55.25m
                        }
                };
        }

        public List<Project> GetProjects()
        {
            return new List<Project>
                {
                    new Project
                        {
                            Id = 1,
                            IsSelected = true,
                            Name = "Spleiselag1"
                        },
                    new Project
                        {
                            Id = 2,
                            Name = "Spleiselag2"
                        }
                };
        }

        public void SetActiveProject(Project activeProject)
        {
            throw new NotImplementedException();
        }

        public void SaveMember(Member member)
        {
            throw new NotImplementedException();
        }

        public List<Member> GetMembers()
        {
            return new List<Member>
                {
                    new Member
                        {
                            Id = 1,
                            Name = "Testperson1",
                            ProjectId = 1
                        },
                    new Member
                        {
                            Id = 2,
                            Name = "Testperson2",
                            ProjectId = 1
                        },
                    new Member
                        {
                            Id = 3,
                            Name = "Testperson3",
                            ProjectId = 1
                        }
                };
        }

        public Project GetActiveProject()
        {
            return GetProjects().Single(p => p.IsSelected);
        }

        public void SaveTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetTransactions()
        {
            return new List<Transaction>
                {
                    new Transaction
                        {
                            Id = 1,
                            Amount = 123.40m,
                            Description = "Hotel",
                            PaidBy = 1,
                            PaidByMember = GetMembers().Single(m => m.Id==1),
                            PaidDate = new DateTime(2013,4,12)
                        },
                    new Transaction
                        {
                            Id = 2,
                            Amount = 321.00m,
                            Description = "Rental car",
                            PaidBy = 2,
                            PaidByMember = GetMembers().Single(m => m.Id==2),
                            PaidDate = new DateTime(2013,4,11)
                        }

                };
        }
    }
}
