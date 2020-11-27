using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models
{
	public class AccessToken
	{
		public DateTime ExpireDate { get; set; }
		public string Token { get; set; }
		public bool Success { get; set; }
	}
}
