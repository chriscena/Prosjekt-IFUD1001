using System;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Sluttprosjekt.Model;

namespace Sluttprosjekt.Tests
{
    [TestClass]
    public class TransactionCalculatorTests
    {
        [TestMethod]
        public void GetTotalDuePaymentForMember_1Member1Transaction_TotalMemberDuePaymentIs0()
        {
            var dbContext = new InMemoryDataContext();
            var project = new Project
                {
                    Id = 1,
                    Name = "Project1",
                    IsSelected = true
                };
            var member = new Member
                {
                    Id = 1,
                    Name = "Member1",
                    ProjectId = 1,
                    Project = project
                };
            project.Members.Add(member);

            var transaction = new Transaction
                {
                    Id = 1,
                    Amount = 10m,
                    Description = "Test transaction",
                    PaidBy = 1,
                    PaidDate = new DateTime(2013,4,1),
                    PaidByMember = member
                };
            member.Transactions.Add(transaction);
            
            dbContext.Insert(project);
            dbContext.Insert(member);
            dbContext.Insert(transaction);
            dbContext.Commit();

            var calculator = new TransactionCalculator(new DataService(dbContext));
            var duePayment = calculator.GetTotalDuePaymentForMember(1);
            Assert.AreEqual(0, duePayment);
        }


        [TestMethod]
        public void GetTotalDuePaymentForMember_2Members1Transaction_TotalMemberDuePaymentIsMinus10And10()
        {
            var dbContext = new InMemoryDataContext();
            var project = new Project
            {
                Id = 1,
                Name = "Project1",
                IsSelected = true
            };
            var member1 = new Member
            {
                Id = 1,
                Name = "Member1",
                ProjectId = 1,
                Project = project
            };
            project.Members.Add(member1);

            var member2 = new Member
            {
                Id = 2,
                Name = "Member2",
                ProjectId = 1,
                Project = project
            };
            project.Members.Add(member2);

            var transaction = new Transaction
            {
                Id = 1,
                Amount = 20.0m,
                Description = "Test transaction",
                PaidBy = 1,
                PaidDate = new DateTime(2013, 4, 1),
                PaidByMember = member1
            };
            member1.Transactions.Add(transaction);

            dbContext.Insert(project);
            dbContext.Insert(member1);
            dbContext.Insert(member2);
            dbContext.Insert(transaction);
            dbContext.Commit();

            var calculator = new TransactionCalculator(new DataService(dbContext));
            var duePayment1 = calculator.GetTotalDuePaymentForMember(1);
            var duePayment2 = calculator.GetTotalDuePaymentForMember(2);
            Assert.AreEqual(-10, duePayment1);
            Assert.AreEqual(10, duePayment2);
        }

        [TestMethod]
        public void GetTotalDuePaymentForMember_2Members2Transaction_TotalMemberDuePaymentIsMinus5And5()
        {
            var dbContext = new InMemoryDataContext();
            var project = new Project
            {
                Id = 1,
                Name = "Project1",
                IsSelected = true
            };
            var member1 = new Member
            {
                Id = 1,
                Name = "Member1",
                ProjectId = 1,
                Project = project
            };
            project.Members.Add(member1);

            var member2 = new Member
            {
                Id = 2,
                Name = "Member2",
                ProjectId = 1,
                Project = project
            };
            project.Members.Add(member2);

            var transaction1 = new Transaction
                {
                    Id = 1,
                    Amount = 20.0m,
                    Description = "Test transaction1",
                    PaidBy = 1,
                    PaidDate = new DateTime(2013, 4, 1),
                    PaidByMember = member1
                };
            member1.Transactions.Add(transaction1);

            var transaction2 = new Transaction
            {
                Id = 2,
                Amount = 10.0m,
                Description = "Test transaction2",
                PaidBy = 2,
                PaidByMember = member2,
                PaidDate = new DateTime(2013, 4, 1)
            };
            member2.Transactions.Add(transaction2);

            dbContext.Insert(project);
            dbContext.Insert(member1);
            dbContext.Insert(member2);
            dbContext.Insert(transaction1);
            dbContext.Insert(transaction2);
            dbContext.Commit();

            var calculator = new TransactionCalculator(new DataService(dbContext));
            var duePayment1 = calculator.GetTotalDuePaymentForMember(1);
            var duePayment2 = calculator.GetTotalDuePaymentForMember(2);
            Assert.AreEqual(-5, duePayment1);
            Assert.AreEqual(5, duePayment2);
        }

        [TestMethod]
        public void GetDuePayments_2Members1Transaction_Returns1Payment()
        {
            var dbContext = new InMemoryDataContext();
            var project = new Project
            {
                Id = 1,
                Name = "Project1",
                IsSelected = true
            };
            var member1 = new Member
            {
                Id = 1,
                Name = "Member1",
                ProjectId = 1,
                Project = project
            };
            project.Members.Add(member1);

            var member2 = new Member
            {
                Id = 2,
                Name = "Member2",
                ProjectId = 1,
                Project = project
            };
            project.Members.Add(member2);

            var transaction = new Transaction
            {
                Id = 1,
                Amount = 20.0m,
                Description = "Test transaction",
                PaidBy = 1,
                PaidDate = new DateTime(2013, 4, 1),
                PaidByMember = member1
            };
            member1.Transactions.Add(transaction);

            dbContext.Insert(project);
            dbContext.Insert(member1);
            dbContext.Insert(member2);
            dbContext.Insert(transaction);
            dbContext.Commit();

            var calculator = new TransactionCalculator(new DataService(dbContext));
            var payments = calculator.GetDuePayments(1);
            Assert.AreEqual(1, payments.Count);
            Assert.AreEqual(member1.Id, payments.First().Payee.Id);
            Assert.AreEqual(member2.Id, payments.First().Payer.Id);
            Assert.AreEqual(10m, payments.First().Amount);
        }

        [TestMethod]
        public void GetDuePayments_3Members2Payments_Returns2Payments()
        {
            var dbContext = new InMemoryDataContext();
            var project = new Project
            {
                Id = 1,
                Name = "Project1",
                IsSelected = true
            };
            var member1 = new Member
            {
                Id = 1,
                Name = "Member1",
                ProjectId = 1,
                Project = project
            };
            project.Members.Add(member1);

            var member2 = new Member
            {
                Id = 2,
                Name = "Member2",
                ProjectId = 1,
                Project = project
            };
            project.Members.Add(member2);

            var member3 = new Member
            {
                Id = 3,
                Name = "Member3",
                ProjectId = 1,
                Project = project
            };
            project.Members.Add(member3);

            var transaction1 = new Transaction
            {
                Id = 1,
                Amount = 40.0m,
                Description = "Test transaction1",
                PaidBy = 1,
                PaidDate = new DateTime(2013, 4, 1),
                PaidByMember = member1
            };
            member1.Transactions.Add(transaction1);

            var transaction2 = new Transaction
            {
                Id = 2,
                Amount = 10.0m,
                Description = "Test transaction2",
                PaidBy = 2,
                PaidByMember = member2,
                PaidDate = new DateTime(2013, 4, 1)
            };
            member2.Transactions.Add(transaction2);

            dbContext.Insert(project);
            dbContext.Insert(member1);
            dbContext.Insert(member2);
            dbContext.Insert(member3);
            dbContext.Insert(transaction1);
            dbContext.Insert(transaction2);
            dbContext.Commit();

            var calculator = new TransactionCalculator(new DataService(dbContext));
            var payments = calculator.GetDuePayments(1);
            Assert.AreEqual(2, payments.Count);
        }
    }
}
