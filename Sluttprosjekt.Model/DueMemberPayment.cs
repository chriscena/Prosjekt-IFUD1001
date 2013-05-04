namespace Sluttprosjekt.Model
{
    /// <summary>
    /// Model for holding a member and its total due amount.
    /// </summary>
    public class DueMemberPayment 
    {
        public Member Member { get; set; }
        public decimal TotalAmount { get; set; }
    }
}