namespace Domain.Adapters
{
	public interface ICheckInAdapter
	{
		Task<bool> CheckInAsync(int id);
	}
}
