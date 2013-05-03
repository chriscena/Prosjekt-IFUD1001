using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sluttprosjekt.Model
{
    public class TransactionCalculator
    {
        private readonly IDataService _dataService;
        public TransactionCalculator(IDataService dataService)
        {
            _dataService = dataService;
        }

        public decimal GetTotalDuePaymentForMember(int memberId)
        {
            var sumPayed = _dataService.GetTransactions().Sum(t => t.Amount);
            var countMembers = _dataService.GetMembers().Count;
            var memberPayed = _dataService.GetTransactions().Where(t => t.PaidBy == memberId).Sum(t => t.Amount);

            return (sumPayed/countMembers) - memberPayed;
        }

        public List<Payment> GetDuePayments(int projectId)
        {
            var duePayments =
                _dataService.GetMembers().Select(m => new DueMemberPayment {Member = m, TotalAmount = GetTotalDuePaymentForMember(m.Id)});

            return GeneratePayments(duePayments);
        }

        private List<Payment> GeneratePayments(IEnumerable<DueMemberPayment> duePayments)
        {
            var orderedDuePayments = duePayments.ToList().OrderBy(p => p.TotalAmount);
            var payments = new List<Payment>();
            while (orderedDuePayments.Any(p => Math.Round(p.TotalAmount,3) != 0.000m))
            {
                var min = orderedDuePayments.First();
                var max = orderedDuePayments.Last();
                var payment = new Payment {Payer = max.Member, Payee = min.Member};
                if (Math.Abs(min.TotalAmount) > max.TotalAmount)
                    payment.Amount = max.TotalAmount;
                else
                    payment.Amount = Math.Abs(min.TotalAmount);
                min.TotalAmount += payment.Amount;
                max.TotalAmount -= payment.Amount;
                payments.Add(payment);
                orderedDuePayments = orderedDuePayments.OrderBy(p => p.TotalAmount);
            }
            return payments;
        }
    }

    public class DueMemberPayment 
    {
        public IMember Member { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class Payment
    {
        public IMember Payer { get; set; }
        public IMember Payee { get; set; }
        public decimal Amount { get; set; }
    }
}
