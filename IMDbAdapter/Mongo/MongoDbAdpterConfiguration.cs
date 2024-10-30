using System.ComponentModel.DataAnnotations;

namespace IMDbAdapter.Mongo
{
	public class MongoDbAdpterConfiguration
	{
		[Required]
		public string DefaultConnection { get; set; }
		
	}
}
