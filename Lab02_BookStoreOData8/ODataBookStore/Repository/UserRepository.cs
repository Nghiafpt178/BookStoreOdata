using ODataBookStore.Models;

namespace ODataBookStore.Repository
{
	public class UserRepository : IUserRepository
	{
        private readonly As2Context _context;

		public UserRepository(As2Context context)
		{
			_context = context;
		}

		public User UserCheck(string email, string password)
		{
			var user = _context.Users.SingleOrDefault(u => u.EmailAddress == email && u.Password == password);
			if(user != null)
			{
				return user;
			}
			return null;
		}
	}
}
