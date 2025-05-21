namespace Domain.Entitys
{
    public class Rental
    {
        public Guid Id { get; set; }
        public int MovieId { get; set; }
        public Guid UserId { get; set; }
        public DateTime WithdrawDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal Amount { get; set; }
    }
}
