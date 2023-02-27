using ODataBookStore.DTOs;
using ODataBookStore.Models;

namespace ODataBookStore.Repository
{
	public interface IUserRepository
	{
		List<User> GetAll();	
		User UserCheck(string email,string password);
        UserRespond GetUserByID(string key);

        UserRespond UpdateUser(string key, UserRespond userRespond);
    }
}
