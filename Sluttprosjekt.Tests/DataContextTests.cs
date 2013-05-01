using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sluttprosjekt.Model;

namespace Sluttprosjekt.Tests
{
    [TestClass]
    public class DataContextTests
    {
        [TestMethod]
        public void CommitMember_1Member_Returns1Member()
        {
            var dataContext = new InMemoryDataContext();
            var member = new Member { Id = 1, Name = "Hans" };
            dataContext.Insert(member);
            dataContext.Commit();

            var savedMember = dataContext.Repository<IMember>().Single();
            Assert.AreEqual(member.Id, savedMember.Id);
        }

        [TestMethod]
        public void CommitTransaction_1Transaction_Returns1Amount()
        {
            var dataContext = new InMemoryDataContext();
            var member = new Member {Id = 1, Name = "Hans"};
            dataContext.Insert(member);
            dataContext.Commit();

            var transaction = new Transaction
                {
                    Description = "Hotell",
                    Amount = 1234.56,
                    PaidBy = 1,
                    Id = 1,
                    PaidDate = new DateTime(2013, 04, 01)
                };
            dataContext.Insert(transaction);
            dataContext.Commit();

            var savedTransaction = dataContext.Repository<ITransaction>().Single();
            Assert.AreEqual(transaction.Amount, savedTransaction.Amount);
        }
    }
}
