namespace Sluttprosjekt.Model
{
    /// <summary>
    /// Payment domain model
    /// </summary>
    public class Payment 
    {
        public Member Payer { get; set; }
        public Member Payee { get; set; }
        public decimal Amount { get; set; }
    }
}