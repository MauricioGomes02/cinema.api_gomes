namespace Domain.Entitys
{
	public class Ticket
	{
		public string Id { get; set; }
		public int MovieId { get; set; }
		public int CheckInId { get; set; }

		public decimal Price { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
