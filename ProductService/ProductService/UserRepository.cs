using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService
{
	public class UserRepository : IUserRepository
	{
		public bool UserExists(string login, string md5Password)
		{
			if (login == "admm" && md5Password.ToLower() == "4439974e191c1b2009f620a40efd0b61")
				return true;

			return false;
		}
	}
}
