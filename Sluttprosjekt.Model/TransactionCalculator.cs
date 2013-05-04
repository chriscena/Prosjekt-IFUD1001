using System;
using System.Collections.Generic;
using System.Linq;

namespace Sluttprosjekt.Model
{
    /// <summary>
    /// Performs calculations for amounts and due payments.
    /// </summary>
    public class TransactionCalculator
    {
        private readonly IDataService _dataService;
        /// <summary>
        /// Performs calculations for amounts and due payments.
        /// </summary>
        /// <param name="dataService"></param>
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

        /// <summary>
        /// Simple greedy strategy, taking the most owed with largest payable.
        /// </summary>
        /// <param name="duePayments"></param>
        /// <returns></returns>
        private List<Payment> GeneratePayments(IEnumerable<DueMemberPayment> duePayments)
        {
            var orderedDuePayments = duePayments.ToList().OrderBy(p => p.TotalAmount);
            var payments = new List<Payment>();
            while (orderedDuePayments.Any(p => Math.Round(p.TotalAmount,3) != 0.000m))
            {
                var min = orderedDuePayments.First();
                var max = orderedDuePayments.Last();
                var payment = new Payment
                    {
                        Payer = max.Member,
                        Payee = min.Member,
                        Amount = Math.Abs(min.TotalAmount) > max.TotalAmount ? max.TotalAmount : Math.Abs(min.TotalAmount)
                    };
                min.TotalAmount += payment.Amount;
                max.TotalAmount -= payment.Amount;
                payments.Add(payment);
                orderedDuePayments = orderedDuePayments.OrderBy(p => p.TotalAmount);
            }
            return payments;
        }
    }
}
