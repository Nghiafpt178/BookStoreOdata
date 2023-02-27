using AutoMapper;
using ODataBookStore.DTOs;
using ODataBookStore.Models;

namespace ODataBookStore.Repository
{
	public class UserRepository : IUserRepository
	{
        private readonly As2Context _context;
        private readonly IMapper mapper;

		public UserRepository(As2Context context, IMapper mapper)
		{
			_context = context;
			this.mapper = mapper;
		}

		public List<User> GetAll()
		{
			return _context.Users.ToList();
		}

		public UserRespond GetUserByID(string key)
		{
            var user = _context.Users.FirstOrDefault(b => b.UserId == key.Trim());
            if (user != null)
			{
                UserRespond userRespond = mapper.Map<UserRespond>(user);
                return userRespond;
            }
            return null;
        }

		public UserRespond UpdateUser(string key, UserRespond userRespond)
		{
            var user = _context.Users.FirstOrDefault(b => b.UserId == key.Trim());
            if (user != null)
            {
                user.EmailAddress = userRespond.EmailAddress;
                user.Password = userRespond.Password;
                user.Source = userRespond.Source;
                user.FirstName = userRespond.FirstName;
                user.MiddleName = userRespond.MiddleName;
                user.LastName = userRespond.LastName;
                user.PubId = userRespond.PubId;
                user.HireDate = userRespond.HireDate;
                _context.Update(user);
                _context.SaveChanges();
                UserRespond userRespond1 = mapper.Map<UserRespond>(user);
                return userRespond1;
            }
            return null;
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
