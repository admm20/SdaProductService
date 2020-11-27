using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService
{
	public interface IUserRepository
	{
		bool UserExists(string login, string md5Password);
	}
}
