namespace Domain.Ports
{
	public interface ICheckInAdapter
	{
		Task<bool> CheckInAsync(int id);
	}
}
