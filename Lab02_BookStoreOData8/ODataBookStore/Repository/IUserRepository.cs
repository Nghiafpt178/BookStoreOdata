using ODataBookStore.Models;

namespace ODataBookStore.Repository
{
	public interface IUserRepository
	{
		User UserCheck(string email,string password);	
	}
}
